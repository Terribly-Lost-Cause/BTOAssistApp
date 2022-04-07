using BTOAssistApp.Data;
using BTOAssistApp.Models;
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
    public partial class BTODetail : ContentPage
    {
        private string BTOId;
        private string id;
        private string image;
        private string location;
        private string block;
        private int roomtype;
        private string sqm;
        private string bus;
        private string mrt;
        private string direction;
        private int yearsleft;
        private int applicants;
        private string longdescription;
        private double downpayment;
        private double fullpayment;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id)); // Notify that there was a change on this property
            }
        }


        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image)); // Notify that there was a change on this property
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location)); // Notify that there was a change on this property
            }
        }
        public string Block
        {
            get { return block; }
            set
            {
                block = value;
                OnPropertyChanged(nameof(Block)); // Notify that there was a change on this property
            }
        }
        public string SQM
        {
            get { return sqm; }
            set
            {
                sqm = value;
                OnPropertyChanged(nameof(SQM)); // Notify that there was a change on this property
            }
        }
        public string Bus
        {
            get { return bus; }
            set
            {
                bus = value;
                OnPropertyChanged(nameof(Bus)); // Notify that there was a change on this property
            }
        }
        public string Mrt
        {
            get { return mrt; }
            set
            {
                mrt = value;
                OnPropertyChanged(nameof(Mrt)); // Notify that there was a change on this property
            }
        }
        public string Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                OnPropertyChanged(nameof(Direction)); // Notify that there was a change on this property
            }
        }
        public int YearsLeft
        {
            get { return yearsleft; }
            set
            {
                yearsleft = value;
                OnPropertyChanged(nameof(YearsLeft)); // Notify that there was a change on this property
            }
        }

        public int Applicants
        {
            get { return applicants; }
            set
            {
                applicants = value;
                OnPropertyChanged(nameof(Applicants)); // Notify that there was a change on this property
            }
        }

        public int RoomType
        {
            get { return roomtype; }
            set
            {
                roomtype = value;
                OnPropertyChanged(nameof(RoomType)); // Notify that there was a change on this property
            }
        }

        public string LongDescription
        {
            get { return longdescription; }
            set
            {
                longdescription = value;
                OnPropertyChanged(nameof(LongDescription)); // Notify that there was a change on this property
            }
        }
        public double DownPayment
        {
            get { return downpayment; }
            set
            {
                downpayment = value;
                OnPropertyChanged(nameof(DownPayment)); // Notify that there was a change on this property
            }
        }
        public double FullPayment
        {
            get { return fullpayment; }
            set
            {
                fullpayment = value;
                OnPropertyChanged(nameof(FullPayment)); // Notify that there was a change on this property
            }
        }


        public BTODetail(string id)
        {
            InitializeComponent();

            BTOId = id;

            Task.Run(async () =>
            {
                BTOAssistDatabase database = await BTOAssistDatabase.Instance;
                List<PhoneInfo> allPhoneInfo = await database.GetAllPhoneInfoAsync();

                foreach (var i in allPhoneInfo)
                {
                    Trace.WriteLine("homePageDeviceID: " + i.deviceID);
                    Trace.WriteLine("homePageAccessToken: " + i.accessToken);
                }
            });

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            PostGre postGre = new PostGre();
            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            BTO BTODetails = postGre.GetBTODetailsAsync(BTOId);

            Id = BTODetails.ID;
            Image = BTODetails.Image;
            Location = BTODetails.Location;
            Block = BTODetails.Block.ToString();
            SQM = BTODetails.SQM.ToString();
            Bus = BTODetails.Bus.ToString();
            Mrt = BTODetails.MRT.ToString();
            Direction = BTODetails.Direction.ToString();
            RoomType = BTODetails.RoomType;
            YearsLeft = BTODetails.YearsLeft;
            Applicants = BTODetails.Applicants;
            LongDescription = BTODetails.LongDescription.ToString();
            DownPayment = BTODetails.DownPayment;
            FullPayment = BTODetails.FullPayment;
            BindingContext = this;
        }

        async void ApplyBTO(object sender, EventArgs args)
        {
            var BTODeets = (Button)sender;
            string id = BTODeets.AutomationId;

            await Navigation.PushAsync(new ApplicationPage1());
            //await Shell.Current.GoToAsync("//ApplicationPage1");

            Trace.WriteLine(id);
        }
    }
}