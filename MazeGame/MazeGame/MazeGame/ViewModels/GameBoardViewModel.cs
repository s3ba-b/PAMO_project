using System.Collections;
using System.Linq;
using System.Windows.Input;
using MazeGame.Helpers;
using MazeGame.Views;
using Q_Learning;
using Xamarin.Forms;
using static Xamarin.Forms.Application;

namespace MazeGame.ViewModels
{
    public class GameBoardViewModel
    {
        private readonly MazeViewModel _mazeViewModel;
        private readonly GameplayController _gameplayController;
        
        public GameBoardViewModel(int mazeIndex)
        {
            var mazeSettings = GetMazeSettings(MazeExamples.GetMazeModels().ToArray()[mazeIndex - 1]);
            _mazeViewModel = new MazeViewModel(mazeSettings);
            _gameplayController = new GameplayController(_mazeViewModel);
            Content = GetContent();
        }

        public Grid Content { get; set; }
        public double Width => Current.MainPage.Width;
        public double Height => Current.MainPage.Height;

        private Grid GetContent()
        {
            var grid = new Grid();
            
            grid.Children.Add(new Maze
            {
                BindingContext = _mazeViewModel
            });
            
            grid.Children.Add(GetControls());

            return grid;
        }

        private StackLayout GetControls()
        {
            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 0, 25)
            };
            
            stack.Children.Add(new Button()
            {
                Text = "Up",
                WidthRequest = 50,
                HeightRequest = 50,
                Margin = new Thickness(50, 0, 50,0),
                Command = UpButtonCommand
            });

            var internalStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 50
            };
            
            stack.Children.Add(internalStack);
            
            internalStack.Children.Add(new Button()
            {
                Text = "Left",
                WidthRequest = 50,
                HeightRequest = 50,
                Command = LeftButtonCommand
            });
            
            internalStack.Children.Add(new Button()
            {
                Text = "Right",
                WidthRequest = 50,
                HeightRequest = 50,
                Command = RightButtonCommand
            });
            
            stack.Children.Add(new Button()
            {
                Text = "Down",
                WidthRequest = 50,
                HeightRequest = 50,
                Margin = new Thickness(50, 0, 50,0),
                Command = DownButtonCommand
            });

            return stack;
        }

        public ICommand DownButtonCommand => new Command(_gameplayController.MoveDownButtonClicked);

        public ICommand RightButtonCommand => new Command(_gameplayController.MoveRightButtonClicked);

        public ICommand LeftButtonCommand => new Command(_gameplayController.MoveLeftButtonClicked);

        public ICommand UpButtonCommand => new Command(_gameplayController.MoveUpButtonClicked);

        private MazeSettings GetMazeSettings(MazeModel model) => new MazeSettings
        {
            XPos = Width / 2,
            YPos = (model.SizeOfCell * model.QuantityOfRows) / 2 + 15,
            WindowHeight = Height,
            WindowWidth = Width,
            Model = model
        };
    }
}
