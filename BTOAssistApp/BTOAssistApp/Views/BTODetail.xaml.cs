using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BTODetail : ContentPage
    {
        private string BTOId;
        private string id;
        private string image;
        private string location;
        private string block;
        private int roomtype;
        private string sqm;
        private string bus;
        private string mrt;
        private string direction;
        private int yearsleft;
        private int applicants;
        private string longdescription;
        private double downpayment;
        private double fullpayment;
        private string test;
        private string btntext;
        private string btnenabled;
        private string warningmsg;
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

        public string BtnText
        {
            get { return btntext; }
            set
            {
                btntext = value;
                OnPropertyChanged(nameof(BtnText)); // Notify that there was a change on this property
            }
        }
        public string EnableBtn
        {
            get { return btnenabled; }
            set
            {
                btnenabled = value;
                OnPropertyChanged(nameof(EnableBtn)); // Notify that there was a change on this property
            }
        }

        public string Test
        {
            get { return test; }
            set
            {
                test = value;
                OnPropertyChanged(nameof(Test)); // Notify that there was a change on this property
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


        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image)); // Notify that there was a change on this property
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location)); // Notify that there was a change on this property
            }
        }
        public string Block
        {
            get { return block; }
            set
            {
                block = value;
                OnPropertyChanged(nameof(Block)); // Notify that there was a change on this property
            }
        }
        public string SQM
        {
            get { return sqm; }
            set
            {
                sqm = value;
                OnPropertyChanged(nameof(SQM)); // Notify that there was a change on this property
            }
        }
        public string Bus
        {
            get { return bus; }
            set
            {
                bus = value;
                OnPropertyChanged(nameof(Bus)); // Notify that there was a change on this property
            }
        }
        public string Mrt
        {
            get { return mrt; }
            set
            {
                mrt = value;
                OnPropertyChanged(nameof(Mrt)); // Notify that there was a change on this property
            }
        }
        public string Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                OnPropertyChanged(nameof(Direction)); // Notify that there was a change on this property
            }
        }
        public int YearsLeft
        {
            get { return yearsleft; }
            set
            {
                yearsleft = value;
                OnPropertyChanged(nameof(YearsLeft)); // Notify that there was a change on this property
            }
        }

        public int Applicants
        {
            get { return applicants; }
            set
            {
                applicants = value;
                OnPropertyChanged(nameof(Applicants)); // Notify that there was a change on this property
            }
        }

        public int RoomType
        {
            get { return roomtype; }
            set
            {
                roomtype = value;
                OnPropertyChanged(nameof(RoomType)); // Notify that there was a change on this property
            }
        }

        public string LongDescription
        {
            get { return longdescription; }
            set
            {
                longdescription = value;
                OnPropertyChanged(nameof(LongDescription)); // Notify that there was a change on this property
            }
        }
        public double DownPayment
        {
            get { return downpayment; }
            set
            {
                downpayment = value;
                OnPropertyChanged(nameof(DownPayment)); // Notify that there was a change on this property
            }
        }
        public double FullPayment
        {
            get { return fullpayment; }
            set
            {
                fullpayment = value;
                OnPropertyChanged(nameof(FullPayment)); // Notify that there was a change on this property
            }
        }


        public BTODetail(string id)
        {
            WarningMsg = "False";
            InitializeComponent();

            BTOId = id;

            Task.Run(async () =>
            {
                BTOAssistDatabase database = await BTOAssistDatabase.Instance;
                List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();
            });

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
            {
                Command = new Command(async () => {

                    Device.BeginInvokeOnMainThread(async () => {
                        var homepage = new HomePage();
                        await Navigation.PopToRootAsync();
                        await Shell.Current.GoToAsync("//HomePage");
                    });
                })
            });

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


                if (results1.ToString() == "0")
                {
                    BtnText = "Apply";
                    EnableBtn = "true";
                    WarningMsg = "False";
                }
                else
                {
                    BtnText = "Not Applicable";
                    EnableBtn = "False";
                    WarningMsg = "True";
                }
            });
            }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                //await Navigation.PopToRootAsync();
                //await Shell.Current.GoToAsync("//HomePage");
                

            });

            return base.OnBackButtonPressed();
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            GetLink();
            const string getHouseInfo = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getOneBTOInfo";
            var values = new Dictionary<string, string>
                      {
                          { "id", BTOId},

                      };
            var newUrl = new Uri(QueryHelpers.AddQueryString(getHouseInfo, values));
            HttpResponseMessage getHouseInfoResponse = await client.GetAsync(newUrl);
            var responseString = await getHouseInfoResponse.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(responseString);
            var array = data["result"] as JArray;
            var details = array[0];
            Id = details["id"].ToString();
            Image = details["image"].ToString();
            Location = details["location"].ToString();
            Block = details["block"].ToString();
            SQM = details["sqm"].ToString();
            Bus = details["bus"].ToString();
            Mrt = details["mrt"].ToString();
            Direction = details["direction"].ToString();
            RoomType = Int32.Parse(details["roomtype"].ToString());
            YearsLeft = Int32.Parse(details["yearsleft"].ToString());
            Applicants = Int32.Parse(details["applicants"].ToString());
            LongDescription = details["longdescription"].ToString();
            DownPayment = Double.Parse(details["downpayment"].ToString());
            FullPayment = Double.Parse(details["fullpayment"].ToString());
            Test = "sssssssssssssss";
            BindingContext = this;
        }

        async void ApplyBTO(object sender, EventArgs args)
        {
            var BTODeets = (Button)sender;
            string id = BTODeets.AutomationId;
            await Navigation.PushAsync(new ApplicationPage1(id));
        }
    }
}