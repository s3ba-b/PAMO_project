using System.Linq;
using MazeGame.Helpers;
using MazeGame.Views;
using Q_Learning;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace MazeGame.ViewModels
{
    public class GameBoardViewModel
    {
        private readonly MazeViewModel _MazeViewModel;
        
        public GameBoardViewModel(int mazeIndex)
        {
            _MazeViewModel = new MazeViewModel(GetMazeSettings(MazeExamples.GetMazeModels().ToArray()[mazeIndex - 1]));
            Content = GetContent();
        }

        public Grid Content { get; set; }
        public double Width => Current.MainPage.Width;
        public double Height => Current.MainPage.Height;

        private Grid GetContent()
        {
            var grid = new Grid();

            grid.Children.Add(GetButton());
            grid.Children.Add(new Maze
            {
                BindingContext = _MazeViewModel
            });

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
