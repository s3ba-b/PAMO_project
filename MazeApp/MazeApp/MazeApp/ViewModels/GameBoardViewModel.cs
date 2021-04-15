using MazeApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class GameBoardViewModel : BindableBase, INavigationAware
    {
        private readonly MazeViewModel _MazeViewModel;

        public GameBoardViewModel()
        {
            //_MazeViewModel = mazeViewModel;
            //Width = width;
            //Content = GetContent();
        }

        public Grid Content { get; set; }
        public double Width { get; set; }
        public DelegateCommand StartCommand => new DelegateCommand(Start);

        private Grid GetContent()
        {
            var grid = new Grid();

            grid.Children.Add(new Maze());
            grid.Children.Add(GetButton());

            return grid;
        }

        private Button GetButton()
        {
            //var button1 = new Button
            //{
            //    Content = "START",
            //    MaxHeight = 50,
            //    MinHeight = 50,
            //    MaxWidth = 150,
            //    MinWidth = 150,
            //    Margin = new Thickness((Width / 2) - 75, 10, 0, 0),
            //    HorizontalAlignment = HorizontalAlignment.Left,
            //    VerticalAlignment = VerticalAlignment.Top
            //};

            //button.Click += StartButtonClicked;

            return new Button
            {
                Text = "START",
                HeightRequest = 50,
                WidthRequest = 150,
                Margin = new Thickness((Width / 2) - 75, 10, 0, 0),
                Command = StartCommand
            };
        }

        private void Start()
        {
            _MazeViewModel.VisualizeWalk();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var mazeIndex = parameters.GetValue<string>("mazeIndex");
        }
    }
}
