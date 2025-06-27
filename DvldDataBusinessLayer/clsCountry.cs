using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvldDataAccessLayer;

namespace DvldDataBusinessLayer
{
    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();

        }

        


        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;

            bool isFound = clsCountryData.GetCountryInfoByName(ref CountryID,  CountryName);
            if (isFound)
            {
                return new clsCountry(CountryID, CountryName);
            }
            return null;



        }


        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            bool isFound = clsCountryData.GetCountryInfoByID(CountryID,ref CountryName);
            if (isFound)
            {
                return new clsCountry(CountryID,CountryName); 
            }
            return null;



        }

        public static bool IsCustomerActiveByLicenseID(int LicenseID)
        {
            return clsCustomerData.IsThereCustomerActiveByLicenseID(LicenseID);
        }

       


    }
}
