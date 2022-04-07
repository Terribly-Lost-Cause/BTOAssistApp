using BTOAssistApp.Data;
using BTOAssistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBTO : ContentPage
    {
        private string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID)); // Notify that there was a change on this property
            }
        }

        public AddBTO()
        {
            
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            Guid myuuid = Guid.NewGuid();
            var uuid = myuuid.ToString();
            var newBto = new BTO();
            newBto.ID = uuid;
            newBto.Image = "https://resources.homes.hdb.gov.sg/nf/2022-03/obf/tg_d1c1/carousel/01_perspective_tg_d1c1.jpg";
            newBto.Location = "Punggol";
            newBto.Block = "120";//<make diff
            newBto.YearsLeft = 5;
            newBto.MinLeaseLeft = 15;
            newBto.MaxLeaseLeft = 99;
            newBto.StartDate = new DateTime(2022, 3, 26, 9, 00, 00);
            newBto.EndDate = new DateTime(2023, 3, 26, 22, 00, 00);
            newBto.Applicants = 100;
            newBto.RoomType = 3;
            newBto.NumberofUnits = 1000;
            newBto.SQM = 80;
            newBto.Bus = "83, 82, 3, 136";
            newBto.MRT = "Punggol";
            newBto.Direction = "5 Mins Walk To Waterway Point";
            newBto.LongDescription = "The perfect location for first time home buyers. Located near the Waterway Point Mall and Punggol MRT. There is also a bus stop nearby. ";
            newBto.DownPayment = 34800;
            newBto.FullPayment = 348000;

            BTOAssistDatabase database = await BTOAssistDatabase.Instance;
            await database.AddBTOAsync(newBto);
        }
    }
}