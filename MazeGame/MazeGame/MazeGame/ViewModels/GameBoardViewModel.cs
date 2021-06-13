using System;
using System.Linq;
using MazeGame.Helpers;
using MazeGame.Views;
using Q_Learning;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MazeGame.ViewModels
{
    public class GameBoardViewModel
    {
        private readonly MazeViewModel _MazeViewModel;
        private readonly DisplayInfo _displayInfo;
        private int _mazeIndex;
        
        public GameBoardViewModel(int mazeIndex)
        {
            _displayInfo = DeviceDisplay.MainDisplayInfo;
            _mazeIndex = mazeIndex;
            _MazeViewModel = new MazeViewModel(GetMazeSettings(MazeExamples.GetMazeModels().ToArray()[_mazeIndex]));
            Content = GetContent();
        }

        public Grid Content { get; set; }
        public double Width => _displayInfo.Width;
        public double Height => _displayInfo.Height;

        private Grid GetContent()
        {
            var grid = new Grid();

            grid.Children.Add(new Maze
            {
                BindingContext = _MazeViewModel
            });
            grid.Children.Add(GetButton());

            return grid;
        }

        private Button GetButton()
        {
            var button = new Button
            {
                Text = "START",
                MinimumHeightRequest = 50,
                MinimumWidthRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            button.Clicked += async (sender, args) => _MazeViewModel.VisualizeWalk();

            return button;
        }
        
        private MazeSettings GetMazeSettings(MazeModel model) => new MazeSettings
        {
            XPos = Width / 2,
            YPos = Height / 2,
            WindowHeight = Height,
            WindowWidth = Width,
            Model = model
        };
    }
}
