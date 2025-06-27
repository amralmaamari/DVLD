using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{

 

    public class clsCar
    {

        

        enum enMode { Update = 0, AddNew = 1 };
        enMode _Mode=enMode.AddNew;

        public int CarID { get; set; }
        public int CarTypeID { get; set; }
        public clsCarTypes CarTypeInfo; 
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Color { get; set; }
        public string Engine { get; set; }
        public clsTransmissionTypes.enTransmissionType TransmissionTypeID { get; set; }
        public clsTransmissionTypes TransmissionTypeInfo; 
        public int UserIDCreated { get; set; }
        public clsUser UserInfo;
        public DateTime DateOfCreated { get; set; }


        public int TotalCars = 1;
        public clsCar()
        {
            this.CarID = -1;
            this.CarTypeID = 1;
            this.Model = "";
            this.Year = DateTime.Now;
            this.Color = "";
            this.Engine = "";
            this.TransmissionTypeID = clsTransmissionTypes.enTransmissionType.DCT;
            this.UserIDCreated = -1;
            this.DateOfCreated = DateTime.Now;

          


            _Mode = enMode.AddNew;
        }

    


        
   

        private clsCar(int carID, int carTypeID, string model, DateTime year,
            string color, string engine, clsTransmissionTypes.enTransmissionType transmissionTypeID,
            int userIDCreated, DateTime dateOfCreated)
        {
            this.CarID = carID;
            this.CarTypeID = carTypeID;
            this.Model = model;
            this.Year = year;
            this.Color = color;
            this.Engine = engine;
            this.TransmissionTypeID = transmissionTypeID;
            this.UserIDCreated = userIDCreated;
            this.DateOfCreated = dateOfCreated;


            CarTypeInfo = clsCarTypes.Find(this.CarTypeID);
            TransmissionTypeInfo = clsTransmissionTypes.Find(this.TransmissionTypeID);
            UserInfo = clsUser.FindByUserID(this.UserIDCreated);

            _Mode = enMode.Update;
        }

        private bool _AddNewCarAvailability()
        {
            // here whrn the car is Add Successfully will add TotalCarsHave

            clsCarAvailability CarAvailability = new clsCarAvailability();
            CarAvailability.CarID = this.CarID;
            CarAvailability.TotalCars = this.TotalCars;
            return (CarAvailability.Save());
        }

        private bool _AddNewCar()
        {
            this.CarID = clsCarData.AddNewCar(this.CarTypeID,this.Model,
                this.Year,this.Color,this.Engine,(int)this.TransmissionTypeID,
                 this.UserIDCreated,this.DateOfCreated);


            return (this.CarID != -1);

        }


        private bool _UpdateCar()
        {
            return (clsCarData.UpdateCar(this.CarID, this.CarTypeID,this.Model,
                this.Year,this.Color,this.Engine,(int)this.TransmissionTypeID,
                this.UserIDCreated,this.DateOfCreated));

        }

        public static DataTable GetAllCars()
        {
            return clsCarData.GetAllCars();
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCar())
                    {
                        _AddNewCarAvailability();


                        _Mode = enMode.Update;
                        //onCarCreted(new CarEventArgs(this.CarID));
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateCar();


            }
            return false;
        }



        public static clsCar GetCarInfoByID(int CarID)
        {
           
            int CarTypeID = -1;
            string Model = "";
            DateTime Year = DateTime.Now;
            string Color = "";
            string Engine = "";
            int TransmissionTypeID =-1;

            int UserIDCreated = -1;
            DateTime DateOfCreated = DateTime.Now;

            if (clsCarData.GetCarInfoByID(CarID,ref CarTypeID, ref Model, ref Year,
               ref Color, ref Engine, ref TransmissionTypeID, ref UserIDCreated, ref DateOfCreated))
            {
                return new clsCar(CarID, CarTypeID, Model, Year, Color, Engine,
                    (clsTransmissionTypes.enTransmissionType) TransmissionTypeID, UserIDCreated, DateOfCreated);
            }

            return null;
        }

        //here if the car return from the customer will incerse of car Avaliave
        //if the customer rent car will decrease the car that we have
        //public  bool UpdateCarNumber(bool Increase=false)
        //{

        //    if (Increase == true)
        //    {
        //        return clsCarData.UpdateCarNumber(this.CarID, ++this.TotalCars);
        //    }
        //    else
        //    {
        //        return clsCarData.UpdateCarNumber(this.CarID, --this.TotalCars);

        //    }
        //}


        public static clsCar GetCarInfoByModel(string Model)
        {

            int CarID = -1;
            int CarTypeID = -1;
            DateTime Year = DateTime.Now;
            string Color = "";
            string Engine = "";
            int TransmissionTypeID = -1;
            int UserIDCreated = -1;
            DateTime DateOfCreated = DateTime.Now;

            if (clsCarData.GetCarInfoByModel(ref CarID, ref CarTypeID,  Model, ref Year,
               ref Color, ref Engine, ref TransmissionTypeID, ref UserIDCreated, ref DateOfCreated))
            {
                return new clsCar(CarID, CarTypeID, Model, Year, Color, Engine,
                    (clsTransmissionTypes.enTransmissionType)TransmissionTypeID
                     ,  UserIDCreated, DateOfCreated);
            }

            return null;
        }


    }

   
}
