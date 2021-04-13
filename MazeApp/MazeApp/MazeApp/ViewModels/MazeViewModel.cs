using MazeApp.Helpers;
using MazeApp.Views;
using Q_Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MazeViewModel
    {
        private readonly MazeSettings _Settings;
        private readonly MazeConstructor _Constructor;
        private readonly IEnumerable<CellViewModel> _CellsViewModelsList;

        public MazeViewModel(MazeSettings settings)
        {
            _Settings = settings;
            _Constructor = new MazeConstructor(settings);
            _CellsViewModelsList = GetCellsViewModelsList();

            Content = GetMazeVisualization();
        }

        public UIElement Content { get; set; }

        private UIElement GetMazeVisualization()
        {
            var maze = new Grid();

            maze.Children.Add(GetLatticeGrid());
            maze.Children.Add(GetBorderWalls());
            maze.Children.Add(GetMazeWalls());

            return maze;
        }

        public void VisualizeWalk()
        {
            var intelligence = new Intelligence(_Settings.Model);
            var moves = intelligence.GetMoves();
            moves = moves.Skip(1);
            moves = moves.Take(moves.Count() - 1);

            foreach (var move in moves)
            {
                _CellsViewModelsList.Where(cell => cell.Id == move).First().State = ESquareState.Crossed;
            }
        }

        private Grid GetMazeWalls()
        {
            var walls = new Grid();
            var wallsList = _Constructor.GetMazeWallsViews();

            foreach (var wall in wallsList)
            {
                walls.Children.Add(wall);
            }

            return walls;
        }

        private Grid GetBorderWalls()
        {
            var borderWalls = new Grid();

            //top border
            var wall = new Wall(new WallViewModel(_Settings.StartXPos, _Settings.StartYPos, _Settings.StartXPos + _Settings.MazeWidth - 1, _Settings.StartYPos));
            borderWalls.Children.Add(wall);

            //bot border
            wall = new Wall(new WallViewModel(_Settings.StartXPos, _Settings.StartYPos + _Settings.MazeHeight - 1, _Settings.StartXPos + _Settings.MazeWidth - 1, _Settings.StartYPos + _Settings.MazeHeight - 1));
            borderWalls.Children.Add(wall);

            //left border
            wall = new Wall(new WallViewModel(_Settings.StartXPos, _Settings.StartYPos, _Settings.StartXPos, _Settings.StartYPos + _Settings.MazeHeight - 1));
            borderWalls.Children.Add(wall);

            //right border
            wall = new Wall(new WallViewModel(_Settings.StartXPos + _Settings.MazeWidth - 1, _Settings.StartYPos, _Settings.StartXPos + _Settings.MazeWidth - 1, _Settings.StartYPos + _Settings.MazeHeight - 1));
            borderWalls.Children.Add(wall);

            return borderWalls;
        }

        private Grid GetLatticeGrid()
        {
            var lattice = new Grid();

            _CellsViewModelsList.Where(v => v.Id == _Settings.Model.Start).FirstOrDefault().State = ESquareState.IsStart;
            _CellsViewModelsList.Where(v => v.Id == _Settings.Model.Goal).FirstOrDefault().State = ESquareState.IsGoal;

            foreach (var cellViewModel in _CellsViewModelsList)
            {
                lattice.Children.Add(new Cell(cellViewModel));
            }

            return lattice;
        }

        private IEnumerable<CellViewModel> GetCellsViewModelsList()
        {
            var cellsViewModels = new List<CellViewModel>();
            var currentX = _Settings.StartXPos;
            var currentY = _Settings.StartYPos;
            int cellId = 0;

            for (int rowNumber = 1; rowNumber <= _Settings.Model.QuantityOfRows; rowNumber++)
            {
                for (int columnNumber = 1; columnNumber <= _Settings.Model.QuantityOfColumns; columnNumber++)
                {
                    var cellViewModel = new CellViewModel(cellId, currentX, currentY, _Settings.Model.SizeOfCell);
                    cellsViewModels.Add(cellViewModel);
                    cellId++;
                    currentX += _Settings.Model.SizeOfCell;
                }

                currentY += _Settings.Model.SizeOfCell;
                currentX = _Settings.StartXPos;
            }

            return cellsViewModels;
        }
    }
}
