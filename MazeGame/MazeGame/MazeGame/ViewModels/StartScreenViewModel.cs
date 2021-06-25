using System;
using MazeGame.Helpers;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class StartScreenViewModel
    {
        private INavigation _navigation;
        public StartScreenViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Content = GetContent();
        }

        public Command StartButtonCommand => new Command(StartButtonClicked);
        public StackLayout Content { get; set; }
        
        private async void StartButtonClicked()
        {
            await _navigation.PushAsync(new Views.Menu());
        }

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
                Text = StringConsts.LABEL_ENTER_NAME,
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
    }
}