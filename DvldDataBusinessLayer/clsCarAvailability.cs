using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DvldDataBusinessLayer.clsCarAvailability;

namespace DvldDataBusinessLayer
{
    public class clsCarAvailability
    {

        enum enMode { Update = 0, AddNew = 1 };
        enMode _Mode = enMode.AddNew;


        public enum enModeCar { Rent=0,Return=1};
        public enModeCar ModeCar;
        public int CarAvailabilityID {  get; set; }
        public int CarID {  get; set; }
        public int TotalCars {  get; set; }
        public int CarsRented {  get; set; }
        public int AvailableCars {  get; set; }

        public clsCarAvailability()
        {
            this.CarAvailabilityID = -1;
            this.CarID = -1;
            this.TotalCars = 1;
            this.CarsRented = 0;
            this.AvailableCars = 0;
            _Mode=enMode.AddNew;
        }

        private clsCarAvailability(int carAvailabilityID, int carID, int totalCars, int carsRented, int availableCars)
        {
            this.CarAvailabilityID = carAvailabilityID;
            this.CarID = carID;
            this.TotalCars = totalCars;
            this.CarsRented = carsRented;
            this.AvailableCars = availableCars;
            _Mode = enMode.Update;
        }


        public static DataTable GetAllCarAvailability()
        {
            return clsCarAvailabilityData.GetAllCarAvailability();
        }

       private bool _AddNewCarAvailability()
        {
            this.CarAvailabilityID=clsCarAvailabilityData.AddNewCarAvailability(this.CarID,this.TotalCars);
            return (this.CarAvailabilityID!=0);
        }


        private bool _UpdateCarAvailability()
        {
            return clsCarAvailabilityData.UpdateCarAvailability(this.CarAvailabilityID, this.CarID, this.TotalCars, this.CarsRented);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCarAvailability())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateCarAvailability();


            }
            return false;
        }
        public static clsCarAvailability GetCarAvailabilityInfoByID(int CarAvailabilityID)
        {
            int CarID = -1;
            int TotalCars = -1;
            int CarsRented = -1;
            int AvailableCars = -1;

            if (clsCarAvailabilityData.GetCarAvailabilityInfoByID(CarAvailabilityID, ref CarID, ref TotalCars, ref CarsRented, ref AvailableCars))
            {
                return new clsCarAvailability(CarAvailabilityID, CarID, TotalCars, CarsRented, AvailableCars);
            }
            return null;
        }

        public static clsCarAvailability GetCarAvailabilityInfoByCarID(int CarID)
        {
            int CarAvailabilityID = -1;
            int TotalCars = -1;
            int CarsRented = -1;
            int AvailableCars = -1;

            if (clsCarAvailabilityData.GetCarAvailabilityInfoByCarID(ref CarAvailabilityID,  CarID, ref TotalCars, ref CarsRented, ref AvailableCars))
            {
                return new clsCarAvailability(CarAvailabilityID, CarID, TotalCars, CarsRented, AvailableCars);
            }
            return null;
        }

        public bool UpdateCarsRented(enModeCar ModeCar)
        {
            switch (ModeCar)
            {
                case enModeCar.Rent:
                    return clsCarAvailabilityData.UpdateCarsRented(this.CarAvailabilityID, ++this.CarsRented);
                case enModeCar.Return:
                    return clsCarAvailabilityData.UpdateCarsRented(this.CarAvailabilityID, --this.CarsRented);
                default:
                    return false;   
            }

        }
    }
}
