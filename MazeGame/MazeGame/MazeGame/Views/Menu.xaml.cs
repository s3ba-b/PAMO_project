using MazeGame.ViewModels;

namespace MazeGame.Views
{
    public partial class Menu
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(Navigation);
        }
    }
}