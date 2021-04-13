using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Maze App";
            Content = VisualizeMaze();
        }

        public Grid Content { get; private set; }

        private Grid VisualizeMaze()
        {
            var grid = new Grid();

            var cell = new Views.Cell();

            cell.IsVisible = true;
            

            grid.Children.Add(cell);

            return grid;
        }
    }
}
