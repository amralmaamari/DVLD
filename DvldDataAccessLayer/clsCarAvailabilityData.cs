using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsCarAvailabilityData
    {
        public static DataTable GetAllCarAvailability()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select * from CarAvailability";
            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dataTable;



        }

        public static int AddNewCarAvailability(int CarID, int TotalCars)
        {
            int CarAvailabilityID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO CarAvailability
                           (CarID,
                            TotalCars,
                            CarsRented                           
                           )
                     VALUES
                           (@CarID,
                            @TotalCars,
                            @CarsRented
                        )
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@TotalCars", TotalCars);
            command.Parameters.AddWithValue("@CarsRented", 0);

            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    CarAvailabilityID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return CarAvailabilityID;
        }


        public static bool UpdateCarAvailability(int CarAvailabilityID, int CarID, int TotalCars, int CarsRented)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE CarAvailability  
                                SET CarID = @CarID  
                                , TotalCars = @TotalCars  
                                , CarsRented = @CarsRented  
                             
                            
                            WHERE CarAvailabilityID=@CarAvailabilityID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarAvailabilityID", CarAvailabilityID);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@TotalCars", TotalCars);
            command.Parameters.AddWithValue("@CarsRented", CarsRented);




            try
            {
                connection.Open();

                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    IsUpdate = true;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsUpdate;
        }


        public static bool GetCarAvailabilityInfoByID(int CarAvailabilityID, ref int CarID, ref int TotalCars, ref int CarsRented, ref int AvailableCars)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from CarAvailability 
                            where CarAvailabilityID =@CarAvailabilityID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarAvailabilityID", CarAvailabilityID);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@TotalCars", TotalCars);
            command.Parameters.AddWithValue("@CarsRented", CarsRented);
            command.Parameters.AddWithValue("@AvailableCars", AvailableCars);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarID = (int)reader["CarID"];
                    TotalCars = (int)reader["TotalCars"];
                    CarsRented = (int)reader["CarsRented"];
                    AvailableCars = (int)reader["AvailableCars"];



                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }



        public static bool GetCarAvailabilityInfoByCarID(ref int CarAvailabilityID, int CarID, ref int TotalCars, ref int CarsRented, ref int AvailableCars)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from CarAvailability 
                            where CarID =@CarID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarAvailabilityID", CarAvailabilityID);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@TotalCars", TotalCars);
            command.Parameters.AddWithValue("@CarsRented", CarsRented);
            command.Parameters.AddWithValue("@AvailableCars", AvailableCars);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarAvailabilityID = (int)reader["CarAvailabilityID"];
                    TotalCars = (int)reader["TotalCars"];
                    CarsRented = (int)reader["CarsRented"];
                    AvailableCars = (int)reader["AvailableCars"];



                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }

        public static bool UpdateCarsRented(int CarAvailabilityID, int CarsRented)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE CarAvailability  
                                SET CarsRented = @CarsRented  
                                
                            
                            WHERE CarAvailabilityID=@CarAvailabilityID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarAvailabilityID", CarAvailabilityID);
            command.Parameters.AddWithValue("@CarsRented", CarsRented);




            try
            {
                connection.Open();

                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    IsUpdate = true;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsUpdate;
        }


    }

}
