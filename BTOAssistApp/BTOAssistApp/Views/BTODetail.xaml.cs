using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
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

        HttpClient client = new HttpClient();

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
            InitializeComponent();

            BTOId = id;

            Task.Run(async () =>
            {
                BTOAssistDatabase database = await BTOAssistDatabase.Instance;
                List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();

                foreach (var i in allPhoneInfo)
                {
                    Trace.WriteLine("homePageDeviceID: " + i.deviceID);
                    Trace.WriteLine("homePageAccessToken: " + i.accessToken);
                }
            });

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            const string getHouseInfo = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getOneBTOInfo";
            var values = new Dictionary<string, string>
                      {
                          { "id", BTOId},

                      };
            var newUrl = new Uri(QueryHelpers.AddQueryString(getHouseInfo, values));
            //var stringContent = new FormUrlEncodedContent(values);
            HttpResponseMessage getHouseInfoResponse = await client.GetAsync(newUrl);
            var responseString = await getHouseInfoResponse.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(responseString);
            var array = data["result"] as JArray;
            var details = array[0];

            Console.WriteLine("array: " + array[0]);
            //PostGre postGre = new PostGre();
            //BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            //BTO BTODetails = postGre.GetBTODetailsAsync(array[0]["id"]);

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
            //await Shell.Current.GoToAsync("//ApplicationPage1");
            Trace.WriteLine(id);
        }
    }
}