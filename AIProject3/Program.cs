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
            Console.WriteLine("Is a person playing? Y/N");
            string input = Console.ReadLine();

            if (input == "Y")
            {
                Console.WriteLine("Do you wanna go first? Y/N");
                bool first = Console.ReadLine() == "Y";

                char humanToken = first ? 'X' : 'O';
                char botToken = first ? 'O' : 'X';

                Agent x = new Agent("test" + botToken + ".txt", botToken, new StringBuilder("_________"), false);
                DoFile writerA = new DoFile();
                var Astionary = writerA.ReadStatetionary("test" + botToken + ".txt");
                x.setStatetionary(Astionary);
                StringBuilder humanBoard = x.getCurrentBoard();

                if(!first)
                    x.makeMove(x.getCurrentBoard());

                while (true)
                {
                    humanBoard = x.getCurrentBoard();
                    x.setcurrentBoard(humanBoard);

                    var test = x.getCurrentBoard();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            Console.Write(test[i * 3 + q]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                    Console.WriteLine("Which space do you wanna go? 0...8");
                    int place;
                    Int32.TryParse(Console.ReadLine(), out place);

                    humanBoard[place] = humanToken;
                    x.setcurrentBoard(humanBoard);

                    var Atest = humanBoard;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            Console.Write(Atest[i * 3 + q]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                    if (x.hasWon(botToken))
                    {
                        Console.WriteLine("You Lost! :(");
                        x.giveReinforcement(true);
                        break;
                    }
                    else if (x.hasWon(humanToken))
                    {
                        Console.WriteLine("You Win! :)");
                        x.giveReinforcement(false);
                        break;
                    }
                    else if (!x.getCurrentBoard().ToString().Contains('_'))
                    {
                        Console.WriteLine("Tie Game");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("X gets to move now: ");
                        x.makeMove(humanBoard);
                        humanBoard = x.getCurrentBoard();
                    }
                    
                    if (x.hasWon(botToken))
                    {
                        x.giveReinforcement(true);
                        var Btest = x.getCurrentBoard();
                        for (int i = 0; i < 3; i++)
                        {
                            for (int q = 0; q < 3; q++)
                            {
                                Console.Write(Btest[i * 3 + q]);
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        Console.WriteLine("You Lost! :(");
                        break;
                    }
                    else if (x.hasWon(humanToken))
                    {
                        Console.WriteLine("You Win! :)");
                        x.giveReinforcement(false);
                        break;
                    }
                    else if (!x.getCurrentBoard().ToString().Contains('_'))
                    {
                        Console.WriteLine("Tie Game");
                        break;
                    }

                    DoFile Bwriter = new DoFile();
                    Bwriter.WriteStatetionary("test" + botToken + ".txt", x.getStatetionary());
                }
            }
            else
            {
                for (int loop = 0; loop < 1000; loop++)
                {
                    if (loop % 10 == 0)
                        Console.WriteLine(loop);
                    //play itself
                    Agent x = new Agent("testX", 'X', new StringBuilder("_________"));
                    Agent o = new Agent("testO", 'O', new StringBuilder("_________"));
                    int ctr = 0;

                    DoFile writerA = new DoFile();

                    //                Console.WriteLine("Read:");
                    var Astionary = writerA.ReadStatetionary("testX.txt");
                    var Cstionary = writerA.ReadStatetionary("testO.txt");

                    //foreach (var blep in Astionary)
                    //    Console.WriteLine("{0}: {1}", blep.Key, String.Join(",", blep.Value.Select(p => p.ToString()).ToArray()));

                    x.setStatetionary(Astionary);
                    o.setStatetionary(Cstionary);

                    while (true)
                    {
                        var test = x.getCurrentBoard();
                       /*                        for (int i = 0; i < 3; i++)
                                                {
                                                    for (int q = 0; q < 3; q++)
                                                    {
                                                        Console.Write(test[i * 3 + q]);
                                                    }
                                                    Console.WriteLine();
                                                }
                                                Console.WriteLine();
                        */
                        if (x.hasWon('X'))
                        {
//                            Console.WriteLine("X wins");
                            x.giveReinforcement(true);
                            o.giveReinforcement(false);
                            /*foreach (var k in x.getStatetionary())
                            {
                                Console.WriteLine("key: {0} value: ", k.Key);
                                foreach (var d in k.Value)
                                {
                                    Console.Write("{0} ", d);
                                }
                            }*/

                            break;
                        }
                        else if (x.hasWon('O'))
                        {
 //                           Console.WriteLine("O wins");
                            x.giveReinforcement(false);
                            o.giveReinforcement(true);
                            /*foreach (var k in x.getStatetionary())
                            {
                                Console.WriteLine("key: {0} value: ", k.Key);
                                foreach (var d in k.Value)
                                {
                                    Console.Write("{0} ", d);
                                }
                            }*/
                            break;
                        }
                        else if (!x.getCurrentBoard().ToString().Contains('_'))
                        {
 //                           Console.WriteLine("Tie Game");
                            break;
                        }
                        if (ctr % 2 == 0)
                            x.makeMove(o.getCurrentBoard());
                        else
                            o.makeMove(x.getCurrentBoard());
                        ctr++;
                    }

                    DoFile writer = new DoFile();

                    //                Console.WriteLine("Write:");
                    writer.WriteStatetionary("testX.txt", x.getStatetionary());
                    writer.WriteStatetionary("testO.txt", o.getStatetionary());


                    //foreach (var blep in stionary)
                    //    Console.WriteLine("{0}: {1}", blep.Key, String.Join(",", blep.Value.Select(p => p.ToString()).ToArray()));
                }
            }                       
        }
    }
}
