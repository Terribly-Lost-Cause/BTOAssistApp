using BTOAssistApp.Data;
using BTOAssistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            List<BTO>  re = await database.GetBTOAsync();
        }
    }
}