﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MazeGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wall: ContentView
    {
        public Wall()
        {
            InitializeComponent();
        }
    }
}