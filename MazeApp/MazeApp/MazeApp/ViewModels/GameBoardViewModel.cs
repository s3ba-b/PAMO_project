using MazeApp.Helpers;
using MazeApp.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Q_Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class GameBoardViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private int _mazeIndex;
        
        public GameBoardViewModel(INavigationService navigationService, Maze mazeView)
        {
            _navigationService = navigationService;
            Content = GetContent(mazeView);
        }

        public Grid Content { get; set; }
        public double Height => DeviceDisplay.MainDisplayInfo.Height;
        public double Width => DeviceDisplay.MainDisplayInfo.Width;
        public DelegateCommand StartCommand => new DelegateCommand(Start);

        private Grid GetContent(Maze mazeView)
        {
            var grid = new Grid();
            InitializeMaze(mazeView);

            //grid.Children.Add(mazeView);
            //grid.Children.Add(GetButton());

            return grid;
        }

        private void InitializeMaze(Maze mazeView)
        {
            var model = MazeExamples.GetMazeModels().ToArray()[_mazeIndex];
            var mazeViewModel = mazeView.BindingContext as MazeViewModel;
            mazeViewModel.Initialize(GetMazeSettings(model));
        }

        private MazeSettings GetMazeSettings(MazeModel model) => new MazeSettings
        {
            XPos = Width / 2,
            YPos = Height / 2,
            WindowHeight = Height,
            WindowWidth = Width,
            Model = model
        };

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
            //_MazeViewModel.VisualizeWalk();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _mazeIndex = Int32.Parse(parameters.GetValue<string>("mazeIndex"));
        }
    }
}
