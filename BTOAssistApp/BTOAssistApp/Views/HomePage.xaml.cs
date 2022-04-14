using BTOAssistApp.Data;
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
using Newtonsoft.Json.Linq;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string devID;
        private string id;
        private string accesstoken;
        HttpClient client = new HttpClient();
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
            


            }



        protected override async void OnAppearing()
        {

            base.OnAppearing();
            Uri getBTOInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getBTOInfo");

            HttpResponseMessage response = await client.GetAsync(getBTOInfo);


            string content = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(content);
            var array = data["result"] as JArray;
            var sortarray = data["sorted"] as JArray;

            AllBTO.Clear();
            BTOSorted.Clear();

            foreach (var house in array)
            {
                var classhouse = house.ToObject<BTO>();
                classhouse.Block = "Block " + classhouse.Block;
                AllBTO.Add(classhouse);
            }

            foreach (var sorthouse in sortarray)
            {
                var sortclasshouse = sorthouse.ToObject<BTO>();
                sortclasshouse.Block = "Block " + sortclasshouse.Block;
                BTOSorted.Add(sortclasshouse);
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
            Console.WriteLine(">>>>>>>>>>>>>>", BTO);
            string id = BTO.AutomationId;

            Trace.WriteLine(">>>>>> "+id);
            await Navigation.PushAsync(new BTODetail(id));
        }
    }
}