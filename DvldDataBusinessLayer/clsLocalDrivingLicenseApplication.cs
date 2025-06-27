using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DvldDataBusinessLayer.clsApplications;
using static System.Net.Mime.MediaTypeNames;

namespace DvldDataBusinessLayer
{

    //Here is the tecaher did Inhertaince
    public class clsLocalDrivingLicenseApplication : clsApplications
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            //it come from the Inheritance of the Application ♥
            get
            {
                // return( base.PersonInfo.FullName);
                return clsPeople.Find(ApplicantPersonID).FullName;

            }
        }

        //Teacher he didn't use the ApplicationID as here just see his solution bez he did Inher



        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }



        //Here i have to write all of the base class 
        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID,
            int applicationID, int licenseClassID,
            int applicantPersonID, DateTime applicationDate
            , int applicationTypeID, enApplicationStatus applicationStatus,
            DateTime lastStatusDate, float paidFees
            , int createdByUserID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(this.LicenseClassID);

            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;


            Mode = enMode.Update;
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {

            if (clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID))
                return true;

            return false;
        }


        //here u have to check the docotr Solution
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;
            bool IsFound = (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID));


            if (IsFound)
            {
                clsApplications Application = clsApplications.FindBaseApplication(ApplicationID);


                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                         ApplicationID, LicenseClassID,
                         Application.ApplicantPersonID, Application.ApplicationDate
                        , Application.ApplicationTypeID, Application.ApplicationStatus,
                         Application.LastStatusDate, Application.PaidFees
                        , Application.CreatedByUserID);

            }

            return null;
        }


        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = -1;
            bool IsFound = (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ref LocalDrivingLicenseApplicationID, ApplicationID, ref LicenseClassID));


            if (IsFound)
            {
                clsApplications Application = clsApplications.FindBaseApplication(ApplicationID);


                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                         ApplicationID, LicenseClassID,
                         Application.ApplicantPersonID, Application.ApplicationDate
                        , Application.ApplicationTypeID, Application.ApplicationStatus,
                         Application.LastStatusDate, Application.PaidFees
                        , Application.CreatedByUserID);

            }

            return null;
        }


        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.

            base.Mode = (clsApplications.enMode)this.Mode;
            if (!base.Save())
                return false;


            if (Mode == enMode.AddNew)
            {
                if (_AddNewLocalDrivingLicenseApplication())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }

            if (Mode == enMode.Update)
            {
                return (_UpdateLocalDrivingLicenseApplication());

            }
            return false;
        }


        //the above of constructor teacher solve it in other way check it out ☺


        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        //delete the LDapplication and Delete the ApplictionAppoinentment
        //u have to check the Delete Again 
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application

            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;

            //Then we delete the base Application
            //this not work the below check it after see the Teacher
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }



        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool AttendedTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        // i didn't do it bez there is no clsTest

        //public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        // public byte GetPassedTestCount()


        public byte GetPassedTestCount()
        {
            return clsTestData.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }


        public bool Cancel()
        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 2);
        }


        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }


        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                 Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    return -1;


            }
            else
            {
                DriverID = Driver.DriverID;
            }

            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.

                this.SetComplete();
                return License.LicenseID;
            }

            return -1;





        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

    }
}
