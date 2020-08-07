using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JKRentals.Models;
using System.IO;

namespace JKRentals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage1 : ContentPage
    {
        public ApplicationEntry app = new ApplicationEntry();

        string fileName;

        public EntryPage1(bool newApp, string filename)
        {
            InitializeComponent();
            app.CurrentPage = 1;
            if (newApp)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tempJKR_" + timestamp + "_app.txt");
            }
            else
            {
                // read from the application filename here

            }
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Are you sure you want to discard this application?", "Any unsaved data will be lost.", "Yes", "No");
                if (result) await this.Navigation.PopAsync(); // or anything else
            });

            return true;
        }

        protected override void OnAppearing()
        {
            app.CurrentPage = 1;
            base.OnAppearing();
        }


        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumValue.Text = StepperValue.Value.ToString();
        }

        private async void ReturnBtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Test Stub", "This feature has not been implemented yet. However, you would go back to the previous screen.", "OK");

            // save current application to text file
            SaveData();
            WriteToTempFile();

            // return to previous page
            await Navigation.PopAsync();
        }

        public void SaveData()
        {
            // save fields into ApplicationEntry
            app.FirstName = FName.Text;
            app.MiddleInitial = MI.Text;
            app.LastName = LName.Text;

            app.SocSecNo = SSN.Text;
            app.DOB = DOB.Date;

            app.DriverLicenseNo = DLN.Text;

            app.PhoneNo = PhoneNo.Text;
            app.AltPhoneNo = AltPhoneNo.Text;

            app.Email = Email.Text;

            app.NoInhabitants = Convert.ToInt32(NumValue.Text);

            app.AppFilename = fileName;
        }

        public void WriteToTempFile()
        {
            using (var writer = new StreamWriter(File.Create(fileName)))
            {
                writer.WriteLine(app.FirstName + '\n' +
                                        app.MiddleInitial + '\n' +
                                        app.LastName + '\n' +
                                        app.SocSecNo + '\n' +
                                        app.DOB + '\n' +
                                        app.DriverLicenseNo + '\n' +
                                        app.PhoneNo + '\n' +
                                        app.AltPhoneNo + '\n' +
                                        app.Email + '\n' +
                                        app.NoInhabitants + '\n' +
                                        app.DesiredAddress + '\n' +
                                        app.MoveDate + '\n' +
                                        app.CurrentAddress + '\n' +
                                        app.CurrentCSZ + '\n' +
                                        app.CurrMoveDate + '\n' +
                                        app.CurrLandlordName + '\n' +
                                        app.CurrLandlordPhone + '\n' +
                                        app.CurrRent + '\n' +
                                        app.CurrMoveReason + '\n' +
                                        app.PrevAddress1 + '\n' +
                                        app.PrevCSZ1 + '\n' +
                                        app.PrevMoveDate1 + '\n' +
                                        app.PrevMoveOutDate1 + '\n' +
                                        app.PrevLandlordName1 + '\n' +
                                        app.PrevLandlordPhone1 + '\n' +
                                        app.PrevRent1 + '\n' +
                                        app.PrevAddress2 + '\n' +
                                        app.PrevCSZ2 + '\n' +
                                        app.PrevMoveDate2 + '\n' +
                                        app.PrevMoveOutDate2 + '\n' +
                                        app.PrevLandlordName2 + '\n' +
                                        app.PrevLandlordPhone2 + '\n' +
                                        app.PrevRent2 + '\n' +
                                        app.CurrentEmployer + '\n' +
                                        app.EmpPhoneNo + '\n' +
                                        app.GrossWages + '\n' +
                                        app.ManagerName + '\n' +
                                        app.HireDate + '\n' +
                                        app.IncomeSrcs + '\n' +
                                        app.Explanation + '\n' +
                                        app.StayDuration + '\n' +
                                        app.Pets + '\n' +
                                        app.NumEvictions + '\n' +
                                        app.NumFelonies + '\n' +
                                        app.BrokeLease + '\n' +
                                        app.Smoke + '\n' +
                                        app.CheckAcct + '\n' +
                                        app.NumVehicles + '\n' +
                                        app.AmtReady + '\n' +
                                        app.LimitRent + '\n' +
                                        app.MoneyValue + '\n' +
                                        app.EmergencyName + '\n' +
                                        app.EmergencyPhone + '\n' +
                                        app.HearSource + '\n' +
                                        app.WhyRent + '\n' +
                                        app.AddlInfo);
            }
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
                // save data and move on
                SaveData();
                WriteToTempFile();

                bool allgood = app.CheckForCompletion();

                if (!allgood)
                {
                    await DisplayAlert("Incomplete Form", "You have not entered values for some fields. Please go back and review your answers.", "OK");
                    return;
                }

                bool socgood = app.IsSocialSecurityNumber(app.SocSecNo);
                bool phonegood = app.IsPhoneNumber(app.PhoneNo);
                bool emailgood = app.IsEmail(app.Email);
                
                if (!socgood)
                {
                    await DisplayAlert("Invalid Format", "Your Social Security Number is in an invalid format. Don't forget to add the dashes between the numbers.", "OK");
                    return;
                }

                if (!phonegood)
                {
                    await DisplayAlert("Invalid Format", "One or more of your phone numbers are in an invalid format.", "OK");
                    return;
                }

                if (!emailgood)
                {
                    await DisplayAlert("Invalid Format", "Your email address is in an invalid format.", "OK");
                    return;
                }

                await Navigation.PushAsync(new EntryPage2(this));
            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Are you sure you want to discard this application?", "Any unsaved data will be lost.", "Yes", "No");
                if (result) await this.Navigation.PopAsync(); // or anything else
            });
        }
    }
}