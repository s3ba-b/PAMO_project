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
        private readonly MazeViewModel _mazeViewModel;
        private readonly List<int> _qLearningPath;
        private readonly GameplayController _gameplayController;
        private List<int> _crossedCells;
        
        public HintsProvider(MazeViewModel mazeViewModel, GameplayController gameplayController)
        {
            _mazeViewModel = mazeViewModel;
            var intelligence = new Intelligence(_mazeViewModel.Settings.Model);
            _qLearningPath = gameplayController.QLearningPath;
            _gameplayController = gameplayController;
            _crossedCells = gameplayController.CrossedCells;
            HintsLeft = 3;
        }
        
        public ICommand GetHintCommand => new Command(GetHint);
        
        
        public int HintsLeft { get; private set; }

        private void GetHint()
        {
            if (HintsLeft != 0)
            {
                var hintsIds = GetPathDescent();
                // var currentStepInPath = _qLearningPath.FindIndex(x => x.Equals(_gameplayController.CurrentPosition));
            
                _mazeViewModel.CellsViewModelsList.ForEach(cell =>
                {
                    if (hintsIds.Contains(cell.Id))
                    {
                        cell.State = ESquareState.IsHint;
                    }
                });

                HintsLeft -= 1;
                MessagingCenter.Send<HintsProvider>(this, "Hints left updated");
            }
        }

        private IEnumerable<int> GetPathDescent()
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