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
using Newtonsoft.Json;
using BTOAssistApp.Models;
using Plugin.DeviceInfo;
using BTOAssistApp.Data;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp;
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
        private string redirect;
        private string pagegrid;
        private string webgrid;

        


    public string Redirect
        {
            get { return redirect; }
            set
            {
                redirect = value;
                OnPropertyChanged(nameof(Redirect)); // Notify that there was a change on this property
            }
        }
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
        public string PageGrid
        {
            get { return pagegrid; }
            set
            {
                pagegrid = value;
                OnPropertyChanged(nameof(PageGrid)); // Notify that there was a change on this property
            }

        }
        public string WebGrid
        {
            get { return webgrid; }
            set
            {
                webgrid = value;
                OnPropertyChanged(nameof(WebGrid)); // Notify that there was a change on this property
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
        //async void OnLogin(object sender, EventArgs args)
        //{
        //    /*await Task.Delay(1000);
        //    WebView webView = new WebView();
        //    webView.WidthRequest = 50000;
        //    webView.HeightRequest = 50000;
        //    webView.IsVisible = true;
        //    //webView.Source = "https://sandbox.api.myinfo.gov.sg/com/v3/authorise?client_id=STG2-MYINFO-SELF-TEST&attributes=uinfin,name,sex,race,nationality,dob,email,mobileno,regadd,housingtype,hdbtype,marital,edulevel,noa-basic,ownerprivate,cpfcontributions,cpfbalances&purpose=demonstrating MyInfo APIs&state=123&redirect_uri=http://localhost:3001/callback";
        //    webView.Source = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/#";

        //    Test.Children.Add(webView);*/
        //    //await Shell.Current.GoToAsync("//HomePage");

        //}
        

        

        void webviewNavigating(object sender, WebNavigatedEventArgs e)
        {
            //labelLoading.IsVisible = false;
            var Singpass = e.Url;
            if(Singpass.Contains("callback?code=") == true)
            {
                Trace.WriteLine(Singpass);
                var authCode = Singpass.Split('=');
                authCode = authCode[1].Split('&');

                
                client = new HttpClient();
                Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonData");

                Task.Run(async () =>
                {
                    //string code = "code";
                    Trace.WriteLine("authCode[0] "+authCode[0]);
                    //string parametersJson = JsonConvert.SerializeObject(new { coded = "authCode[0].ToString()" });
                    var values = new Dictionary<string, string>
                      {
                          { "code", authCode[0].ToString() },
  
                      };
                    //string json = JsonConvert.SerializeObject(new { "PropertyA" = obj.PropertyA });
                    var stringContent = new FormUrlEncodedContent(values);
                    
                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(getToken, stringContent);
                        //string str = "" + response.Content.ToString() + " : " + response.StatusCode;
                        var responseString = await response.Content.ReadAsStringAsync();
                        dynamic gibberish = JObject.Parse(responseString);

                        //accessToken 
                        Trace.WriteLine(gibberish);
                        //var accessToken = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(responseString);
                        //dynamic data = JObject.Parse(responseString);
                        

                        var uwu = JObject.Parse(responseString);
                        JArray uwu1 = (JArray)uwu[0];
                        Trace.WriteLine((string)uwu1["sub"]);
                        Trace.WriteLine(">>>>>>>>>>>>>>>>> AuthCode"+ authCode[0].ToString());
                        //Trace.WriteLine(">>>>>>>>>>>>>>>>> Access Token" + accessToken["accessToken"]);



                        var newPhoneInfo = new PhoneInfo();
                        Guid myuuid = Guid.NewGuid();
                        var uuid = myuuid.ToString();
                        //newPhoneInfo.deviceID = CrossDeviceInfo.Current.Id.ToString();
                        //newPhoneInfo.accessToken = accessToken["accessToken"];

                        BTOAssistDatabase database = await BTOAssistDatabase.Instance;
                        await database.AddDataAsync(newPhoneInfo);
                    }
                    catch (Exception ehdhfg){
                        Trace.WriteLine(ehdhfg);
                    }
                    
                });

                //WebGrid = "false";
                //Shell.Current.GoToAsync("//HomePage");
            }
            else
            {

            }
            
            
            
        }


        public Launch()
        {

            

            InitializeComponent();

            

            BindingContext = this;

            Progress = "0%";
            ErrorGrid = "false";
            ButtonGrid = "false";
            PageGrid = "true";
            WebGrid = "false";
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
                                Redirect = "true";
                                if(Progress == "100%")
                                {
                                    await Task.Delay(1000);
                                    PageGrid = "false";
                                    WebGrid = "true";
                                    
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