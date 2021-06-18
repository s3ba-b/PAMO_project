using System;
using System.Collections;
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
                
                internalStack.Children.Add(
                    new Button()
                    {
                        Text = $"Maze {i}",
                        Command = MazeButtonCommand,
                        CommandParameter = i.ToString(),
                        WidthRequest = 200
                    }
                    );
                internalStack.Children.Add( new Label()
                {
                    Text = "Score 1000",
                    FontSize = 20,
                    VerticalTextAlignment = TextAlignment.Center,
                });
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
                BindingContext = new GameBoardViewModel(int.Parse(index), _navigation)
            });
        }
    }
}
