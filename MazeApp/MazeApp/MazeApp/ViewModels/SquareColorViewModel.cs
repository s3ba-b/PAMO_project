using MazeApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class SquareColorViewModel
    {
        private ESquareState _State;

        public SquareColorViewModel() { }

        public void Initialize(double topLeftX, double topLeftY, double height, double width)
        {
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Height = height;
            Width = width;
            Content = GetSquare();
            State = ESquareState.Empty;
        }

        public Grid Content { get; set; }
        public double TopLeftX { get; set; }
        public double TopLeftY { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public ESquareState State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;

                if (GetStateColor.TryGetValue(_State, out SolidColorBrush color))
                {
                    Content.Background = color;
                }
            }
        }

        private Grid GetSquare()
        {
            //double canvasPadding = 3;

            //Canvas canvas = new Canvas();
            //canvas.Margin = new Thickness(TopLeftX + canvasPadding, TopLeftY + canvasPadding, 0, 0);
            //canvas.MaxHeight = Height - (canvasPadding * 2);
            //canvas.MinHeight = Height - (canvasPadding * 2);
            //canvas.MaxWidth = Width - (canvasPadding * 2);
            //canvas.MinWidth = Width - (canvasPadding * 2);
            //canvas.HorizontalAlignment = HorizontalAlignment.Left;
            //canvas.VerticalAlignment = VerticalAlignment.Top;

            //return canvas;
            return new Grid();
        }

        private Dictionary<ESquareState, SolidColorBrush> GetStateColor => new Dictionary<ESquareState, SolidColorBrush>
        {
            { ESquareState.Empty, Brush.Transparent },
            { ESquareState.Crossed, Brush.LightGreen },
            { ESquareState.IsGoal, Brush.Red },
            { ESquareState.IsStart, Brush.Green }
        };

    }
}
