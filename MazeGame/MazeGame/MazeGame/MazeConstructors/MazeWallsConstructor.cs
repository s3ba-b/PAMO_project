using System;
using System.Collections.Generic;
using MazeGame.Helpers;
using MazeGame.ViewModels;
using MazeGame.Views;
using Q_Learning;

namespace MazeGame.MazeConstructors
{
    /// <summary>
    /// Methods with logic creating walls views for the maze.
    /// </summary>
    public interface IMazeWallsConstructor
    {
        IEnumerable<Wall> GetMazeWallsViews();
    }
    
    public class MazeWallsConstructor : IMazeWallsConstructor
    {
        private readonly MazeSettings _settings;
        private readonly MazeModel _model;

        public MazeWallsConstructor(MazeSettings settings)
        {
            _settings = settings;
            _model = _settings.Model;
        }

        public IEnumerable<Wall> GetMazeWallsViews()
        {
            var walls = new List<Wall>();

            for (int i = 0; i < _model.QuantityOfSquares; i++)
            {
                for (int j = 0; j < _model.QuantityOfSquares; j++)
                {
                    if (ShouldBeWallBetween(i, j))
                    {
                        var (x1, y1, x2, y2) = GetEndpointsOfWall(i, j);
                        walls.Add(new Wall
                        {
                            BindingContext = new WallViewModel(x1, y1, x2, y2)
                        });
                    }
                }
            }

            return walls;
        }

        private bool ShouldBeWallBetween(int firstSquareIndex, int secondSquareIndex)
        {
            var indexesAreNotTheSame = firstSquareIndex != secondSquareIndex;
            var squaresAreNeighbors = (firstSquareIndex - secondSquareIndex == 1) ||
                                      (secondSquareIndex - firstSquareIndex == 1) ||
                                      (firstSquareIndex - secondSquareIndex == _model.QuantityOfColumns) ||
                                      (secondSquareIndex - firstSquareIndex == _model.QuantityOfColumns);
            var moveIsNotAllowed = _model.Matrix[firstSquareIndex][secondSquareIndex] == 0;

            return indexesAreNotTheSame && squaresAreNeighbors && moveIsNotAllowed;
        }

        private (double x1, double y1, double x2, double y2) GetEndpointsOfWall(int firstSquareId, int secondSquareId)
        {
            (double x, double y) firstSquareTopLeftCornerPosition = GetPositionOfSquareTopLeftCorner(firstSquareId);
            double x1 = -1, y1 = -1, x2 = -1, y2 = -1;

            //top wall of first square
            if (firstSquareId == (secondSquareId + _model.QuantityOfColumns))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x + _model.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y;
            }

            //bottom wall of first square
            if (firstSquareId == (secondSquareId - _model.QuantityOfColumns))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y + _model.SizeOfCell;
                x2 = firstSquareTopLeftCornerPosition.x + _model.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y + _model.SizeOfCell;
            }

            //left wall of first square
            if (firstSquareId == (secondSquareId + 1))
            {
                x1 = firstSquareTopLeftCornerPosition.x;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x;
                y2 = firstSquareTopLeftCornerPosition.y + _model.SizeOfCell;
            }

            //right wall of first square
            if (firstSquareId == (secondSquareId - 1))
            {
                x1 = firstSquareTopLeftCornerPosition.x + _model.SizeOfCell;
                y1 = firstSquareTopLeftCornerPosition.y;
                x2 = firstSquareTopLeftCornerPosition.x + _model.SizeOfCell;
                y2 = firstSquareTopLeftCornerPosition.y + _model.SizeOfCell;
            }

            return (x1, y1, x2, y2);
        }

        private (double x, double y) GetPositionOfSquareTopLeftCorner(int index)
        {
            var matrix = GetLatticeMatrix();

            for (int row = 0; row < _model.QuantityOfRows; row++)
            {
                for (int column = 0; column < _model.QuantityOfColumns; column++)
                {
                    if (matrix[row, column] == index)
                    {
                        double x = (column * _model.SizeOfCell) + _settings.StartXPos;
                        double y = (row * _model.SizeOfCell) + _settings.StartYPos;

                        return (x, y);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private int[,] GetLatticeMatrix()
        {
            int[,] matrix = new int[_model.QuantityOfRows, _model.QuantityOfColumns];
            int index = 0;

            for (int row = 0; row < _model.QuantityOfRows; row++)
            {
                for (int column = 0; column < _model.QuantityOfColumns; column++)
                {
                    matrix[row, column] = index;
                    index++;
                }
            }

            return matrix;
        }
    }
}