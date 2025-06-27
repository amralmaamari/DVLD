using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DvldDataBusinessLayer.clsRentType;
using static DvldDataBusinessLayer.clsTestType;

namespace DvldDataBusinessLayer
{
    public class clsRentType
    {

        enum enMode { AddNew=0,Update=1};
        public enum enRentType { Daily=1,Weekly=2,Monthly=3,Hourly=4};
       

        enMode Mode = enMode.AddNew;
        public enRentType RentTypeID {  get; set; }
        public string RentTypeName {  get; set; }
        public decimal RentTypeFees {  get; set; }
        public string RentTypeDescription {  get; set; }

        public clsRentType() {
            this.RentTypeID = enRentType.Daily;
            this.RentTypeName = "";
            this.RentTypeFees = 0;
            this.RentTypeDescription = "";
            Mode = enMode.AddNew;
        }

        public clsRentType(enRentType RentTypeID,string RentTypeName,decimal RentTypeFees,string RentTypeDescription)
        {
            this.RentTypeID = RentTypeID;
            this.RentTypeName = RentTypeName;
            this.RentTypeFees = RentTypeFees;
            this.RentTypeDescription = RentTypeDescription;
            Mode = enMode.Update;
        }


        public static DataTable GetAllRentTypes()
        {
            return clsRentTypeData.GetAllRentTypes();
        }


        private bool _AddNewRentType()
        {

           this.RentTypeID=(enRentType)clsRentTypeData.AddNewRentType(this.RentTypeName,this.RentTypeFees,this.RentTypeDescription);
            return ((int)this.RentTypeID != -1);


        }


        private bool _UpdateRentType()
        {
            return clsRentTypeData.UpdateRentType((int)this.RentTypeID,this.RentTypeName, this.RentTypeFees, this.RentTypeDescription);


        }


        public static clsRentType Find(clsRentType.enRentType RentTypeID)
        
        {

            string RentTypeName = "";
            decimal RentTypeFees = 0;
            string RentTypeDescription = "";
            if (clsRentTypeData.GetRentTypeInfoByID((int)RentTypeID,ref RentTypeName, ref RentTypeFees, ref RentTypeDescription))
            {
                return new clsRentType(RentTypeID, RentTypeName, RentTypeFees, RentTypeDescription);
            }
           

            return null;
        }

        public static clsRentType Find(string RentTypeName)
        {


            int RentTypeID = -1;
            decimal RentTypeFees = 0;
            string RentTypeDescription = "";
            if (clsRentTypeData.GetRentTypeInfoByName(ref RentTypeID,  RentTypeName, ref RentTypeFees, ref RentTypeDescription))
            {
                return new clsRentType((enRentType) RentTypeID, RentTypeName, RentTypeFees, RentTypeDescription);
            }


            return null;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewRentType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:
                    if (_UpdateRentType())
                    {
                        return true;
                    }
                    return false;

            }
            return false;
        }

    }
}
