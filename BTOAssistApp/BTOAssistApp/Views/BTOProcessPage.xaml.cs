using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BTOProcessPage : ContentPage
    {
        private string step2numbercolor;
        private string step2areacolor;
        private string step2textcolor;

        private string step3numbercolor;
        private string step3areacolor;
        private string step3textcolor;

        private string step4numbercolor;
        private string step4areacolor;
        private string step4textcolor;

        private string step5numbercolor;
        private string step5areacolor;
        private string step5textcolor;

        private string step6numbercolor;
        private string step6areacolor;
        private string step6textcolor;

        private string step7numbercolor;
        private string step7areacolor;
        private string step7textcolor;

        private string btoprocessvisibity;
        private string errorpagevisibility;
        private string second;
        private string countdown;

        HttpClient client;


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

        public string BTOProcessVisability
        {
            get { return btoprocessvisibity; }
            set
            {
                btoprocessvisibity = value;
                OnPropertyChanged(nameof(BTOProcessVisability)); // Notify that there was a change on this property
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
        public string Step2NumberColor
        {
            get { return step2numbercolor; }
            set
            {
                step2numbercolor = value;
                OnPropertyChanged(nameof(Step2NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step2AreaColor
        {
            get { return step2areacolor; }
            set
            {
                step2areacolor = value;
                OnPropertyChanged(nameof(Step2AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step2TextColor
        {
            get { return step2textcolor; }
            set
            {
                step2textcolor = value;
                OnPropertyChanged(nameof(Step2TextColor)); // Notify that there was a change on this property
            }
        }

        public string Step3NumberColor
        {
            get { return step3numbercolor; }
            set
            {
                step3numbercolor = value;
                OnPropertyChanged(nameof(Step3NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step3AreaColor
        {
            get { return step3areacolor; }
            set
            {
                step3areacolor = value;
                OnPropertyChanged(nameof(Step3AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step3TextColor
        {
            get { return step3textcolor; }
            set
            {
                step3textcolor = value;
                OnPropertyChanged(nameof(Step3TextColor)); // Notify that there was a change on this property
            }
        }

        public string Step4NumberColor
        {
            get { return step4numbercolor; }
            set
            {
                step4numbercolor = value;
                OnPropertyChanged(nameof(Step4NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step4AreaColor
        {
            get { return step4areacolor; }
            set
            {
                step4areacolor = value;
                OnPropertyChanged(nameof(Step4AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step4TextColor
        {
            get { return step4textcolor; }
            set
            {
                step4textcolor = value;
                OnPropertyChanged(nameof(Step4TextColor)); // Notify that there was a change on this property
            }
        }

        public string Step5NumberColor
        {
            get { return step5numbercolor; }
            set
            {
                step5numbercolor = value;
                OnPropertyChanged(nameof(Step5NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step5AreaColor
        {
            get { return step5areacolor; }
            set
            {
                step5areacolor = value;
                OnPropertyChanged(nameof(Step5AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step5TextColor
        {
            get { return step5textcolor; }
            set
            {
                step5textcolor = value;
                OnPropertyChanged(nameof(Step5TextColor)); // Notify that there was a change on this property
            }
        }

        public string Step6NumberColor
        {
            get { return step6numbercolor; }
            set
            {
                step6numbercolor = value;
                OnPropertyChanged(nameof(Step6NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step6AreaColor
        {
            get { return step6areacolor; }
            set
            {
                step6areacolor = value;
                OnPropertyChanged(nameof(Step6AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step6TextColor
        {
            get { return step6textcolor; }
            set
            {
                step6textcolor = value;
                OnPropertyChanged(nameof(Step6TextColor)); // Notify that there was a change on this property
            }
        }

        public string Step7NumberColor
        {
            get { return step7numbercolor; }
            set
            {
                step7numbercolor = value;
                OnPropertyChanged(nameof(Step7NumberColor)); // Notify that there was a change on this property
            }
        }
        public string Step7AreaColor
        {
            get { return step7areacolor; }
            set
            {
                step7areacolor = value;
                OnPropertyChanged(nameof(Step7AreaColor)); // Notify that there was a change on this property
            }
        }
        public string Step7TextColor
        {
            get { return step7textcolor; }
            set
            {
                step7textcolor = value;
                OnPropertyChanged(nameof(Step7TextColor)); // Notify that there was a change on this property
            }
        }

        public BTOProcessPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            HttpClient client = new HttpClient();
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

                var results = btodata["resultForCPFPage"];


                if (results.ToString() == "0")
                {
                    ErrorPageVisability = "true";
                    BTOProcessVisability = "false";
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
                    BTOProcessVisability = "true";
                    int status = Int16.Parse(results[0]["status"].ToString());
                    Console.WriteLine(status);
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>> "+status.GetType());
                    switch (status)
                    {
                        case 1:
                            Step2NumberColor = "#0C8188";
                            Step2AreaColor = "#FAFFFF";
                            Step2TextColor = "#707070";

                            Step3NumberColor = "#0C8188";
                            Step3AreaColor = "#FAFFFF";
                            Step3TextColor = "#707070";

                            Step4NumberColor = "#0C8188";
                            Step4AreaColor = "#FAFFFF";
                            Step4TextColor = "#707070";

                            Step5NumberColor = "#0C8188";
                            Step5AreaColor = "#FAFFFF";
                            Step5TextColor = "#707070";

                            Step6NumberColor = "#0C8188";
                            Step6AreaColor = "#FAFFFF";
                            Step6TextColor = "#707070";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;
                            break;
                        case 2:
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";

                            Step3NumberColor = "#0C8188";
                            Step3AreaColor = "#FAFFFF";
                            Step3TextColor = "#707070";

                            Step4NumberColor = "#0C8188";
                            Step4AreaColor = "#FAFFFF";
                            Step4TextColor = "#707070";

                            Step5NumberColor = "#0C8188";
                            Step5AreaColor = "#FAFFFF";
                            Step5TextColor = "#707070";

                            Step6NumberColor = "#0C8188";
                            Step6AreaColor = "#FAFFFF";
                            Step6TextColor = "#707070";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;
                            break;

                        case 3:

                            
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";
                            
                            Step3NumberColor = "white";
                            Step3AreaColor = "#0C8188";
                            Step3TextColor = "#0C8188";

                            Step4NumberColor = "#0C8188";
                            Step4AreaColor = "#FAFFFF";
                            Step4TextColor = "#707070";

                            Step5NumberColor = "#0C8188";
                            Step5AreaColor = "#FAFFFF";
                            Step5TextColor = "#707070";

                            Step6NumberColor = "#0C8188";
                            Step6AreaColor = "#FAFFFF";
                            Step6TextColor = "#707070";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;

                            break;

                        case 4:
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";

                            Step3NumberColor = "white";
                            Step3AreaColor = "#0C8188";
                            Step3TextColor = "#0C8188";

                            Step4NumberColor = "white";
                            Step4AreaColor = "#0C8188";
                            Step4TextColor = "#0C8188";

                            Step5NumberColor = "#0C8188";
                            Step5AreaColor = "#FAFFFF";
                            Step5TextColor = "#707070";

                            Step6NumberColor = "#0C8188";
                            Step6AreaColor = "#FAFFFF";
                            Step6TextColor = "#707070";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;
                            break;

                        case 5:
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";

                            Step3NumberColor = "white";
                            Step3AreaColor = "#0C8188";
                            Step3TextColor = "#0C8188";

                            Step4NumberColor = "white";
                            Step4AreaColor = "#0C8188";
                            Step4TextColor = "#0C8188";

                            Step5NumberColor = "white";
                            Step5AreaColor = "#0C8188";
                            Step5TextColor = "#0C8188";

                            Step6NumberColor = "#0C8188";
                            Step6AreaColor = "#FAFFFF";
                            Step6TextColor = "#707070";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;
                            break;

                        case 6:
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";

                            Step3NumberColor = "white";
                            Step3AreaColor = "#0C8188";
                            Step3TextColor = "#0C8188";

                            Step4NumberColor = "white";
                            Step4AreaColor = "#0C8188";
                            Step4TextColor = "#0C8188";

                            Step5NumberColor = "white";
                            Step5AreaColor = "#0C8188";
                            Step5TextColor = "#0C8188";

                            Step6NumberColor = "white";
                            Step6AreaColor = "#0C8188";
                            Step6TextColor = "#0C8188";

                            Step7NumberColor = "#0C8188";
                            Step7AreaColor = "#FAFFFF";
                            Step7TextColor = "#707070";
                            BindingContext = this;
                            break;

                        case 7:
                            Step2NumberColor = "white";
                            Step2AreaColor = "#0C8188";
                            Step2TextColor = "#0C8188";

                            Step3NumberColor = "white";
                            Step3AreaColor = "#0C8188";
                            Step3TextColor = "#0C8188";

                            Step4NumberColor = "white";
                            Step4AreaColor = "#0C8188";
                            Step4TextColor = "#0C8188";

                            Step5NumberColor = "white";
                            Step5AreaColor = "#0C8188";
                            Step5TextColor = "#0C8188";

                            Step6NumberColor = "white";
                            Step6AreaColor = "#0C8188";
                            Step6TextColor = "#0C8188";

                            Step7NumberColor = "white";
                            Step7AreaColor = "#0C8188";
                            Step7TextColor = "#0C8188";
                            BindingContext = this;
                            break;

                        
                    }
                        
                        


                    
                }
            });
            const string getnric = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getAppliedBTOinfo";
            var personValues = new Dictionary<string, string>
                      {
                         { "deviceid", CrossDeviceInfo.Current.Id.ToString()}

                      };
            var newUrl = new Uri(QueryHelpers.AddQueryString(getnric, personValues));
            Console.WriteLine(newUrl);
            HttpResponseMessage personResponse = await client.GetAsync(newUrl);
            string personContent = await personResponse.Content.ReadAsStringAsync();
            Console.WriteLine(personContent);


        }
    }
}