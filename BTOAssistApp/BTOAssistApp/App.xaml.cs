﻿using BTOAssistApp.Services;
using BTOAssistApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new Launch();
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
