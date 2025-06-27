using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsLicenseClass
    {
        public enum enLicenseClasse { SmallMotorcycle = 1, HeavyMotorcycle = 2, Ordinary = 3, Commercial = 4, Agricultural = 5, Smallandmediumbus = 6, Truckandheavy = 7 };
        public enLicenseClasse LicenseClassID { get; set; }
        public string ClassName  { get; set; }
        public string ClassDescription { get; set; }
        public Byte MinimumAllowedAge { get; set; }
        public Byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }



        enum enMode { Update=1,AddNew=2};

        enMode Mode = enMode.AddNew;
        

        public clsLicenseClass()
        {
            this.LicenseClassID = enLicenseClasse.SmallMotorcycle;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            Mode = enMode.AddNew;
        }

        private clsLicenseClass(enLicenseClasse LicenseClassID, string className, string classDescription, Byte minimumAllowedAge, Byte defaultValidityLength, float classFees)
        {
            this.LicenseClassID= LicenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
            Mode = enMode.Update;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClass Find(int licenseID)
        {
            string ClassName = "";
            string ClassDescription = "";
            Byte MinimumAllowedAge =0;
            Byte DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByID(licenseID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass((enLicenseClasse)licenseID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return null;
        }


        public static clsLicenseClass Find(string ClassName)
        {
            int licenseID=-1;
            string ClassDescription = "";
            Byte MinimumAllowedAge = 0;
            Byte DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByName(ref licenseID,  ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicenseClass((enLicenseClasse)licenseID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return null;
        }



        private bool _AddNewLicenseClass()
        {
            //call DataAccess Layer 

            this.LicenseClassID =(enLicenseClasse) clsLicenseClassData.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);


            return ((int)this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            //call DataAccess Layer 

            return clsLicenseClassData.UpdateLicenseClass((int)this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }

            return false;
        }
    }
}
