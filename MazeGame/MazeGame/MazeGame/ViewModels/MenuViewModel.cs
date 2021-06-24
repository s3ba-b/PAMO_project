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
        private StackLayout GetContent()
        {
            // Graphic with the application title.
            var titleImage = new Image()
            {
                Source = ImageSource.FromFile(file: "title.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 250,
            };

            // Button to change settings.
            var settingsButton = new ImageButton()
            {
                Source = ImageSource.FromFile(file: "settings_button_1.png"),
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 40,
            };

            // Event that changes "settingsButton" graphic when the button is released.  
            settingsButton.Released += (object sender, EventArgs e) =>
            {
                settingsButton.Source = ImageSource.FromFile(file: "settings_button_1.png");
            };

            // Event that changes "settingsButton" graphic when the button is pressed.
            settingsButton.Pressed += (object sender, EventArgs e) =>
            {
                settingsButton.Source = ImageSource.FromFile(file: "settings_button_2.png");
            };


            // Button to start a new game.
            var startGameButton = new ImageButton
            {
                Source = ImageSource.FromFile(file: "start_button_1.png"),
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 50,
                Command = StartButtonCommand,
            };

            // Event that changes "startGameButton" graphic when the button is released. 
            startGameButton.Released += (object sender, EventArgs e) =>
            {
                startGameButton.Source = ImageSource.FromFile(file: "start_button_1.png");
            };

            // Event that changes "startGameButton" graphic when the button is pressed.
            startGameButton.Pressed += (object sender, EventArgs e) =>
            {
                startGameButton.Source = ImageSource.FromFile(file: "start_button_2.png");
            };

            var nameLabel = new Label
            {
                Text = this.name,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            var nameStackLayout = new StackLayout
            {
                Margin = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    nameLabel, settingsButton
                }
            };


            var stackLayout = new StackLayout
            {
                Margin = 30,
                Spacing = 120,

                Children =
                {
                    nameStackLayout, titleImage, startGameButton
                }
            };


            return stackLayout;
        }

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