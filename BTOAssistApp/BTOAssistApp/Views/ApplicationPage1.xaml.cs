
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Plugin.DeviceInfo;
using BTOAssistApp.Data;
using BTOAssistApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Windows.Input;

namespace BTOAssistApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationPage1 : ContentPage
    { 

        private string devID;
        
        private string accesstoken;
        private string sub;
        private string page2;

        private string page1vis;
        private string page2vis;
        private string page3vis;
        private string page4vis;
        private string page5vis;
        private string applicantname;
        private string applicantnameread;
        private string applicantnric;
        private string applicantnricread;
        private string applicantmobile;
        private string applicantmobileread;
        private string applicantgender;
        private string applicantgenderread;
        private string applicantage;
        private string applicantageread;
        private string applicantmarital;
        private string applicantmaritalread;
        private string applicantcpf;
        private string applicantcpfread;
        private string applicantoccupation;
        private string applicantoccupationread;
        private string applicantcitizenship;
        private string applicantcitizenshipread;
        private string subapplicantname;
        private string subapplicantnameread;
        private string subapplicantnric;
        private string subapplicantnricread;
        private string subapplicantmobile;
        private string subapplicantmobileread;
        private string subapplicantgender;
        private string subapplicantgenderread;
        private string subapplicantage;
        private string subapplicantageread;
        private string subapplicantmarital;
        private string subapplicantmaritalread;
        private string subapplicantcpf;
        private string subapplicantcpfread;
        private string subapplicantoccupation;
        private string subapplicantoccupationread;
        private string subapplicantcitizenship;
        private string subapplicantcitizenshipread;
        private string subapplicantrelationship;
        private string subapplicantrelationshipread;
        private string mothername;
        private string mothernameread;
        private string mothernric;
        private string mothernricread;
        private string mothermobile;
        private string mothermobileread;
        private string motherage;
        private string motherageread;
        private string mothermarital;
        private string mothermaritalread;
        private string mothercpf;
        private string mothercpfread;
        private string motheroccupation;
        private string motheroccupationread;
        private string mothercitizenship;
        private string mothercitizenshipread;
        private string motheraddress;
        private string motheraddressread;
        private string fathername;
        private string fathernameread;
        private string fathernric;
        private string fathernricread;
        private string fathermobile;
        private string fathermobileread;
        private string fatherage;
        private string fatherageread;
        private string fathermarital;
        private string fathermaritalread;
        private string fathercpf;
        private string fathercpfread;
        private string fatheroccupation;
        private string fatheroccupationread;
        private string fathercitizenship;
        private string fathercitizenshipread;
        private string fatheraddress;
        private string fatheraddressread;
        private string webgrid;
        private string source;
        private string webpageid;
        private string enablebtn1;
        private string enablebtn2;
        private string enablebtn3;
        private string enablebtn4;
        private string[] counter;
        public ICommand BackCommand { get; private set; }



        HttpClient client;

        

        public string EnableBtn1
        {
            get { return enablebtn1; }
            set
            {
                enablebtn1 = value;
                OnPropertyChanged(nameof(EnableBtn1)); // Notify that there was a change on this property
            }
        }

        public string EnableBtn2
        {
            get { return enablebtn2; }
            set
            {
                enablebtn2 = value;
                OnPropertyChanged(nameof(EnableBtn2)); // Notify that there was a change on this property
            }
        }

        public string EnableBtn3
        {
            get { return enablebtn3; }
            set
            {
                enablebtn3 = value;
                OnPropertyChanged(nameof(EnableBtn3)); // Notify that there was a change on this property
            }
        }

        public string EnableBtn4
        {
            get { return enablebtn4; }
            set
            {
                enablebtn4 = value;
                OnPropertyChanged(nameof(EnableBtn4)); // Notify that there was a change on this property
            }
        }

        

        public string WebPageID
        {
            get { return webpageid; }
            set
            {
                webpageid = value;
                OnPropertyChanged(nameof(WebPageID)); // Notify that there was a change on this property
            }
        }
        public string Page1Vis
        {
            get { return page1vis; }
            set
            {
                page1vis = value;
                OnPropertyChanged(nameof(Page1Vis)); // Notify that there was a change on this property
            }
        }
        public string Page2Vis
        {
            get { return page2vis; }
            set
            {
                page2vis = value;
                OnPropertyChanged(nameof(Page2Vis)); // Notify that there was a change on this property
            }
        }
        public string Page3Vis
        {
            get { return page3vis; }
            set
            {
                page3vis = value;
                OnPropertyChanged(nameof(Page3Vis)); // Notify that there was a change on this property
            }
        }
        public string Page4Vis
        {
            get { return page4vis; }
            set
            {
                page4vis = value;
                OnPropertyChanged(nameof(Page4Vis)); // Notify that there was a change on this property
            }
        }
        public string Page5Vis
        {
            get { return page5vis; }
            set
            {
                page5vis = value;
                OnPropertyChanged(nameof(Page5Vis)); // Notify that there was a change on this property
            }
        }
        public string Page2
        {
            get { return page2; }
            set
            {
                page2 = value;
                OnPropertyChanged(nameof(Page2)); // Notify that there was a change on this property
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
        public string Sub
        {
            get { return sub; }
            set
            {
                sub = value;
                OnPropertyChanged(nameof(Sub)); // Notify that there was a change on this property
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
        public string WebGrid
        {
            get { return webgrid; }
            set
            {
                webgrid = value;
                OnPropertyChanged(nameof(WebGrid)); // Notify that there was a change on this property
            }
        }
        public string Source
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged(nameof(Source)); // Notify that there was a change on this property
            }
        }
        public string ApplicantName
        {
            get { return applicantname; }
            set
            {
                applicantname = value;
                OnPropertyChanged(nameof(ApplicantName)); // Notify that there was a change on this property
            }
        }
        public string ApplicantNameRead
        {
            get { return applicantnameread; }
            set
            {
                applicantnameread = value;
                OnPropertyChanged(nameof(ApplicantNameRead)); // Notify that there was a change on this property
            }
        }
        
        public string ApplicantNRIC
        {
            get { return applicantnric; }
            set
            {
                applicantnric = value;
                OnPropertyChanged(nameof(ApplicantNRIC)); // Notify that there was a change on this property
            }
        }
        public string ApplicantNRICRead
        {
            get { return applicantnricread; }
            set
            {
                applicantnricread = value;
                OnPropertyChanged(nameof(ApplicantNRICRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantMobile
        {
            get { return applicantmobile; }
            set
            {
                applicantmobile = value;
                OnPropertyChanged(nameof(ApplicantMobile)); // Notify that there was a change on this property
            }
        }
        public string ApplicantMobileRead
        {
            get { return applicantmobileread; }
            set
            {
                applicantmobileread = value;
                OnPropertyChanged(nameof(ApplicantMobileRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantGender
        {
            get { return applicantgender; }
            set
            {
                applicantgender = value;
                OnPropertyChanged(nameof(ApplicantGender)); // Notify that there was a change on this property
            }
        }
        public string ApplicantGenderRead
        {
            get { return applicantgenderread; }
            set
            {
                applicantgenderread = value;
                OnPropertyChanged(nameof(ApplicantGenderRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantAge
        {
            get { return applicantage; }
            set
            {
                applicantage = value;
                OnPropertyChanged(nameof(ApplicantAge)); // Notify that there was a change on this property
            }
        }
        public string ApplicantAgeRead
        {
            get { return applicantageread; }
            set
            {
                applicantageread = value;
                OnPropertyChanged(nameof(ApplicantAgeRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantMarital
        {
            get { return applicantmarital; }
            set
            {
                applicantmarital = value;
                OnPropertyChanged(nameof(ApplicantMarital)); // Notify that there was a change on this property
            }
        }
        public string ApplicantMaritalRead
        {
            get { return applicantmaritalread; }
            set
            {
                applicantmaritalread = value;
                OnPropertyChanged(nameof(ApplicantMaritalRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantCPF
        {
            get { return applicantcpf; }
            set
            {
                applicantcpf = value;
                OnPropertyChanged(nameof(ApplicantCPF)); // Notify that there was a change on this property
            }
        }
        public string ApplicantOccupation
        {
            get { return applicantoccupation; }
            set
            {
                applicantoccupation = value;
                OnPropertyChanged(nameof(ApplicantOccupation)); // Notify that there was a change on this property
            }
        }
        public string ApplicantOccupationRead
        {
            get { return applicantoccupationread; }
            set
            {
                applicantoccupationread = value;
                OnPropertyChanged(nameof(ApplicantOccupationRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantCPFRead
        {
            get { return applicantcpfread; }
            set
            {
                applicantcpfread = value;
                OnPropertyChanged(nameof(ApplicantCPFRead)); // Notify that there was a change on this property
            }
        }
        public string ApplicantCitizenship
        {
            get { return applicantcitizenship; }
            set
            {
                applicantcitizenship = value;
                OnPropertyChanged(nameof(ApplicantCitizenship)); // Notify that there was a change on this property
            }
        }
        public string ApplicantCitizenshipRead
        {
            get { return applicantcitizenshipread; }
            set
            {
                applicantcitizenshipread = value;
                OnPropertyChanged(nameof(ApplicantCitizenshipRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantName
        {
            get { return subapplicantname; }
            set
            {
                subapplicantname = value;
                OnPropertyChanged(nameof(SubApplicantName)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantNameRead
        {
            get { return subapplicantnameread; }
            set
            {
                subapplicantnameread = value;
                OnPropertyChanged(nameof(SubApplicantNameRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantNRIC
        {
            get { return subapplicantnric; }
            set
            {
                subapplicantnric = value;
                OnPropertyChanged(nameof(SubApplicantNRIC)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantNRICRead
        {
            get { return subapplicantnricread; }
            set
            {
                subapplicantnricread = value;
                OnPropertyChanged(nameof(SubApplicantNRICRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantMobile
        {
            get { return subapplicantmobile; }
            set
            {
                subapplicantmobile = value;
                OnPropertyChanged(nameof(SubApplicantMobile)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantMobileRead
        {
            get { return subapplicantmobileread; }
            set
            {
                subapplicantmobileread = value;
                OnPropertyChanged(nameof(SubApplicantMobileRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantGender
        {
            get { return subapplicantgender; }
            set
            {
                subapplicantgender = value;
                OnPropertyChanged(nameof(SubApplicantGender)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantGenderRead
        {
            get { return subapplicantgenderread; }
            set
            {
                subapplicantgenderread = value;
                OnPropertyChanged(nameof(SubApplicantGenderRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantAge
        {
            get { return subapplicantage; }
            set
            {
                subapplicantage = value;
                OnPropertyChanged(nameof(SubApplicantAge)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantAgeRead
        {
            get { return subapplicantageread; }
            set
            {
                subapplicantageread = value;
                OnPropertyChanged(nameof(SubApplicantAgeRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantMarital
        {
            get { return subapplicantmarital; }
            set
            {
                subapplicantmarital = value;
                OnPropertyChanged(nameof(SubApplicantMarital)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantMaritalRead
        {
            get { return subapplicantmaritalread; }
            set
            {
                subapplicantmaritalread = value;
                OnPropertyChanged(nameof(SubApplicantMaritalRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantCPF
        {
            get { return subapplicantcpf; }
            set
            {
                subapplicantcpf = value;
                OnPropertyChanged(nameof(SubApplicantCPF)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantCPFRead
        {
            get { return subapplicantcpfread; }
            set
            {
                subapplicantcpfread = value;
                OnPropertyChanged(nameof(SubApplicantCPFRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantOccupation
        {
            get { return subapplicantoccupation; }
            set
            {
                subapplicantoccupation = value;
                OnPropertyChanged(nameof(SubApplicantOccupation)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantOccupationRead
        {
            get { return subapplicantoccupationread; }
            set
            {
                subapplicantoccupationread = value;
                OnPropertyChanged(nameof(SubApplicantOccupationRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantCitizenship
        {
            get { return subapplicantcitizenship; }
            set
            {
                subapplicantcitizenship = value;
                OnPropertyChanged(nameof(SubApplicantCitizenship)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantCitizenshipRead
        {
            get { return subapplicantcitizenshipread; }
            set
            {
                subapplicantcitizenshipread = value;
                OnPropertyChanged(nameof(SubApplicantCitizenshipRead)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantRelationship
        {
            get { return subapplicantrelationship; }
            set
            {
                subapplicantrelationship = value;
                OnPropertyChanged(nameof(SubApplicantRelationship)); // Notify that there was a change on this property
            }
        }
        public string SubApplicantRelationshipRead
        {
            get { return subapplicantrelationshipread; }
            set
            {
                subapplicantrelationshipread = value;
                OnPropertyChanged(nameof(SubApplicantRelationshipRead)); // Notify that there was a change on this property
            }
        }


        public string MotherName
        {
            get { return mothername; }
            set
            {
                mothername = value;
                OnPropertyChanged(nameof(MotherName)); // Notify that there was a change on this property
            }
        }
        public string MotherNameRead
        {
            get { return mothernameread; }
            set
            {
                mothernameread = value;
                OnPropertyChanged(nameof(MotherNameRead)); // Notify that there was a change on this property
            }
        }
        public string MotherNRIC
        {
            get { return mothernric; }
            set
            {
                mothernric = value;
                OnPropertyChanged(nameof(MotherNRIC)); // Notify that there was a change on this property
            }
        }
        public string MotherNRICRead
        {
            get { return mothernricread; }
            set
            {
                mothernricread = value;
                OnPropertyChanged(nameof(MotherNRICRead)); // Notify that there was a change on this property
            }
        }
        public string MotherMobile
        {
            get { return mothermobile; }
            set
            {
                mothermobile = value;
                OnPropertyChanged(nameof(MotherMobile)); // Notify that there was a change on this property
            }
        }
        public string MotherMobileRead
        {
            get { return mothermobileread; }
            set
            {
                mothermobileread = value;
                OnPropertyChanged(nameof(MotherMobileRead)); // Notify that there was a change on this property
            }
        }
        
        
        public string MotherAge
        {
            get { return motherage; }
            set
            {
                motherage = value;
                OnPropertyChanged(nameof(MotherAge)); // Notify that there was a change on this property
            }
        }
        public string MotherAgeRead
        {
            get { return motherageread; }
            set
            {
                motherageread = value;
                OnPropertyChanged(nameof(MotherAgeRead)); // Notify that there was a change on this property
            }
        }
        public string MotherMarital
        {
            get { return mothermarital; }
            set
            {
                mothermarital = value;
                OnPropertyChanged(nameof(MotherMarital)); // Notify that there was a change on this property
            }
        }
        public string MotherMaritalRead
        {
            get { return mothermaritalread; }
            set
            {
                mothermaritalread = value;
                OnPropertyChanged(nameof(MotherMaritalRead)); // Notify that there was a change on this property
            }
        }
        public string MotherCPF
        {
            get { return mothercpf; }
            set
            {
                mothercpf = value;
                OnPropertyChanged(nameof(MotherCPF)); // Notify that there was a change on this property
            }
        }
        public string MotherCPFRead
        {
            get { return mothercpfread; }
            set
            {
                mothercpfread = value;
                OnPropertyChanged(nameof(MotherCPFRead)); // Notify that there was a change on this property
            }
        }
        public string MotherOccupation
        {
            get { return motheroccupation; }
            set
            {
                motheroccupation = value;
                OnPropertyChanged(nameof(MotherOccupation)); // Notify that there was a change on this property
            }
        }
        public string MotherOccupationRead
        {
            get { return motheroccupationread; }
            set
            {
                motheroccupationread = value;
                OnPropertyChanged(nameof(MotherOccupationRead)); // Notify that there was a change on this property
            }
        }
        public string MotherCitizenship
        {
            get { return mothercitizenship; }
            set
            {
                mothercitizenship = value;
                OnPropertyChanged(nameof(MotherCitizenship)); // Notify that there was a change on this property
            }
        }
        public string MotherCitizenshipRead
        {
            get { return mothercitizenshipread; }
            set
            {
                mothercitizenshipread = value;
                OnPropertyChanged(nameof(MotherCitizenshipRead)); // Notify that there was a change on this property
            }
        }
        public string MotherAddress
        {
            get { return motheraddress; }
            set
            {
                motheraddress = value;
                OnPropertyChanged(nameof(MotherAddress)); // Notify that there was a change on this property
            }
        }
        public string MotherAddressRead
        {
            get { return motheraddressread; }
            set
            {
                motheraddressread = value;
                OnPropertyChanged(nameof(MotherAddressRead)); // Notify that there was a change on this property
            }
        }
        public string FatherName
        {
            get { return fathername; }
            set
            {
                fathername = value;
                OnPropertyChanged(nameof(FatherName)); // Notify that there was a change on this property
            }
        }
        public string FatherNameRead
        {
            get { return fathernameread; }
            set
            {
                fathernameread = value;
                OnPropertyChanged(nameof(FatherNameRead)); // Notify that there was a change on this property
            }
        }
        public string FatherNRIC
        {
            get { return fathernric; }
            set
            {
                fathernric = value;
                OnPropertyChanged(nameof(FatherNRIC)); // Notify that there was a change on this property
            }
        }
        public string FatherNRICRead
        {
            get { return fathernricread; }
            set
            {
                fathernricread = value;
                OnPropertyChanged(nameof(FatherNRICRead)); // Notify that there was a change on this property
            }
        }
        public string FatherMobile
        {
            get { return fathermobile; }
            set
            {
                fathermobile = value;
                OnPropertyChanged(nameof(FatherMobile)); // Notify that there was a change on this property
            }
        }
        public string FatherMobileRead
        {
            get { return fathermobileread; }
            set
            {
                fathermobileread = value;
                OnPropertyChanged(nameof(FatherMobileRead)); // Notify that there was a change on this property
            }
        }

        public string FatherAge
        {
            get { return fatherage; }
            set
            {
                fatherage = value;
                OnPropertyChanged(nameof(FatherAge)); // Notify that there was a change on this property
            }
        }
        public string FatherAgeRead
        {
            get { return fatherageread; }
            set
            {
                fatherageread = value;
                OnPropertyChanged(nameof(FatherAgeRead)); // Notify that there was a change on this property
            }
        }
        public string FatherMarital
        {
            get { return fathermarital; }
            set
            {
                fathermarital = value;
                OnPropertyChanged(nameof(FatherMarital)); // Notify that there was a change on this property
            }
        }
        public string FatherMaritalRead
        {
            get { return fathermaritalread; }
            set
            {
                fathermaritalread = value;
                OnPropertyChanged(nameof(FatherMaritalRead)); // Notify that there was a change on this property
            }
        }
        public string FatherCPF
        {
            get { return fathercpf; }
            set
            {
                fathercpf = value;
                OnPropertyChanged(nameof(FatherCPF)); // Notify that there was a change on this property
            }
        }
        public string FatherCPFRead
        {
            get { return fathercpfread; }
            set
            {
                fathercpfread = value;
                OnPropertyChanged(nameof(FatherCPFRead)); // Notify that there was a change on this property
            }
        }
        public string FatherOccupation
        {
            get { return fatheroccupation; }
            set
            {
                fatheroccupation = value;
                OnPropertyChanged(nameof(FatherOccupation)); // Notify that there was a change on this property
            }
        }
        public string FatherOccupationRead
        {
            get { return fatheroccupationread; }
            set
            {
                fatheroccupationread = value;
                OnPropertyChanged(nameof(FatherOccupationRead)); // Notify that there was a change on this property
            }
        }
        public string FatherCitizenship
        {
            get { return fathercitizenship; }
            set
            {
                fathercitizenship = value;
                OnPropertyChanged(nameof(FatherCitizenship)); // Notify that there was a change on this property
            }
        }
        public string FatherCitizenshipRead
        {
            get { return fathercitizenshipread; }
            set
            {
                fathercitizenshipread = value;
                OnPropertyChanged(nameof(FatherCitizenshipRead)); // Notify that there was a change on this property
            }
        }
        public string FatherAddress
        {
            get { return fatheraddress; }
            set
            {
                fatheraddress = value;
                OnPropertyChanged(nameof(FatherAddress)); // Notify that there was a change on this property
            }
        }
        public string FatherAddressRead
        {
            get { return fatheraddressread; }
            set
            {
                fatheraddressread = value;
                OnPropertyChanged(nameof(FatherAddressRead)); // Notify that there was a change on this property
            }
        }

        public List<string> ApplicantCounter = new List<string>();
        public List<string> SubCounter = new List<string>();
        public List<string> MotherCounter = new List<string>();
        public List<string> FatherCounter = new List<string>();


        /**/
        public ApplicationPage1(string id)
        {
            

        InitializeComponent();

            WebPageID = id;
            

            const string paymentRoute = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/create-checkout-session";
            var paymentValues = new Dictionary<string, string>
                      {
                          { "id", id},

                      };
            var url = new Uri(QueryHelpers.AddQueryString(paymentRoute, paymentValues));
            //var stringContent = new FormUrlEncodedContent(values);
            Source = url.ToString();
            Console.WriteLine(id);
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
            {
                Command = new Command(async () => {

                    Device.BeginInvokeOnMainThread(async () => {
                        var result = await this.DisplayAlert("Alert!", "All data will be lost if you leave this page. Do you really want to exit?", "Yes", "No");
                        if (result) await this.Navigation.PopAsync(); // or anything else
                    });


                })
            });


            var deviceId = CrossDeviceInfo.Current.Id;
            
            Task.Run(async () =>
        {
            //textchanged

            client = new HttpClient();

            
            const string getPerson = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getPersonInfo";
            Console.WriteLine(">>>>>>>>>>>>>>>>>>CrossDeviceInfo.Current.Id.ToString()>>>>>>>>>>>>> "+CrossDeviceInfo.Current.Id.ToString());
            var personValues = new Dictionary<string, string>
                      {
                          { "deviceid", CrossDeviceInfo.Current.Id.ToString()},

                      };
            var newUrl = new Uri(QueryHelpers.AddQueryString(getPerson, personValues));


            HttpResponseMessage personResponse = await client.GetAsync(newUrl);

            string personContent = await personResponse.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(personContent);
            

            var array = data["result"] as JArray;
            
            Console.Write(array[0].GetType());
            foreach(var key in array[0])
            {
                //Console.WriteLine(array[0][key]);
            }
            
            var values = JObject.FromObject(array[0]).ToObject<Dictionary<string, object>>();
            Console.WriteLine(">>>>>>>>>>>Values>>>>>>>>>>>" + values);
        foreach (KeyValuePair<string, Object> entry in values)
        {
            if ((entry.Value == null|| entry.Value.ToString() == "-" ) && entry.Key != "phoneid")
                {
                    
                    
                    if (entry.Key.StartsWith("sub")|| entry.Key == "relationship")
                    {
                        SubCounter.Add(entry.Key.ToString());
                        //EnableBtn2 = "false";
                        BindingContext = this;
                    }
                   else if (entry.Key.StartsWith("mother"))
                    {
                        MotherCounter.Add(entry.Key.ToString());
                        //EnableBtn3 = "false";
                        BindingContext = this;
                    }
                   else if (entry.Key.StartsWith("father"))
                    {
                        FatherCounter.Add(entry.Key.ToString());
                        //EnableBtn4 = "false";
                        BindingContext = this;
                    }
                    
                }
                else
                {
                    try
                    {
                        if (entry.Key != "phoneid")
                        {
                            /*switch (entry.Key.ToString()){
                                case "name":
                                    var test = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>" + entry.Key +":" + test);

                            }*/
                            var personValue = DecryptStringFromBytes_Aes(Convert.FromBase64String(entry.Value.ToString().Split(',')[0]), Convert.FromBase64String(entry.Value.ToString().Split(',')[1]), Convert.FromBase64String(entry.Value.ToString().Split(',')[2]));
                            checker(entry.Key.ToString(), personValue.ToString());



                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            

            for (var i = 0; i < ApplicantCounter.Count; i++)
            {
                Console.WriteLine(ApplicantCounter[i]);
            }
            Console.WriteLine(ApplicantCounter.Count);
            Console.WriteLine("ApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.CountApplicantCounter.Count");



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
                            catch(Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            
                        }
                    }
                }
            }

            return plaintext;
        }

        protected void checker(string key, string value)
        {
            if (value == null || value == "-")
            {
                return;
            }
            else
            {
                switch (key)
                {
                    case "name":
                        ApplicantName = value.ToString();
                        ApplicantNameRead = "true";
                        break;
                    case "uinfin":
                        ApplicantNRIC = value.ToString();
                        ApplicantNRICRead = "true";
                        break;
                    case "mobileno":
                        ApplicantMobile = value.ToString();
                        ApplicantMobileRead = "true";
                        break;
                    case "sex":
                        ApplicantGender = value.ToString();
                        ApplicantGenderRead = "true";
                        break;
                    case "dob":
                        ApplicantAge = value.ToString();
                        ApplicantAgeRead = "true";
                        break;
                    case "marital":
                        ApplicantMarital = value.ToString();
                        ApplicantMaritalRead = "true";
                        break;
                    case "nationality":
                        ApplicantCitizenship = value.ToString();
                        ApplicantCitizenshipRead = "true";
                        break;
                    case "occupation":
                        ApplicantOccupation = value.ToString();
                        ApplicantOccupationRead = "true";
                        break;
                    case "cpfcontributions":
                        ApplicantCPF = "$" + value.ToString();
                        ApplicantCPFRead = "true";
                        break;
                    case "subname":
                        SubApplicantName = value.ToString();
                        break;
                    case "subuinfin":
                        SubApplicantNRIC = value.ToString();
                        break;
                    case "submobileno":
                        SubApplicantMobile = value.ToString();
                        break;
                    case "subsex":
                        SubApplicantGender = value.ToString();
                        break;
                    case "subdob":
                        SubApplicantAge = value.ToString();
                        break;
                    case "submarital":
                        SubApplicantMarital = value.ToString();
                        break;
                    case "subnationality":
                        SubApplicantCitizenship = value.ToString();
                        break;
                    case "suboccupation":
                        SubApplicantOccupation = value.ToString();
                        break;
                    case "subcpfcontributions":
                        SubApplicantCPF = value.ToString();
                        break;
                    case "relationship":
                        SubApplicantRelationship = value.ToString();
                        break;
                    case "mothername":
                        MotherName = value.ToString();
                        break;
                    case "motheruinfin":
                        MotherNRIC = value.ToString();
                        break;
                    case "mothermobileno":
                        MotherMobile = value.ToString();
                        break;
                    case "mothernationality":
                        MotherCitizenship = value.ToString();
                        break;
                    case "motherdob":
                        MotherAge = value.ToString();
                        break;
                    case "mothermarital":
                        MotherMarital = value.ToString();
                        break;
                    case "motheraddress":
                        MotherAddress = value.ToString();
                        break;
                    case "fathername":
                        FatherName = value.ToString();
                        break;
                    case "fatheruinfin":
                        FatherNRIC = value.ToString();
                        break;
                    case "fathermobileno":
                        FatherMobile = value.ToString();
                        break;
                    case "fathernationality":
                        FatherCitizenship = value.ToString();
                        break;
                    case "fatherdob":
                        FatherAge = value.ToString();
                        break;
                    case "fathermarital":
                        FatherMarital = value.ToString();
                        break;
                    case "fatheraddress":
                        FatherAddress = value.ToString();
                        break;
                }
            }
        }


        protected override async void OnAppearing()
        {
            BindingContext = this;

            Page1Vis = "true";
            Page2Vis = "false";
            Page3Vis = "false";
            Page4Vis = "false";
            Page5Vis = "false";
            WebGrid = "false";

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            for(var i = 0; i < ApplicantCounter.Count; i++)
            {
                Console.WriteLine(ApplicantCounter[i]);
            }
            Console.WriteLine(ApplicantCounter);

            switch (entry.AutomationId)
            {
                case "CheckName":
                    if (entry.Text != null || entry.Text != "")
                    {
                        if (ApplicantCounter.Contains("name"))
                        {
                            ApplicantCounter.Remove("name");
                        }
                    }
                    break;
                case "CheckNRIC":
                    if (entry.Text != null || entry.Text != "")
                    {
                        
                    }
                    break;
                case "CheckMobile":
                    if (entry.Text != null || entry.Text != "")
                    {
                        
                    }
                    break;
                case "CheckGender":
                    if (entry.Text != null || entry.Text != "")
                    {
                        
                    }
                    break;
                case "CheckMarital":
                    if (entry.Text != null || entry.Text != "" || entry.Text != "-")
                    {
                        if (ApplicantCounter.Contains("marital"))
                        {
                            ApplicantCounter.Remove("marital");
                        }
                    }
                    break;
                case "CheckCPF":
                    if (entry.Text != null || entry.Text != "")
                    {
                        
                    }
                    break;
                case "CheckOccupation":
                    if (entry.Text != null || entry.Text != "" || entry.Text != "-")
                    {
                        if (ApplicantCounter.Contains("occupation"))
                        {
                            ApplicantCounter.Remove("occupation");
                        }
                    }
                    break;
                case "CheckCitizenship":
                    if (entry.Text != null || entry.Text != "")
                    {
                        
                    }
                    break;
            }
            if (ApplicantCounter.Count == 0)
            {
                EnableBtn1 = "true";
                BindingContext = this;
            }
            Console.WriteLine(entry.Text);

        }


        async private void Button_Clicked(object sender, EventArgs e)
        {
            var BTOPage = (Button)sender;
            int id = Convert.ToInt32(BTOPage.AutomationId);

            switch (id)
            {
                case 1:
                    

                    Page1Vis = "false";
                    Page2Vis = "true";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    WebGrid = "false";
                    break;
                case 2:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "true";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    WebGrid = "false";
                    break;
                case 3:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "true";
                    Page5Vis = "false";
                    WebGrid = "false";
                    break;
                case 4:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    Page5Vis = "true";
                    WebGrid = "false";
                    ApplicantNameLabel.Text = ApplicantName;
                    ApplicantNRICLabel.Text = ApplicantNRIC;
                    ApplicantGenderLabel.Text = ApplicantGender;
                    ApplicantAgeLabel.Text = ApplicantAge;
                    ApplicantMobileLabel.Text = ApplicantMobile;
                    ApplicantMaritalLabel.Text = ApplicantMarital;
                    ApplicantOccupationLabel.Text = ApplicantOccupation;
                    ApplicantCitizenshipLabel.Text = ApplicantCitizenship;
                    ApplicantCPFLabel.Text = ApplicantCPF;
                    SubApplicantNameLabel.Text = SubApplicantName;
                    SubApplicantNRICLabel.Text = SubApplicantNRIC;
                    SubApplicantGenderLabel.Text = SubApplicantGender;
                    SubApplicantAgeLabel.Text = SubApplicantAge;
                    SubApplicantMobileLabel.Text = SubApplicantMobile;
                    SubApplicantMaritalLabel.Text = SubApplicantMarital;
                    SubApplicantOccupationLabel.Text = SubApplicantOccupation;
                    SubApplicantCitizenshipLabel.Text = SubApplicantCitizenship;
                    SubApplicantCPFLabel.Text = SubApplicantCPF;
                    SubApplicantRelationshipLabel.Text = SubApplicantRelationship;
                    MotherNameLabel.Text = MotherName;
                    MotherNRICLabel.Text = MotherNRIC;
                    MotherGenderLabel.Text = "FEMALE ";
                    MotherAgeLabel.Text = MotherAge;
                    MotherMobileLabel.Text = MotherMobile;
                    MotherMaritalLabel.Text = MotherMarital;
                    MotherCitizenshipLabel.Text = MotherCitizenship;
                    MotherAddressLabel.Text = MotherAddress;
                    FatherNameLabel.Text = FatherName;
                    FatherNRICLabel.Text = FatherNRIC;
                    FatherGenderLabel.Text = "MALE ";
                    FatherAgeLabel.Text = FatherAge;
                    FatherMobileLabel.Text = FatherMobile;
                    FatherMaritalLabel.Text = FatherMarital;
                    FatherCitizenshipLabel.Text = FatherCitizenship;
                    FatherAddressLabel.Text = FatherAddress;
                    break;
                case 5:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    await Task.Run(async () =>
                    {
                        var values = new Dictionary<string, string> { };
                        values.Add("deviceid", CrossDeviceInfo.Current.Id.ToString());
                        values.Add("uinfin", ApplicantNRIC.ToString());

                        if (page2store.IsChecked == true)
                        {
                            values.Add("subname", EncryptStringToBytes_Aes(SubApplicantName.ToString()));
                            values.Add("subuinfin", EncryptStringToBytes_Aes(SubApplicantNRIC.ToString()));
                            values.Add("subsex", EncryptStringToBytes_Aes(SubApplicantGender.ToString()));
                            values.Add("subdob", EncryptStringToBytes_Aes(SubApplicantAge.ToString()));
                            values.Add("submobileno", EncryptStringToBytes_Aes(SubApplicantMobile.ToString()));
                            values.Add("submarital", EncryptStringToBytes_Aes(SubApplicantMarital.ToString()));
                            values.Add("suboccupation", EncryptStringToBytes_Aes(SubApplicantOccupation.ToString()));
                            values.Add("subnationality", EncryptStringToBytes_Aes(SubApplicantCitizenship.ToString()));
                            values.Add("subcpfcontributions", EncryptStringToBytes_Aes(SubApplicantCPF.ToString()));
                            values.Add("relationship", EncryptStringToBytes_Aes(SubApplicantRelationship.ToString()));
                        }
                        if (page3store.IsChecked == true)
                        {
                            values.Add("mothername", EncryptStringToBytes_Aes(MotherName.ToString()));
                            values.Add("motheruinfin ", EncryptStringToBytes_Aes(MotherNRIC.ToString()));
                            values.Add("motherdob", EncryptStringToBytes_Aes(MotherAge.ToString()));
                            values.Add("mothermobileno", EncryptStringToBytes_Aes(MotherMobile.ToString()));
                            values.Add("mothermarital ", EncryptStringToBytes_Aes(MotherMarital.ToString()));
                            values.Add("mothernationality", EncryptStringToBytes_Aes(MotherCitizenship.ToString()));
                            values.Add("motheraddress", EncryptStringToBytes_Aes(MotherAddress.ToString()));
                        }
                        if (page4store.IsChecked == true)
                        {
                            values.Add("fathername", EncryptStringToBytes_Aes(FatherName.ToString()));
                            values.Add("fatheruinfin ", EncryptStringToBytes_Aes(FatherNRIC.ToString()));
                            values.Add("fatherdob", EncryptStringToBytes_Aes(FatherAge.ToString()));
                            values.Add("fathermobileno", EncryptStringToBytes_Aes(FatherMobile.ToString()));
                            values.Add("fathermarital ", EncryptStringToBytes_Aes(FatherMarital.ToString()));
                            values.Add("fathernationality", EncryptStringToBytes_Aes(FatherCitizenship.ToString()));
                            values.Add("fatheraddress", EncryptStringToBytes_Aes(FatherAddress.ToString()));
                        }

                        client = new HttpClient();
                        Uri saveParticularInfo = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/saveApplicantFamilyParticulars");
                        var saveParticularInfoStringContent = new FormUrlEncodedContent(values);
                        HttpResponseMessage insertIntoPersonTable = await client.PostAsync(saveParticularInfo, saveParticularInfoStringContent);
                        var personInfoResponseString = await insertIntoPersonTable.Content.ReadAsStringAsync();
                    });

                    WebGrid = "true";
                    
                    break;
            }
        }
        static string EncryptStringToBytes_Aes(string plainText)
        {
            // Check arguments.
            if (plainText == "")
            {
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
                IV = aesAlg.IV;
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


        async void paymentNavigating(object sender, WebNavigatedEventArgs e)
        {
            var paymentURL = e.Url;

            if (e.Url.Contains("success.html"))
            {
                
                WebGrid = "false";
                client = new HttpClient();
                Task.Run(async () =>
                {

                    //string code = "code";
                    const string appliedBTO = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/insertAppliedBTOinfo";
                    var appliedBTOValues = new Dictionary<string, string>
                      {
                        {"deviceid", CrossDeviceInfo.Current.Id.ToString() },
                        { "uinfin", EncryptStringToBytes_Aes(ApplicantNRIC.ToString()) },
                    { "btoid", EncryptStringToBytes_Aes(WebPageID.ToString())}

                      };
                    var appliedBTOContent = new FormUrlEncodedContent(appliedBTOValues);
                    HttpResponseMessage insertIntoAppliedBTO = await client.PostAsync(appliedBTO, appliedBTOContent);
                    var appliedBTOResponseString = await insertIntoAppliedBTO.Content.ReadAsStringAsync();
                });

                await Navigation.PopToRootAsync();
                Shell.Current.GoToAsync("//BTOProcessPage");


            }
            else if (e.Url.Contains("cancel.html"))
            {
                WebGrid = "false";
                Page5Vis = "true";
                const string paymentRoute = "https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/create-checkout-session";
                var paymentValues = new Dictionary<string, string>
                      {
                          { "id", WebPageID.ToString()},

                      };
                var url = new Uri(QueryHelpers.AddQueryString(paymentRoute, paymentValues));
                //var stringContent = new FormUrlEncodedContent(values);
                Source = url.ToString();
            }

        }
            private void Button_Clicked2(object sender, EventArgs e)
        {
            var BTOPage = (Button)sender;
            int id = Convert.ToInt32(BTOPage.AutomationId);

            switch (id)
            {
                case 1:
                    Page1Vis = "true";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    

                    break;
                case 2:
                    Page1Vis = "false";
                    Page2Vis = "true";
                    Page3Vis = "false";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    
                    break;
                case 3:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "true";
                    Page4Vis = "false";
                    Page5Vis = "false";
                    break;
                case 4:
                    Page1Vis = "false";
                    Page2Vis = "false";
                    Page3Vis = "false";
                    Page4Vis = "true";
                    Page5Vis = "false";
                    break;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert!", "All data will be lost if you leave this page. Do you really want to exit?", "Yes", "No");
                if (result) await this.Navigation.PopAsync(); // or anything else
            });
            
            return true;
        }

        private void SingpassRedirect_Navigating(object sender, WebNavigatingEventArgs e)
        {

        }
    }

    
}