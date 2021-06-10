using MazeApp.Helpers;
using MazeApp.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MazeApp.ViewModels
{
    public class CellViewModel : BindableBase
    {
        private IContainerProvider _containerProvider;
        private SquareColor _SquareColorView;
        private SquareColorViewModel _SquareColorViewModel;

        public CellViewModel() { }

        public void Initialize(IContainerProvider containerProvider, int id, double topLeftX, double topLeftY, int size)
        {
            _containerProvider = containerProvider;
            Id = id;
            Height = size;
            Width = size;
            var squareColorView = _containerProvider.Resolve<SquareColor>();
            var squareColorViewModel = _containerProvider.Resolve<SquareColorViewModel>();
            squareColorViewModel.Initialize(topLeftX, topLeftY, Height, Width);
            squareColorView.BindingContext = squareColorViewModel;
            _SquareColorView = squareColorView;
            _SquareColorViewModel = squareColorViewModel;
            //Content = CreateCell(topLeftX, topLeftY);
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

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            var line = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 2
            };

            line.StrokeCap = 0;
            canvas.DrawLine(10, 10, 40, 10, line);
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

            grid.Children.Add(_SquareColorView);

            return grid;
        }

        private Line GetLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line();

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = Brush.Gray;
            line.StrokeThickness = 0.5;

            return line;
        }
    }
}
