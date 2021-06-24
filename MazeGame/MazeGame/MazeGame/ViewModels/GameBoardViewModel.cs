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
        private readonly INavigation _navigation;
        private readonly MazeViewModel _mazeViewModel;
        private readonly GameplayController _gameplayController;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly ScoreDb _scoreDb;
        private Button _getHintsButton;
        private Label _hintsLeftLabel;

        public GameBoardViewModel(int mazeIndex, INavigation navigation, ScoreDb scoreDb)
        {
            _navigation = navigation;
            var mazeSettings = GetMazeSettings(MazeExamples.GetMazeModels().ToArray()[mazeIndex - 1]);
            _mazeViewModel = new MazeViewModel(mazeSettings);
            _scoreCalculator = new ScoreCalculator();
            _scoreDb = scoreDb;
            Content = GetContent();
            _gameplayController = new GameplayController(_mazeViewModel, _getHintsButton, _hintsLeftLabel);
            _getHintsButton.Command = GetHintCommand;
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
            
            var hintStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(10, 0, 0, 10)
            };
            
            _getHintsButton = new Button()
            {
                Text = "Get hint"
            };
            
            _hintsLeftLabel = new Label()
            {
                Text = $"Hints left {GameplayConsts.START_AMOUNT_OF_HINTS}",
                FontSize = 20,
                VerticalTextAlignment = TextAlignment.Center,
            };

            hintStack.Children.Add(_getHintsButton);

            hintStack.Children.Add(_hintsLeftLabel);

            grid.Children.Add(hintStack);

            return grid;
        }

        private StackLayout GetControls()
        {
            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 0, 70)
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

        public ICommand DownButtonCommand => new Command(() => ShowWonInfo(_gameplayController.MoveDownButtonClicked()));

        public ICommand RightButtonCommand => new Command(() => ShowWonInfo(_gameplayController.MoveRightButtonClicked()));

        public ICommand LeftButtonCommand => new Command(() => ShowWonInfo(_gameplayController.MoveLeftButtonClicked()));

        public ICommand UpButtonCommand => new Command(() => ShowWonInfo(_gameplayController.MoveUpButtonClicked()));
        
        public ICommand GetHintCommand => new Command(_gameplayController.GetHintClicked);

        private async void ShowWonInfo(bool isGameWon)
        {
            if(!_scoreCalculator.IsGameStarted) _scoreCalculator.StartGame();
            if (!isGameWon) return;
            _scoreCalculator.EndGame();
            var score = _scoreCalculator.Score;
            var scoreObj = new Score
            {
                MazeId = _mazeViewModel.Settings.Model.Id,
                BestScore = score
            };
            if (_scoreDb.Get(_mazeViewModel.Settings.Model.Id) != null)
            {
                _scoreDb.Update(scoreObj);
            }
            else
                _scoreDb.Add(scoreObj); 
            MessagingCenter.Send<GameBoardViewModel>(this, "Score updated");
            await Current.MainPage.DisplayAlert("You won!", $"Your score is {score}!", "Thanks!");
            await _navigation.PopAsync();
        }

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
