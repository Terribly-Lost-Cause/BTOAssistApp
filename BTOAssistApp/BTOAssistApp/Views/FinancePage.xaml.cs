using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancePage : ContentPage
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

        public FinancePage()
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
                    Trace.WriteLine("accessToken: " + i.accessToken);
                }
                DevID = BTODataDetails.deviceID;
                //Sub = BTODataDetails.sub;
                AccessToken = BTODataDetails.accessToken;
                //Trace.WriteLine(">>>>>>>>>>>>> Sub:" + Sub);
                Trace.WriteLine(">>>>>>>>>>>>> DevID:" + DevID);
                Trace.WriteLine("AccessToken: " + AccessToken);
            });
            
        }
    }
}