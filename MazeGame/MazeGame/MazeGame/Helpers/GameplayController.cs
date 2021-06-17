using System;
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
            var nextPos = _currentPosition - _mazeSettings.Model.QuantityOfColumns;
            if (!IsMoveProhibited(_currentPosition, nextPos))
            {
                _currentPosition = nextPos;
                UpdateCrossedList(_currentPosition);
                UpdateState();
            }
            
        }
        
        public void MoveLeftButtonClicked()
        {
            var nextPos = _currentPosition - 1;
            var isBorderOfMaze = false;
            if (_currentPosition != 0)
            {
                isBorderOfMaze = _currentPosition % _mazeSettings.Model.QuantityOfColumns == 0;
            }
            if (!(IsMoveProhibited(_currentPosition, nextPos) || isBorderOfMaze))
            {
                _currentPosition = nextPos;
                UpdateCrossedList(_currentPosition);
                UpdateState();
            }
        }
        
        public void MoveRightButtonClicked()
        {
            var nextPos = _currentPosition + 1;
            var isBorderOfMaze = false;
            if (_currentPosition != 0)
            {
                isBorderOfMaze = _mazeSettings.Model.QuantityOfColumns % _currentPosition == 1;
            }
            if (!(IsMoveProhibited(_currentPosition, nextPos) || isBorderOfMaze))
            {
                _currentPosition = nextPos;
                UpdateCrossedList(_currentPosition);
                UpdateState();
            }
        }
        
        public void MoveDownButtonClicked()
        {
            var nextPos = _currentPosition + _mazeSettings.Model.QuantityOfColumns;
            if (!IsMoveProhibited(_currentPosition, nextPos))
            {
                _currentPosition = nextPos;
                UpdateCrossedList(_currentPosition);
                UpdateState();
            }
        }

        private void UpdateState()
        { 
            _cells.ForEach(x =>
            {
                if (x.Id == _mazeSettings.Model.Start || x.Id == _mazeSettings.Model.Goal)
                {
                    return;
                }
                
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