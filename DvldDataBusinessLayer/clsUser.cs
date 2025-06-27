using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsUser
    {

       enum enMode { Update=0,AddNew=1 };
        enMode _Mode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        // هذي يسمى كمبزيشن الذي معنها انك تقدر توصل للشخص عن طريق اليوزر هذت
        public clsPeople PersonInfo;
        public string Username { get; set; }
        public string Password { get; set; }

        //{-1=All,1=CreateAppLicense,2=CreateCarAndRentCar.4=CreateUserAndMangeTheOtherUser }
        public enum enPermissions { eAll = -1, pCreateAppLicense = 1, pCreateAndRentCar = 2, pCreateUserAndManageUsers = 4 };
        public enPermissions Permission { get; set; } // Store the user's permissions


        
        public bool IsActive { get; set; }


        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = true;
            this.Permission = enPermissions.eAll;
      
            //new
            _Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string Username, string password, enPermissions permission, bool IsActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.PersonInfo = clsPeople.Find(this.PersonID);
            this.Username = Username;
            this.Password = password;
            this.Permission= permission;
            this.IsActive = IsActive;
            //Update;
            _Mode = enMode.Update;
        }


      

        public static clsUser FindByUsernameAndPassword(string Username, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            int Permission = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(ref UserID, ref PersonID,  Username,  Password,ref Permission, ref IsActive))
                return new clsUser(UserID, PersonID, Username, Password,(enPermissions)Permission, IsActive);

            return null;
        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string Username = ""; string Password = "";
            int Permission = -1;
            bool IsActive=false;

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref Username, ref Password,ref Permission, ref IsActive))
                return new clsUser(UserID, PersonID, Username, Password, (enPermissions)Permission, IsActive);

            return null;
        }


        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string Username = ""; string Password = "";
            int Permission = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByPersonID(ref UserID,  PersonID, ref Username, ref Password,ref Permission, ref IsActive))
                return new clsUser(UserID, PersonID, Username, Password, (enPermissions)Permission, IsActive);

            return null;
        }

        //public static clsUser GetUserInfoByUsername(string Username)
        //{
        //    int PersonID = -1;
        //    int UserID = -1; string Password = "";
        //    bool IsActive = false;

        //    if (clsUserData.GetUserInfoByUserID(ref UserID, ref PersonID,  Username, ref Password, ref IsActive))
        //        return new clsUser(UserID, PersonID, Username, Password, IsActive);

        //    return null;
        //}


        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }


        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        //this i want to make because when will set the Permissions to the user if have all permission want to return -1 and save it in the database


        //private int _TotalOfPermissions(enPermissions permissions)
        //{
        //    int TotalOfPermissions = 7;//total of permissions in the enum above

        //    return ((int)enPermissions.pCreateAppLicense + (int)enPermissions.pCreateUserAndManageUsers + (int)enPermissions.pCreateAndRentCar);
        //}
        private  bool _AddNewUser()
        {
            this.UserID= clsUserData.AddNewUser(this.PersonID,this.Username,this.Password,(int)this.Permission, this.IsActive);
            return (this.UserID != -1);
        }


        private bool _UpdateUser()
        {
           return  clsUserData.UpdateUser(this.UserID, this.PersonID, this.Username, this.Password, (int)this.Permission, this.IsActive);
          
        }


        public static DataTable GetAllUser()
        {
            return clsUserData.GetAllUser();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public  bool Save()
        {
            if(_Mode == enMode.AddNew)
            {
               if (_AddNewUser())
                {
                    _Mode=enMode.Update;
                    return true;

                }
                return false;
               
            }

            if (_Mode == enMode.Update)
            {
                _UpdateUser();
                 return true;
                
            }

            return false;
        }



        
    }
}
