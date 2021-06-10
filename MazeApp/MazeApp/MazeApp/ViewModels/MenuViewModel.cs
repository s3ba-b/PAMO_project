using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Q_Learning;
using System.Linq;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MenuViewModel : BindableBase, INavigationAware
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
            await _navigationService.NavigateAsync("GameBoard", new NavigationParameters { { "mazeIndex" , parameter } });

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new System.NotImplementedException();
        }

        private Grid GetContent()
        {
            var mazeModelsCount = MazeExamples.GetMazeModels().Count();

            StackLayout stack = new StackLayout();

            for(int i = 1; i <= mazeModelsCount; i++)
            {
                stack.Children.Add(new Button()
                {
                    Text = $"Maze {i}",
                    CommandParameter = i.ToString(),
                    Command = NavigateCommand
                });
            }
                
            var grid = new Grid();
            grid.Children.Add(stack);

            return grid;
        }
    }
}
