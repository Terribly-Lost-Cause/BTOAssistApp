
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
    }
}