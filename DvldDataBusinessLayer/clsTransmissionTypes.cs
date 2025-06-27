using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DvldDataBusinessLayer.clsRentType;

namespace DvldDataBusinessLayer
{
    public class clsTransmissionTypes
    {
        enum enMode { AddNew = 0, Update = 1 };

        enMode Mode =enMode.AddNew;
        public enum enTransmissionType { MT = 1, AT = 2, CVT = 3, DCT = 4 , SAT=5,
         TT=6,DSG=7,AMT=8, ECVT=9 , SMT=10};


        public enTransmissionType TransmissionTypeID {  get; set; }
        public string Name {  get; set; }

        public clsTransmissionTypes()
        {
            this.TransmissionTypeID=enTransmissionType.MT;
            this.Name = "";
            Mode = enMode.AddNew;
        }

        private clsTransmissionTypes(enTransmissionType TransmissionTypeID, string Name)
        {
            this.TransmissionTypeID = TransmissionTypeID;
            this.Name = Name;
            Mode = enMode.Update;
        }




        public static DataTable GetAllTransmissionTypes()
        {
            return clsTransmissionTypeData.GetAllTransmissionTypes();
        }


        private bool _AddNewRentType()
        {

            this.TransmissionTypeID = (enTransmissionType)clsTransmissionTypeData.AddNewTransmissionType(this.Name);
            return ((int)this.TransmissionTypeID != -1);


        }


        private bool _UpdateRentType()
        {
            return clsTransmissionTypeData.UpdateTransmissionType((int)this.TransmissionTypeID, Name);


        }


        public static clsTransmissionTypes Find(clsTransmissionTypes.enTransmissionType TransmissionTypeID)
        {

            string Name = "";
            
            if (clsTransmissionTypeData.GetTransmissionTypeInfoByID((int)TransmissionTypeID, ref Name))
            {
                return new clsTransmissionTypes(TransmissionTypeID, Name);
            }


            return null;
        }

        public static clsTransmissionTypes Find(string Name)
        {

            int TransmissionTypeID=-1;

            if (clsTransmissionTypeData.GetTransmissionTypeInfoByName(ref TransmissionTypeID,  Name))
            {
                return new clsTransmissionTypes((enTransmissionType)TransmissionTypeID, Name);
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
