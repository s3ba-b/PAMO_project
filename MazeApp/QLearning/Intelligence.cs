using System;
using System.Collections.Generic;

namespace Q_Learning
{
    /// <summary>
    /// Most important class. There is Q-Learning which learns to solve the maze.
    /// </summary>
    public class Intelligence
    {
        private const double _Gamma = 0.5;
        private const double _LearnRate = 0.5;
        private const int _MaxEpochs = 1000;

        private readonly Random _Random;
        private readonly MazeModel _Maze;

        public Intelligence(MazeModel maze)
        {
            _Maze = maze;
            _Random = new Random(1);
        }

        /// <summary>
        /// Train and get list of moves to do.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetMoves()
        {
            Train();
            return GetWalkMoves();
        }

        /// <summary>
        /// Go through maze matrix looking for the best way to get the goal.
        /// Establishing the quality of each move thanks to reward matrix.
        /// </summary>
        private void Train()
        {
            for (int epoch = 0; epoch < _MaxEpochs; ++epoch)
            {
                int currState = _Random.Next(0, _Maze.Reward.Length);

                while (true)
                {
                    int nextState = GetRandNextState(currState, _Maze.Matrix);
                    List<int> possNextNextStates = GetPossNextStates(nextState, _Maze.Matrix);
                    double maxQ = double.MinValue;

                    for (int j = 0; j < possNextNextStates.Count; ++j)
                    {
                        int nns = possNextNextStates[j];  // short alias
                        double q = _Maze.Quality[nextState][nns];
                        if (q > maxQ) maxQ = q;
                    }

                    _Maze.Quality[currState][nextState] =
                        ((1 - _LearnRate) * _Maze.Quality[currState][nextState]) +
                        (_LearnRate * (_Maze.Reward[currState][nextState] + (_Gamma * maxQ)));
                    currState = nextState;
                    if (currState == _Maze.Goal) break;
                }
            }
        }

        /// <summary>
        /// Get list of moves. Established by height of each move quality.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetWalkMoves()
        {
            List<int> moves = new List<int>();

            int curr = _Maze.Start; int next;
            moves.Add(curr);

            while (curr != _Maze.Goal)
            {
                next = ArgMax(_Maze.Quality[curr]);
                moves.Add(next);
                curr = next;
            }

            return moves;
        }

        private List<int> GetPossNextStates(int s, int[][] matrix)
        {
            List<int> result = new List<int>();

            for (int j = 0; j < matrix.Length; ++j)
            {
                if (matrix[s][j] == 1) result.Add(j);
            }

            return result;
        }

        private int GetRandNextState(int s, int[][] matrix)
        {
            List<int> possNextStates = GetPossNextStates(s, matrix);
            int ct = possNextStates.Count;
            int idx = _Random.Next(0, ct);

            return possNextStates[idx];
        }

        private int ArgMax(double[] vector)
        {
            double maxVal = vector[0];
            int idx = 0;

            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > maxVal)
                {
                    maxVal = vector[i]; idx = i;
                }
            }

            return idx;
        }

        #region Console Run

        public Intelligence() { }

        public void RunExamplesInConsole(IEnumerable<MazeModel> mazes)
        {
            foreach(var maze in mazes)
            {
                Console.WriteLine($"Analyzing maze {maze.Id} using Q-learning");
                Train();

                Console.WriteLine($"Done. Q matrix for maze {maze.Id}: ");
                Print(maze);

                Console.WriteLine($"Using Q to walk from cell {maze.Start} to {maze.Goal}");
                PrintWalk(maze);
            }
        }

        private void Print(MazeModel maze)
        {
            int ns = maze.Quality.Length;
            Console.WriteLine($"[0] [1] . . [{maze.QuantityOfSquares - 1}]");

            for (int i = 0; i < ns; ++i)
            {
                for (int j = 0; j < ns; ++j)
                {
                    Console.Write(maze.Quality[i][j].ToString("F2") + " ");
                }

                Console.WriteLine();
            }
        }

        private void PrintWalk(MazeModel maze)
        {
            int curr = maze.Start; int next;
            Console.Write(curr + "->");

            while (curr != maze.Goal)
            {
                next = ArgMax(maze.Quality[curr]);
                Console.Write(next + "->");
                curr = next;
            }

            Console.WriteLine("done");
        }

        #endregion

    }
}

