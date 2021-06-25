using MazeGame.MazeConstructors;
using Xamarin.Forms.Shapes;

namespace MazeGame.ViewModels
{
    public class WallViewModel
    {
        public WallViewModel(double x1, double y1, double x2, double y2)
        {
            Width = x2 - x1 + 1;
            Height = y2 - y1 + 1;
            var constructor = new MazeConstructor();
            Content = constructor.GetWallView(x1, y1, x2, y2);
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public Line Content { get; set; }
    }
}
