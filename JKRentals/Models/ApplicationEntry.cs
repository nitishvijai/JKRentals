using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace JKRentals.Models
{
    class ApplicationEntry
    {
        public ApplicationEntry()
        {
            Pg1Complete = false;
            AppComplete = false;
        }

        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        public string SocSecNo { get; set; }
        public DateTime DOB { get; set; }

        public string DriverLicenseNo { get; set; }

        public string PhoneNo { get; set; }
        public string AltPhoneNo { get; set; }

        public string Email { get; set; }

        public int NoInhabitants { get; set; }

        // Application checks
        private Boolean Pg1Complete { get; set; }
        private Boolean AppComplete { get; set; }

        public Boolean CheckForCompletion()
        {
            string[] requiredFields = { FirstName, LastName, SocSecNo, DriverLicenseNo, PhoneNo, Email };

            foreach (string item in requiredFields)
            {
                if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                {
                    Pg1Complete = false;
                    AppComplete = false;
                    return false;
                }
            }

            Pg1Complete = true;
            AppComplete = true;

            return true;
        }

        public bool IsSocialSecurityNumber(string value)
        {
            return Regex.IsMatch(value, @"^\d{3}-\d{2}-\d{4}$");
        }

        public bool IsPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
        }

        public bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$");
        }

    }
}
