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
            if (Singpass.Contains("callback?code=") == true)
            {
                Trace.WriteLine("Callback>>>> "+Singpass);
                var authCode = Singpass.Split('=');
                authCode = authCode[1].Split('&');


                client = new HttpClient();
                Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonData");

                Task.Run(async () =>
                {
                    //string code = "code";
                    Trace.WriteLine("authCode[0] " + authCode[0]);
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
                        string content = await response.Content.ReadAsStringAsync();
                        Trace.WriteLine("content: " + content.ToString());
                        /*dynamic gibberish = JObject.Parse(responseString);
                         
                        //accessToken 
                        Trace.WriteLine(gibberish);*/
                        JObject json = JObject.Parse(responseString);
                        Console.WriteLine("full thing >>>> " + json);
                        Console.WriteLine("accessToken >>>> " + json["accessToken"]);
                        Console.WriteLine("decokedToken >>>> " + json["decodedToken"]["sub"]);

                       
                        var newPhoneInfo = new PhoneInfo();
                        PostGre postGre = new PostGre();
                        postGre.CheckDataAsync("d");

                        newPhoneInfo.deviceID = CrossDeviceInfo.Current.Id.ToString();
                        Console.WriteLine("stored deviceID >>>> " + newPhoneInfo.deviceID);
                        newPhoneInfo.accessToken = json["accessToken"].ToString();
                        Console.WriteLine("stored accessToken >>>> " +newPhoneInfo.accessToken);
                        newPhoneInfo.sub = json["decodedToken"]["sub"].ToString();
                        BTOAssistDatabase database = await BTOAssistDatabase.Instance;

                        await database.DeleteAllPhoneInfoAsync();
                        var count1 = await database.GetAllPhoneInfoAsync();
                        Console.WriteLine(">>>> Here 1");
                        await database.CheckDataAsync(newPhoneInfo);
                        Console.WriteLine(">>>> Here 2");
                        //await database.CheckDataAsync(newPhoneInfo);//<-----
                        Console.WriteLine(">>>> Here 3");
                        var count2 = await database.GetAllPhoneInfoAsync();
                        Console.WriteLine(">>>> Here 4");
                        await database.CheckDataAsync(newPhoneInfo);
                        Console.WriteLine(">>>> Here 5");
                        var count3 = await database.GetAllPhoneInfoAsync();
                    }
                    catch (Exception ehdhfg)
                    {
                        Trace.WriteLine(ehdhfg);
                    }

                });

                //WebGrid = "false";
                //Shell.Current.GoToAsync("//HomePage");
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
                                if (Progress == "100%")
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