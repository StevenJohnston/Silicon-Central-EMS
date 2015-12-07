using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace question_3
{
    class Program
    {
        static bool[] primes; //primes are false
        static void Main(string[] args)
        {
            int lines = Convert.ToInt16(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                int luckyNumberCount = 0;
                string[] lineIn = Console.ReadLine().Split(' ');
                int a = Convert.ToInt16(lineIn[0]);
                int b = Convert.ToInt16(lineIn[1]);

                primes = new bool[9*9* Convert.ToString(b).Length];
                findPrimes();
                for (int x = a; x <= b; x++)
                {
                    if (!primes[sumOfDigits(x)] && !primes[sumOfSquares(x)])
                    {
                        luckyNumberCount++;
                    }
                }
                
                Console.WriteLine(luckyNumberCount);
            }
            Console.ReadKey();
        }
        static int sumOfDigits(int num)
        {
            int sum = 0;
            foreach (char ch in Convert.ToString(num))
            {
                sum += ch - '0';
            }
            return sum;
        }
        static int sumOfSquares(int num)
        {
            int sum = 0;
            foreach (char ch in Convert.ToString(num))
            {
                sum += Convert.ToInt16(Math.Pow((ch-'0'),2));
            }
            return sum;
        }
        public static void findPrimes()
        {
            for (var i = 2; i <= primes.Length-1; i++)
            {
                if (primes[i] == false)
                {
                    for (var x = i * 2; x <= primes.Length-1; x += i)
                    {
                        primes[x] = true;
                    }
                }
            }
        }
    }
}
