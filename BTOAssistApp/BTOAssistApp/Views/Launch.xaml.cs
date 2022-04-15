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
                Trace.WriteLine("Callback>>>> " + Singpass);
                var authCode = Singpass.Split('=');
                authCode = authCode[1].Split('&');


                client = new HttpClient();
                Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonData");
                Uri insertData = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/insertPhoneInfo");
                Uri insertPersonData = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/insertPersonInfo");
                /*string[] dirs = Directory.GetDirectories(rootpath, "*", SearchOption.AllDirectories);
                foreach (string files in dirs)
                {
                    Console.WriteLine(files);
                }*/
                Task.Run(async () =>
                {
                    //string code = "code";
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
                        //string str = "" + response.Content.ToString() + " : " + response.StatusCode;
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
                        Console.WriteLine("full thing >>>> " + json["personData"]["mobileno"]["nbr"]["value"]);
                        Console.WriteLine("full thing >>>> " + json["personData"]["sex"]["desc"]);
                        Console.WriteLine("full thing >>>> " + nullChecker(json["personData"]["marital"]["desc"].ToString()));
                        Console.WriteLine("full thing >>>> " + json["personData"]["nationality"]["desc"]);
                        Console.WriteLine("full thing >>>> " + json["personData"]["occupation"]["desc"]);

                        if (json["personData"]["marital"]["desc"].ToString() == "")
                        {
                            Console.WriteLine("BLEGH");
                        }


                        var cpf = json["personData"]["cpfcontributions"];

                        var cpfAmount = cpf["history"] as JArray;
                        var count = 0;
                        var amount = 0.0;
                        var math = "";
                        var average = 0.0;
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
                            }
                        }
                        if (amount == 0 || math == "")
                        {
                            average = 0;
                        }
                        else
                        {
                            average = amount / count;
                        }
                        var test = json["personData"]["name"]["value"].ToString();

                        //EncryptStringToBytes_Aes(json["personData"]["name"]["value"].ToString());
                        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>",Key);

                        using (Aes myAes = Aes.Create())
                        {

                            var personValues = new Dictionary<string, string>
                      {
                          { "phoneid", CrossDeviceInfo.Current.Id.ToString() },
                          { "name", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["name"]["value"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "uinfin", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["uinfin"]["value"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "mobileno", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["mobileno"]["nbr"]["value"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "sex", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["sex"]["desc"].ToString(), myAes.Key, myAes.IV))+","+ Convert.ToBase64String(myAes.Key)+","+ Convert.ToBase64String(myAes.IV) },
                          { "dob", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["dob"]["value"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "marital", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["marital"]["desc"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "nationality", Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["nationality"]["desc"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "occupation",Convert.ToBase64String(EncryptStringToBytes_Aes(json["personData"]["occupation"]["desc"].ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) },
                          { "cpfcontributions", Convert.ToBase64String(EncryptStringToBytes_Aes(average.ToString(), myAes.Key, myAes.IV))+","+Convert.ToBase64String(myAes.Key)+","+Convert.ToBase64String(myAes.IV) }

                            };
                            







                        var personStringContent = new FormUrlEncodedContent(personValues);
                        HttpResponseMessage insertIntoPersonTable = await client.PostAsync(insertPersonData, personStringContent);
                        var personInfoResponseString = await insertIntoPersonTable.Content.ReadAsStringAsync();
                        Uri getPersonInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/insertPersonInfo");
                        HttpResponseMessage personInfoResponse = await client.GetAsync(getPersonInfo);
                        string personContent = await personInfoResponse.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(personContent);
                        Console.WriteLine(">>>>>>>>>>>" + data);
                        }
                        ////////////////////////////////////////////////////




                        /*
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
                        */
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

        public string nullChecker(string jsonObject)
        {
            if (jsonObject == "")
            {
                jsonObject = "!";
            }
            else
            {
                jsonObject = jsonObject + "!";
            }
            Console.WriteLine(jsonObject);
            return jsonObject;

        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            

            if (plainText == "")
            {
                plainText = "!";
            }
            else
            {
                plainText = plainText + "!";
            }

            byte[] encrypted;
            Console.WriteLine("Key >>>>>>>>>>>>> "+ Convert.ToBase64String(Key));
            Console.WriteLine("Key >>>>>>>>>>>>> " + Convert.ToBase64String(IV));
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
            
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
            return encrypted;
        }
        /*
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        */
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