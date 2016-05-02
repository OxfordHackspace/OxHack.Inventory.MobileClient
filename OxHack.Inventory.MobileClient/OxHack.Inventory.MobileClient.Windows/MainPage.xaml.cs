﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OxHack.Inventory.MobileClient.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

			var app = new MobileClient.App();

			this.Loaded += (s, e) => app.HackStart();

			LoadApplication(app);
        }
    }
}
