using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DvldDataBusinessLayer
{
    public class clsApplications
    {
        public enum enApplicationTypeID {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }

        public enum enApplicationStatus {   New=1, Cancelled = 2, Completed = 3      }

        public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode = enMode.AddNew;

      public int ApplicationID { get; set; }
      public int ApplicantPersonID { get; set; }
        public clsPeople PersonInfo;
      public string ApplicantFullName
        {
            get { return clsPeople.Find(ApplicantPersonID).FullName; }
        }
      public DateTime ApplicationDate { get; set; }
      public int ApplicationTypeID { set; get; }

      public clsApplicationType ApplicationTypeInfo;
      public enApplicationStatus ApplicationStatus { get; set; }
      public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
      public DateTime LastStatusDate { get; set; }
      public float PaidFees { get; set; }
      public int CreatedByUserID { get; set; }

        //Application1.CreatedByUserInfo.Person
        //this is called Compistion
        public clsUser CreatedByUserInfo;






        public clsApplications()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID =-1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            
            //Mode Add New
            Mode=enMode.AddNew;
        }

        //comsistion class it fill here in update only 
        private clsApplications(int applicationID, int applicantPersonID, DateTime applicationDate
            , int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate, float paidFees
            , int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.ApplicationTypeInfo = clsApplicationType.Find(this.ApplicationTypeID);
            this.PersonInfo = clsPeople.Find(this.ApplicantPersonID);
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            Mode = enMode.Update;
        }
       
        private bool _AddNewApplication()
        {
            this.ApplicationID=clsApplicationsData.AddNewApplications(
                this.ApplicantPersonID, this.ApplicationDate, (int)this.ApplicationTypeID,
                (byte) this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees,this.CreatedByUserID);

            return (ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return (clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,(int) this.ApplicationTypeID,(byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID));
            
        }



        public static clsApplications FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate=DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;

          
            if (clsApplicationsData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate
                , ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return  new clsApplications(ApplicationID,  ApplicantPersonID,  ApplicationDate
                ,  ApplicationTypeID, (enApplicationStatus) ApplicationStatus,  LastStatusDate,  PaidFees  ,  CreatedByUserID);

            }

            return null;
        }


        public bool Cancel()
        {
            return (clsApplicationsData.UpdateStatus(this.ApplicationID,(int)enApplicationStatus.Cancelled));
        }

        public bool SetComplete()
        {
            return (clsApplicationsData.UpdateStatus(this.ApplicationID, (int)enApplicationStatus.Completed));
        }
        public bool Save()
        {
            if(Mode == enMode.AddNew)
            {
                if (_AddNewApplication())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }


            if (Mode == enMode.Update)
            {
                return _UpdateApplication();
               
            }

            return false;
        }

        public bool Delete()
        {
            return (clsApplicationsData.DeleteApplication(this.ApplicationID));
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return (clsApplicationsData.IsApplicationExist(ApplicationID));
        }

        public  bool DoesPersonHaveActiveApplication( int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplications.enApplicationTypeID ApplicationTypeID)
            {
                return clsApplicationsData.GetActiveApplicationID(PersonID,(int) ApplicationTypeID);
            }


        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplications.enApplicationTypeID ApplicationTypeID, clsLicenseClass.enLicenseClasse LicenseClass)
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID,(int) LicenseClass);
        }


        public int GetActiveApplicationID(clsApplications.enApplicationTypeID ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

    }
}