using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{

    public class clsApplicationType
    {
         public int ApplicationTypeID { get; set; }
         public string ApplicationTypeTitle { get; set; }
         public float ApplicationFees { get; set; }

        enum enMode { AddNew = 0,Update=1};
        private enMode _Mode;

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationFees =-1;
            _Mode = enMode.AddNew;
        }

        private clsApplicationType(int applicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationFees = applicationFees;
            _Mode = enMode.Update;
        }

        public static DataTable GetAllApplictionTypes()
        {
            return clsApplictionTypesData.GetAllApplicationTypes();
        }


        private bool _AddNewApplictionType()
        {

            this.ApplicationTypeID = clsApplictionTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);
            return (this.ApplicationTypeID != -1);
            

        }
        private  bool _UpdateApplictionType()
        {
         
            return (clsApplictionTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees) );
          

        }


        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;
            if (clsApplictionTypesData.GetApplicationTypeInfoByID(ApplicationTypeID,ref ApplicationTypeTitle,ref ApplicationFees))
                return new clsApplicationType(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);

            return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplictionType())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    

                case enMode.Update:
                    if (_UpdateApplictionType())
                    {
                        return true;
                    }
                    return false;

            }
            return false;
        }
      
    }
}
