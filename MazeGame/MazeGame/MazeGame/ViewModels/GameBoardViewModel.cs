using System.Linq;
using MazeGame.Helpers;
using MazeGame.MazeConstructors;
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
            var constructor = new MazeConstructor();
            Content = constructor.GetGameBoardView(
                _mazeViewModel,
                out _getHintsButton,
                out _hintsLeftLabel,
                GetHintCommand,
                UpButtonCommand,
                DownButtonCommand,
                LeftButtonCommand,
                RightButtonCommand);
        }

        public Grid Content { get; set; }
        public double Width => Current.MainPage.Width;
        public double Height => Current.MainPage.Height;
        
        public Command DownButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveDown()));

        public Command RightButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveRight()));

        public Command LeftButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveLeft()));

        public Command UpButtonCommand => new Command(() => ShowWonInfo(_gameplayController.TryMoveUp()));
        
        public Command GetHintCommand => new Command(GetHintClicked);

        private void UpdateRemainingHintsNumber()
        {
            if (_hintsLeft == 0)
            {
                _getHintsButton.IsEnabled = false;
            }
            _hintsLeftLabel.Text = $"Hints left {_hintsLeft}";
        }

        private void GetHintClicked()
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
