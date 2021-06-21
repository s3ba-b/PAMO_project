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
        public ICommand MazeButtonCommand => new Command<string>(MazeButtonClicked);

        private Grid GetContent()
        {
            var backgroundImage = new Image()
            {
                Source = ImageSource.FromFile(file: "background.jpg"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
              //  VerticalOptions  = LayoutOptions.FillAndExpand,
                //Aspect = Aspect.AspectFill
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
                     new ImageButton {
                        Source = ImageSource.FromFile(file: "settings_button_1.png"),
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = 40,
                     }
                    }
                };
            
            var titleImage = new Image()
            {
                Source = ImageSource.FromFile(file: "title.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions  = LayoutOptions.Center,
                HeightRequest = 250,

            };
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();
            
            var nameButton = new Button() { };
            var settingsButton = new Button() { };
            var startGameButton = new Button() { };
            

            /*
            StackLayout stack = new StackLayout();

            for(int i = 1; i <= mazeModelsCount; i++)
            {
                stack.Children.Add(new Button()
                {
                    Text = $"Maze {i}",
                    Command = MazeButtonCommand,
                    CommandParameter = i.ToString()
                });
            }
            */

            var grid = new Grid();
            grid.Children.Add(backgroundImage);
            grid.Children.Add(nameStack);
            grid.Children.Add(titleImage);
            //grid.Children.Add(stack);

            return grid;
        }
        
        
        
        private async void MazeButtonClicked(string index)
        {
            await _navigation.PushAsync(new GameBoard
            {
                BindingContext = new GameBoardViewModel(int.Parse(index))
            });
        }
    }
}
