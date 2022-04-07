﻿using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string devID;
        private string id;
        private string accesstoken;

        public string DevID
        {
            get { return devID; }
            set
            {
                devID = value;
                OnPropertyChanged(nameof(DevID)); // Notify that there was a change on this property
            }
        }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id)); // Notify that there was a change on this property
            }
        }
        public string AccessToken
        {
            get { return accesstoken; }
            set
            {
                accesstoken = value;
                OnPropertyChanged(nameof(AccessToken)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<BTO> AllBTO { get; set; } = new ObservableCollection<BTO>();
        public ObservableCollection<BTO> BTOSorted { get; set; } = new ObservableCollection<BTO>();
        public HomePage()
        {
            InitializeComponent();
            //var deviceId = CrossDeviceInfo.Current.Id;
            //Task.Run(async () =>
            //{

            //    BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            //    //await database.DeleteAllPhoneInfoAsync();
            //    PhoneInfo BTODataDetails = await database.GetBTODataAsync(deviceId);
            //    List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();
                
            //    foreach (var i in allPhoneInfo)
            //    {
            //        Trace.WriteLine("deviceID: " + i.deviceID);
            //        Trace.WriteLine("accessToken: " + i.accessToken);
            //    }
            //    DevID = BTODataDetails.deviceID;
            //    //Sub = BTODataDetails.sub;
            //    AccessToken = BTODataDetails.accessToken;
            //    //Trace.WriteLine(">>>>>>>>>>>>> Sub:" + Sub);
            //    Trace.WriteLine(">>>>>>>>>>>>> DevID:" + DevID);
            //    Trace.WriteLine("AccessToken: " + AccessToken);
            //});


            }



        protected override async void OnAppearing()
        {

            base.OnAppearing();

            


            AllBTO.Clear();
            BTOSorted.Clear();


            PostGre postGre = new PostGre();
            List<BTO> dbBto = postGre.GetAllBTOAsync();
            List<BTO> dbBtoPopular = postGre.GetBTOPopularityAsync();

            foreach (var eachBTO in dbBto)
            {
                eachBTO.Block = "Block " + eachBTO.Block;
                AllBTO.Add(eachBTO);
            }

            foreach (var eachBTOSorted in dbBtoPopular)
            {
                eachBTOSorted.Block = "Block " + eachBTOSorted.Block;
                BTOSorted.Add(eachBTOSorted);
            }

            BindingContext = this;
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();

            BindingContext = null;
        }

        async void ViewDetail(object sender, EventArgs args)
        {
            var BTO = (Frame)sender;
            string id = BTO.AutomationId;

            Trace.WriteLine(id);
            await Navigation.PushAsync(new BTODetail(id));
        }
    }
}