using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeGame.ViewModels;
using Xamarin.Forms.Internals;

namespace MazeGame.Helpers
{
    public class GameplayController
    {
        private IEnumerable<CellViewModel> _cells;
        private int _currentPosition;
        private MazeSettings _mazeSettings;
        private List<int> _crossedCells = new List<int>();

        public GameplayController(MazeViewModel mazeViewModel)
        {
            _cells = mazeViewModel.CellsViewModelsList;
            _currentPosition = mazeViewModel.Settings.Model.Start;
            _mazeSettings = mazeViewModel.Settings;
            _crossedCells.Add(mazeViewModel.Settings.Model.Start);
        }

        public void MoveUpButtonClicked()
        {
            _currentPosition -= _mazeSettings.Model.QuantityOfColumns;
            UpdateCrossedList(_currentPosition);
            UpdateState();
        }
        
        public void MoveLeftButtonClicked()
        {
            _currentPosition -= 1;
            UpdateCrossedList(_currentPosition);
            UpdateState();
        }
        
        public void MoveRightButtonClicked()
        {
            _currentPosition += 1;
            UpdateCrossedList(_currentPosition);
            UpdateState();
        }
        
        public void MoveDownButtonClicked()
        {
            _currentPosition += _mazeSettings.Model.QuantityOfColumns;
            UpdateCrossedList(_currentPosition);
            UpdateState();
        }

        private void UpdateState()
        { 
            _cells.ForEach(x =>
            {
                if (_crossedCells.Contains(x.Id))
                {
                    x.State = ESquareState.Crossed;
                }
                else
                {
                    x.State = ESquareState.Empty;
                }
            });
        }

        private void UpdateCrossedList(int pos)
        {
            if (_crossedCells.Contains(pos))
            {
                _crossedCells.RemoveAt(_crossedCells.Count - 1);
            }
            else
            {
                _crossedCells.Add(pos);
            }
        }
    }
}