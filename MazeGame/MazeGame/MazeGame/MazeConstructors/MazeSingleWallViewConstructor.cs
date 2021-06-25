using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeGame.MazeConstructors
{
    public interface IMazeSingleWallViewConstructor
    {
        Line GetWall(double x1, double y1, double x2, double y2);
    }
    
    public class MazeSingleWallViewConstructor
    {
        public Line GetWall(double x1, double y1, double x2, double y2) =>
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