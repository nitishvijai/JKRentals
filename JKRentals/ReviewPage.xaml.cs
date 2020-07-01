using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using JKRentals.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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
            string submit = await DisplayActionSheet("Are you ready to submit your application?", "Yes", "No");

            if (submit == "Yes")
            {
                // mail current application file to rep
                SendMail();

                await DisplayAlert("Thank You", "Thank you for submitting your application. A representative will get back to you within two weeks regarding your application. Once again, thank you for considering JK Rentals in your search for a new home.", "OK");

                await Navigation.PopToRootAsync();
            }
            else { /* discard action and do nothing */ }
        }

        public void SendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient server = new SmtpClient("smtp.gmail.com");
                Attachment file = new Attachment(_app.AppFilename);

                mail.From = new MailAddress("OBFUSCATED");
                mail.To.Add("OBFUSCATED");
                mail.Subject = "JK Rentals Automated Application";
                mail.Body = "Hello,\n\nAttached to this email is an automated application for your rental property.\n\nThanks,\nJK Rentals Mobile App.";
                mail.Attachments.Add(file);

                server.Port = 587;
                server.Host = "smtp.gmail.com";
                server.EnableSsl = true;
                server.UseDefaultCredentials = false;
                server.Credentials = new System.Net.NetworkCredential("OBFUSCATED", "OBFUSCATED");
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                server.Send(mail);
            }
            catch (Exception ex)
            {
                DisplayAlert("Failed", "Email failed to send. Please try again.", "OK");
            }

        }
    }
}