
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationPage1 : ContentPage
    {

        

        private string devID;
        
        private string accesstoken;
        private string sub;
        private string page2;

        private string page1vis;
        private string page2vis;
        private string page3vis;
        private string page4vis;

        HttpClient client;

        public string Page1Vis
        {
            get { return page1vis; }
            set
            {
                page1vis = value;
                OnPropertyChanged(nameof(Page1Vis)); // Notify that there was a change on this property
            }
        }
        public string Page2Vis
        {
            get { return page2vis; }
            set
            {
                page2vis = value;
                OnPropertyChanged(nameof(Page2Vis)); // Notify that there was a change on this property
            }
        }
        public string Page3Vis
        {
            get { return page3vis; }
            set
            {
                page3vis = value;
                OnPropertyChanged(nameof(Page3Vis)); // Notify that there was a change on this property
            }
        }
        public string Page4Vis
        {
            get { return page4vis; }
            set
            {
                page4vis = value;
                OnPropertyChanged(nameof(Page4Vis)); // Notify that there was a change on this property
            }
        }

        public string Page2
        {
            get { return page2; }
            set
            {
                page2 = value;
                OnPropertyChanged(nameof(Page2)); // Notify that there was a change on this property
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
        public string Sub
        {
            get { return sub; }
            set
            {
                sub = value;
                OnPropertyChanged(nameof(Sub)); // Notify that there was a change on this property
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
        public ApplicationPage1()
        {
            InitializeComponent();

            var deviceId = CrossDeviceInfo.Current.Id;


            Task.Run(async () =>
        {

            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            //await database.DeleteAllPhoneInfoAsync();
            PhoneInfo BTODataDetails = await database.GetBTODataAsync(deviceId);
            List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();

            foreach (var i in allPhoneInfo)
            {
                Trace.WriteLine("deviceID: " + i.deviceID);
                Trace.WriteLine("appPageAccessToken: " + i.accessToken);
            }
            DevID = BTODataDetails.deviceID;
            Sub = BTODataDetails.sub;
            AccessToken = BTODataDetails.accessToken;
            Trace.WriteLine(">>>>>>>>>>>>> Sub:" + Sub);
            Trace.WriteLine(">>>>>>>>>>>>> DevID:" + DevID);
            Trace.WriteLine("AccessToken: " + AccessToken);

            //const string url = "https://customer-information.azure-api.net/customers/search/taxnbr";
            //var param = new Dictionary<string, string>() { { "CIKey", "123456789" } };


            client = new HttpClient();

            //string code = "code";
            const string getToken = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/testRoute/";
            //string parametersJson = JsonConvert.SerializeObject(new { coded = "authCode[0].ToString()" });
            var values = new Dictionary<string, string>
                  {
                      {"Sub", Sub},
                      {"AccessToken", AccessToken},

                  };
            Trace.WriteLine("SEESEESEESEESEESEESEESEE");
            var newUrl = new Uri(QueryHelpers.AddQueryString(getToken, values));

            //string json = JsonConvert.SerializeObject(new { "PropertyA" = obj.PropertyA });
            var stringContent = new FormUrlEncodedContent(values);

            Trace.WriteLine("<><><><><><><><><><>"+newUrl);
            try
            {
                HttpResponseMessage response = await client.GetAsync(newUrl);
                Trace.WriteLine(">>>>>>>>>>>>>>>>>>"+response);
                //var stat = response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                Trace.WriteLine("content: " + content.ToString());
                //Trace.WriteLine("stat: " + stat.ToString());
            }
            catch (Exception ehdhfg)
            {
                Trace.WriteLine(ehdhfg);
            }

        });

        }

        protected override async void OnAppearing()
        {
            BindingContext = this;

            Page1Vis = "true";
            Page2Vis = "false";
            Page3Vis = "false";
            Page4Vis = "false";

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var BTOPage = (Button)sender;
            int id = Convert.ToInt32(BTOPage.AutomationId);

            switch (id)
            {
                case 1:
                    Page1Vis = "false";
                    Page2Vis = "true";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    break;
                case 2:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "true";
                    Page4Vis = "false";
                    break;
                case 3:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "true";
                    break;
                case 4:
                    // code block
                    break;
            }
        }
    }
}