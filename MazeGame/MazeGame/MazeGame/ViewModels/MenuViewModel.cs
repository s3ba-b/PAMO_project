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

        private String name { get; set;}
        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            this.name = "Enter your name";
            Content = GetContent();
        }

        public Grid Content { get; private set; }
        public ICommand StartButtonCommand => new Command(StartButtonClicked);

        /**
         * 
         */
        private Grid GetContent()
        {
            
            // Graphic with the application title.
            var titleImage = new Image()
            {
                Source = ImageSource.FromFile(file: "title.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions  = LayoutOptions.Center,
                HeightRequest = 250,
            };
            
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

            var nameLabel = new Label
            {
                Text = this.name,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
            };
            

            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),

                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    
                    new RowDefinition(),
                    new RowDefinition(),

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                }
                
            };
            Grid.SetRow(nameLabel,1);
            Grid.SetColumn(nameLabel,1);
            grid.Children.Add(nameLabel);
            
            Grid.SetRow(settingsButton,1);
            Grid.SetColumn(settingsButton,2);
            grid.Children.Add(settingsButton);

            Grid.SetRow(titleImage,3);
            Grid.SetColumn(titleImage,1);
            grid.Children.Add(titleImage);

            Grid.SetRow(startGameButton,5);
            Grid.SetColumn(startGameButton,1);
            grid.Children.Add(startGameButton);

            
            /*
            grid.Children.Add(settingsButton);
            grid.Children.Add(titleImage);
            grid.Children.Add(startGameButton);
            */

            /*
            grid.Children.Add(nameLabel, 0,1,0,0);
            grid.Children.Add(settingsButton, 2,2, 0,0);
            grid.Children.Add(titleImage, 0, 0, 1, 1);
            grid.Children.Add(startGameButton, 0, 0, 2,2);
            */

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