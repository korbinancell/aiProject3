using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject3
{
    class Agent
    {
        char token;
        string filename;
        Agent opponent;
        bool playingAgent;
        Random rando;
        StringBuilder currentBoard;
        Stack<Tuple<StringBuilder, int>> previousMoves;
        Dictionary<StringBuilder, double[]> statetionary = new Dictionary<StringBuilder, double[]>();

        public Agent(string filenm, char tokn, StringBuilder board, Agent opnt = null)
        {
            filename = filenm;
            token = tokn;
            currentBoard = board;
            opponent = opnt;
            playingAgent = !(opponent == null);

            rando = new Random(Guid.NewGuid().GetHashCode());
            previousMoves = new Stack<Tuple<StringBuilder, int>>();
            statetionary = new Dictionary<StringBuilder, double[]>();
        }

        private bool seenState()
        {
            StringBuilder test, cb = currentBoard;
            double[] hd, turned;

            //hFlip
            test = new StringBuilder(cb[2].ToString() + cb[1].ToString() + cb[0].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[8].ToString() + cb[7].ToString() + cb[6].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[2] + hd[1] + hd[0] + hd[5] + hd[4] + hd[3] + hd[8] + hd[7] + hd[6] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //vFlip
            test = new StringBuilder(cb[6].ToString() + cb[7].ToString() + cb[8].ToString() + cb[3].ToString() + cb[4].ToString() + cb[5].ToString() + cb[0].ToString() + cb[1].ToString() + cb[2].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[6] + hd[7] + hd[8] + hd[3] + hd[4] + hd[5] + hd[0] + hd[1] + hd[2] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //r90
            test = new StringBuilder(cb[6].ToString() + cb[3].ToString() + cb[0].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[8].ToString() + cb[5].ToString() + cb[2].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[2] + hd[5] + hd[8] + hd[1] + hd[4] + hd[7] + hd[0] + hd[3] + hd[6] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //r90vFlip
            test = new StringBuilder(cb[8].ToString() + cb[5].ToString() + cb[2].ToString() + cb[7].ToString() + cb[4].ToString() + cb[1].ToString() + cb[6].ToString() + cb[3].ToString() + cb[0].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[8] + hd[5] + hd[2] + hd[7] + hd[4] + hd[1] + hd[6] + hd[3] + hd[0] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //r180
            test = new StringBuilder(cb[8].ToString() + cb[7].ToString() + cb[6].ToString() + cb[5].ToString() + cb[4].ToString() + cb[3].ToString() + cb[2].ToString() + cb[1].ToString() + cb[0].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[8] + hd[7] + hd[6] + hd[5] + hd[4] + hd[3] + hd[2] + hd[1] + hd[0] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //r270
            test = new StringBuilder(cb[2].ToString() + cb[5].ToString() + cb[8].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[0].ToString() + cb[3].ToString() + cb[6].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[6] + hd[3] + hd[0] + hd[7] + hd[4] + hd[1] + hd[8] + hd[5] + hd[2] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            //r270vFlup
            test = new StringBuilder(cb[0].ToString() + cb[3].ToString() + cb[6].ToString() + cb[1].ToString() + cb[4].ToString() + cb[7].ToString() + cb[2].ToString() + cb[5].ToString() + cb[8].ToString());
            if (statetionary.ContainsKey(test))
            {
                hd = statetionary[test];
                turned = new double[] { hd[0] + hd[3] + hd[6] + hd[1] + hd[4] + hd[7] + hd[2] + hd[5] + hd[8] };

                statetionary.Remove(test);
                statetionary.Add(currentBoard, turned);
                return true;
            }

            return statetionary.ContainsKey(currentBoard);

        }

        public void makeMove()
        {
            if(seenState())
            {
                if (playingAgent)
                {
                    Tuple<int, double>[] sorted = new Tuple<int, double>[9];
                    for(int i=0;i<9;i++)
                        sorted[i] = Tuple.Create(i, statetionary[currentBoard][i]);

                    Array.Sort(sorted, (x,y) =>  x.Item2.CompareTo(y.Item2));

                    int a = Math.Max(rando.Next(9), rando.Next(9));

                    previousMoves.Push(Tuple.Create(new StringBuilder(currentBoard.ToString()), sorted[a].Item1));

                    currentBoard[ sorted[a].Item1 ] = token;
                }
            }
        }
    }
}
