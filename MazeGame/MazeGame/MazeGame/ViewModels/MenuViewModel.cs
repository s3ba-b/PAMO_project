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
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();

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

            var grid = new Grid();
            grid.Children.Add(stack);

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
