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
using Microsoft.AspNetCore.WebUtilities;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string devID;
        private string id;
        private string accesstoken;
        private string warningmsg;
        private ObservableCollection<BTO> allbto;
        public ObservableCollection<BTO> btosorted;
        HttpClient client = new HttpClient();
        public string WarningMsg
        {
            get { return warningmsg; }
            set
            {
                warningmsg = value;
                OnPropertyChanged(nameof(WarningMsg)); // Notify that there was a change on this property
            }
        }
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

        public ObservableCollection<BTO> AllBTO
        {
            get { return allbto; }
            set
            {
                allbto = value;
                OnPropertyChanged(nameof(AllBTO)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<BTO> BTOSorted
        {
            get { return btosorted; }
            set
            {
                btosorted = value;
                OnPropertyChanged(nameof(BTOSorted)); // Notify that there was a change on this property
            }
        }
        public HomePage()
        {
            base.OnAppearing();
            WarningMsg = "False";

            GetLink();
            CallGetBTOLink();
            InitializeComponent();
            BindingContext = this;
        }



        protected override async void OnAppearing()
        {
            WarningMsg = "False";

            GetLink();
        }
        protected async void GetLink()
        {
            HttpClient client = new HttpClient();
            await Task.Run(async () =>
            {
                const string getBTO = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getAppliedBTOinfo";

                var BTOValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()}

                      };
                var newGetBTOUrl = new Uri(QueryHelpers.AddQueryString(getBTO, BTOValues));
                //var stringContent = new FormUrlEncodedContent(values);


                HttpResponseMessage BTOResponse = await client.GetAsync(newGetBTOUrl);
                string BTOContent = await BTOResponse.Content.ReadAsStringAsync();

                JObject btodata = JObject.Parse(BTOContent);
                var btoarray = btodata["resultForCPFPage"] as JArray;


                var license = btodata["key"].ToString();

                var results1 = btodata["resultForBTOStatusPage"];
                var results = btodata["resultForCPFPage"];


                /*if (results1.ToString() == "0")
                {
                    WarningMsg = "False";
                }
                else
                {
                    WarningMsg = "True";
                }*/
            });
        }
        protected async void CallGetBTOLink()
        {
            Uri getBTOInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getBTOInfo");
            HttpResponseMessage response = await client.GetAsync(getBTOInfo);
            string content = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(content);
            var array = data["result"] as JArray;
            var sortarray = data["sorted"] as JArray;
            ObservableCollection<BTO> TempAllBTO = new ObservableCollection<BTO>();
            ObservableCollection<BTO> TempBTOSorted = new ObservableCollection<BTO>();



            foreach (var house in array)
            {
                var classhouse = house.ToObject<BTO>();
                classhouse.Block = "Block " + classhouse.Block;
                TempAllBTO.Add(classhouse);
            }
            foreach (var sorthouse in sortarray)
            {
                var sortclasshouse = sorthouse.ToObject<BTO>();
                sortclasshouse.Block = "Block " + sortclasshouse.Block;
                TempBTOSorted.Add(sortclasshouse);



            }
            AllBTO = TempAllBTO;
            BTOSorted = TempBTOSorted;
            Console.WriteLine(TempBTOSorted.Count());
            BindingContext = this;

        }


        /*
                protected override void OnDisappearing()
                {
                    base.OnDisappearing();

                    ObservableCollection<BTO> TempAllBTO = new ObservableCollection<BTO>();
                    ObservableCollection<BTO> TempBTOSorted = new ObservableCollection<BTO>();
                    AllBTO = TempAllBTO;
                    BTOSorted = TempBTOSorted;
                    Console.WriteLine(TempBTOSorted.Count());
                    BindingContext = this;
                }
        */
        async void ViewDetail(object sender, EventArgs args)
        {
            var BTO = (Frame)sender;
            string id = BTO.AutomationId;
            await Navigation.PushAsync(new BTODetail(id));
        }
    }
}