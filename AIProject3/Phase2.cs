using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject3
{
    class Phase2
    {
        public bool gameOver(int[] currentBoard)
        {
            bool empty0 = true;
            bool empty6 = true;
            int sum = 0;

            for (int i = 0; i < 6; i++)
                if (currentBoard[i] != 0) empty0 = false;

            for (int i = 6; i < 12; i++)
                if (currentBoard[i] != 0) empty6 = false;

            if (empty0)
            {
                for (int i = 6; i < 12; i++)
                    sum += currentBoard[i];

                currentBoard[18] += sum;
            }
            else if (empty6)
            {
                for (int i = 0; i < 6; i++)
                    sum += currentBoard[i];

                currentBoard[12] += sum;
            }

            return empty0 || empty6;

        }

        public void printBoard(int[] board)
        {
            for (int i = 11; i >5; i--)
                Console.Write(board[i].ToString() + " ");
            Console.WriteLine("Pot p2: " + board[18].ToString());

            for (int i = 0; i < 6; i++)
                Console.Write(board[i].ToString() + " ");
            Console.WriteLine("Pot p1: " + board[12].ToString());
        }

        public void DoPhase2()
        {
            int[] MasterBoard = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0 };
            Magent six = new Magent("test6.txt", 6, MasterBoard);
            Magent zero = new Magent("test0.txt", 0, MasterBoard);
            DoFile reader = new DoFile();

            six.statetionary = reader.ReadStatetionary("test6.txt");
            zero.statetionary = reader.ReadStatetionary("test0.txt");

            Console.WriteLine("Is a person playing? Y/N");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                Console.WriteLine("Do you wanna go first? Y/N");
                bool first = Console.ReadLine().ToLower() == "y";

                Magent player = first ? zero : six;
                Magent bot = first ? six : zero;

                bot.favorExploration = false;

                if (!first)
                    bot.makeMove();

                bool endGame = false;
                while (!endGame)
                {
                    bool canMove = true;
                    

                    while (canMove)
                    {
                        if (gameOver(MasterBoard))
                        {
                            endGame = true;
                            break;
                        }

                        printBoard(MasterBoard);

                        Console.WriteLine("Which space do you wanna go? {0}...{1}", player.token, player.token+5);
                        int place;
                        Int32.TryParse(Console.ReadLine(), out place);

                        canMove = player.makeMove(place);

                        Console.Clear();
                    }

                    if (endGame)
                        break;

                    canMove = true;
                    Console.WriteLine("Computers Options:");

                    while (canMove)
                    {
                        if (gameOver(MasterBoard))
                        {
                            endGame = true;
                            break;
                        }

                        canMove = bot.makeMove();

                        Console.WriteLine("---------------------------------");
                    }
                }

                Console.Clear();
                printBoard(MasterBoard);
                if (MasterBoard[12 + player.token] > MasterBoard[12 + bot.token])
                {
                    Console.WriteLine("You Win! :)");

                    player.giveReinforcement(true);
                    bot.giveReinforcement(false);
                }
                else if (MasterBoard[12 + player.token] == MasterBoard[12 + bot.token])
                {
                    Console.WriteLine("Tie Game");

                    player.giveReinforcement(true);
                    bot.giveReinforcement(true);
                }
                else
                {
                    Console.WriteLine("You Loose! :)");

                    player.giveReinforcement(false);
                    bot.giveReinforcement(true);
                }

                player.writeStationary();
                bot.writeStationary();
            }

            else
            {
                int hold = 0;
                for (int i = 0; i < 100000; i++)
                {
                    for (int q = 0; q < 12; q++)
                        MasterBoard[q] = 4;
                    for (int q = 12; q < 19; q++)
                        MasterBoard[q] = 0;

                    if (i % 1000 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(hold);
                        string progBar = "(" + (i - hold >= 1000 ? "=" : "*") + (i - hold >= 2000 ? "=" : "*") + (i - hold >= 3000 ? "=" : "*") + (i - hold >= 4000 ? "=" : "*") + (i - hold >= 5000 ? "=" : "*") + (i - hold >= 6000 ? "=" : "*") + (i - hold >= 7000 ? "=" : "*") + (i - hold >= 8000 ? "=" : "*") + (i - hold >= 9000 ? "=" : "*") + (i - hold >= 10000 ? "=" : "*") + ")";
                        Console.WriteLine(progBar);
                        Console.WriteLine("Playing Games");

                        if (i % 10000 == 0 && i != 0)
                        {
                            hold = i;
                            zero.writeStationary();
                            six.writeStationary();
                        }

                        
                    }

                    bool endGame = false;
                    while (!endGame)
                    {
                        bool canMove = true;

                        while (canMove)
                        {
                            if (gameOver(MasterBoard))
                            {
                                endGame = true;
                                break;
                            }

                            canMove = zero.makeMove();
                        }

                        if (endGame)
                            break;

                        canMove = true;

                        while (canMove)
                        {
                            if (gameOver(MasterBoard))
                            {
                                endGame = true;
                                break;
                            }

                            canMove = six.makeMove();
                        }
                    }

                    if (MasterBoard[12 + zero.token] > MasterBoard[12 + six.token])
                    {
                        zero.giveReinforcement(true);
                        six.giveReinforcement(false);
                    }
                    else if (MasterBoard[12 + zero.token] == MasterBoard[12 + six.token])
                    {
                        zero.giveReinforcement(true);
                        six.giveReinforcement(true);
                    }
                    else
                    {
                        zero.giveReinforcement(false);
                        six.giveReinforcement(true);
                    }

                }
                Console.Clear();
                Console.WriteLine(100000);
                Console.WriteLine("(==========)");
                zero.writeStationary();
                six.writeStationary();
            }
        }
    }
}