using MazeGame.ViewModels;

namespace MazeGame.Views
{
    public partial class StartScreen
    {
        public StartScreen()
        {
            InitializeComponent();
            BindingContext = StartScreenViewModel.GetStartScreenViewModel(Navigation);
        }
    }
}