using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JKRentals.Models;
using System.Reflection;


namespace JKRentals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgreePage : ContentPage
    {
        public ApplicationEntry thisApp;
        EntryPage1 ep1;



        public AgreePage(EntryPage1 p1)
        {
            
            var assembly = typeof(AgreePage).GetTypeInfo().Assembly;
            
            ep1 = p1;
            thisApp = ep1.app;
            ep1.app.CurrentPage = 6;
            thisApp.CurrentPage = 6;
            

            EULA.Text = text;

            NamePlaceHolder.Text = "Applicant: " + thisApp.FirstName + " " + thisApp.LastName;
            DatePlaceHolder.Text = "Today's Date: " + DateTime.Today.ToShortDateString();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ep1.app.CurrentPage = 6;
            thisApp.CurrentPage = 6;
            base.OnAppearing();
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            string confirm = await DisplayActionSheet("Are you ready to submit your application?", "Yes", "No");

            if (confirm == "Yes")
            {
                await Navigation.PushAsync(new ReviewPage(ep1.app));
            }
            else { /* discard action and do nothing */ }
        }
    }
}