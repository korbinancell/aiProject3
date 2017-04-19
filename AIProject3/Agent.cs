﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject3
{
    class Agent
    {
        double decayFactor;
        bool favorExploration;
        char token;
        string filename;
        Agent opponent;
        bool playingAgent;
        Random rando;
        StringBuilder currentBoard;
        Stack<Tuple<StringBuilder, int>> previousMoves;
        Dictionary<string, double[]> statetionary = new Dictionary<string, double[]>();

        public Agent(string filenm, char tokn, StringBuilder board, Agent opnt = null, bool favrExplrtn = true)
        {
            decayFactor = .5;
            favorExploration = favrExplrtn;
            
            filename = filenm;
            token = tokn;
            currentBoard = board;
            opponent = opnt;
            playingAgent = !(opponent == null);

            rando = new Random(Guid.NewGuid().GetHashCode());
            previousMoves = new Stack<Tuple<StringBuilder, int>>();
            statetionary = new Dictionary<string, double[]>();

            Console.WriteLine(currentBoard);
        }

        public StringBuilder getCurrentBoard()
        {
            return currentBoard;
        }
        
        private bool seenState()
        {
            StringBuilder cb = currentBoard;
            string test;
            double[] hd, turned;

            //hFlip
            test = new StringBuilder(cb[2].ToString() + cb[1].ToString() + cb[0].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[8].ToString() + cb[7].ToString() + cb[6].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[2] + hd[1] + hd[0] + hd[5] + hd[4] + hd[3] + hd[8] + hd[7] + hd[6] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //vFlip
            test = new StringBuilder(cb[6].ToString() + cb[7].ToString() + cb[8].ToString() + cb[3].ToString() + cb[4].ToString() + cb[5].ToString() + cb[0].ToString() + cb[1].ToString() + cb[2].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[6] + hd[7] + hd[8] + hd[3] + hd[4] + hd[5] + hd[0] + hd[1] + hd[2] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //r90
            test = new StringBuilder(cb[6].ToString() + cb[3].ToString() + cb[0].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[8].ToString() + cb[5].ToString() + cb[2].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[2] + hd[5] + hd[8] + hd[1] + hd[4] + hd[7] + hd[0] + hd[3] + hd[6] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //r90vFlip
            test = new StringBuilder(cb[8].ToString() + cb[5].ToString() + cb[2].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[6].ToString() + cb[3].ToString() + cb[0].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[8] + hd[5] + hd[2] + hd[7] + hd[4] + hd[1] + hd[6] + hd[3] + hd[0] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //r180
            test = new StringBuilder(cb[8].ToString() + cb[7].ToString() + cb[6].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[2].ToString() + cb[1].ToString() + cb[0].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[8] + hd[7] + hd[6] + hd[5] + hd[4] + hd[3] + hd[2] + hd[1] + hd[0] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //r270
            test = new StringBuilder(cb[2].ToString() + cb[5].ToString() + cb[8].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[0].ToString() + cb[3].ToString() + cb[6].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[6] + hd[3] + hd[0] + hd[7] + hd[4] + hd[1] + hd[8] + hd[5] + hd[2] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            //r270vFlup
            test = new StringBuilder(cb[0].ToString() + cb[3].ToString() + cb[6].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[2].ToString() + cb[5].ToString() + cb[8].ToString()).ToString();
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[0] + hd[3] + hd[6] + hd[1] + hd[4] + hd[7] + hd[2] + hd[5] + hd[8] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard.ToString(), turned);
                return true;
            }

            return statetionary.ContainsKey(currentBoard.ToString());

        }

        public void makeMove(StringBuilder boardRigtNow)
        {
            Console.WriteLine("makeMove function");
            currentBoard = boardRigtNow;
            if(!seenState())
            {
                double[] h = { 0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5};
                statetionary.Add(currentBoard.ToString(), h);
                Console.WriteLine("hello from makeMove");
            }
            if (favorExploration)
            {
                Tuple<int, double>[] sorted = new Tuple<int, double>[9];
                for(int i=0;i<9;i++)
                    sorted[i] = Tuple.Create(i, statetionary[currentBoard.ToString()][i]);

                Array.Sort(sorted, (x,y) =>  x.Item2.CompareTo(y.Item2));

                int a = Math.Max(rando.Next(9), rando.Next(9));
                while(currentBoard[a] != '_')
                    a = Math.Max(rando.Next(9), rando.Next(9));


                previousMoves.Push(Tuple.Create(new StringBuilder(currentBoard.ToString()), sorted[a].Item1));
                Console.WriteLine("current board is {0}", currentBoard.ToString());
                currentBoard[ sorted[a].Item1 ] = token; 
                Console.WriteLine("current board is {0}", currentBoard.ToString());

            }
        }

        public bool hasWon(char tkn)
        {
            StringBuilder tempo = new StringBuilder();
            tempo = currentBoard;    

            for(int i = 0; i < 4; i++)
            {
                //straight on the side
                if (tempo[0] == tkn && tempo[1] == tkn && tempo[2] == tkn)
                    return true;
                //straight throught the middle
                if (tempo[3] == tkn && tempo[4] == tkn && tempo[5] == tkn)
                    return true;
                //diagonally through the middle
                if (tempo[0] == tkn && tempo[4] == tkn && tempo[8] == tkn)
                    return true;
                var test = new StringBuilder(tempo[2].ToString() + tempo[1].ToString() + tempo[0].ToString() + tempo[5].ToString() + tempo[4].ToString() + tempo[3].ToString() + tempo[8].ToString() + tempo[7].ToString() + tempo[6].ToString());
                tempo = test;
            }

            return false;
        }

        public void giveReinforcement(bool didIWin)
        {
            var mostRecentmove = previousMoves.Pop();
            Console.WriteLine(mostRecentmove.Item1);

            Console.WriteLine("stationary: ");
            Console.WriteLine(statetionary.Count());
            foreach (var k in statetionary)
            {
                Console.WriteLine(k.Key);
            }

            double[] values = statetionary[mostRecentmove.Item1.ToString()];
            double currentDecay = decayFactor;

            values[mostRecentmove.Item2] = didIWin ? 1 : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
            statetionary.Remove(mostRecentmove.Item1.ToString());
            statetionary.Add(mostRecentmove.Item1.ToString(), values);
            currentDecay /= 2;

            while(previousMoves.Count > 0)
            {
                mostRecentmove = previousMoves.Pop();
                values = statetionary[mostRecentmove.Item1.ToString()];
                values[mostRecentmove.Item2] = didIWin ? values[mostRecentmove.Item2] + (values[mostRecentmove.Item2] * currentDecay) : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
                currentDecay /= 2;
            }
        }

    }
}
