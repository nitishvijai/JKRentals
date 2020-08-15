using JKRentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JKRentals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage5 : ContentPage
    {
        public ApplicationEntry thisApp;
        EntryPage1 ep1;

        public EntryPage5(EntryPage1 p1)
        {
            InitializeComponent();
            ep1 = p1;
            thisApp = ep1.app;
            ep1.app.CurrentPage = 5;
            thisApp.CurrentPage = 5;
            PopulateFields(thisApp);
        }

        public void PopulateFields(ApplicationEntry app)
        {
            AddlInfo.Text = ep1.app.AddlInfo;
        }

        protected override void OnAppearing()
        {
            ep1.app.CurrentPage = 5;
            thisApp.CurrentPage = 5;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            SaveData();
            base.OnDisappearing();
        }

        public void SaveData()
        {
            ep1.app.AddlInfo = AddlInfo.Text;
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            SaveData();
            await Navigation.PushAsync(new AgreePage(ep1));
        }
    }
}