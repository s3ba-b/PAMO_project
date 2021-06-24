using System.Collections.Generic;
using MazeGame.Helpers;
using MazeGame.ViewModels;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.MazeConstructors
{
    public interface IMazeConstructor
    {
        IEnumerable<Wall> GetMazeWallsViews(MazeSettings settings);
        Grid GetCellView(double topLeftX, double topLeftY, double size, SquareColorViewModel squareColorViewModel);
        BoxView GetSquareColorView(double size, double topLeftX, double topLeftY);
        Line GetWallView(double x1, double y1, double x2, double y2);
    }
    
    public class MazeConstructor : IMazeConstructor
    {
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
