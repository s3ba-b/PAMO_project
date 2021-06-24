using System.Collections.Generic;
using MazeGame.ViewModels;
using Xamarin.Forms.Internals;

namespace MazeGame.Helpers
{
    public interface IGameplayController
    {
        List<int> CrossedCells { get; }
        bool TryMoveUp();
        bool TryMoveLeft();
        bool TryMoveRight();
        bool TryMoveDown();
    }
    
    public class GameplayController : IGameplayController
    {
        private readonly IEnumerable<CellViewModel> _cellsViewModels;
        private readonly MazeSettings _mazeSettings;
        private int _currentPosition;

        public GameplayController(MazeViewModel mazeViewModel)
        {
            _cellsViewModels = mazeViewModel.CellsViewModelsList;
            _mazeSettings = mazeViewModel.Settings;
            CrossedCells = new List<int> {mazeViewModel.Settings.Model.Start};
            _currentPosition = mazeViewModel.Settings.Model.Start;
        }
        
        public List<int> CrossedCells { get; }

        public bool TryMoveUp()
        {
            var nextPos = _currentPosition - _mazeSettings.Model.QuantityOfColumns;
            if (IsMoveProhibited(_currentPosition, nextPos)) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateCellsState();
        }
        
        public bool TryMoveLeft()
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
            return UpdateCellsState();

        }
        
        public bool TryMoveRight()
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
            return UpdateCellsState();

        }
        
        public bool TryMoveDown()
        {
            var nextPos = _currentPosition + _mazeSettings.Model.QuantityOfColumns;

            if (IsMoveProhibited(_currentPosition, nextPos)) return false;
            
            _currentPosition = nextPos;
            UpdateCrossedList(_currentPosition);
            return UpdateCellsState();
        }

        private bool UpdateCellsState()
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

                x.State = CrossedCells.Contains(x.Id) ? ESquareState.Crossed : ESquareState.Empty;
            });

            return false;
        }

        private void UpdateCrossedList(int pos)
        {
            if (CrossedCells.Contains(pos))
            {
                CrossedCells.RemoveAt(CrossedCells.Count - 1);
            }
            else
            {
                CrossedCells.Add(pos);
            }
        }
        
        private bool IsMoveProhibited(int currentPos, int nextPos)
        {
            if (nextPos < 0 || nextPos > _mazeSettings.Model.QuantityOfSquares - 1)
            {
                return true;
            }
            return _mazeSettings.Model.Matrix[currentPos][nextPos] == 0;
        }
    }
}