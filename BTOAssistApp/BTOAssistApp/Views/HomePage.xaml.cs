﻿using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public ObservableCollection<BTO> AllBTO { get; set; } = new ObservableCollection<BTO>();
        public ObservableCollection<BTO> BTOSorted { get; set; } = new ObservableCollection<BTO>();
        public HomePage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            List<BTO> listofBTO = await database.GetBTOAsync();
            List<BTO> listofBTOSorted = await database.GetBTOPopularityAsync();

            AllBTO.Clear();
            BTOSorted.Clear();

            foreach (var eachBTO in listofBTO)
            {
                eachBTO.Block = "Block " + eachBTO.Block;
                AllBTO.Add(eachBTO);
            }

            foreach (var eachBTOSorted in listofBTOSorted)
            {
                eachBTOSorted.Block = "Block " + eachBTOSorted.Block;
                BTOSorted.Add(eachBTOSorted);
            }
        }
    }
}