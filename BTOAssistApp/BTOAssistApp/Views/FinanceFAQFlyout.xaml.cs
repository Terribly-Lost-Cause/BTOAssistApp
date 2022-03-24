using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinanceFAQFlyout : ContentPage
    {
        public ListView ListView;

        public FinanceFAQFlyout()
        {
            InitializeComponent();

            BindingContext = new FinanceFAQFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class FinanceFAQFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FinanceFAQFlyoutMenuItem> MenuItems { get; set; }

            public FinanceFAQFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FinanceFAQFlyoutMenuItem>(new[]
                {
                    new FinanceFAQFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new FinanceFAQFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new FinanceFAQFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new FinanceFAQFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new FinanceFAQFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}