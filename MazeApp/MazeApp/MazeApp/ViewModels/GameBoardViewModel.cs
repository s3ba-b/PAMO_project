using MazeApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class GameBoardViewModel
    {
        private readonly MazeViewModel _MazeViewModel;

        public GameBoardViewModel(MazeViewModel mazeViewModel, double width)
        {
            _MazeViewModel = mazeViewModel;
            Width = width;
            Content = GetContent();
        }

        public UIElement Content { get; set; }
        public double Width { get; set; }

        private Grid GetContent()
        {
            var grid = new Grid();

            grid.Children.Add(new Maze(_MazeViewModel));
            grid.Children.Add(GetButton());

            return grid;
        }

        private Button GetButton()
        {
            var button = new Button
            {
                Content = "START",
                MaxHeight = 50,
                MinHeight = 50,
                MaxWidth = 150,
                MinWidth = 150,
                Margin = new Thickness((Width / 2) - 75, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            button.Click += StartButtonClicked;

            return button;
        }

        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            _MazeViewModel.VisualizeWalk();
        }

    }
}
