using System;
using Q_Learning;
using System.Linq;
using System.Windows.Input;
using MazeGame.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MazeGame.ViewModels
{
    public class MenuViewModel
    {
        private readonly INavigation _navigation;

        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Content = GetContent();
        }

        public Grid Content { get; private set; }
        public ICommand StartButtonCommand => new Command(StartButtonClicked);

        private Grid GetContent()
        {
            

            
            var titleImage = new Image()
            {
                Source = ImageSource.FromFile(file: "title.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions  = LayoutOptions.Center,
                HeightRequest = 250,

            };
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();
            
            // Button to change settings.
            var settingsButton = new ImageButton()
            {
                Source = ImageSource.FromFile(file: "settings_button_1.png"),
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
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
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 40,
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

            StackLayout nameStack = new StackLayout()
            {
                //Orientation = StackOrientation.Horizontal,
                Orientation = StackOrientation.Vertical,
                //HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        Text = "Enter your name",
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                    },
                    settingsButton
                }
            };
            

            var grid = new Grid();
            grid.Children.Add(nameStack);
            grid.Children.Add(titleImage);
            grid.Children.Add(startGameButton);

            return grid;
        }
        
        /**
         * Even handling for StartButton. That starting new maze randomly from 1-3 range.  
         */
        private async void StartButtonClicked()
        {
            await _navigation.PushAsync(new GameBoard
            {
                BindingContext = new GameBoardViewModel(new Random().Next(1,3))
            });
        }
    }
}