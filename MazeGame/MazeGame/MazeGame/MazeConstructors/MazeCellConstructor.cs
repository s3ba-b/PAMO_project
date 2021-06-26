using MazeGame.ViewModels;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.MazeConstructors
{
    /// <summary>
    /// Methods which are used for creating a cell view.
    /// </summary>
    public interface IMazeCellConstructor
    {
        Grid GetCellView();
    }
    
    public class MazeCellConstructor : IMazeCellConstructor
    {
        private readonly double _topLeftX;
        private readonly double _topLeftY;
        private readonly double _size;
        private readonly SquareColorViewModel _squareColorViewModel;
        
        public MazeCellConstructor(double topLeftX, double topLeftY, double size,
            SquareColorViewModel squareColorViewModel)
        {
            _topLeftX = topLeftX;
            _topLeftY = topLeftY;
            _size = size;
            _squareColorViewModel = squareColorViewModel;
        }
        
        public Grid GetCellView()
        {
            var grid = new Grid();

            var shiftBeyondCorners = _size - 1;

            var topLine = GetLine(_topLeftX, _topLeftY, _topLeftX + shiftBeyondCorners, _topLeftY);
            grid.Children.Add(topLine);

            var botLine = GetLine(_topLeftX, _topLeftY + shiftBeyondCorners, _topLeftX + shiftBeyondCorners, _topLeftY + shiftBeyondCorners);
            grid.Children.Add(botLine);

            var leftLine = GetLine(_topLeftX, _topLeftY, _topLeftX, _topLeftY + shiftBeyondCorners);
            grid.Children.Add(leftLine);

            var rightLine = GetLine(_topLeftX + shiftBeyondCorners, _topLeftY, _topLeftX + shiftBeyondCorners, _topLeftY + shiftBeyondCorners);
            grid.Children.Add(rightLine);

            grid.Children.Add(new SquareColor
            {
                BindingContext = _squareColorViewModel
            });

            return grid;
        }

        private static Line GetLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brush.Gray,
                StrokeThickness = 0.5
            };

            return line;
        }
    }
}