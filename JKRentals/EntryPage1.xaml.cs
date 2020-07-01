using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JKRentals.Models;


namespace JKRentals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage1 : ContentPage
    {
        ApplicationEntry app = new ApplicationEntry();

        public EntryPage1()
        {
            InitializeComponent();
        }


        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumValue.Text = StepperValue.Value.ToString();
        }

        private async void ReturnBtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Test Stub", "This feature has not been implemented yet. However, you would go back to the previous screen.", "OK");

            // save current application to text file

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
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            string confirm = await DisplayActionSheet("Are you ready to submit?", "Yes", "No");

            if (confirm == "Yes")
            {
                // save data and move on
                SaveData();
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

                await Navigation.PushAsync(new ReviewPage { 
                    BindingContext = app
                });
            }
            else {  /* discard action and do nothing */  }
        }
    }
}