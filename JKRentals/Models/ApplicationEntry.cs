using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace JKRentals.Models
{
    public class ApplicationEntry
    {
        public ApplicationEntry()
        {
            AppComplete = false;
        }

        public int CurrentPage { get; set; }

        // -------------------------------- PAGE 1 -------------------------------- 

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

        public string AppFilename { get; set; }

        // -------------------------------- PAGE 2 -------------------------------- 

        public string DesiredAddress { get; set; }

        public DateTime MoveDate { get; set; }

        // Current address variables
        public string CurrentAddress { get; set; }
        public string CurrentCSZ { get; set; }
        public DateTime CurrMoveDate { get; set; }
        public string CurrLandlordName { get; set; }
        public string CurrLandlordPhone { get; set; }
        public int CurrRent { get; set; }
        public string CurrMoveReason { get; set; }

        // Previous address variables
        public string PrevAddress1 { get; set; }
        public string PrevCSZ1 { get; set; }
        public DateTime PrevMoveDate1 { get; set; }
        public DateTime PrevMoveOutDate1 { get; set; }
        public string PrevLandlordName1 { get; set; }
        public string PrevLandlordPhone1 { get; set; }
        public int PrevRent1 { get; set; }

        public string PrevAddress2 { get; set; }
        public string PrevCSZ2 { get; set; }
        public DateTime PrevMoveDate2 { get; set; }
        public DateTime PrevMoveOutDate2 { get; set; }
        public string PrevLandlordName2 { get; set; }
        public string PrevLandlordPhone2 { get; set; }
        public int PrevRent2 { get; set; }

        // -------------------------------- PAGE 3 -------------------------------- 

        public string CurrentEmployer { get; set; }
        public string EmpPhoneNo { get; set; }
        public int GrossWages { get; set; }
        public string ManagerName { get; set; }
        public DateTime HireDate { get; set; }
        public string IncomeSrcs { get; set; }
        public string Explanation { get; set; }

        // -------------------------------- PAGE 4 -------------------------------- 

        public string StayDuration { get; set; }
        public string Pets { get; set; }
        public int NumEvictions { get; set; }
        public int NumFelonies { get; set; }
        public bool BrokeLease { get; set; }
        public bool Smoke { get; set; }
        public bool CheckAcct { get; set; }
        public int NumVehicles { get; set; }
        public bool AmtReady { get; set; }
        public string LimitRent { get; set; }
        public string MoneyValue { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyPhone { get; set; }
        public string HearSource { get; set; }
        public string WhyRent { get; set; }

        // -------------------------------- PAGE 5 -------------------------------- 

        public string AddlInfo { get; set; }


        // Application check
        private Boolean AppComplete { get; set; }

        public Boolean CheckForCompletion()
        {
            string[] requiredFields = null;
            AppComplete = true;

            if (CurrentPage == 1)
            {
                requiredFields = new string[]{ FirstName, LastName, SocSecNo, DriverLicenseNo, PhoneNo, Email };
            }
            else if (CurrentPage == 2)
            {
                requiredFields = new string[] { DesiredAddress, CurrentAddress, CurrentCSZ, CurrMoveReason };
            }
            else if (CurrentPage == 3)
            {
                requiredFields = new string[] { CurrentEmployer, EmpPhoneNo, GrossWages.ToString(), ManagerName, HireDate.ToString() };
            }
            else if (CurrentPage == 4)
            {
                requiredFields = new string[] { StayDuration, Pets, EmergencyName, EmergencyPhone, LimitRent, MoneyValue, HearSource, WhyRent };
            }
            else if (CurrentPage == 5)
            {
                return true;
            }

            foreach (string item in requiredFields)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    AppComplete = false;
                    return false;
                }
            }

            return AppComplete;
            
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

        public string FormatSSN(string SSN)
        {
            if (SSN == null)
            {
                return SSN;
            }

            if (SSN.Length == 9)
            {
                string formatted = "";

                for (int i = 0; i < SSN.Length; ++i)
                {
                    if (i == 3 || i == 5)
                    {
                        formatted += "-";
                    }

                    formatted += SSN[i];
                }

                SSN = formatted;
            }
            else
            {
                // forget it and move on
            }

            return SSN;
        }

        public string FormatPhone(string PhoneNo)
        {
            if (PhoneNo == null)
            {
                return PhoneNo;
            }

            if (PhoneNo.Length == 10)
            {
                string formatted = "";

                for (int i = 0; i < PhoneNo.Length; ++i)
                {
                    if (i == 3 || i == 6)
                    {
                        formatted += "-";
                    }

                    formatted += PhoneNo[i];
                }

                PhoneNo = formatted;
            }
            else
            {
                // forget it & move on
            }

            return PhoneNo;
        }
    }
}
