using System;
using System.Collections.Generic;
using Q_Learning;
using System.Linq;
using System.Resources;
using System.Windows.Input;
using MazeGame.Helpers;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Xaml;
using ResourceManager = System.Resources.ResourceManager;

[assembly: NeutralResourcesLanguage("en-US")]

namespace MazeGame.ViewModels
{
    public class MenuViewModel
    {
        private readonly INavigation _navigation;
        private readonly ScoreDb _scoreDb;
        private readonly List<Label> _labels;
        private String name { get; set; }

        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            this.name = StringConsts.LABEL_ENTER_NAME;
            _labels = new List<Label>();
            _scoreDb = new ScoreDb();
            Content = GetContent();

            MessagingCenter.Subscribe<GameBoardViewModel>(this, "Score updated",
                (sender) => { UpdateBestScoreForMaze(); });
        }

        public StackLayout Content { get; private set; }
        public ICommand StartButtonCommand => new Command(StartButtonClicked);

        /**
         * 
         */
        

        /**
         * Event handling for StartButton. That starting new maze randomly from 1-3 range.  
         */
        private async void StartButtonClicked()
        {
            await _navigation.PushAsync(
                new GameBoard
                {
                    BindingContext = new GameBoardViewModel(new Random().Next(1, 3), _navigation, _scoreDb)
                }
            );
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