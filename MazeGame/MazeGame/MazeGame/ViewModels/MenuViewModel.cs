using System;
using System.Collections;
using System.Collections.Generic;
using Q_Learning;
using System.Linq;
using System.Windows.Input;
using MazeGame.Helpers;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MazeGame.ViewModels
{
    public class MenuViewModel
    {
        private readonly INavigation _navigation;
        private readonly ScoreDb _scoreDb;
        private readonly List<Label> _labels;

        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _labels = new List<Label>();
            Content = GetContent();
            _scoreDb = new ScoreDb();
            MessagingCenter.Subscribe<GameBoardViewModel> (this, "Score updated", (sender) =>
            {
                UpdateBestScoreForMaze();
            });
        }

        public Grid Content { get; private set; }
        public ICommand MazeButtonCommand => new Command<string>(MazeButtonClicked);

        private Grid GetContent()
        {
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical, 
                HorizontalOptions = LayoutOptions.Center
            };


            for(int i = 1; i <= mazeModelsCount; i++)
            {
                var internalStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 50
                };

                _labels.Add(new Label()
                {
                    Text = $"Score {GetBestScoreForMaze(i)}",
                    FontSize = 20,
                    VerticalTextAlignment = TextAlignment.Center,
                });
                
                internalStack.Children.Add(
                    new Button()
                    {
                        Text = $"Maze {i}",
                        Command = MazeButtonCommand,
                        CommandParameter = i.ToString(),
                        WidthRequest = 200
                    }
                    );
                internalStack.Children.Add( _labels.Last());
                stack.Children.Add( internalStack );
            }

            var grid = new Grid();
            grid.Children.Add(stack);

            return grid;
        }
        
        private async void MazeButtonClicked(string index)
        {
            await _navigation.PushAsync(new GameBoard
            {
                BindingContext = new GameBoardViewModel(int.Parse(index), _navigation, _scoreDb)
            });
        }

        private int GetBestScoreForMaze(int index)
        {
            return _scoreDb?.Get(index)?.BestScore ?? 0;
        }

        private void UpdateBestScoreForMaze()
        {
            for (int i = 0; i < _labels.Count; i++)
            {
                _labels[i].Text = $"Score {GetBestScoreForMaze(i + 1)}";
            }
        }
    }
}
