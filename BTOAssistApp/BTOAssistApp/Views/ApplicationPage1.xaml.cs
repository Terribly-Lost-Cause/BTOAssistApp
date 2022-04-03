
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Plugin.DeviceInfo;
using BTOAssistApp.Data;
using BTOAssistApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationPage1 : ContentPage
    {
        private string devID;
        private string id;
        private string accesstoken;
        HttpClient client;
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
        protected override async void OnAppearing()
        {
            var deviceId = CrossDeviceInfo.Current.Id;

            Trace.WriteLine(deviceId);
            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            PhoneInfo BTODataDetails = await database.GetBTODataAsync(deviceId);
            List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();
            foreach(var i in allPhoneInfo)
            {
                Trace.WriteLine("deviceID: "+i.deviceID);
                Trace.WriteLine("accessToken: "+i.accessToken);
            }
            DevID = BTODataDetails.deviceID;
            AccessToken = BTODataDetails.accessToken;
            Trace.WriteLine(">>>>>>>>>>>>> DevID:" + DevID);
            Trace.WriteLine(">>>>>>>>>>>>> AccessToken:" + AccessToken);
            
        }
        public ApplicationPage1()
        {
            InitializeComponent();

            
            
        Task.Run(async () =>
        {

            client = new HttpClient();

            //string code = "code";
            Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonData");
            //string parametersJson = JsonConvert.SerializeObject(new { coded = "authCode[0].ToString()" });
            var values = new Dictionary<string, string>
                  {
                      { "code", accesstoken },

                  };
            //string json = JsonConvert.SerializeObject(new { "PropertyA" = obj.PropertyA });
            var stringContent = new FormUrlEncodedContent(values);

            Trace.WriteLine("<><><><><><><><><><>");
            try
            {
                HttpResponseMessage response = await client.PostAsync(getToken, stringContent);
                Trace.WriteLine(">>>>>><<<<<<");
                var stat = response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                Trace.WriteLine("content: " + content.ToString());
                Trace.WriteLine("stat: " + stat.ToString());
            }
            catch (Exception ehdhfg)
            {
                Trace.WriteLine(ehdhfg);
            }

        });

        }
    }
}