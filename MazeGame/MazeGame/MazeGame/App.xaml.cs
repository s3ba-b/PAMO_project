﻿using Xamarin.Forms;

namespace MazeGame
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new Views.StartScreen() );
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
