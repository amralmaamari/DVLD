using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsCustomer
    {
        enum enMode { AddNew=0, Update = 1 }

        enMode Mode=enMode.AddNew;
        public int CustomerID { get; set; }

        public int LicenseID { get; set; }
        public clsLicense LiceseInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsActive { get; set; }


        public clsCustomer()
         {
            this.CustomerID = -1;
            this.LicenseID = -1;
            this.CreatedByUserID = -1;
            this.IsActive = false;
            Mode = enMode.AddNew;
        }


        private clsCustomer(int CustomerID, int LicenseID, int CreatedByUserID, bool IsActive)
        {
            this.CustomerID = CustomerID;
            this.LicenseID= LicenseID;
            this.CreatedByUserID=CreatedByUserID;
            this.IsActive=IsActive;

            LiceseInfo = clsLicense.GetLicenseInfoByID(LicenseID);

            Mode = enMode.Update;
        }

        private bool _AddNewCustomer()
        {
           this.CustomerID=clsCustomerData.AddNewCustomer(this.LicenseID, this.CreatedByUserID);
            return (this.CustomerID != -1);
        }



        public static DataTable GetAllCustomer()
        {
            return clsCustomerData.GetAllCustomer();
        }

        private bool _UpdateCustomer()
        {
            return (clsCustomerData.UpdateCustomer(this.CustomerID, this.IsActive));

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCustomer())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateCustomer();


            }
            return false;
        }

        public static bool IsThereCustomerActiveByLicenseID(int LicenseID)
        {
            return clsCustomerData.IsThereCustomerActiveByLicenseID(LicenseID);
        }

        public  bool IsThereCustomerActiveByLicenseID()
        {
            return clsCustomerData.IsThereCustomerActiveByLicenseID(this.LicenseID);
        }


        public static clsCustomer GetCustomerInfoByID(int CustomerID)
        {
            int LicenseID = -1;
            int CreatedByUserID = -1;
            bool IsActive = false;

           if (clsCustomerData.GetCustomerInfoByID(CustomerID,ref LicenseID,ref CreatedByUserID
               ,ref IsActive))
            {
                return new clsCustomer(CustomerID, LicenseID, CreatedByUserID, IsActive);
            }

            return null;
          
        }

        public static bool DeleteCustomer(int CustomerID)
        {
            return clsCustomerData.DeleteCustomer(CustomerID);
        }

        public  bool DeleteCustomer()
        {
            return clsCustomerData.DeleteCustomer(this.CustomerID);
        }


    }
}
