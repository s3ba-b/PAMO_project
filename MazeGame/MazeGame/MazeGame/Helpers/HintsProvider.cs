using System.Collections.Generic;
using System.Linq;
using Q_Learning;

namespace MazeGame.Helpers
{
    public interface IHintsProvider
    {
        IEnumerable<int> GetHintCellsIndexes();
    }
    
    public class HintsProvider : IHintsProvider
    {
        private readonly IEnumerable<int> _qLearningPath;
        private readonly IEnumerable<int> _crossedCells;
        
        public HintsProvider(IEnumerable<int> crossedCells, MazeModel model)
        {
            _crossedCells = crossedCells;
            _qLearningPath = GetQLearningPath(model);
        }

        public IEnumerable<int> GetHintCellsIndexes()
        {
            var pathDescent = _crossedCells.Except(_qLearningPath).ToList();
            
            if (!pathDescent.Any())
            {
                return _qLearningPath.Except(_crossedCells).Take(3);
            }
            
            if (pathDescent.Count() >= 3)
            {
                pathDescent.Reverse();
                return pathDescent.Take(3);
            }

            if (!pathDescent.Any() || pathDescent.Count() >= 3) return null;
            
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
        
        private static IEnumerable<int> GetQLearningPath(MazeModel model)
        {
            var intelligence = new Intelligence(model);
            return intelligence.GetMoves();
        }
    }
}