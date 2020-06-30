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
    public partial class EntryPage1 : ContentPage
    {
        public EntryPage1()
        {
            InitializeComponent();
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumValue.Text = StepperValue.Value.ToString();
        }

        private void ReturnBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}