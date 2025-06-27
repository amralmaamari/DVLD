using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsCarRentalApplication : clsRentalApplication
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        public int CarRentalApplicationID { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }


        public clsCarRentalApplication()
        {
            this.CarRentalApplicationID = -1;
            this.Notes = "";
            this.IsActive = false;
            _Mode = enMode.AddNew;
        }

        private clsCarRentalApplication(int carRentalApplicationID,string notes, bool isActive,
            int rentalApplicationID, int carID, int customerID, DateTime startDateTime,
            DateTime endDateTime, string currentLocation, int rentTypeID,
            int createdByUserID, DateTime createdDate,
            float paidFees, DateTime lastStatusDate, enRentStatus rentStatus)
      
        {
            this.CarRentalApplicationID = carRentalApplicationID;
            this.Notes = notes;
            this.IsActive = isActive;
            this.RentalApplicationID = rentalApplicationID;
            this.CarID = carID;
            this.CustomerID = customerID;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.CurrentLocation = currentLocation;
            this.RentTypeID = rentTypeID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;
            this.PaidFees = paidFees;
            this.LastStatusDate = lastStatusDate;
            this.RentStatus = rentStatus;

            this.CarInfo = clsCar.GetCarInfoByID(this.CarID);
            this.CustomerInfo = clsCustomer.GetCustomerInfoByID(this.CustomerID);
            this.RentTypeInfo = clsRentType.Find((clsRentType.enRentType)this.RentTypeID);


            _Mode = enMode.Update;
        }


        public static DataTable GetAllCarRentalApplications()
        {
            return clsCarRentalApplicationData.GetAllCarRentalApplications();
        }

        public static clsCarRentalApplication FindByCarRentalApplicationID(int CarRentalApplicationID)
        {
            int RentApplicationID = -1;
            int CreatedByUserID = -1;
            string Notes = "";
            bool IsActive = false;

            bool IsFound = clsCarRentalApplicationData.GetCarRentalApplicationInfoByID(CarRentalApplicationID,
                ref RentApplicationID,
               ref CreatedByUserID, ref Notes, ref IsActive);
            
            if (IsFound) {
                clsRentalApplication RentApplication= clsRentalApplication.FindByRentalApplicationInfoByID(RentApplicationID);

                return new clsCarRentalApplication(CarRentalApplicationID, Notes, IsActive,
                    RentApplicationID, RentApplication.CarID, RentApplication.CustomerID, RentApplication.StartDateTime, RentApplication.EndDateTime,
                    RentApplication.CurrentLocation, RentApplication.RentTypeID, CreatedByUserID, RentApplication.CreatedDate,
                    RentApplication.PaidFees, RentApplication.LastStatusDate, RentApplication.RentStatus);

            }
            return null;
        }

        private bool _AddNewCarRentalApplications()
        {
            

           this.CarRentalApplicationID= clsCarRentalApplicationData.AddNewCarRentalApplications(this.RentalApplicationID,
                this.CreatedByUserID, this.Notes, this.IsActive);

            return (this.CarRentalApplicationID != -1);
        }

        private bool _UpdateCarRentalApplication()
        {
           return ( clsCarRentalApplicationData.UpdateCarRentalApplication(this.CarRentalApplicationID,
                this.RentalApplicationID,this.CreatedByUserID,this.Notes,
                this.IsActive) );

        }

      


        public bool Save()
        {
            //here to trace
            base.Mode = (clsRentalApplication.enMode)this._Mode;
            if (!base.Save())
                return false;

            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCarRentalApplications())
                    {
                        
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCarRentalApplication();

            }

            return false;
        }

        public  bool Delete()
        {

            if (clsCarRentalApplicationData.DeleteCarRentalApplication(this.CarRentalApplicationID))
            {
                
                if (DeleteRentalApplication(this.RentalApplicationID))
                    return true;
                else
                    return false;
            }
            else
                return false;
           
        }

    }
}
