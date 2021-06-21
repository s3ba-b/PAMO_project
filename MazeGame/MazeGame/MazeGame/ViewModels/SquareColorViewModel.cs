using MazeGame.Helpers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class SquareColorViewModel
    {
        private ESquareState _State;

        public SquareColorViewModel(double topLeftX, double topLeftY, double height, double width)
        {
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Height = height;
            Width = width;
            Content = GetSquare();
            State = ESquareState.Empty;
        }

        public BoxView Content { get; set; }
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

                if (GetStateColor.TryGetValue(_State, out Color color))
                {
                    Content.Color = color;
                }
            }
        }

        private BoxView GetSquare()
        {
            double canvasPadding = 3;

            var square = new BoxView()
            {
                WidthRequest = Width - (canvasPadding * 2),
                HeightRequest = Height - (canvasPadding * 2),
                Margin = new Thickness(TopLeftX + canvasPadding, TopLeftY + canvasPadding, 0, 0),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start
            };

            return square;
        }

        private Dictionary<ESquareState, Color> GetStateColor => new Dictionary<ESquareState, Color>
        {
            { ESquareState.Empty, Color.Transparent },
            { ESquareState.Crossed, Color.LightGreen },
            { ESquareState.IsGoal, Color.Red },
            { ESquareState.IsStart, Color.Green },
            { ESquareState.IsHint, Color.Fuchsia }
        };

    }
}
