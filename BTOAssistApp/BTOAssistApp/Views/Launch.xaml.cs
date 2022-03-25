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
        public string ErrorMsg
        {
            get { return errormsg; }
            set
            {
                errormsg = value;
                OnPropertyChanged(nameof(ErrorMsg)); // Notify that there was a change on this property
            }
        }
        public Launch()
        {
            InitializeComponent();
            BindingContext = this;

            Progress = "0%";
            ErrorGrid = "false";
            var counter = 0;
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

                            HttpResponseMessage response = await client.GetAsync(uri);
                            if (response.IsSuccessStatusCode)
                            {
                                ring.Progress = 1;
                                Progress = "100%";
                                await Task.Delay(1000);
                                await Shell.Current.GoToAsync("//AboutPage");
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
                return true;
            });
            
        }
    }
}