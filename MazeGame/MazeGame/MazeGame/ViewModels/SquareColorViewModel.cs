using MazeGame.Helpers;
using System.Collections.Generic;
using MazeGame.MazeConstructors;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class SquareColorViewModel
    {
        private ESquareState _state;

        public SquareColorViewModel(double topLeftX, double topLeftY, double height, double width)
        {
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Height = height;
            Width = width;
            var constructor = new MazeConstructor();
            Content = constructor.GetSquareColorView(width, topLeftX, topLeftY);
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
                return _state;
            }
            set
            {
                _state = value;

                if (GetStateColor.TryGetValue(_state, out var color))
                {
                    Content.Color = color;
                }
            }
        }

        private static Dictionary<ESquareState, Color> GetStateColor => new Dictionary<ESquareState, Color>
        {
            { ESquareState.Empty, Color.Transparent },
            { ESquareState.Crossed, Color.LightGreen },
            { ESquareState.IsGoal, Color.Red },
            { ESquareState.IsStart, Color.Green }
        };

    }
}
