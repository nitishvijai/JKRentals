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
        }

        protected override void OnDisappearing()
        {
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
    }
}