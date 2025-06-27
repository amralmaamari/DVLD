using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsLicense 
    {

       public int LicenseID { get; set; }
       public int ApplicationID { get; set; }
       public   clsApplications ApplictionInfo;

       public int DriverID { get; set; }
        //clsDriver DriverInfo;
       public int LicenseClass { get; set; }
       public clsLicenseClass LicenseInfo;

       public DateTime IssueDate { get; set; }
       public DateTime ExpirationDate { get; set; }
       public String Notes { get; set; }
       public float PaidFees { get; set; }
       public bool IsActive { get; set; }

       public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement=3, LostReplacement=4 };
       public enIssueReason IssueReason { get; set; }

        public clsDetainedLicensecs DetainedInfo { set; get; }

        public string IssueReasonText
        {
            get { return GetIssueReasonText(IssueReason); }
        }

        public int CreatedByUserID { get; set; }
       clsUser UserInfo;

        public bool IsDetained
        {
            get { return clsDetainedLicensecs.IsLicenseDetained(this.LicenseID); }
        }

        enum enMode { AddNew=0,Update=1};
        enMode Mode=enMode.AddNew;

        /*
        1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost. 
        */

        public clsLicense() {

            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID,
             int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
             String Notes, float PaidFees, bool IsActive,
             enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID=LicenseID;
            this.ApplicationID = ApplicationID;
            ApplictionInfo=clsApplications.FindBaseApplication(this.ApplicationID);
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            
            LicenseInfo = clsLicenseClass.Find(this.LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            Mode = enMode.Update;
        }


        private bool _AddNewLicense()
        {
          this.LicenseID=  clsLicensesData.AddNewLicense(this.ApplicationID,this.DriverID,this.LicenseClass,this.IssueDate
                ,this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,(byte)this.IssueReason
                ,this.CreatedByUserID);

            return (this.LicenseID != -1);
        }


        private bool _UpdateLicense()
        {
         return    clsLicensesData.UpdateLicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate
                  , this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason
                  , this.CreatedByUserID);

            
        }


        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewLicense())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }

            if (Mode == enMode.Update)
            {
                return _UpdateLicense();
            }

            return false;
        }


        public static clsLicense GetLicenseInfoByID(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason =(byte) enIssueReason.FirstTime;
            int CreatedByUserID = 0;

          if(  clsLicensesData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID,
                ref LicenseClass, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive,
                ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate
                    , ExpirationDate, Notes, PaidFees, IsActive,(enIssueReason) IssueReason, CreatedByUserID);
            }

            return null;



        }


        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1 );
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
          
            return clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);


        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicensesData.GetDriverLicenses(DriverID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicensesData.DeactivateLicense(this.LicenseID);
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch(IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public static bool DeactivateLicense(int LicenseClassID)
        {
            return clsLicensesData.DeactivateLicense(LicenseClassID);
        }


        //will make it when i comre to the clsDetainedLicense
        //  public int Detain(float FineFees, int CreatedByUserID)
        // 



        //thw below code have to understand them Properly
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID=this.ApplictionInfo.ApplicantPersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID =(int) clsApplications.enApplicationTypeID.RenewDrivingLicense;
            Application.ApplicationStatus =  clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees =(float) clsApplicationType.Find((int) clsApplications.enApplicationTypeID.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }


            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass= this.LicenseClass;
            NewLicense.IssueDate=DateTime.Now;
            int DefaultValidityLength = this.LicenseInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);

            NewLicense.Notes = Notes;
            //this it should come from the Form becase add some Extra Charage from TotalFees
            NewLicense.PaidFees = this.LicenseInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = Application.CreatedByUserID;

            if (!NewLicense.Save())
                return null;


            //we need to deactivate the old License.
            DeactivateCurrentLicense();
             return NewLicense;


        }


        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.ApplictionInfo.ApplicantPersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                                (int)clsApplications.enApplicationTypeID.ReplaceDamagedDrivingLicense :
                                (int)clsApplications.enApplicationTypeID.ReplaceLostDrivingLicense;



                
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = (float)clsApplicationType.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }


            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;           

            NewLicense.ExpirationDate = this.ExpirationDate;

            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
                return null;


            //we need to deactivate the old License.
            DeactivateCurrentLicense();
            return NewLicense;


        }

        public int Detain(float FineFees,int CreatedByUserID)
        {

            clsDetainedLicensecs detainedLicensecs = new clsDetainedLicensecs();
            detainedLicensecs.LicenseID = this.LicenseID;
            detainedLicensecs.DetainDate = DateTime.Now;
            detainedLicensecs.FineFees = FineFees;
            detainedLicensecs.CreatedByUserID = CreatedByUserID;


            if (!detainedLicensecs.Save())
            {
                return -1;
               
            }
            else
            {

                return detainedLicensecs.DetainID;
            }
        }


        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.ApplictionInfo.ApplicantPersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationTypeID.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = (float)clsApplicationType.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return false;
            }
            ApplicationID=Application.ApplicationID;


            clsDetainedLicensecs detainedLicensecs = clsDetainedLicensecs.FindByLicenseID(this.LicenseID);
           
            return (detainedLicensecs.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID));
           
        }



    }
}
