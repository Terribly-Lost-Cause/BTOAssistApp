using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Plugin.DeviceInfo;
using System.Security.Cryptography;
using System.IO;
using System.Collections.ObjectModel;
using Syncfusion;
using Syncfusion.Licensing;
using System.Collections;
using Syncfusion.SfChart.XForms;
using Newtonsoft.Json;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CPFPage : ContentPage
    {
        private string cpfamount;
        private string cpfprogressring;
        private string cpfpercentage;
        private string currentcpf;
        private string second;
        private string countdown;

        private string cpfpagevisibity;
        private string errorpagevisibility;
        HttpClient client;

        public string CPFPageVisability
        {
            get { return cpfpagevisibity; }
            set
            {
                cpfpagevisibity = value;
                OnPropertyChanged(nameof(CPFPageVisability)); // Notify that there was a change on this property
            }
        }

        public string ErrorPageVisability
        {
            get { return errorpagevisibility; }
            set
            {
                errorpagevisibility = value;
                OnPropertyChanged(nameof(ErrorPageVisability)); // Notify that there was a change on this property
            }
        }
        public string Countdown
        {
            get { return countdown; }
            set
            {
                countdown = value;
                OnPropertyChanged(nameof(Countdown)); // Notify that there was a change on this property
            }
        }

        public string Second
        {
            get { return second; }
            set
            {
                second = value;
                OnPropertyChanged(nameof(Second)); // Notify that there was a change on this property
            }
        }

        public string CPFAmount
        {
            get { return cpfamount; }
            set
            {
                cpfamount = value;
                OnPropertyChanged(nameof(CPFAmount)); // Notify that there was a change on this property
            }
        }
        public string CPFProgressRing
        {
            get { return cpfprogressring; }
            set
            {
                cpfprogressring = value;
                OnPropertyChanged(nameof(CPFProgressRing)); // Notify that there was a change on this property
            }
        }
        public string CPFPercentage
        {
            get { return cpfpercentage; }
            set
            {
                cpfpercentage = value;
                OnPropertyChanged(nameof(CPFPercentage)); // Notify that there was a change on this property
            }
        }
        public string CurrentCPF
        {
            get { return currentcpf; }
            set
            {
                currentcpf = value;
                OnPropertyChanged(nameof(CurrentCPF)); // Notify that there was a change on this property
            }
        }


        public List<CPFHistory> Data { get; set; }
        Dictionary<string, double> cpfRecords = new Dictionary<string, double>();

        
        public CPFPage()
        {
            CPFPageVisability = "False";
            ErrorPageVisability = "False";
            InitializeComponent();
            
            Task.Run(async () =>
            {

                HttpClient client = new HttpClient();

                const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";
                var personValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()},

                      };
                var newUrl = new Uri(QueryHelpers.AddQueryString(getPerson, personValues));
                //var stringContent = new FormUrlEncodedContent(values);


                HttpResponseMessage personResponse = await client.GetAsync(newUrl);

                string personContent = await personResponse.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(personContent);
                var array = data["result"] as JArray;
                Console.WriteLine("array  " + array[0]);
                var values = JObject.FromObject(array[0]).ToObject<Dictionary<string, object>>();
                Console.WriteLine(">>>>>>>>>>>Values>>>>>>>>>>>" + values);
                foreach (KeyValuePair<string, Object> entry in values)
                {
                    try
                    {
                        if (entry.Key == "cpfcontributionhistory")
                        {
                            
                            var personValue = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                            JArray contributionHistory = JArray.Parse(personValue);
                            Console.WriteLine(contributionHistory);
                            var dateArray = new HashSet<string>();
                            foreach (JObject item in contributionHistory) // <-- Note that here we used JObject instead of usual JProperty
                            {
                                var date = item.GetValue("month");
                                string year = date["value"].ToString().Substring(0, 4);

                                dateArray.Add(year);
                            }
                            List<string> hList = dateArray.ToList();

                            //picker.ItemsSource = hList;


                            dateArray.ToList<String>().ForEach(x => Console.WriteLine(x));
                            Console.WriteLine("{}{}{}{}{}{}{}{}}{}");

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            });
           
        }


        protected override async void OnAppearing()
        {

             










            client = new HttpClient();
            BindingContext = this;
            double downpayment = 0.0;
            await Task.Run(async () =>
            {
                const string getBTO = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getAppliedBTOinfo";

                var BTOValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()}

                      };
                var newGetBTOUrl = new Uri(QueryHelpers.AddQueryString(getBTO, BTOValues));
                //var stringContent = new FormUrlEncodedContent(values);


                HttpResponseMessage BTOResponse = await client.GetAsync(newGetBTOUrl);
                string BTOContent = await BTOResponse.Content.ReadAsStringAsync();
                
                JObject btodata = JObject.Parse(BTOContent);
                var btoarray = btodata["resultForCPFPage"] as JArray;


                var license = btodata["key"].ToString();

                var results = btodata["resultForBTOStatusPage"];


                if(results.ToString() == "0")
                {
                    ErrorPageVisability = "true";
                    CPFPageVisability = "false";
                    var counter = 5;
                    var stat = true;
                    Second = "Seconds";
                    Countdown = counter.ToString();
                    BindingContext = this;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        if (counter != 1)
                        {
                            Console.WriteLine("HERE AGAIN");
                            counter -= 1;
                            Countdown = counter.ToString();

                            if (counter == 1)
                            {
                                Second = "Second";
                            }
                            else
                            {
                                Second = "Seconds";
                            }
                            stat = true;
                            BindingContext = this;
                        }
                        else
                        {
                            stat = false;
                            Shell.Current.GoToAsync("//HomePage");
                        }
                        return stat;
                    });
                }
                else
                {
                    ErrorPageVisability = "false";
                    CPFPageVisability = "true";
                    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license.ToString());
                bool isValid = SyncfusionLicenseProvider.ValidateLicense(Syncfusion.Licensing.Platform.Xamarin);
                Console.WriteLine(isValid.ToString());
                downpayment = double.Parse(results[0]["downpayment"].ToString());
                CPFAmount = "$"+ results[0]["downpayment"].ToString();
                Console.WriteLine("array  " + results[0]["downpayment"]);
                            ///////////////////////////
            const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";
            Console.WriteLine(">>>>>>>>>>>>>>>>>>CrossDeviceInfo.Current.Id.ToString()>>>>>>>>>>>>> " + CrossDeviceInfo.Current.Id.ToString());
            var personValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()},

                      };
            var newUrl = new Uri(QueryHelpers.AddQueryString(getPerson, personValues));
            //var stringContent = new FormUrlEncodedContent(values);


            HttpResponseMessage personResponse = await client.GetAsync(newUrl);

            //string str = "te";
            string personContent = await personResponse.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(personContent);
            var array = data["result"] as JArray;
            Console.WriteLine("array  " + array[0]);
            var values = JObject.FromObject(array[0]).ToObject<Dictionary<string, object>>();
            Console.WriteLine(">>>>>>>>>>>Values>>>>>>>>>>>" + values);
            double cpfVal = 0.0;
            foreach (KeyValuePair<string, Object> entry in values)
            {
                Console.WriteLine(entry.Value);
                try
                {
                    if (entry.Key == "cpfcontributions")
                    {
                        
                        var test = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                        
                        cpfVal = double.Parse(test.ToString());
                        Console.WriteLine(cpfVal);
                        CurrentCPF = "Current: $"+cpfVal.ToString();
                        var progress = (cpfVal / downpayment);
                        var progressPercent = progress * 100;
                        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>> "+ progress.ToString());
                        CPFPercentage = Math.Round(progressPercent, 2).ToString() + "%";
                        CPFProgressRing = progress.ToString();

                        //var personValue = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                        //checker(entry.Key.ToString(), personValue.ToString());



                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                

            }
                }

                
            });

        }
        
        

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");

            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");

            }
            //cipherText = Convert.ToBase64String(cipherText);


            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                //Key = aesAlg.Key;
                //IV = aesAlg.IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(Key, IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            try
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                        }
                    }
                }
            }

            return plaintext;
        }


        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Dictionary<string, double> cpfRecords = new Dictionary<string, double>();

            var _picker = sender as Picker;
            //var collection = viewModel.pizzaCollection;
            var selectedYear = _picker.SelectedItem;
            Console.WriteLine(selectedYear);

            Task.Run(async () =>
            {

                HttpClient client = new HttpClient();

                const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";
                var personValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()},

                      };
                var newUrl = new Uri(QueryHelpers.AddQueryString(getPerson, personValues));
                //var stringContent = new FormUrlEncodedContent(values);


                HttpResponseMessage personResponse = await client.GetAsync(newUrl);

                //string str = "te";
                string personContent = await personResponse.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(personContent);
                var array = data["result"] as JArray;
                Console.WriteLine("array  " + array[0]);
                var values = JObject.FromObject(array[0]).ToObject<Dictionary<string, object>>();
                Console.WriteLine(">>>>>>>>>>>Values>>>>>>>>>>>" + values);
                foreach (KeyValuePair<string, Object> entry in values)
                {
                    //Console.WriteLine(entry.Value);
                    try
                    {
                        if (entry.Key == "cpfcontributionhistory")
                        {

                            var personValue = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                            JArray contributionHistory = JArray.Parse(personValue);
                            Console.WriteLine(contributionHistory);
                            var dateArray = new HashSet<string>();
                            int count = 0;
                            Data = new List<CPFHistory>()
                            {

                            };
                            Console.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMM "+Data.Count);
                            foreach (JObject item in contributionHistory) // <-- Note that here we used JObject instead of usual JProperty
                            {
                                var date = item.GetValue("month");
                                string year = date["value"].ToString().Substring(0, 4);

                                dateArray.Add(year);


                                
                               //Data.Clear();
                               // var currentYear = Int16.Parse(DateTime.Now.Year.ToString());

                                
                                if (_picker.SelectedIndex == 1)
                                {
                                    BindingContext = null;
                                    Console.WriteLine("SEEEEMEEEEE "+ _picker.SelectedIndex);
                                    BindingContext = this;

                                    /*var amount = item.GetValue("amount");
                                    Console.WriteLine(">>> >>> >>> date " + date["value"] + " >>>  >>>  >>> amount " + amount["value"]);
                                   // cpfRecords.Add(date["value"].ToString(), Double.Parse(amount["value"].ToString()));




                                    Data.Add(new CPFHistory(date["value"].ToString(), Double.Parse(amount["value"].ToString())));*/

                                }
                                else if (_picker.SelectedIndex == 2)
                                {
                                    BindingContext = null;
                                    Console.WriteLine("SEEEEMEEEEE " + _picker.SelectedIndex);
                                    BindingContext = this;
                                }

                            }
                            dateArray.ToList<String>().ForEach(x => Console.WriteLine(x));
                            Console.WriteLine("{}{}{}{}{}{}{}{}}{}");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

            });

        }


    }
    

    public class ViewModel
    {

        public List<CPFHistory> Data { get; set; }
        Dictionary<string, double> cpfRecords = new Dictionary<string, double>();



        public ViewModel()
        {
            Data = new List<CPFHistory>()
             {
                
             };


            Task.Run(async () =>
            {
                
                HttpClient client = new HttpClient();

                const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";
                var personValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()},

                      };
                var newUrl = new Uri(QueryHelpers.AddQueryString(getPerson, personValues));


                HttpResponseMessage personResponse = await client.GetAsync(newUrl);

                string personContent = await personResponse.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(personContent);
                var array = data["result"] as JArray;
                Console.WriteLine("array  " + array[0]);
                var values = JObject.FromObject(array[0]).ToObject<Dictionary<string, object>>();
                Console.WriteLine(">>>>>>>>>>>Values>>>>>>>>>>>" + values);
                foreach (KeyValuePair<string, Object> entry in values)
                {
                    try
                    {
                        if (entry.Key == "cpfcontributionhistory")
                        {
                            var personValue = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                            JArray contributionHistory = JArray.Parse(personValue);
                            Console.WriteLine(contributionHistory);
                            var dateArray = new HashSet<string>() ;
                            int count = 0;
                            foreach (JObject item in contributionHistory) // <-- Note that here we used JObject instead of usual JProperty
                            {
                                var date = item.GetValue("month");
                                string year = date["value"].ToString().Substring(0,4);

                                dateArray.Add(year);

                                var currentYear = Int16.Parse(DateTime.Now.Year.ToString());
                                

                                if (year == currentYear.ToString())
                                {
                                var amount = item.GetValue("amount");
                                Console.WriteLine(">>> >>> >>> date " + date["value"] + " >>>  >>>  >>> amount " + amount["value"]);
                                cpfRecords.Add(date["value"].ToString(), Double.Parse(amount["value"].ToString()));
                                Data.Add(new CPFHistory(date["value"].ToString(), Double.Parse(amount["value"].ToString())));
                                }
                                
                            }
                            dateArray.ToList<String>().ForEach(x => Console.WriteLine(x));
                            Console.WriteLine("{}{}{}{}{}{}{}{}}{}");

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
                var test2 = DecryptStringFromBytes_Aes(Convert.FromBase64String(array[0]["uinfin"].ToString().Split(',')[0]), Convert.FromBase64String(array[0]["name"].ToString().Split(',')[1]), Convert.FromBase64String(array[0]["name"].ToString().Split(',')[2]));









            });
            


        }



        
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");

            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");

            }
            //cipherText = Convert.ToBase64String(cipherText);


            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                //Key = aesAlg.Key;
                //IV = aesAlg.IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(Key, IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            try
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                        }
                    }
                }
            }

            return plaintext;
        }

    }

    public class CPFHistory
    {
        public string Month { get; set; }

        public double Amount { get; set; }

        public CPFHistory(string month, double amount)
        {
            this.Month = month;
            this.Amount = amount;
        }
    
}
    
    

    


}