﻿using MazeApp.Helpers;
using MazeApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Q_Learning;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MazeApp.ViewModels
{
    public class MenuViewModel
    {
        private readonly INavigationService _navigationService;

        public DelegateCommand<string> NavigateCommand { get; }

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            Content = GetButtons();
        }

        public async void OnNavigateCommandExecuted(string uri) =>
            await _navigationService.NavigateAsync(uri);

        public Grid Content { get; set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public Views.Menu Menu { get; set; }
        public GameBoard GameBoard { get; set; }
        public Button BackButton { get; set; }

        private Grid GetButtons()
        {
            //var canvas = new Canvas();
            //canvas.HorizontalAlignment = HorizontalAlignment.Left;
            //canvas.VerticalAlignment = VerticalAlignment.Top;
            //canvas.Margin = new Thickness((Width / 2) - 75, 0, 0, 0);

            //// button 1

            //var button1 = new Button();
            //button1.Content = "MAZE 1"; ;
            //button1.MaxHeight = 50;
            //button1.MinHeight = 50;
            //button1.MaxWidth = 150;
            //button1.MinWidth = 150;
            //button1.Margin = new Thickness(0, 75, 0, 0);
            //button1.Click += Maze1ButtonClicked;

            //canvas.Children.Add(button1);

            //// button 2

            //var button2 = new Button();
            //button2.Content = "MAZE 2"; ;
            //button2.MaxHeight = 50;
            //button2.MinHeight = 50;
            //button2.MaxWidth = 150;
            //button2.MinWidth = 150;
            //button2.Margin = new Thickness(0, 150, 0, 0);
            //button2.Click += Maze2ButtonClicked;

            //canvas.Children.Add(button2);

            //// button 3

            //var button3 = new Button();
            //button3.Content = "MAZE 3"; ;
            //button3.MaxHeight = 50;
            //button3.MinHeight = 50;
            //button3.MaxWidth = 150;
            //button3.MinWidth = 150;
            //button3.Margin = new Thickness(0, 225, 0, 0);
            //button3.Click += Maze3ButtonClicked;

            //canvas.Children.Add(button3);

            //Menu = canvas;

            //var grid = new Grid();
            //grid.Children.Add(canvas);

            //var backButton = GetBackButton();
            //backButton.Visibility = Visibility.Collapsed;
            //BackButton = backButton;
            //grid.Children.Add(backButton);

            //return grid;

            var buttons = new List<Button>()
            {
                new Button()
                {
                    Text = "Maze 1",
                    Command = new DelegateCommand(Maze1ButtonClicked)
                },
                new Button()
                {
                    Text = "Maze 2",
                    Command = new DelegateCommand(Maze2ButtonClicked)
                },
                new Button()
                {
                    Text = "Maze 3",
                    Command = new DelegateCommand(Maze3ButtonClicked)
                }
            };

            var grid = new Grid();

            StackLayout stack = new StackLayout();

            foreach (var button in buttons)
                stack.Children.Add(button);

            grid.Children.Add(stack);

            return grid;
        }

        //private Button GetBackButton()
        //{
        //    var button = new Button();
        //    button.Content = "BACK";
        //    button.MaxHeight = 50;
        //    button.MinHeight = 50;
        //    button.MaxWidth = 150;
        //    button.MinWidth = 150;
        //    button.Margin = new Thickness(10, 10, 0, 0);
        //    button.HorizontalAlignment = HorizontalAlignment.Left;
        //    button.VerticalAlignment = VerticalAlignment.Top;
        //    button.Click += BackButtonClicked;

        //    return button;
        //}

        //private void BackButtonClicked(object sender, RoutedEventArgs e)
        //{
        //    Menu.Visibility = Visibility.Visible;
        //    GameBoard.Visibility = Visibility.Collapsed;
        //    GameBoard = null;
        //    Content.Children.Remove(GameBoard);
        //    BackButton.Visibility = Visibility.Collapsed;
        //}

        private async void Maze1ButtonClicked()
        {
            //Menu.Visibility = Visibility.Collapsed;
            //var mazeViewModel = new MazeViewModel(GetMazeSettings(MazeExamples.Example_1()));
            //var gameBoardViewModel = new GameBoardViewModel(mazeViewModel, Width);
            //GameBoard = new GameBoard(gameBoardViewModel);
            //GameBoard.Visibility = Visibility.Visible;
            //Content.Children.Add(GameBoard);
            //BackButton.Visibility = Visibility.Visible;

            await _navigationService.NavigateAsync("NavigationPage/GameBoard");
        }

        private void Maze2ButtonClicked()
        {
            //Menu.Visibility = Visibility.Collapsed;
            //var mazeViewModel = new MazeViewModel(GetMazeSettings(MazeExamples.Example_2()));
            //var gameBoardViewModel = new GameBoardViewModel(mazeViewModel, Width);
            //GameBoard = new GameBoard(gameBoardViewModel);
            //GameBoard.Visibility = Visibility.Visible;
            //Content.Children.Add(GameBoard);
            //BackButton.Visibility = Visibility.Visible;
        }

        private void Maze3ButtonClicked()
        {
            //Menu.Visibility = Visibility.Collapsed;
            //var mazeViewModel = new MazeViewModel(GetMazeSettings(MazeExamples.Example_3()));
            //var gameBoardViewModel = new GameBoardViewModel(mazeViewModel, Width);
            //GameBoard = new GameBoard(gameBoardViewModel);
            //GameBoard.Visibility = Visibility.Visible;
            //Content.Children.Add(GameBoard);
            //BackButton.Visibility = Visibility.Visible;
        }

        private MazeSettings GetMazeSettings(MazeModel maze)
        {
            var settings = new MazeSettings();

            settings.XPos = Width / 2;
            settings.YPos = Height / 2;
            settings.WindowHeight = Height;
            settings.WindowWidth = Width;
            settings.Model = maze;

            return settings;
        }
    }
}
