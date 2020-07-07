using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using JKRentals.Models;

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
            PopulateTextBoxes(thisApp);
        }


        protected override bool OnBackButtonPressed()
        {
            SaveData();
            Navigation.PopAsync();
            return true;
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
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            string confirm = await DisplayActionSheet("Are you ready to submit your application?", "Yes", "No");

            if (confirm == "Yes")
            {
                // save data and move on
                SaveData();

                await Navigation.PushAsync(new ReviewPage(ep1.app));
            }
            else { /* discard action and do nothing */ }
        }
    }
}