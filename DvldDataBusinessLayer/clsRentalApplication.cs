using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsRentalApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;


      

        public int RentalApplicationID { get; set; }
        public int CarID { get; set; }

        public int CustomerID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string CurrentLocation { get; set; }
        public int RentTypeID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public float PaidFees { get; set; }
        public DateTime LastStatusDate { get; set; }

        public clsCar CarInfo;
        public clsCustomer CustomerInfo;
        public clsRentType RentTypeInfo;

        public enum enRentStatus { New = 1, Complete = 2, Cancelled=3 }
      
        public enRentStatus RentStatus { get; set; }

        public string RentStatusName
        {
            get
            {
                return ((byte)RentStatus == 1 ? "New" : (byte)RentStatus == 2 ? "Completed" : "Cancelled");
            }
        }

        public clsRentalApplication()
        {
            this.RentalApplicationID = -1;
            this.CarID = -1;
            this.CustomerID = -1;
            this.StartDateTime = DateTime.MinValue;
            this.EndDateTime = DateTime.MinValue;
            this.CurrentLocation = string.Empty;
            this.RentTypeID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            this.PaidFees = 0.0f;
            this.LastStatusDate = DateTime.Now;
            this.RentStatus = enRentStatus.New;

            Mode = enMode.AddNew;
        }

        private clsRentalApplication(int rentalApplicationID,int carID, int customerID, DateTime startDateTime, 
            DateTime endDateTime, string currentLocation, int rentTypeID,
            int createdByUserID, DateTime createdDate, 
            float paidFees, DateTime lastStatusDate, enRentStatus rentStatus)
        {
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


            CarInfo=clsCar.GetCarInfoByID(this.CarID);
            CustomerInfo=clsCustomer.GetCustomerInfoByID(this.CustomerID);
            RentTypeInfo = clsRentType.Find((clsRentType.enRentType)this.RentTypeID);
            Mode = enMode.Update;
        }

        public static DataTable GetAllRentalApplications()
        {
            return clsRentalApplicationData.GetAllRentalApplications();
        }

        private bool _AddNewRentalApplications()
        {


            //here make the car  decress from CarAvailability because we rent it
            if (!clsCarAvailability.GetCarAvailabilityInfoByCarID(CarID).UpdateCarsRented(clsCarAvailability.enModeCar.Rent))
                return false;



            this.RentalApplicationID= clsRentalApplicationData.AddNewRentalApplications(this.CarID, this.CustomerID, this.StartDateTime, this.EndDateTime, this.CurrentLocation,
           this.RentTypeID,this.CreatedByUserID, this.CreatedDate,this.PaidFees,this.LastStatusDate,(byte)this.RentStatus) ;

            return (this.RentalApplicationID != -1);
        }
        private bool _UpdateRentalApplication()
        {
             return clsRentalApplicationData.UpdateRentalApplication(this.RentalApplicationID,
                 this.CarID, this.CustomerID,
                 this.StartDateTime, this.EndDateTime, this.CurrentLocation,
                this.RentTypeID, this.CreatedByUserID, this.CreatedDate,
                this.PaidFees, this.LastStatusDate, (byte)this.RentStatus);

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewRentalApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateRentalApplication();

            }

            return false;
        }

        public static bool DeleteRentalApplication(int RentalApplicationID)
        {

            return clsRentalApplicationData.DeleteRentalApplication(RentalApplicationID);
        }

        public static bool IsRentalApplicationExist(int RentalApplicationID)
        {
            return clsRentalApplicationData.IsRentalApplicationExist(RentalApplicationID) ;
        }

        public static bool UpdateStatus(int RentalApplicationID, enRentStatus NewRentStatus)
        {
            return clsRentalApplicationData.UpdateStatus(RentalApplicationID,(byte) NewRentStatus) ;
        }


        
        public static clsRentalApplication FindByRentalApplicationInfoByID(int RentalApplicationID)
        {
                int CarID = -1;
                int CustomerID = -1;
                DateTime StartDateTime = DateTime.MinValue;
                DateTime EndDateTime = DateTime.MinValue;
                string CurrentLocation = string.Empty;
                int RentTypeID = -1;
                int CreatedByUserID = -1;
                DateTime CreatedDate = DateTime.Now;
                float PaidFees = -1.0f;
                DateTime LastStatusDate = DateTime.Now;
                byte RentStatus = 0;

                if (clsRentalApplicationData.GetRentalApplicationInfoByID(RentalApplicationID, ref CarID, ref CustomerID,
                     ref StartDateTime, ref EndDateTime,
                     ref CurrentLocation, ref RentTypeID, ref CreatedByUserID, ref CreatedDate,
                     ref PaidFees, ref LastStatusDate, ref RentStatus))
                    {
                        return new clsRentalApplication(RentalApplicationID, CarID, CustomerID, StartDateTime
                            , EndDateTime, CurrentLocation, RentTypeID, CreatedByUserID, CreatedDate,
                            PaidFees, LastStatusDate,(enRentStatus) RentStatus);
                    }

                return null;


            }
    }
}
