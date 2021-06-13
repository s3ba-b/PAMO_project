using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class WallViewModel
    {
        public WallViewModel() { }

        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }
    }
}
