using MazeApp.Helpers;
using MazeApp.Views;
using Prism.Ioc;
using Prism.Mvvm;
using Q_Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MazeViewModel : BindableBase
    {
        private readonly IContainerProvider _containerProvider;
        private MazeSettings _Settings;
        private MazeConstructor _Constructor;
        private IEnumerable<CellViewModel> _CellsViewModelsList;

        public MazeViewModel(IContainerProvider containerProvider) 
        {
            _containerProvider = containerProvider;
        }

        public Grid Content { get; set; }

        public void Initialize(MazeSettings settings)
        {
            _Settings = settings;
            _Constructor = new MazeConstructor(settings, _containerProvider);
            _CellsViewModelsList = GetCellsViewModelsList();
            Content = GetMazeVisualization();
        }

        private Grid GetMazeVisualization()
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
            var wallView = _containerProvider.Resolve<Wall>();
            var wallViewModel = _containerProvider.Resolve<WallViewModel>();
            wallViewModel.X1 = _Settings.StartXPos;
            wallViewModel.Y1 = _Settings.StartYPos;
            wallViewModel.X2 = _Settings.StartXPos + _Settings.MazeWidth - 1;
            wallViewModel.Y2 = _Settings.StartYPos;
            wallViewModel.Stroke = Brush.Black;
            wallViewModel.StrokeThickness = 6;
            wallView.BindingContext = wallViewModel;
            borderWalls.Children.Add(wallView);

            //bot border
            wallView = _containerProvider.Resolve<Wall>();
            wallViewModel = _containerProvider.Resolve<WallViewModel>();
            wallViewModel.X1 = _Settings.StartXPos;
            wallViewModel.Y1 = _Settings.StartYPos + _Settings.MazeHeight - 1;
            wallViewModel.X2 = _Settings.StartXPos + _Settings.MazeWidth - 1;
            wallViewModel.Y2 = _Settings.StartYPos + _Settings.MazeHeight - 1;
            wallViewModel.Stroke = Brush.Black;
            wallViewModel.StrokeThickness = 6;
            wallView.BindingContext = wallViewModel;
            borderWalls.Children.Add(wallView);

            //left border
            wallView = _containerProvider.Resolve<Wall>();
            wallViewModel = _containerProvider.Resolve<WallViewModel>();
            wallViewModel.X1 = _Settings.StartXPos;
            wallViewModel.Y1 = _Settings.StartYPos;
            wallViewModel.X2 = _Settings.StartXPos;
            wallViewModel.Y2 = _Settings.StartYPos + _Settings.MazeHeight - 1;
            wallViewModel.Stroke = Brush.Black;
            wallViewModel.StrokeThickness = 6;
            wallView.BindingContext = wallViewModel;
            borderWalls.Children.Add(wallView);

            //right border
            wallView = _containerProvider.Resolve<Wall>();
            wallViewModel = _containerProvider.Resolve<WallViewModel>();
            wallViewModel.X1 = _Settings.StartXPos + _Settings.MazeWidth - 1;
            wallViewModel.Y1 = _Settings.StartYPos;
            wallViewModel.X2 = _Settings.StartXPos + _Settings.MazeWidth - 1;
            wallViewModel.Y2 = _Settings.StartYPos + _Settings.MazeHeight - 1;
            wallViewModel.Stroke = Brush.Black;
            wallViewModel.StrokeThickness = 6;
            wallView.BindingContext = wallViewModel;
            borderWalls.Children.Add(wallView);

            return borderWalls;
        }

        private Grid GetLatticeGrid()
        {
            var lattice = new Grid();

            _CellsViewModelsList = _CellsViewModelsList.Select(v =>
            {
                if(v.Id == _Settings.Model.Start)
                {
                    v.State = ESquareState.IsStart;
                }

                return v;
            });

            _CellsViewModelsList = _CellsViewModelsList.Select(v =>
            {
                if (v.Id == _Settings.Model.Goal)
                {
                   v.State = ESquareState.IsGoal;
                }

                return v;
            });

            foreach (var cellViewModel in _CellsViewModelsList)
            {
                var cellView = _containerProvider.Resolve<Views.Cell>();
                cellView.BindingContext = cellViewModel;
                lattice.Children.Add(cellView);
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
                    var cellViewModel = _containerProvider.Resolve<CellViewModel>();
                    cellViewModel.Initialize(_containerProvider, cellId, currentX, currentY, _Settings.Model.SizeOfCell);
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
