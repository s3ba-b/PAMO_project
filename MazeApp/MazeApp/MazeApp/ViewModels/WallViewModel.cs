using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeApp.ViewModels
{
    public class WallViewModel : BindableBase
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
