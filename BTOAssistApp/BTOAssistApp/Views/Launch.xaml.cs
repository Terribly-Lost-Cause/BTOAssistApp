using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Net.Http;
namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Launch : ContentPage
    {
        HttpClient client;

        private string progress;
        private string progressgrid;
        private string errorgrid;
        private string buttongrid;
        private string errormsg;

        public string Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress)); // Notify that there was a change on this property
            }
        }
        public string ProgressGrid
        {
            get { return progressgrid; }
            set
            {
                progressgrid = value;
                OnPropertyChanged(nameof(ProgressGrid)); // Notify that there was a change on this property
            }
        }
        public string ErrorGrid
        {
            get { return errorgrid; }
            set
            {
                errorgrid = value;
                OnPropertyChanged(nameof(ErrorGrid)); // Notify that there was a change on this property
            }
        }
        public string ButtonGrid
        {
            get { return buttongrid; }
            set
            {
                buttongrid = value;
                OnPropertyChanged(nameof(ButtonGrid)); // Notify that there was a change on this property
            }
        }
        public string ErrorMsg
        {
            get { return errormsg; }
            set
            {
                errormsg = value;
                OnPropertyChanged(nameof(ErrorMsg)); // Notify that there was a change on this property
            }
        }
        async void OnLogin(object sender, EventArgs args)
        {
            await Task.Delay(1000);
            WebView webView = new WebView();
            webView.WidthRequest = 50000;
            webView.HeightRequest = 50000;
            webView.IsVisible = true;
            //webView.Source = "https://sandbox.api.myinfo.gov.sg/com/v3/authorise?client_id=STG2-MYINFO-SELF-TEST&attributes=uinfin,name,sex,race,nationality,dob,email,mobileno,regadd,housingtype,hdbtype,marital,edulevel,noa-basic,ownerprivate,cpfcontributions,cpfbalances&purpose=demonstrating MyInfo APIs&state=123&redirect_uri=http://localhost:3001/callback";
            webView.Source = "https://www.google.com/";

            Test.Children.Add(webView);
            //await Shell.Current.GoToAsync("//HomePage");

        }
        
        public Launch()
        {
            InitializeComponent();
            BindingContext = this;

            Progress = "0%";
            ErrorGrid = "false";
            ButtonGrid = "false";
            var counter = 0;
            var stat = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Run(async () =>
                {
                    bool isShowingMyPage2 = Application.Current.MainPage is AppShell;
                    if (isShowingMyPage2 == true)
                    {
                        counter += 1;
                    }

                    if (counter == 5)
                    {
                        var current = Connectivity.NetworkAccess;
                        if (current == NetworkAccess.Internet)
                        {
                            ring.Progress = 0.5;
                            Progress = "50%";
                            client = new HttpClient();
                            Uri uri = new Uri("https://sandbox.api.myinfo.gov.sg/com/v3/person-sample/S9812381D");
                            Uri redirect = new Uri("https://sandbox.api.myinfo.gov.sg/com/v3/authorise");
                            Uri singpass = new Uri("https://id.singpass.gov.sg/static/ndi_embedded_auth.js");
                            HttpResponseMessage response = await client.GetAsync(uri);

                            if (response.IsSuccessStatusCode)
                            {
                                ring.Progress = 1;
                                Progress = "100%";
                                ProgressGrid = "false";
                                ButtonGrid = "true";
                                stat = false;
                                
                                if(Progress == "100%")
                                {

                                    WebView webView = new WebView();
                                    webView.WidthRequest = 500;
                                    webView.HeightRequest = 500;
                                    webView.IsVisible = false;
                                    webView.Source = "https://www.google.com";
                                    
                                    Stl.Children.Add(webView);
                                    

                                }
                            }
                            else
                            {
                                ProgressGrid = "false";
                                ErrorGrid = "true";
                                ErrorMsg = "Singpass is unavailable";
                            }

                        }
                        else
                        {
                            ProgressGrid = "false";
                            ErrorGrid = "true";
                            ErrorMsg = "Singpass is unavailable";
                        }
                    }
                });
                return stat;
            });
        }
    }
}