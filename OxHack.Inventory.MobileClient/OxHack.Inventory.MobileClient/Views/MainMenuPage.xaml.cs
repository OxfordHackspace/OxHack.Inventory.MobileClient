﻿using OxHack.Inventory.MobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage(MainMenuViewModel viewModel)
        {
            this.InitializeComponent();

            this.BindingContext = viewModel;
        }
    }
}
