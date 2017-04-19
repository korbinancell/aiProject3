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
            //play itself
            Agent x = new Agent("temp_filename", 'X', new StringBuilder("_________"));
            Agent o = new Agent("temp_filename", 'O', new StringBuilder("_________"), x);
            int ctr = 0;

            while (true)
            {
                var test = x.getCurrentBoard();
                for (int i = 0; i < 3; i++)
                {
                    for (int q = 0; q < 3; q++)
                    {
                        Console.Write(test[i * 3 + q]);
                    }
                    Console.WriteLine();
                }

                if (x.hasWon('X'))
                {
                    x.giveReinforcement(true);
                    o.giveReinforcement(false);
                    Console.WriteLine("X wins");
                    break;
                }
                else if (x.hasWon('O'))
                {
                    x.giveReinforcement(false);
                    o.giveReinforcement(true);
                    Console.WriteLine("O wins");
                    break;
                }
                else if (!x.getCurrentBoard().ToString().Contains('_'))
                {
                    var cb = x.getCurrentBoard().ToString();
                    break;
                }
                if(ctr%2 == 0)
                {
                    x.makeMove(o.getCurrentBoard());
                }
                else 
                {
                    o.makeMove(x.getCurrentBoard());
                }
                ctr++;
              }

            /*StringBuilder test;
            StringBuilder cb = new StringBuilder("XOOOXOOOX");

            StringBuilder tempo = new StringBuilder();
            tempo = cb;
            char tkn = 'X';

            for (int i = 0; i < 4; i++)
            {
                //straight on the side
                Console.WriteLine(tempo[0] == tkn && tempo[1] == tkn && tempo[2] == tkn);
                //Console.WriteLine(true);
                //straight throught the middle
                Console.WriteLine(tempo[3] == tkn && tempo[4] == tkn && tempo[5] == tkn);
                //Console.WriteLine(true);
                //diagonally through the middle
                Console.WriteLine(tempo[0] == tkn && tempo[4] == tkn && tempo[8] == tkn);
                    //Console.WriteLine(true);
                var thing = new StringBuilder(tempo[6].ToString() + tempo[3].ToString() + tempo[0].ToString() + tempo[7].ToString() + tempo[4].ToString() + tempo[1].ToString() + tempo[8].ToString() + tempo[5].ToString() + tempo[2].ToString());
                tempo = thing;
            }
            */

            /*
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
            */
        }
    }
}
