using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MenuViewModel
    {
        private readonly INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            Content = GetContent();
        }

        public DelegateCommand<string> NavigateCommand { get; }
        public Grid Content { get; private set; }

        public async void OnNavigateCommandExecuted(string parameter) =>
            await _navigationService.NavigateAsync("NavigationPage/GameBoard", new NavigationParameters { { "mazeIndex" , parameter } });

        private Grid GetContent()
        {
            var buttons = new List<Button>()
            {
                new Button()
                {
                    Text = "Maze 1",
                    CommandParameter = "1",
                    Command = NavigateCommand
                },
                new Button()
                {
                    Text = "Maze 2",
                    CommandParameter = "2",
                    Command = NavigateCommand
                },
                new Button()
                {
                    Text = "Maze 3",
                    CommandParameter = "3",
                    Command = NavigateCommand
                }
            };

            var grid = new Grid();

            StackLayout stack = new StackLayout();

            foreach (var button in buttons)
                stack.Children.Add(button);

            grid.Children.Add(stack);

            return grid;
        }
    }
}
