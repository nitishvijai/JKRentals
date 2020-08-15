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
    public partial class EntryPage4 : ContentPage
    {
        ApplicationEntry thisApp;
        EntryPage1 ep1;

        public EntryPage4(EntryPage1 p1)
        {
            InitializeComponent();
            ep1 = p1;
            thisApp = ep1.app;
            ep1.app.CurrentPage = 4;
            thisApp.CurrentPage = 4;
            PopulateFields(thisApp);
        }

        public void PopulateFields(ApplicationEntry _app)
        {
            HouseLife.Text = ep1.app.StayDuration;
            Pets.Text = ep1.app.Pets;

            Evictions.Value = ep1.app.NumEvictions;
            NumEvictions.Text = Evictions.Value.ToString();

            Felonies.Value = ep1.app.NumFelonies;
            NumFelonies.Text = Felonies.Value.ToString();

            BrokeLease.IsToggled = ep1.app.BrokeLease;
            if (BrokeLease.IsToggled)
            {
                YN_BrokeLease.Text = "Yes";
            }
            else
            {
                YN_BrokeLease.Text = "No";
            }

            Smoke.IsToggled = ep1.app.Smoke;
            if (Smoke.IsToggled)
            {
                YN_Smoke.Text = "Yes";
            }
            else
            {
                YN_Smoke.Text = "No";
            }

            CheckAcct.IsToggled = ep1.app.CheckAcct;
            if (CheckAcct.IsToggled)
            {
                YN_CheckAcct.Text = "Yes";
            }
            else
            {
                YN_CheckAcct.Text = "No";
            }

            Vehicles.Value = ep1.app.NumVehicles;
            NumVehicles.Text = Vehicles.Value.ToString();

            AmtReady.IsToggled = ep1.app.AmtReady;
            if (AmtReady.IsToggled)
            {
                YN_AmtReady.Text = "Yes";
            }
            else
            {
                YN_AmtReady.Text = "No";
            }

            LimitRent.Text = ep1.app.LimitRent;
            MoneyValue.Text = ep1.app.MoneyValue;
            EmergencyName.Text = ep1.app.EmergencyName;
            EmergencyPhone.Text = ep1.app.EmergencyPhone;
            HearSource.Text = ep1.app.HearSource;
            WhyRent.Text = ep1.app.WhyRent;
        }

        protected override void OnAppearing()
        {
            ep1.app.CurrentPage = 4;
            thisApp.CurrentPage = 4;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            SaveData();
            base.OnDisappearing();
        }

        private void Evictions_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumEvictions.Text = Evictions.Value.ToString();
        }

        private void Felonies_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumFelonies.Text = Felonies.Value.ToString();
        }

        private void Vehicles_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumVehicles.Text = Vehicles.Value.ToString();
        }

        private void BrokeLease_Toggled(object sender, ToggledEventArgs e)
        {
            if (BrokeLease.IsToggled)
            {
                YN_BrokeLease.Text = "Yes";
            }
            else
            {
                YN_BrokeLease.Text = "No";
            }
        }

        private void Smoke_Toggled(object sender, ToggledEventArgs e)
        {
            if (Smoke.IsToggled)
            {
                YN_Smoke.Text = "Yes";
            }
            else
            {
                YN_Smoke.Text = "No";
            }
        }

        private void CheckAcct_Toggled(object sender, ToggledEventArgs e)
        {
            if (CheckAcct.IsToggled)
            {
                YN_CheckAcct.Text = "Yes";
            }
            else
            {
                YN_CheckAcct.Text = "No";
            }
        }

        private void AmtReady_Toggled(object sender, ToggledEventArgs e)
        {
            if (AmtReady.IsToggled)
            {
                YN_AmtReady.Text = "Yes";
            }
            else
            {
                YN_AmtReady.Text = "No";
            }
        }

        public void SaveData()
        {
            ep1.app.StayDuration = HouseLife.Text;
            ep1.app.Pets = Pets.Text;
            ep1.app.NumEvictions = (int)Evictions.Value;
            ep1.app.NumFelonies = (int)Felonies.Value;
            ep1.app.BrokeLease = BrokeLease.IsToggled;
            ep1.app.Smoke = Smoke.IsToggled;
            ep1.app.CheckAcct = CheckAcct.IsToggled;
            ep1.app.NumVehicles = (int)Vehicles.Value;
            ep1.app.AmtReady = AmtReady.IsToggled;
            ep1.app.LimitRent = LimitRent.Text;
            ep1.app.MoneyValue = MoneyValue.Text;
            ep1.app.EmergencyName = EmergencyName.Text;
            ep1.app.EmergencyPhone = EmergencyPhone.Text;
            ep1.app.HearSource = HearSource.Text;
            ep1.app.WhyRent = WhyRent.Text;
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            SaveData();

            ep1.WriteToTempFile();

            bool allgood = ep1.app.CheckForCompletion();

            if (!allgood)
            {
                await DisplayAlert("Incomplete Form", "You have not entered values for some fields. Please go back and review your answers.", "OK");
                return;
            }

            bool phonegood = ep1.app.IsPhoneNumber(EmergencyPhone.Text);

            if (!phonegood)
            {
                await DisplayAlert("Invalid Format", "A phone number is in an invalid format.", "OK");
                return;
            }

            await Navigation.PushAsync(new EntryPage5(ep1));
        }

        private void EmergencyPhone_Completed(object sender, EventArgs e)
        {
            EmergencyPhone.Text = ep1.app.FormatPhone(EmergencyPhone.Text);
        }
    }
}