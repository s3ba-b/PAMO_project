using System.Linq;
using System.Windows.Input;
using MazeGame.Helpers;
using MazeGame.Views;
using Q_Learning;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
        private int _hintsLeft;

        public GameBoardViewModel(int mazeIndex, INavigation navigation, ScoreDb scoreDb)
        {
            var mazeSettings = GetMazeSettings(MazeExamples.GetMazeModels().ToArray()[mazeIndex - 1]);
            _navigation = navigation;
            _hintsLeft = GameplayConsts.START_AMOUNT_OF_HINTS;
            _mazeViewModel = new MazeViewModel(mazeSettings);
            _scoreCalculator = new ScoreCalculator();
            _scoreDb = scoreDb;
            _gameplayController = new GameplayController(_mazeViewModel);
            Content = GetContent();
        }

        public Grid Content { get; set; }
        public double Width => Current.MainPage.Width;
        public double Height => Current.MainPage.Height;
        
        public ICommand DownButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveDown()));

        public ICommand RightButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveRight()));

        public ICommand LeftButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveLeft()));

        public ICommand UpButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveUp()));
        
        public ICommand GetHintCommand => new Command(GetHintClicked);

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
                Text = "Get hint",
                Command = GetHintCommand
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
        
        private void UpdateRemainingHintsNumber()
        {
            if (_hintsLeft == 0)
            {
                _getHintsButton.IsEnabled = false;
            }
            _hintsLeftLabel.Text = $"Hints left {_hintsLeft}";
        }

        public void GetHintClicked()
        {
            if (_hintsLeft == 0) return;
            
            var hintsProvider = new HintsProvider(_gameplayController.CrossedCells, _mazeViewModel.Settings.Model);
            var hintsIds = hintsProvider.GetHintCellsIndexes();
            
            _mazeViewModel.CellsViewModelsList.ForEach(cell =>
            {
                if (hintsIds.Contains(cell.Id))
                {
                    cell.State = ESquareState.IsHint;
                }
            });

            _hintsLeft -= 1;
            UpdateRemainingHintsNumber();
        }

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
