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
        public HintsProvider(MazeViewModel mazeViewModel, GameplayController gameplayController)
        {
            _mazeViewModel = mazeViewModel;
            var intelligence = new Intelligence(_mazeViewModel.Settings.Model);
            _qLearningPath = intelligence.GetMoves().ToList();
            _gameplayController = gameplayController;
        }

        public ICommand GetHintCommand => new Command(GetHint);

        private void GetHint()
        {
            var hintsIds = new List<int>();
            var currentStepInPath = _qLearningPath.FindIndex(x => x.Equals(_gameplayController.CurrentPosition));
            for (int i = currentStepInPath + 1; i < currentStepInPath + 4; i++)
            {
                hintsIds.Add(_qLearningPath[i]);
            }
            _mazeViewModel.CellsViewModelsList.ForEach(cell =>
            {
                if (hintsIds.Contains(cell.Id))
                {
                    cell.State = ESquareState.IsHint;
                }
            });
        }
    }
}