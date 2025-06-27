using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsTestType
    {
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public enTestType TestTypeID { get; set; }

        enum enMode { Update = 0, AddNew = 1 };
        private enMode _Mode;


   

        public clsTestType()
        {
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;

            _Mode = enMode.AddNew;
        }

        private clsTestType(clsTestType.enTestType testTypeID, string testTypeTitle, string testTypeDescription, decimal testTypeFees)
        {
            TestTypeID =  testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
            _Mode = enMode.Update;
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }


        private bool _AddNewTestType()
        {

            this.TestTypeID =(clsTestType.enTestType) clsTestTypeData.AddNewTestType(this.TestTypeTitle,this.TestTypeDescription, this.TestTypeFees);
            return (this.TestTypeTitle != "");


        }


        private bool _UpdateTestType()
        {

            return (clsTestTypeData.UpdateTestType((int)this.TestTypeID,this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees));


        }


        public static clsTestType Find(enTestType TestTypeID)
        {

            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFees = 0;
            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID,ref TestTypeTitle,ref TestTypeDescription,ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

            return null;
        }

       
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:
                    if (_UpdateTestType())
                    {
                        return true;
                    }
                    return false;

            }
            return false;
        }
    }
}
