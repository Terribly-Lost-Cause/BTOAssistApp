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
using System.IO;
using System.Security.Cryptography;


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
        byte[] Key;
        byte[] IV;



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
       


        void webviewNavigating(object sender, WebNavigatedEventArgs e)
        {
            //labelLoading.IsVisible = false;
            var Singpass = e.Url;
            if (Singpass.Contains("callback?code=") == true)
            {
                Trace.WriteLine("Callback>>>> " + Singpass);
                var authCode = Singpass.Split('=');
                authCode = authCode[1].Split('&');


                client = new HttpClient();
                Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonData");
                Uri insertPersonData = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/insertPersonInfo");
                /*string[] dirs = Directory.GetDirectories(rootpath, "*", SearchOption.AllDirectories);
                foreach (string files in dirs)
                {
                    Console.WriteLine(files);
                }*/
                Task.Run(async () =>
                {
                    //string code = "code";

                    var testcode = authCode[0];
                    Trace.WriteLine("authCode[0] " + authCode[0]);
                    //string parametersJson = JsonConvert.SerializeObject(new { coded = "authCode[0].ToString()" });
                    //string json = JsonConvert.SerializeObject(new { "PropertyA" = obj.PropertyA });

                    var values = new Dictionary<string, string>
                      {
                          { "code", authCode[0].ToString() },

                      };
                    var stringContent = new FormUrlEncodedContent(values);

                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(getToken, stringContent);
                        //^^^^^string str = "" + response.Content.ToString() + " : " + response.StatusCode;
                        var responseString = await response.Content.ReadAsStringAsync();
                        string content = await response.Content.ReadAsStringAsync();
                        Trace.WriteLine("content: " + content.ToString());

                        //responseString = responseString.Replace(null, "-");
                        /*dynamic gibberish = JObject.Parse(responseString);
                         
                        //accessToken 
                        Trace.WriteLine(gibberish);*/
                        JObject json = JObject.Parse(responseString);
                        
                        /*var jsonString = JsonConvert.SerializeObject(json);
                        var look = "";
                        jsonString = jsonString.Replace(null, "-");*/
                        //Console.WriteLine(json);
                        

                        if (json["personData"]["marital"]["desc"].ToString() == "")
                        {
                            Console.WriteLine("BLEGH");
                        }
                        var average = 0.0;


                        var cpf = json["personData"]["cpfcontributions"];
                        try
                        {
                            var cpfAmount = cpf["history"] as JArray;
                            var count = 0;
                            var amount = 0.0;
                            var math = "";
                            foreach (var i in cpfAmount)
                            {
                                if (Double.Parse(i["amount"]["value"].ToString()) == 0 && count == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    amount += Double.Parse(i["amount"]["value"].ToString());
                                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> amount in for loop " + i["amount"]["value"].ToString());
                                    math = math + i["amount"]["value"].ToString() + "+";
                                    count += 1;
                                    if (amount == 0 || math == "")
                                    {
                                        average = 0;
                                    }
                                    else
                                    {
                                        average =  amount / count;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            average = 0;
                        }
                        
                        var test = json["personData"];
                        var occType = "";
                        var occupation = json["personData"]["occupation"].ToString();
                        if (occupation.Contains("value"))
                        {
                            occType = json["personData"]["occupation"]["value"].ToString();
                        }
                        else if (occupation.Contains("desc"))
                        {
                            occType = json["personData"]["occupation"]["desc"].ToString();
                        }
                        
                        int year = DateTime.Now.Year;
                        int personYear = Int32.Parse(json["personData"]["dob"]["value"].ToString().Substring(0, 4));
                        int age =  year - personYear;
                        Console.WriteLine("::::::::::::: " + CrossDeviceInfo.Current.Id.ToString());
                        Console.WriteLine("::::::::::::: "+json["personData"]["name"]["value"].ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["uinfin"]["value"].ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["mobileno"]["nbr"]["value"].ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["sex"]["desc"].ToString());
                        Console.WriteLine("::::::::::::: " + age.ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["marital"]["desc"].ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["nationality"]["desc"].ToString());
                        Console.WriteLine("::::::::::::: " + json["personData"]["cpfcontributions"]["history"].ToString());
                        Console.WriteLine("::::::::::::: " + occType.ToString());
                        Console.WriteLine("::::::::::::: " + average.ToString());

                        //EncryptStringToBytes_Aes(json["personData"]["name"]["value"].ToString());
                        


                        using (Aes myAes = Aes.Create())
                        {

                            var personValues = new Dictionary<string, string>

                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString() },
                          { "name", EncryptStringToBytes_Aes(json["personData"]["name"]["value"].ToString()) },
                          { "uinfin", EncryptStringToBytes_Aes(json["personData"]["uinfin"]["value"].ToString())},
                          { "mobileno", EncryptStringToBytes_Aes(json["personData"]["mobileno"]["nbr"]["value"].ToString()) },
                          { "sex", EncryptStringToBytes_Aes(json["personData"]["sex"]["desc"].ToString()) },
                          { "dob", EncryptStringToBytes_Aes(age.ToString()) },
                          { "marital", EncryptStringToBytes_Aes(json["personData"]["marital"]["desc"].ToString()) },
                          { "nationality", EncryptStringToBytes_Aes(json["personData"]["nationality"]["desc"].ToString()) },
                          { "occupation",EncryptStringToBytes_Aes(occType.ToString()) },
                          { "cpfcontributions", EncryptStringToBytes_Aes(average.ToString())},
                          { "cpfcontributionhistory", EncryptStringToBytes_Aes(json["personData"]["cpfcontributions"]["history"].ToString()) }
                            };

                        var personStringContent = new FormUrlEncodedContent(personValues);
                        HttpResponseMessage insertIntoPersonTable = await client.PostAsync(insertPersonData, personStringContent);
                        var personInfoResponseString = await insertIntoPersonTable.Content.ReadAsStringAsync();
                        


                            /*const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";

                            HttpResponseMessage personResponse = await client.GetAsync(getPerson);


                            string receivedPersonContent = await personResponse.Content.ReadAsStringAsync();
                            JObject receivedData = JObject.Parse(receivedPersonContent);
                            var array = receivedData["result"] as JArray;*/
                        }
                        
                    }
                    catch (Exception ehdhfg)
                    {
                        Console.WriteLine(ehdhfg);
                    }

                });

                WebGrid = "false";
                Shell.Current.GoToAsync("//HomePage");
            }



        }

        

        static string EncryptStringToBytes_Aes(string plainText)
        {
            // Check arguments.
            if (plainText == "")
            {
                Console.WriteLine("IT'S EMPTY");
                plainText = "-";
            }
            
            
            byte[] encrypted;
            byte[] Key;
            byte[] IV;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                Key = aesAlg.Key;
                IV = aesAlg.IV ;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            
            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted) + "," + Convert.ToBase64String(Key) + "," + Convert.ToBase64String(IV);
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