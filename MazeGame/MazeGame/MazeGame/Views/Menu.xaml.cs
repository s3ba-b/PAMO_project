using MazeGame.ViewModels;
using Xamarin.Forms;


namespace MazeGame.Views
{
    public partial class Menu
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(Navigation);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}