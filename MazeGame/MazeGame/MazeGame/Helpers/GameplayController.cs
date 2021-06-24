using System;
using System.Collections;
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
        private IEnumerable<CellViewModel> _cells;
        private MazeSettings _mazeSettings;
        
        public GameplayController(MazeViewModel mazeViewModel)
        {
            _cells = mazeViewModel.CellsViewModelsList;
            CurrentPosition = mazeViewModel.Settings.Model.Start;
            _mazeSettings = mazeViewModel.Settings;
            CrossedCells.Add(mazeViewModel.Settings.Model.Start);
            var intelligence = new Intelligence(mazeViewModel.Settings.Model);
            QLearningPath = intelligence.GetMoves().ToList();
        }
        
        public int CurrentPosition { get; set; }
        public List<int> QLearningPath { get; set; }
        public List<int> CrossedCells = new List<int>();

        public bool MoveUpButtonClicked()
        {
            var nextPos = CurrentPosition - _mazeSettings.Model.QuantityOfColumns;
            if (!IsMoveProhibited(CurrentPosition, nextPos))
            {
                CurrentPosition = nextPos;
                UpdateCrossedList(CurrentPosition);
                return UpdateState();
            }

            return false;
        }
        
        public bool MoveLeftButtonClicked()
        {
            var nextPos = CurrentPosition - 1;
            var isBorderOfMaze = false;
            if (CurrentPosition != 0)
            {
                isBorderOfMaze = CurrentPosition % _mazeSettings.Model.QuantityOfColumns == 0;
            } if (!(IsMoveProhibited(CurrentPosition, nextPos) || isBorderOfMaze))
            {
                CurrentPosition = nextPos;
                UpdateCrossedList(CurrentPosition);
                return UpdateState();
            }

            return false;
        }
        
        public bool MoveRightButtonClicked()
        {
            var nextPos = CurrentPosition + 1;
            var isBorderOfMaze = false;
            if (CurrentPosition != 0)
            {
                isBorderOfMaze = _mazeSettings.Model.QuantityOfColumns % CurrentPosition == 1;
            }
            if (!(IsMoveProhibited(CurrentPosition, nextPos) || isBorderOfMaze))
            {
                CurrentPosition = nextPos;
                UpdateCrossedList(CurrentPosition);
                return UpdateState();
            }

            return false;
        }
        
        public bool MoveDownButtonClicked()
        {
            var nextPos = CurrentPosition + _mazeSettings.Model.QuantityOfColumns;
            if (!IsMoveProhibited(CurrentPosition, nextPos))
            {
                CurrentPosition = nextPos;
                UpdateCrossedList(CurrentPosition);
                return UpdateState();
            }

            return false;
        }

        private bool UpdateState()
        { 
            if (CurrentPosition == _mazeSettings.Model.Goal)
            {
                return true;
            }
            
            _cells.ForEach(x =>
            {
                if (x.Id == _mazeSettings.Model.Start || x.Id == _mazeSettings.Model.Goal)
                {
                    return;
                }
                
                if (CrossedCells.Contains(x.Id))
                {
                    x.State = ESquareState.Crossed;
                }
                else
                {
                    x.State = ESquareState.Empty;
                }
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