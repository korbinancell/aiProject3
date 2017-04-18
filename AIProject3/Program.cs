using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject3
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder test;
            StringBuilder cb = new StringBuilder("OXX______");
            Console.WriteLine("Normal");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                {
                    Console.Write(cb[i * 3 + q]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //hFlip
            test = new StringBuilder(cb[2].ToString() + cb[1].ToString() + cb[0].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[8].ToString() + cb[7].ToString() + cb[6].ToString());
           
            Console.WriteLine("hFlip");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                {
                    Console.Write(test[i * 3 + q]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //vFlip
            test = new StringBuilder(cb[6].ToString() + cb[7].ToString() + cb[8].ToString() + cb[3].ToString() + cb[4].ToString() + cb[5].ToString() + cb[0].ToString() + cb[1].ToString() + cb[2].ToString());
            Console.WriteLine("vFlip");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //r90
            test = new StringBuilder(cb[6].ToString() + cb[3].ToString() + cb[0].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[8].ToString() + cb[5].ToString() + cb[2].ToString());
            Console.WriteLine("r90");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //r90vFlip
            test = new StringBuilder(cb[8].ToString() + cb[5].ToString() + cb[2].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[6].ToString() + cb[3].ToString() + cb[0].ToString());
            Console.WriteLine("r90vFlip");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //r180
            test = new StringBuilder(cb[8].ToString() + cb[7].ToString() + cb[6].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[2].ToString() + cb[1].ToString() + cb[0].ToString());
            Console.WriteLine("r180");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //r270
            test = new StringBuilder(cb[2].ToString() + cb[5].ToString() + cb[8].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[0].ToString() + cb[3].ToString() + cb[6].ToString());
            Console.WriteLine("r270");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            //r270vFlup
            test = new StringBuilder(cb[0].ToString() + cb[3].ToString() + cb[6].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[2].ToString() + cb[5].ToString() + cb[8].ToString());
            Console.WriteLine("r270vFlup");
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                    Console.Write(test[i * 3 + q]);
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
}
