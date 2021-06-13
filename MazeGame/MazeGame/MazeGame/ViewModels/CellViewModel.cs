using MazeGame.Helpers;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.ViewModels
{
    public class CellViewModel
    {
        private readonly SquareColorViewModel _SquareColorViewModel;

        public CellViewModel(int id, double topLeftX, double topLeftY, int size) 
        {
            Id = id;
            Height = size;
            Width = size;
            _SquareColorViewModel = new SquareColorViewModel(topLeftX, topLeftY, Height, Width);
            Content = CreateCell(topLeftX, topLeftY);
        }

        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public Grid Content { get; set; }
        public ESquareState State
        {
            get
            {
                return _SquareColorViewModel.State;
            }
            set
            {
                _SquareColorViewModel.State = value;
            }
        }

        private Grid CreateCell(double topLeftX, double topLeftY)
        {
            var grid = new Grid();

            var shiftBeyondCornersInX = Width - 1;
            var shiftBeyondCornersInY = Height - 1;

            var topLine = GetLine(topLeftX, topLeftY, topLeftX + shiftBeyondCornersInX, topLeftY);
            grid.Children.Add(topLine);

            var botLine = GetLine(topLeftX, topLeftY + shiftBeyondCornersInY, topLeftX + shiftBeyondCornersInX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(botLine);

            var leftLine = GetLine(topLeftX, topLeftY, topLeftX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(leftLine);

            var rightLine = GetLine(topLeftX + shiftBeyondCornersInX, topLeftY, topLeftX + shiftBeyondCornersInX, topLeftY + shiftBeyondCornersInY);
            grid.Children.Add(rightLine);

            grid.Children.Add(new SquareColor
            {
                BindingContext = _SquareColorViewModel
            });

            return grid;
        }

        private Line GetLine(double x1, double y1, double x2, double y2)
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
