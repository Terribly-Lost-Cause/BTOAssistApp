using BTOAssistApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BTOAssistApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}