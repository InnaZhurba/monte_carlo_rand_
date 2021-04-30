using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monte_carlo_rand_
{
    class Program
    {
        static void Main(string[] args)
        {
            int N;

            Console.WriteLine("Hello.\n Its the monte_carlo method.\n Please, write number of samples:");
            N = Convert.ToInt32(Console.ReadLine());

            monte_carlo_method mk = new monte_carlo_method(N,1,1.427);

            mk.start();

            Console.ReadLine();
        }
    }
}
