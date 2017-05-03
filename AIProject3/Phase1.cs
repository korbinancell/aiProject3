using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject3
{
    class Phase1
    {
        public void printBoard(StringBuilder board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int q = 0; q < 3; q++)
                {
                    Console.Write(board[i * 3 + q]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DoPhase1()
        {
            StringBuilder MasterBoard = new StringBuilder("_________");
            Agent x = new Agent("testX.txt", 'X', MasterBoard);
            Agent o = new Agent("testO.txt", 'O', MasterBoard);

            Console.WriteLine("Is a person playing? Y/N");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                Console.WriteLine("Do you wanna go first? Y/N");
                bool first = Console.ReadLine().ToLower() == "y";

                Agent player = first ? x : o;
                Agent bot = first ? o : x;

                bot.favorExploration = false;

                if (!first)
                    bot.makeMove();

                while (true)
                {
                    printBoard(MasterBoard);

                    Console.WriteLine("Which space do you wanna go? 0...8");
                    int place;
                    Int32.TryParse(Console.ReadLine(), out place);

                    player.makeMove(place);

                    if (player.hasWon(player.token))
                    {
                        Console.Clear();
                        printBoard(MasterBoard);
                        Console.WriteLine("You Win! :)");

                        player.giveReinforcement(true);
                        bot.giveReinforcement(false);
                        player.writeStationary();
                        bot.writeStationary();

                        break;
                    }
                    else if (!MasterBoard.ToString().Contains('_'))
                    {
                        Console.Clear();
                        printBoard(MasterBoard);
                        Console.WriteLine("Tie Game");

                        player.giveReinforcement(true);
                        bot.giveReinforcement(true);
                        player.writeStationary();
                        bot.writeStationary();

                        break;
                    }

                    Console.Clear();

                    Console.WriteLine("Computers Options:");

                    bot.makeMove();

                    if (player.hasWon(bot.token))
                    {
                        Console.Clear();
                        printBoard(MasterBoard);
                        Console.WriteLine("You Lose :(");

                        player.giveReinforcement(false);
                        bot.giveReinforcement(true);
                        player.writeStationary();
                        bot.writeStationary();

                        break;
                    }
                    else if (!MasterBoard.ToString().Contains('_'))
                    {
                        Console.Clear();
                        printBoard(MasterBoard);
                        Console.WriteLine("Tie Game");

                        player.giveReinforcement(true);
                        bot.giveReinforcement(true);
                        player.writeStationary();
                        bot.writeStationary();

                        break;
                    }
                }
            }

            else
            {
                int hold = 0;
                for (int i = 0; i < 100000; i++)
                {
                    MasterBoard.Replace('X', '_');
                    MasterBoard.Replace('O', '_');

                    if (i % 1000 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(hold);
                        string progBar = "(" + (i - hold >= 1000 ? "=" : "*") + (i - hold >= 2000 ? "=" : "*") + (i - hold >= 3000 ? "=" : "*") + (i - hold >= 4000 ? "=" : "*") + (i - hold >= 5000 ? "=" : "*") + (i - hold >= 6000 ? "=" : "*") + (i - hold >= 7000 ? "=" : "*") + (i - hold >= 8000 ? "=" : "*") + (i - hold >= 9000 ? "=" : "*") + (i - hold >= 10000 ? "=" : "*") + ")";
                        Console.WriteLine(progBar);
                        Console.WriteLine("Playing Games");

                        if (i % 10000 == 0)
                            hold = i;
                    }

                    while (true)
                    {
                        x.makeMove();

                        if (x.hasWon(x.token))
                        {
                            x.giveReinforcement(true);
                            o.giveReinforcement(false);

                            break;
                        }
                        else if (!MasterBoard.ToString().Contains('_'))
                        {
                            x.giveReinforcement(true);
                            o.giveReinforcement(true);

                            break;
                        }

                        o.makeMove();

                        if (o.hasWon(o.token))
                        {
                            x.giveReinforcement(false);
                            o.giveReinforcement(true);

                            break;
                        }
                        else if (!MasterBoard.ToString().Contains('_'))
                        {
                            x.giveReinforcement(true);
                            o.giveReinforcement(true);

                            break;
                        }
                    }

                    if (i % 100 == 0)
                    {
                        x.writeStationary();
                        o.writeStationary();
                    }
                }
                Console.Clear();
                Console.WriteLine(100000);
                Console.WriteLine("(==========)");
            }

        }
    }
}
