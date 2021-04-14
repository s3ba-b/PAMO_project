using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeApp.ViewModels
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

        private Line GetWall(double x1, double y1, double x2, double y2)
        {
            var line = new Line();

            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = Brush.Black;
            line.StrokeThickness = 6;

            return line;
        }
    }
}
