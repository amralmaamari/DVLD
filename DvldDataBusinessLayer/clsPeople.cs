using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvldDataAccessLayer;

namespace DvldDataBusinessLayer
{
    public class clsPeople
    {
        enum enMode { AddNew=0,Update=1 };
       private enMode _Mode=enMode.AddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email  { get; set; }
        public int NationalityCountryID { get; set; }
        
        // id on't where he will use it 
        private string _ImagePath;

        public string ImagePath {
            get { return _ImagePath; } set { _ImagePath = value; } 
        }

        //this way you will het the country by the person only not direcet
        public clsCountry CountryInfo;
        public clsPeople()
        {
            PersonID = -1;
            //NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
           // Gendor = 3;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
            _Mode = enMode.AddNew;
        }

        private  clsPeople(int PersonID,  string NationalNo,  string FirstName,  string SecondName
                                            ,  string ThirdName,  string LastName,  DateTime DateOfBirth, byte Gendor
                                            ,  string Address,  string Phone,  string Email,  int NationalityCountryID
                                            ,  string ImagePath)
        {
           this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(this.NationalityCountryID);
            _Mode = enMode.Update;
             

        }



        public static clsPeople Find(int PersonID)
        {

            string NationalNo="";  string FirstName = "";  string SecondName = ""
           ; string ThirdName = "";  string LastName = "";  DateTime DateOfBirth=DateTime.Now; byte Gendor =3//unknow
           ; string Address = "";  string Phone = "";  string Email = "";  int NationalityCountryID=-1
           ; string ImagePath = "";

            if (clsPeopleData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                                                                 ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName
                                                , ThirdName, LastName, DateOfBirth, Gendor
                                                , Address, Phone, Email, NationalityCountryID
                                                , ImagePath);
            else
                return null;
        }

        public static clsPeople Find(string NationalNo)
        {

            int PersonID = -1; string FirstName = ""; string SecondName = ""
           ; string ThirdName = ""; string LastName = ""; DateTime DateOfBirth = DateTime.Now; byte Gendor = 3//unknow
           ; string Address = ""; string Phone = ""; string Email = ""; int NationalityCountryID = -1
           ; string ImagePath = "";

            if (DvldDataAccessLayer.clsPeopleData.GetPersonInfoByNationalNo(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                                                                 ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName
                                                , ThirdName, LastName, DateOfBirth, Gendor
                                                , Address, Phone, Email, NationalityCountryID
                                                , ImagePath);
            else
                return null;
        }

        public static DataTable GetAllPeople()
        {

            return clsPeopleData.GetAllPeople();


        }

        public static int GetAllPeopleCount()
        {

            return DvldDataAccessLayer.clsPeopleData.GetAllPeopleCount();


        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPeopleData.IsPersonExist(PersonID);
        }
        
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleData.IsPersonExist(NationalNo);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

       
        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName
                                                 , this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor
                                                 , this.Address, Phone, this.Email, this.NationalityCountryID
                                                 , this.ImagePath);
            return (this.PersonID != -1);
        }


        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName
                                                 , this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor
                                                 , this.Address, Phone, this.Email, this.NationalityCountryID
                                                 , this.ImagePath);
        }
        public bool Save()
        {
            if(_Mode == enMode.AddNew)
            {
                _AddNewPerson();
                _Mode = enMode.Update;
                return true;
            }
            if (_Mode == enMode.Update)
            {
                _UpdatePerson();
                return true;
            }
            return false;
        }


     
    }
}
