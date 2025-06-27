using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsInternationalLicense:clsApplications
    {

        enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }

        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }

        public clsInternationalLicense(int ApplicationID,int ApplicantPersonID,
            DateTime ApplicationDate,int ApplicationTypeID, enApplicationStatus ApplicationStatus,
            DateTime LastStatusDate,float PaidFees,int CreatedByUserID,
            int InternationalLicenseID
            , int DriverID
            , int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
            , bool IsActive)
        {
            base.ApplicationID=ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.PersonInfo = clsPeople.Find(base.ApplicantPersonID);
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplications.enApplicationTypeID.NewInternationalLicense;
            base.ApplicationStatus =  clsApplications.enApplicationStatus.Completed;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = ApplicantPersonID;
            base.CreatedByUserID = ApplicantPersonID;




            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            
            DriverInfo = clsDriver.FindByDriverID(this.DriverID);

            _Mode = enMode.Update;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {


            clsApplications Applications;

            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID,
                ref ApplicationID
            , ref DriverID
            , ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate
            , ref IsActive, ref CreatedByUserID))
            {
                Applications = clsApplications.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(Applications.ApplicationID, Applications.ApplicantPersonID,
                    Applications.ApplicationDate, Applications.ApplicationTypeID, Applications.ApplicationStatus
                    ,Applications.LastStatusDate, Applications.PaidFees, Applications.CreatedByUserID
                    , InternationalLicenseID,  DriverID, IssuedUsingLocalLicenseID
                    , IssueDate, ExpirationDate, IsActive);
            }

            return null;
        }

        public static clsInternationalLicense GetInternationalLicenseInfoByByApplicationID(int ApplicationID)
        {


            clsApplications Applications;

            int InternationalLicenseID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByByApplicationID(ref InternationalLicenseID,
               ApplicationID
            , ref DriverID
            , ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate
            , ref IsActive, ref CreatedByUserID))
            {
                Applications = clsApplications.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(Applications.ApplicationID, Applications.ApplicantPersonID,
                    Applications.ApplicationDate, Applications.ApplicationTypeID, Applications.ApplicationStatus
                    , Applications.LastStatusDate, Applications.PaidFees, Applications.CreatedByUserID
                    , InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID
                    , IssueDate, ExpirationDate, IsActive);
            }

            return null;
        }

       

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);

        }

        private  bool _AddNewInternationalLicense()
        {
         


            this.InternationalLicenseID= clsInternationalLicenseData.AddNewInternationalLicense(
                 this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID
                    , this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID > 0);
        }


        private bool _UpdateInternationalLicense()
        {
            return (clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID,
                  this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID
                     , this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID));

    
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.

            base.Mode= (clsApplications.enMode)Mode;

            if (!base.Save())
            {
                return false;

            }

            if (_Mode == enMode.AddNew)
            {
                _AddNewInternationalLicense();
                _Mode = enMode.Update;
                return true;
            }
            if (_Mode == enMode.Update)
            {
                _UpdateInternationalLicense();
                return true;
            }
            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);        }
    }



}
