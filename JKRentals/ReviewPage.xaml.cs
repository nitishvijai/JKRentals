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
    public partial class ReviewPage : ContentPage
    {
        ApplicationEntry _app;
        public ReviewPage(ApplicationEntry e)
        {
            InitializeComponent();
            _app = e;
            BindingContext = _app = e;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string submit = await DisplayActionSheet("Are you ready to submit your application?", "OK", "Cancel");

            if (submit == "Yes")
            {
                // mail current application file to rep
            }
            else { /* discard action and do nothing */ }
        }
    }
}