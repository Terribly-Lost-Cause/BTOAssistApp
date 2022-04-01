using BTOAssistApp.ViewModels;
using BTOAssistApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BTOAssistApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {


        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}
