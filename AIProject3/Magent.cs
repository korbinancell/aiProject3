using System;
using System.Collections.Generic;
using System.Linq;

namespace AIProject3
{
    class Magent
    {
        double decayFactor;
        public bool favorExploration { get; set; }
        public int token { get; }
        string filename;
        Random rando;
        int[] currentBoard;
        Stack< Tuple<string, int> > previousMoves;
        public Dictionary<string, double[]> statetionary  { get; set; }

        public Magent(string filenm, int tokn, int[] board, bool favrExplrtn = true)
        {
            DoFile writer = new DoFile();

            decayFactor = .5;
            favorExploration = favrExplrtn;

            filename = filenm;
            token = tokn;
            currentBoard = board;

            rando = new Random(Guid.NewGuid().GetHashCode());
            previousMoves = new Stack<Tuple<string, int>>();
        }

        public bool makeMove()              //returns true if the agent gets another move
        {
            bool anotherMove = false;
            int moveChosen;
            string currentBoardString = string.Join(" ", currentBoard.Take(12));

            if (!statetionary.ContainsKey(currentBoardString))
            {
                double[] h = { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5};
                statetionary.Add(currentBoardString, h);
            }

            List<Tuple<int, double>> sorted = new List<Tuple<int, double>>();

            for (int q = token; q < token+6; q++)
                if (currentBoard[q] != 0)
                    sorted.Add(Tuple.Create(q, statetionary[currentBoardString][q]));

            sorted.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            if (favorExploration)
            {
                moveChosen = Math.Max(rando.Next(sorted.Count), rando.Next(sorted.Count));
            }
            else
            {
                for (int q = sorted.Count - 1; q >= 0; q--)
                    Console.WriteLine(sorted[q]);
                Console.WriteLine();

                moveChosen = sorted.Count - 1;
            }
//Console.WriteLine("moveChosen {0} sortedCount {1} board {2}", moveChosen, sorted.Count, string.Join(" ",currentBoard.Take(12)));

            previousMoves.Push(Tuple.Create( currentBoardString , sorted[moveChosen].Item1));

            
            int i = currentBoard[sorted[moveChosen].Item1];
            int hole = (sorted[moveChosen].Item1 + 1) %12;
            currentBoard[sorted[moveChosen].Item1] = 0;
            for (; i > 0; i--)
            {
                int stolen = hole < 6 ? 11 - hole : Math.Abs(hole - 11);

                if (hole == 6 - token)
                {
                    currentBoard[12 + token]++;
                    if (i - 1 == 0)
                        anotherMove = true;
                    else
                    {
                        i--;
                        currentBoard[hole]++;
                    }
                }

                else if (i - 1 == 0 && currentBoard[hole] == 0 && (hole >= token && hole < token + 6) && currentBoard[stolen] != 0)
                {
                    currentBoard[12 + token] += (currentBoard[stolen] + 1);

                    currentBoard[hole] = currentBoard[stolen] = 0;
                }

                else
                {
                    currentBoard[hole]++;
                }
                hole = (hole + 1) % 12;
            }

            return anotherMove;
        }


        public bool makeMove(int move)
        {
            if (move < token || move >= token + 6 || currentBoard[move] == 0)
                return true;

            bool anotherMove = false;
            string currentBoardString = string.Join(" ", currentBoard.Take(12));

            if (!statetionary.ContainsKey(currentBoardString))
            {
                double[] h = { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 };
                statetionary.Add(currentBoardString, h);
            }

            previousMoves.Push(Tuple.Create(currentBoardString, move));


            int i = currentBoard[move];
            int hole = (move + 1) % 12;
            currentBoard[move] = 0;
            for(;i>0;i--)
            {
                int stolen = hole < 6 ? 11 - hole : Math.Abs(hole - 11);

                if (hole == 6 - token)
                {
                    currentBoard[12 + token]++;
                    if (i - 1 == 0)
                        anotherMove = true;
                    else
                    {
                        i--;
                        currentBoard[hole]++;
                    }
                }

                else if (i - 1 == 0 && currentBoard[hole] == 0 && (hole >= token && hole < token + 6) && currentBoard[stolen] != 0)
                {
                    currentBoard[12 + token] += (currentBoard[stolen] + 1);

                    currentBoard[hole] = currentBoard[stolen] = 0;
                }

                else
                {
                    currentBoard[hole]++;
                }
                hole = (hole+1)%12;
            }

            return anotherMove;
        }

        public void giveReinforcement(bool didIWin)
        {
            var mostRecentmove = previousMoves.Pop();

            double[] values = statetionary[mostRecentmove.Item1];
            double currentDecay = decayFactor;

            values[mostRecentmove.Item2] = didIWin ? 1 : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
            statetionary.Remove(mostRecentmove.Item1);
            statetionary.Add(mostRecentmove.Item1, values);
            currentDecay *= .5;

            while (previousMoves.Count > 0)
            {
                mostRecentmove = previousMoves.Pop();
                values = statetionary[mostRecentmove.Item1];
                values[mostRecentmove.Item2] = didIWin ? values[mostRecentmove.Item2] + ((1 - values[mostRecentmove.Item2]) * currentDecay) : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
                currentDecay *= .5;
            }
        }

        public void writeStationary()
        {
            DoFile writer = new DoFile();

            writer.WriteStatetionary(filename, statetionary);
        }
    }
}
