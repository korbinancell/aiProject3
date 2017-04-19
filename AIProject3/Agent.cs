using System;
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
        Dictionary<StringBuilder, double[]> statetionary = new Dictionary<StringBuilder, double[]>();

        public Agent(string filenm, char tokn, StringBuilder board, Agent opnt = null, bool favrExplrtn = true)
        {
            decayFactor = .5;
            
            filename = filenm;
            token = tokn;
            currentBoard = board;
            opponent = opnt;
            playingAgent = !(opponent == null);

            rando = new Random(Guid.NewGuid().GetHashCode());
            previousMoves = new Stack<Tuple<StringBuilder, int>>();
            statetionary = new Dictionary<StringBuilder, double[]>();

            Console.WriteLine(currentBoard);
        }

        public StringBuilder getCurrentBoard()
        {
            return currentBoard;
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

        public void makeMove(StringBuilder boardRigtNow)
        {
            Console.WriteLine("makeMove function");
            currentBoard = boardRigtNow;
            if(!seenState())
            {
                double[] h = { 0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5};
                statetionary.Add(currentBoard, h);
            }
            //if (playingAgent)
            //{
                Tuple<int, double>[] sorted = new Tuple<int, double>[9];
                for(int i=0;i<9;i++)
                    sorted[i] = Tuple.Create(i, statetionary[currentBoard][i]);

                Array.Sort(sorted, (x,y) =>  x.Item2.CompareTo(y.Item2));

                int a = Math.Max(rando.Next(9), rando.Next(9));
                while(currentBoard[a] != '_')
                    a = Math.Max(rando.Next(9), rando.Next(9));

                previousMoves.Push(Tuple.Create(new StringBuilder(currentBoard.ToString()), sorted[a].Item1));
                currentBoard[ sorted[a].Item1 ] = token;
            //}
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

            Console.WriteLine(statetionary.ContainsKey(mostRecentmove.Item1));
// Ok, the problem is this guy right here v I think we're adding states to the statetionary at like half a step off, like for the opponent instead of ourselves, so we have all of the other states we need, but not actually the ones on the stack, so we're looking for something that isn't there and that gives us the error. We need to go fix that. Make it so.
            double[] values = statetionary[mostRecentmove.Item1];
            double currentDecay = decayFactor;

            values[mostRecentmove.Item2] = didIWin ? 1 : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
            statetionary.Add(mostRecentmove.Item1, values);
            currentDecay /= 2;

            while(previousMoves.Count > 0)
            {
                mostRecentmove = previousMoves.Pop();
                values = statetionary[mostRecentmove.Item1];
                values[mostRecentmove.Item2] = didIWin ? values[mostRecentmove.Item2] + (values[mostRecentmove.Item2] * currentDecay) : values[mostRecentmove.Item2] - (values[mostRecentmove.Item2] * currentDecay);
                currentDecay /= 2;
            }
        }

    }
}
