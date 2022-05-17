using BTOAssistApp.Data;
using BTOAssistApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace BTOAssistApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string devID;
        private string id;
        private string accesstoken;
        private string warningmsg;
        private string firstcarosellvisibility;
        private string secondcarosellvisibility;
        private string pickerresultvisibility;
        private ObservableCollection<BTO> allbto;
        public ObservableCollection<BTO> btosorted;
        public ObservableCollection<BTO> btofilter;
        private ObservableCollection<string> locationitems;
        private ObservableCollection<string> roomtypeitems;

    HttpClient client = new HttpClient();

        public string FirstCarouselVisibility
        {
            get { return firstcarosellvisibility; }
            set
            {
                firstcarosellvisibility = value;
                OnPropertyChanged(nameof(FirstCarouselVisibility)); // Notify that there was a change on this property
            }
        }

        public string SecondCarouselVisibility
        {
            get { return secondcarosellvisibility; }
            set
            {
                secondcarosellvisibility = value;
                OnPropertyChanged(nameof(SecondCarouselVisibility)); // Notify that there was a change on this property
            }
        }

        public string PickerResultVisibility
        {
            get { return pickerresultvisibility; }
            set
            {
                pickerresultvisibility = value;
                OnPropertyChanged(nameof(PickerResultVisibility)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<string> LocationItems
        {
            get { return locationitems; }
            set
            {
                locationitems = value;
                OnPropertyChanged(nameof(LocationItems)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<string> RoomtypeItems
        {
            get { return roomtypeitems; }
            set
            {
                roomtypeitems = value;
                OnPropertyChanged(nameof(RoomtypeItems)); // Notify that there was a change on this property
            }
        }

        public string WarningMsg
        {
            get { return warningmsg; }
            set
            {
                warningmsg = value;
                OnPropertyChanged(nameof(WarningMsg)); // Notify that there was a change on this property
            }
        }
        public string DevID
        {
            get { return devID; }
            set
            {
                devID = value;
                OnPropertyChanged(nameof(DevID)); // Notify that there was a change on this property
            }
        }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id)); // Notify that there was a change on this property
            }
        }
        public string AccessToken
        {
            get { return accesstoken; }
            set
            {
                accesstoken = value;
                OnPropertyChanged(nameof(AccessToken)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<BTO> AllBTO
        {
            get { return allbto; }
            set
            {
                allbto = value;
                OnPropertyChanged(nameof(AllBTO)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<BTO> BTOSorted
        {
            get { return btosorted; }
            set
            {
                btosorted = value;
                OnPropertyChanged(nameof(BTOSorted)); // Notify that there was a change on this property
            }
        }

        public ObservableCollection<BTO> BTOFilter
        {
            get { return btofilter; }
            set
            {
                btofilter = value;
                OnPropertyChanged(nameof(BTOFilter)); // Notify that there was a change on this property
            }
        }
        public HomePage()
        {
            base.OnAppearing();
            WarningMsg = "False";
            
            GetLink();
            CallGetBTOLink();
            InitializeComponent();
            BindingContext = this;
        }



        protected override async void OnAppearing()
        {
            FirstCarouselVisibility = "true";
            SecondCarouselVisibility = "true";
            PickerResultVisibility = "false";
            WarningMsg = "False";
            GetPickerItems();
            GetLink();


        }
        protected async void GetLink()
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

                var results1 = btodata["resultForBTOStatusPage"];
                var results = btodata["resultForCPFPage"];


                /*if (results1.ToString() == "0")
                {
                    WarningMsg = "False";
                }
                else
                {
                    WarningMsg = "True";
                }*/
            });
        }
        protected async void CallGetBTOLink()
        {
            Uri getBTOInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getBTOInfo");
            HttpResponseMessage response = await client.GetAsync(getBTOInfo);
            string content = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(content);
            var array = data["result"] as JArray;
            var sortarray = data["sorted"] as JArray;
            ObservableCollection<BTO> TempAllBTO = new ObservableCollection<BTO>();
            ObservableCollection<BTO> TempBTOSorted = new ObservableCollection<BTO>();



            foreach (var house in array)
            {
                var classhouse = house.ToObject<BTO>();
                classhouse.Block = "Block " + classhouse.Block;
                TempAllBTO.Add(classhouse);
            }
            foreach (var sorthouse in sortarray)
            {
                var sortclasshouse = sorthouse.ToObject<BTO>();
                sortclasshouse.Block = "Block " + sortclasshouse.Block;
                TempBTOSorted.Add(sortclasshouse);



            }
            AllBTO = TempAllBTO;
            BTOSorted = TempBTOSorted;
            Console.WriteLine(TempBTOSorted.Count());
            BindingContext = this;

        }

        protected async void GetPickerItems()
        {
            Uri getBTOInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/pickerItems");
            HttpResponseMessage response = await client.GetAsync(getBTOInfo);
            string content = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(content);
            var locations = data["location"];
            var rooms = data["room"];
            ObservableCollection<string> TempLocation = new ObservableCollection<string>();
            ObservableCollection<string> TempRoom = new ObservableCollection<string>();
            if(LocationItems == null){
                Console.WriteLine("It's null");
            }
            TempLocation.Add("All Locations");
            TempRoom.Add("All Room Types");

            foreach (var x in locations)
            {
                /*string name = x.Key;
                JToken value = x.Value;*/
                Console.WriteLine(x["location"].ToString());
                TempLocation.Add( x["location"].ToString());
                
                //
            }

            foreach (var x in rooms)
            {
                /*string name = x.Key;
                JToken value = x.Value;*/
                TempRoom.Add(x["roomtype"].ToString() + " Rooms");
            }

            LocationItems = TempLocation;
            RoomtypeItems = TempRoom;
            BindingContext = this;


        }

        async void ViewDetail(object sender, EventArgs args)
        {
            var BTO = (Frame)sender;
            string id = BTO.AutomationId;
            await Navigation.PushAsync(new BTODetail(id));
        }
        //getLocationPickerItems

        
        public async void Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedIndex = "";
            //picker.SelectedItem = null;
            Console.WriteLine("Location: "+Location.SelectedIndex);
            Console.WriteLine("Picker: "+picker.SelectedIndex.ToString());
            if (picker.SelectedIndex == -1)
            {

                return;
            }
            else { 
                selectedIndex = picker.SelectedItem.ToString();
                const string filterLocation = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getLocationPickerItems";
                Uri newGetUrl;

                if (selectedIndex != "All Locations")
                {
                    var selectedLocation = new Dictionary<string, string>
                      {
                          { "location", selectedIndex.ToString()}

                      };
                    newGetUrl = new Uri(QueryHelpers.AddQueryString(filterLocation, selectedLocation));
                }
                else
                {
                    Console.WriteLine("It's everything");
                    var selectedLocation = new Dictionary<string, string>
                      {
                          { "location", "everything"}

                      };
                    newGetUrl = new Uri(QueryHelpers.AddQueryString(filterLocation, selectedLocation));
                }


                    HttpResponseMessage response = await client.GetAsync(newGetUrl);
                    string content = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(content);
                    var filterarray = data["getLocation"] as JArray;
                    ObservableCollection<BTO> TempBTOFilter = new ObservableCollection<BTO>();

                    foreach (var filterhouse in filterarray)
                    {
                        var filterclasshouse = filterhouse.ToObject<BTO>();
                        filterclasshouse.Block = "Block " + filterclasshouse.Block;
                        TempBTOFilter.Add(filterclasshouse);



                    }
                    BTOFilter = TempBTOFilter;
                    Console.WriteLine(TempBTOFilter.Count());
                    BindingContext = this;


                    FirstCarouselVisibility = "false";
                    SecondCarouselVisibility = "false";
                    PickerResultVisibility = "true";
                    BindingContext = this;
            }
        }

        private async void Room_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedIndex = "";
            //picker.SelectedItem = null;
            Console.WriteLine("Location: " + Location.SelectedIndex);
            Console.WriteLine("Picker: " + picker.SelectedIndex.ToString());
            if (picker.SelectedIndex == -1)
            {

                return;
            }
            else
            {
                selectedIndex = picker.SelectedItem.ToString();
                const string filterLocation = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getRoomPickerItems";
                Uri newGetUrl;

                if (selectedIndex != "All Room Types")
                {
                    var noOfRooms = selectedIndex.ToString().Substring(0, 1);
                    Console.WriteLine(noOfRooms.ToString());
                    var selectedLocation = new Dictionary<string, string>
                      {
                          { "room", noOfRooms.ToString()}

                      };
                    newGetUrl = new Uri(QueryHelpers.AddQueryString(filterLocation, selectedLocation));
                }
                else
                {
                    var selectedLocation = new Dictionary<string, string>
                      {
                          { "room", "everything"}

                      };
                    newGetUrl = new Uri(QueryHelpers.AddQueryString(filterLocation, selectedLocation));
                }


                    HttpResponseMessage response = await client.GetAsync(newGetUrl);
                    string content = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(content);
                    var filterarray = data["getRooms"] as JArray;
                    ObservableCollection<BTO> TempBTOFilter = new ObservableCollection<BTO>();

                    foreach (var filterhouse in filterarray)
                    {
                        var filterclasshouse = filterhouse.ToObject<BTO>();
                        filterclasshouse.Block = "Block " + filterclasshouse.Block;
                        TempBTOFilter.Add(filterclasshouse);



                    }
                    BTOFilter = TempBTOFilter;
                    Console.WriteLine(TempBTOFilter.Count());
                    BindingContext = this;


                    FirstCarouselVisibility = "false";
                    SecondCarouselVisibility = "false";
                    PickerResultVisibility = "true";
                    BindingContext = this;
            }
        }
    }
}