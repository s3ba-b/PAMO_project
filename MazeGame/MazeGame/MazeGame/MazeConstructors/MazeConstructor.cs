using System.Collections.Generic;
using System.Linq;
using MazeGame.Helpers;
using MazeGame.ViewModels;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.MazeConstructors
{
    public interface IMazeConstructor
    {
        Grid GetGameBoardView(
            MazeViewModel mazeViewModel,
            out Button getHintsButton,
            out Label hintsLeftLabel,
            Command getHintCommand,
            Command upButtonCommand,
            Command downButtonCommand,
            Command leftButtonCommand,
            Command rightButtonCommand);
        Grid GetMazeVisualization(IEnumerable<CellViewModel> cellsViewModelsList, MazeSettings settings);
        IEnumerable<CellViewModel> GetCellsViewModelsList(MazeSettings settings);
        IEnumerable<Wall> GetMazeWallsViews(MazeSettings settings);
        Grid GetCellView(double topLeftX, double topLeftY, double size, SquareColorViewModel squareColorViewModel);
        BoxView GetSquareColorView(double size, double topLeftX, double topLeftY);
        Line GetWallView(double x1, double y1, double x2, double y2);
    }
    
    public class MazeConstructor : IMazeConstructor
    {
        public Grid GetGameBoardView(
            MazeViewModel mazeViewModel, 
            out Button getHintsButton, 
            out Label hintsLeftLabel, 
            Command getHintCommand,
            Command upButtonCommand, 
            Command downButtonCommand,
            Command leftButtonCommand,
            Command rightButtonCommand)
        {
            var constructor = new GameBoardConstructor();
            return constructor.GetGameBoardView(
                mazeViewModel, 
                out getHintsButton, 
                out hintsLeftLabel, 
                getHintCommand,
                upButtonCommand, 
                downButtonCommand,
                leftButtonCommand,
                rightButtonCommand);
        }

        public Grid GetMazeVisualization(IEnumerable<CellViewModel> cellsViewModelsList, MazeSettings settings)
        {
            var maze = new Grid();

            maze.Children.Add(GetLatticeGrid(cellsViewModelsList, settings));
            maze.Children.Add(GetBorderWalls(settings));
            maze.Children.Add(GetMazeWalls(settings));

            return maze;
        }
        
        public IEnumerable<CellViewModel> GetCellsViewModelsList(MazeSettings settings)
        {
            var cellsViewModels = new List<CellViewModel>();
            var currentX = settings.StartXPos;
            var currentY = settings.StartYPos;
            int cellId = 0;

            for (int rowNumber = 1; rowNumber <= settings.Model.QuantityOfRows; rowNumber++)
            {
                for (int columnNumber = 1; columnNumber <= settings.Model.QuantityOfColumns; columnNumber++)
                {
                    var cellViewModel = new CellViewModel(cellId, currentX, currentY, settings.Model.SizeOfCell);
                    cellsViewModels.Add(cellViewModel);
                    cellId++;
                    currentX += settings.Model.SizeOfCell;
                }

                currentY += settings.Model.SizeOfCell;
                currentX = settings.StartXPos;
            }

            return cellsViewModels;
        }
        
        private Grid GetMazeWalls(MazeSettings settings)
        {
            var walls = new Grid();
            var wallsList = GetMazeWallsViews(settings);

            foreach (var wall in wallsList)
            {
                walls.Children.Add(wall);
            }

            return walls;
        }

        private Grid GetBorderWalls(MazeSettings settings)
        {
            var borderWalls = new Grid();

            //top border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(settings.StartXPos, settings.StartYPos,
                    settings.StartXPos + settings.MazeWidth - 1, settings.StartYPos)
            });

            //bot border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(settings.StartXPos, settings.StartYPos + settings.MazeHeight - 1,
                    settings.StartXPos + settings.MazeWidth - 1, settings.StartYPos + settings.MazeHeight - 1)
            });

            //left border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(settings.StartXPos, settings.StartYPos, 
                    settings.StartXPos, settings.StartYPos + settings.MazeHeight - 1)
            });

            //right border
            borderWalls.Children.Add(new Wall
            {
                BindingContext = new WallViewModel(settings.StartXPos + settings.MazeWidth - 1, settings.StartYPos, 
                    settings.StartXPos + settings.MazeWidth - 1, settings.StartYPos + settings.MazeHeight - 1)
            });

            return borderWalls;
        }

        private Grid GetLatticeGrid(IEnumerable<CellViewModel> cellsViewModelsList, MazeSettings settings)
        {
            var lattice = new Grid();

            cellsViewModelsList.Where(v => v.Id == settings.Model.Start).FirstOrDefault().State = ESquareState.IsStart;
            cellsViewModelsList.Where(v => v.Id == settings.Model.Goal).FirstOrDefault().State = ESquareState.IsGoal;

            foreach (var cellViewModel in cellsViewModelsList)
            {
                lattice.Children.Add(new Views.Cell { BindingContext = cellViewModel });
            }

            return lattice;
        }
        
        public IEnumerable<Wall> GetMazeWallsViews(MazeSettings settings)
        {
            var constructor = new MazeWallsConstructor(settings);
            return constructor.GetMazeWallsViews();
        }

        public Grid GetCellView(double topLeftX, double topLeftY, double size, SquareColorViewModel squareColorViewModel)
        {
            var constructor = new MazeCellConstructor(topLeftX, topLeftY, size, squareColorViewModel);
            return constructor.GetCellView();
        }

        public BoxView GetSquareColorView(double size, double topLeftX, double topLeftY)
        {
            var constructor = new MazeSquareColorConstructor();
            return constructor.GetSquareColorView(size, topLeftX, topLeftY);
        }

        public Line GetWallView(double x1, double y1, double x2, double y2)
        {
            var constructor = new MazeSingleWallViewConstructor();
            return constructor.GetWall(x1, y1, x2, y2);
        }
    }
}
