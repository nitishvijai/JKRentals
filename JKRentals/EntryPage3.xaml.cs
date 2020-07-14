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
    public partial class EntryPage3 : ContentPage
    {
        ApplicationEntry thisApp;
        EntryPage1 ep1;

        public EntryPage3(EntryPage1 p1)
        {
            InitializeComponent();
            ep1 = p1;
            thisApp = ep1.app;
            ep1.app.CurrentPage = 3;
            thisApp.CurrentPage = 3;
            PopulateTextBoxes(thisApp);
        }

        protected override void OnDisappearing()
        {
            SaveData();
            base.OnDisappearing();
        }

        public void PopulateTextBoxes(ApplicationEntry _app)
        {
            Employer.Text = _app.CurrentEmployer;
            EmpPhoneNo.Text = _app.EmpPhoneNo;
            GrossWage.Text = _app.GrossWages.ToString();
            ManagerName.Text = _app.ManagerName;
            HireDate.Date = _app.HireDate;
            IncomeSrc.Text = _app.IncomeSrcs;
            Explanation.Text = _app.Explanation;
        }

        public void SaveData()
        {
            ep1.app.CurrentEmployer = Employer.Text;
            ep1.app.EmpPhoneNo = EmpPhoneNo.Text;
            ep1.app.GrossWages = Int32.Parse(GrossWage.Text);
            ep1.app.ManagerName = ManagerName.Text;
            ep1.app.HireDate = HireDate.Date;
            ep1.app.IncomeSrcs = IncomeSrc.Text;
            ep1.app.Explanation = Explanation.Text;
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            // write data to file
            SaveData();
            ep1.WriteToTempFile();

            bool allgood = ep1.app.CheckForCompletion();

            if (!allgood)
            {
                await DisplayAlert("Incomplete Form", "You have not entered values for some fields. Please go back and review your answers.", "OK");
                return;
            }

            bool phonegood = ep1.app.IsPhoneNumber(EmpPhoneNo.Text);

            if (!phonegood)
            {
                await DisplayAlert("Invalid Format", "A phone number is in an invalid format.", "OK");
                return;
            }

            await Navigation.PushAsync(new EntryPage4(ep1));

        }

        private void ReturnBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}