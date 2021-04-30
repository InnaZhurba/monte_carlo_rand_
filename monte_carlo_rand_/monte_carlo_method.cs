using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime;

namespace monte_carlo_rand_
{
    public class monte_carlo_method
    {
        private static double e = 2.71828182846;

        private double[,] data_x_y_realY_N_plus;
        private int data_row_size = 4;
        private int size_N;
        private double sum_1_data_xyYN;

        private double square;

        private double integral;

        private double[,] data_ten_samples;

        private double expected_value;
        private double variance;
        private double standard_deviation;
        private double[] fault;
        private double max_fault;

        private Random rng = new Random();

        public monte_carlo_method(int N_size,double a, double b)
        {
            size_N = N_size;
            data_x_y_realY_N_plus = new double[size_N,data_row_size];
            sum_1_data_xyYN = 0;

            integral = 0;
            square = a * b;

            data_ten_samples = new double[10,2];

            expected_value = 0;
            variance = 0;
            fault = new double[10];
            max_fault = 0;
        }

        public int start()
        {
            double sum_of_integrals = 0;
            double sum_of_additional = 0;

            for (int i = 0; i < 10; i++)
            {
                generate_data_xyYNplus();
                //show_data_xyYNplus();

                integral_calculation();
                data_ten_samples[i, 0] = integral;

                sum_of_integrals += data_ten_samples[i, 0];
            }

                expected_value_calculation(sum_of_integrals);



                max_fault = Math.Abs(data_ten_samples[0, 0] - variance);
                for (int j = 0; j < 10; j++)
                {
                    data_ten_samples[j, 1] = Math.Pow(data_ten_samples[j, 0] - expected_value, 2);
                    sum_of_additional += data_ten_samples[j, 1];
                }

                show_data_ten_samples();

            calculation_variance(sum_of_additional);

            calculation_standard_deviation();

            for (int j = 0; j < 10; j++)
            {
                fault[j] = Math.Abs(data_ten_samples[j, 0] - variance);
                if (fault[j] > max_fault)
                    max_fault = fault[j];
            }

            show_faults();

                Console.WriteLine($"\n***Max_fault = {max_fault}***\n");
            

            return 1;
        }

        public void generate_data_xyYNplus()
        {
            sum_1_data_xyYN = 0;
            for (int i=0;i < size_N;i++)
            {
                for (int j = 0; j < data_row_size; j++)
                {
                    if (j == 0)
                        data_x_y_realY_N_plus[i, j] = rng.NextDouble();

                    if (j == 1)
                        data_x_y_realY_N_plus[i, j] = rng.NextDouble()* 1.427;

                    if (j == 2)
                        data_x_y_realY_N_plus[i, j] = (Math.Pow(e, data_x_y_realY_N_plus[i, 0])
                            + Math.Pow(e, -data_x_y_realY_N_plus[i, 0])) / 2;

                    if (j == 3)
                    {
                        data_x_y_realY_N_plus[i, j] = data_x_y_realY_N_plus[i, 1] < data_x_y_realY_N_plus[i, 2] ? 1 : 0;
                        sum_1_data_xyYN += data_x_y_realY_N_plus[i, j];
                    }                    
                }
            }
        }

        public void show_data_xyYNplus()
        {
            Console.WriteLine("\n***Data: x, y, real y, N+***\n");

            for (int i = 0; i < size_N; i++)
            {
                for (int j = 0; j < data_row_size; j++)
                {
                    Console.Write($" {data_x_y_realY_N_plus[i,j]} :");
                }
                Console.Write("\n");
            }

            Console.WriteLine($"\n***Sum of N+ = {sum_1_data_xyYN}***\n");
        }

        public void integral_calculation()//+show integral
        {
            integral = 0;
            integral = square * (sum_1_data_xyYN/size_N);

            Console.WriteLine($"\n***Integral = {integral}***\n");
        }

        public void expected_value_calculation(double sum)
        {
            expected_value = sum / 10;

            Console.WriteLine($"\n***Expected value = {expected_value}***\n");
        }

        public void show_data_ten_samples()
        {
            Console.WriteLine("\n***Data from 10 samples: expected value, additional***\n");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write($" {data_ten_samples[i, j]} :");
                }
                Console.Write("\n");
            }
        }

        public void calculation_variance(double sum)
        {
            variance = (0.111111111) * sum;//(1 / 9)

            Console.WriteLine($"\n***Variance = {variance}***\n");
        }

        public void calculation_standard_deviation()
        {
            standard_deviation = Math.Sqrt(variance);

            Console.WriteLine($"\n***Standard deviation = {standard_deviation}***\n");
        }

        public void show_faults()
        {
            Console.WriteLine("All faults:");
            for(int i = 0; i < 10; i++)
            {
                Console.Write($"{fault[i]} \n");
            }
        }
    }
}
