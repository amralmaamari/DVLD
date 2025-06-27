using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DvldDataBusinessLayer.clsTransmissionTypes;

namespace DvldDataBusinessLayer
{
    public class clsCarTypes
    {

        enum enMode { AddNew = 0, Update = 1 };

        enMode Mode = enMode.AddNew;


        public int CarTypeID { get; set; }

        //private string _CarTypeName;
        //public string CarTypeName
        //{
        //    set
        //    {
        //        _CarTypeName=clsCarTypes.Find(CarTypeID).CarTypeName;
        //    }
        //    get => _CarTypeName;
        //}
        public string Name { get; set; }


        public clsCarTypes() { 
            this.CarTypeID = -1;
            this.Name ="";
            Mode = enMode.AddNew;
        }

        private clsCarTypes(int carTypeID, string name)
        {
            this.CarTypeID = carTypeID;
            this.Name = name;
            Mode = enMode.Update;
        }


        public static DataTable GetAllCarTypes()
        {
            return clsCarsTypesData.GetAllCarTypes();
        }


        private bool _AddNewCarType()
        {

            this.CarTypeID = clsCarsTypesData.AddNewCarType(this.Name);
            return (this.CarTypeID != -1);


        }


        private bool _UpdateCarType()
        {
            return clsCarsTypesData.UpdateCarType(this.CarTypeID, Name);


        }


        public static clsCarTypes Find(int CarTypeID)
        {

            string Name = "";

            if (clsCarsTypesData.GetCarTypeInfoByID(CarTypeID, ref Name))
            {
                return new clsCarTypes(CarTypeID, Name);
            }


            return null;
        }

        public static clsCarTypes Find(string Name)
        {

            int CarTypeID = -1;

            if (clsCarsTypesData.GetCarTypeInfoByName(ref CarTypeID,  Name))
            {
                return new clsCarTypes(CarTypeID, Name);
            }


            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCarType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:
                    if (_UpdateCarType())
                    {
                        return true;
                    }
                    return false;

            }
            return false;
        }

    }
}
