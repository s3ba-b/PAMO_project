using MazeGame.Helpers;
using MazeGame.Views;
using Q_Learning;
using System.Collections.Generic;
using System.Linq;
using MazeGame.MazeConstructors;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class MazeViewModel
    {
        private MazeConstructor _Constructor;

        public MazeViewModel(MazeSettings settings)
        {
            Settings = settings;
            _Constructor = new MazeConstructor();
            CellsViewModelsList = GetCellsViewModelsList();

            Content = GetMazeVisualization();
        }

        public Grid Content { get; set; }
        
        public IEnumerable<CellViewModel> CellsViewModelsList { get; set; }
        public MazeSettings Settings { get; set; }

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
            var intelligence = new Intelligence(Settings.Model);
            var moves = intelligence.GetMoves();
            moves = moves.Skip(1);
            moves = moves.Take(moves.Count() - 1);

            foreach (var move in moves)
            {
                CellsViewModelsList.Where(cell => cell.Id == move).First().State = ESquareState.Crossed;
            }
        }

        private Grid GetMazeWalls()
        {
            var walls = new Grid();
            var wallsList = _Constructor.GetMazeWallsViews(Settings);

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
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(Settings.StartXPos, Settings.StartYPos,
                    Settings.StartXPos + Settings.MazeWidth - 1, Settings.StartYPos)
            });

            //bot border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(Settings.StartXPos, Settings.StartYPos + Settings.MazeHeight - 1,
                    Settings.StartXPos + Settings.MazeWidth - 1, Settings.StartYPos + Settings.MazeHeight - 1)
            });

            //left border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(Settings.StartXPos, Settings.StartYPos, 
                    Settings.StartXPos, Settings.StartYPos + Settings.MazeHeight - 1)
            });

            //right border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(Settings.StartXPos + Settings.MazeWidth - 1, Settings.StartYPos, 
                    Settings.StartXPos + Settings.MazeWidth - 1, Settings.StartYPos + Settings.MazeHeight - 1)
            });

            return borderWalls;
        }

        private Grid GetLatticeGrid()
        {
            var lattice = new Grid();

            CellsViewModelsList.Where(v => v.Id == Settings.Model.Start).FirstOrDefault().State = ESquareState.IsStart;
            CellsViewModelsList.Where(v => v.Id == Settings.Model.Goal).FirstOrDefault().State = ESquareState.IsGoal;

            foreach (var cellViewModel in CellsViewModelsList)
            {
                lattice.Children.Add(new Views.Cell { BindingContext = cellViewModel });
            }

            return lattice;
        }

        private IEnumerable<CellViewModel> GetCellsViewModelsList()
        {
            var cellsViewModels = new List<CellViewModel>();
            var currentX = Settings.StartXPos;
            var currentY = Settings.StartYPos;
            int cellId = 0;

            for (int rowNumber = 1; rowNumber <= Settings.Model.QuantityOfRows; rowNumber++)
            {
                for (int columnNumber = 1; columnNumber <= Settings.Model.QuantityOfColumns; columnNumber++)
                {
                    var cellViewModel = new CellViewModel(cellId, currentX, currentY, Settings.Model.SizeOfCell);
                    cellsViewModels.Add(cellViewModel);
                    cellId++;
                    currentX += Settings.Model.SizeOfCell;
                }

                currentY += Settings.Model.SizeOfCell;
                currentX = Settings.StartXPos;
            }

            return cellsViewModels;
        }
    }
}
