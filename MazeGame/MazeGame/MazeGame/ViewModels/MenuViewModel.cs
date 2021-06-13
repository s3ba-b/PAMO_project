using System;
using Q_Learning;
using System.Linq;
using MazeGame.Views;
using Xamarin.Forms;

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

        private Grid GetContent()
        {
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();

            StackLayout stack = new StackLayout();

            for(int i = 1; i <= mazeModelsCount; i++)
            {
                var button = new Button()
                {
                    Text = $"Maze {i}",
                    CommandParameter = i.ToString()
                };
                button.Clicked += async (sender, args) => Maze1ButtonClicked(sender, args);
                stack.Children.Add(button);
            }

            var grid = new Grid();
            grid.Children.Add(stack);

            return grid;
        }
        
        private async void Maze1ButtonClicked(object sender, EventArgs e)
        {
            await _navigation.PushAsync(new GameBoard
            {
                BindingContext = new GameBoardViewModel(1)
            });
        }
    }
}
