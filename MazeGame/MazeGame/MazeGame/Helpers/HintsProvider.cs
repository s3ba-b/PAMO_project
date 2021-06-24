using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MazeGame.ViewModels;
using Q_Learning;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MazeGame.Helpers
{
    public class HintsProvider
    {
        private readonly IEnumerable<int> _qLearningPath;
        private readonly IEnumerable<int> _crossedCells;
        
        public HintsProvider(IEnumerable<int> crossedCells, MazeModel model)
        {
            _crossedCells = crossedCells;
            _qLearningPath = GetQLearningPath(model);
        }

        private static IEnumerable<int> GetQLearningPath(MazeModel model)
        {
            var intelligence = new Intelligence(model);
            return intelligence.GetMoves();
        }

        public IEnumerable<int> GetHintCellsIndexes()
        {

            var pathDescent = _crossedCells.Except(_qLearningPath);
            if (pathDescent.Count() == 0)
            {
                return _qLearningPath.Except(_crossedCells).Take(3);
            }
            
            if (pathDescent.Count() >= 3)
            {
                pathDescent.Reverse();
                return pathDescent.Take(3);
            }
            
            if (pathDescent.Count() > 0 && pathDescent.Count() < 3)
            {
                var cellsFromGoodPath = 3 - pathDescent.Count();
                var path = new List<int>();
                path.AddRange(pathDescent);
                var goodPath = _crossedCells.Except(pathDescent);
                path.Add(goodPath.Last());

                if (path.Count() < 3)
                {
                    path.Add(_qLearningPath.Except(_crossedCells).First());
                }

                return path;
            }

            return null;
        }
    }
}