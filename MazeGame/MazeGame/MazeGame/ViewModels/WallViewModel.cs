using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.ViewModels
{
    public class WallViewModel
    {
        public WallViewModel(double x1, double y1, double x2, double y2)
        {
            Width = x2 - x1 + 1;
            Height = y2 - y1 + 1;
            Content = GetWall(x1, y1, x2, y2);
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public Line Content { get; set; }

        private Line GetWall(double x1, double y1, double x2, double y2) =>
            new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brush.Black,
                StrokeThickness = 6
            };
    }
}
