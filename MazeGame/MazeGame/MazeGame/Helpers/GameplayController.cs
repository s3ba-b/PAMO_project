using System.Collections.Generic;
using System.Linq;
using MazeGame.ViewModels;
using Q_Learning;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MazeGame.Helpers
{
    public class GameplayController
    {
        private readonly  IEnumerable<CellViewModel> _cellsViewModels;
        private readonly MazeSettings _mazeSettings;
        private readonly List<int> _crossedCells;
        private int _currentPosition;
        private int _hintsLeft;
        private Button _getHintsButton;
        private Label _hintsLeftLabel;

        public GameplayController(MazeViewModel mazeViewModel, Button button, Label label)
        {
            _cellsViewModels = mazeViewModel.CellsViewModelsList;
            _mazeSettings = mazeViewModel.Settings;
            _crossedCells = new List<int> {mazeViewModel.Settings.Model.Start};
            _currentPosition = mazeViewModel.Settings.Model.Start;
            _hintsLeft = GameplayConsts.START_AMOUNT_OF_HINTS;
            _getHintsButton = button;
            _hintsLeftLabel = label;
        }

        public bool MoveUpButtonClicked()
        {
            var nextPos = _currentPosition - _mazeSettings.Model.QuantityOfColumns;
            if (IsMoveProhibited(_currentPosition, nextPos)) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateState();
        }
        
        public bool MoveLeftButtonClicked()
        {
            var nextPos = _currentPosition - 1;
            var isBorderOfMaze = false;
            
            if (_currentPosition != 0)
            {
                isBorderOfMaze = _currentPosition % _mazeSettings.Model.QuantityOfColumns == 0;
            }

            if (IsMoveProhibited(_currentPosition, nextPos) || isBorderOfMaze) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateState();

        }
        
        public bool MoveRightButtonClicked()
        {
            var nextPos = _currentPosition + 1;
            var isBorderOfMaze = false;
            
            if (_currentPosition != 0)
            {
                isBorderOfMaze = _mazeSettings.Model.QuantityOfColumns % _currentPosition == 1;
            }

            if (IsMoveProhibited(_currentPosition, nextPos) || isBorderOfMaze) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateState();

        }
        
        public bool MoveDownButtonClicked()
        {
            var nextPos = _currentPosition + _mazeSettings.Model.QuantityOfColumns;

            if (IsMoveProhibited(_currentPosition, nextPos)) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateState();
        }

        private bool UpdateState()
        { 
            if (_currentPosition == _mazeSettings.Model.Goal)
            {
                return true;
            }
            
            _cellsViewModels.ForEach(x =>
            {
                if (x.Id == _mazeSettings.Model.Start || x.Id == _mazeSettings.Model.Goal)
                {
                    return;
                }

                x.State = _crossedCells.Contains(x.Id) ? ESquareState.Crossed : ESquareState.Empty;
            });

            return false;
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
        
        private void UpdateRemainingHintsNumber()
        {
            if (_hintsLeft == 0)
            {
                _getHintsButton.IsEnabled = false;
            }
            _hintsLeftLabel.Text = $"Hints left {_hintsLeft}";
        }

        private bool IsMoveProhibited(int currentPos, int nextPos)
        {
            if (nextPos < 0 || nextPos > _mazeSettings.Model.QuantityOfSquares - 1)
            {
                return true;
            }
            return _mazeSettings.Model.Matrix[currentPos][nextPos] == 0;
        }

        public void GetHintClicked()
        {
            if (_hintsLeft != 0)
            {
                var hintsProvider = new HintsProvider(_crossedCells, _mazeSettings.Model);
                var hintsIds = hintsProvider.GetHintCellsIndexes();
                // var currentStepInPath = _qLearningPath.FindIndex(x => x.Equals(_gameplayController.CurrentPosition));
            
                _cellsViewModels.ForEach(cell =>
                {
                    if (hintsIds.Contains(cell.Id))
                    {
                        cell.State = ESquareState.IsHint;
                    }
                });

                _hintsLeft -= 1;
                UpdateRemainingHintsNumber();
            }
        }
    }
}