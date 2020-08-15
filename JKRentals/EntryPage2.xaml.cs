using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JKRentals.Models;
using Android.Views;

namespace JKRentals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage2 : ContentPage
    {
        ApplicationEntry thisApp;
        EntryPage1 ep1;

        public EntryPage2(EntryPage1 p1)
        {
            InitializeComponent();
            ep1 = p1;
            thisApp = ep1.app;
            ep1.app.CurrentPage = 2;
            thisApp.CurrentPage = 2;
            DesiredDate.SetValue(DatePicker.MinimumDateProperty, DateTime.Now);

            PopulateTextBoxes(thisApp);
        }

        protected override void OnAppearing()
        {
            ep1.app.CurrentPage = 2;
            thisApp.CurrentPage = 2;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            SaveData();
            base.OnDisappearing();
        }

        public void PopulateTextBoxes(ApplicationEntry _app)
        {
            DesiredAddress.Text = _app.DesiredAddress;
            DesiredDate.Date = _app.MoveDate;
            CurrentAddress.Text = _app.CurrentAddress;
            CSZ.Text = _app.CurrentCSZ;
            MoveDate.Date = _app.CurrMoveDate;
            LandlordName.Text = _app.CurrLandlordName;
            LandlordPhone.Text = _app.CurrLandlordPhone;
            Rent.Text = _app.CurrRent.ToString();
            MoveReason.Text = _app.CurrMoveReason;

            PrevAddress1.Text = _app.PrevAddress1;
            CSZ1.Text = _app.PrevCSZ1;
            MoveDate1.Date = _app.PrevMoveDate1;
            MoveOutDate1.Date = _app.PrevMoveOutDate1;
            LandlordName1.Text = _app.PrevLandlordName1;
            LandlordPhone1.Text = _app.PrevLandlordPhone1;
            Rent1.Text = _app.PrevRent1.ToString();

            PrevAddress2.Text = _app.PrevAddress2;
            CSZ2.Text = _app.PrevCSZ2;
            MoveDate2.Date = _app.PrevMoveDate2;
            MoveOutDate2.Date = _app.PrevMoveOutDate2;
            LandlordName2.Text = _app.PrevLandlordName2;
            LandlordPhone2.Text = _app.PrevLandlordPhone2;
            Rent2.Text = _app.PrevRent2.ToString();
        }

        public void SaveData()
        {
            // save fields into ApplicationEntry
            ep1.app.DesiredAddress = DesiredAddress.Text;
            ep1.app.MoveDate = DesiredDate.Date;

            ep1.app.CurrentAddress = CurrentAddress.Text;
            ep1.app.CurrentCSZ = CSZ.Text;
            ep1.app.CurrMoveDate = MoveDate.Date;
            ep1.app.CurrLandlordName = LandlordName.Text;
            ep1.app.CurrLandlordPhone = LandlordPhone.Text;

            // perform rent number validation check here
            ep1.app.CurrRent = Int32.Parse(Rent.Text);
            ep1.app.CurrMoveReason = MoveReason.Text;

            ep1.app.PrevAddress1 = PrevAddress1.Text;
            ep1.app.PrevCSZ1 = CSZ1.Text;
            ep1.app.PrevMoveDate1 = MoveDate1.Date;
            ep1.app.PrevMoveOutDate1 = MoveOutDate1.Date;
            ep1.app.PrevLandlordName1 = LandlordName1.Text;
            ep1.app.PrevLandlordPhone1 = LandlordPhone1.Text;
            ep1.app.PrevRent1 = Int32.Parse(Rent1.Text);

            ep1.app.PrevAddress2 = PrevAddress2.Text;
            ep1.app.PrevCSZ2 = CSZ2.Text;
            ep1.app.PrevMoveDate2 = MoveDate2.Date;
            ep1.app.PrevMoveOutDate2 = MoveOutDate2.Date;
            ep1.app.PrevLandlordName2 = LandlordName2.Text;
            ep1.app.PrevLandlordPhone2 = LandlordPhone2.Text;
            ep1.app.PrevRent2 = Int32.Parse(Rent2.Text);
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            // save data and move on
            SaveData();
            ep1.WriteToTempFile();

            bool allgood = ep1.app.CheckForCompletion();

            if (!allgood)
            {
                await DisplayAlert("Incomplete Form", "You have not entered values for some fields. Please go back and review your answers.", "OK");
                return;
            }

            if ((MoveDate1.Date > MoveOutDate1.Date) || (MoveDate2.Date > MoveOutDate2.Date))
            {
                await DisplayAlert("Incorrect Date", "Your move-out date cannot occur before your move-in date. Please go back and correct the date.", "OK");
                return;
            }

            bool phone1, phone2, phone3;

            phone1 = phone2 = phone3 = true;

            if (!string.IsNullOrWhiteSpace(LandlordPhone.Text))
            {
                phone1 = ep1.app.IsPhoneNumber(ep1.app.CurrLandlordPhone);
            }
            if (!string.IsNullOrWhiteSpace(LandlordPhone1.Text))
            {
                phone2 = ep1.app.IsPhoneNumber(ep1.app.PrevLandlordPhone1);
            }
            if (!string.IsNullOrWhiteSpace(LandlordPhone2.Text))
            {
                phone3 = ep1.app.IsPhoneNumber(ep1.app.PrevLandlordPhone2);
            }

            if (!phone1 || !phone2 || !phone3)
            {
                await DisplayAlert("Invalid Format", "One or more of your phone numbers are in an invalid format.", "OK");
                return;
            }

            await Navigation.PushAsync(new EntryPage3(ep1));
        }

        private void LandlordPhone_Completed(object sender, EventArgs e)
        {
            LandlordPhone.Text = ep1.app.FormatPhone(LandlordPhone.Text);
        }

        private void LandlordPhone1_Completed(object sender, EventArgs e)
        {
            LandlordPhone1.Text = ep1.app.FormatPhone(LandlordPhone1.Text);
        }

        private void LandlordPhone2_Completed(object sender, EventArgs e)
        {
            LandlordPhone2.Text = ep1.app.FormatPhone(LandlordPhone2.Text);
        }
    }
}