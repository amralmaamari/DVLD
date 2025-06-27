using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsDriver
    {
        enum enMode { AddNew=0, Update=1 }
        enMode _Mode= enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPeople PersonInfo;
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            _Mode = enMode.AddNew;
        }

        private clsDriver(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            PersonInfo = clsPeople.Find(this.PersonID);
            this.CreatedByUserID= CreatedByUserID;
            CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            this.CreatedDate = CreatedDate;
            _Mode = enMode.Update;
        }



        private bool _AddNewDriver()
        {

            this.DriverID = (clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID));
            return (this.DriverID > -1);
        }

        private  bool _UpdateDriver()
        {
            return (clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID));
           
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }


        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByDriverID(DriverID,ref PersonID, ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDriver(DriverID,PersonID,CreatedByUserID,CreatedDate);
            }
            return null;
        }


        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByPersonID(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }


        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }


        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        //make it When U make InternationalLicenses 
        //public static DataTable GetInternationalLicenses(int DriverID)
        //{

        //}
    }
}
