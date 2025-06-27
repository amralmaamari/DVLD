using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Policy;
using System.Reflection;

namespace DvldDataAccessLayer
{
    public class clsCarData
    {
        public static DataTable GetAllCars()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select Cars.CarID, CarTypes.Name as CarType, Cars.Model,
                            YEAR(Cars.Year)as Year,Cars.Engine,TransmissionTypes.Name as TransmissionType
                            from Cars
                            Inner Join CarTypes On CarTypes.CarTypeID=Cars.CarTypeID
                            Inner Join TransmissionTypes On TransmissionTypes.TransmissionTypeID=Cars.TransmissionTypeID";
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


        public static int AddNewCar(int CarTypeID,string Model,DateTime Year,string Color,
            string Engine,int TransmissionTypeID,int UserIDCreated,DateTime DateOfCreated)
        {
            int CarID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Cars
                           (CarTypeID,
                            Model,
                            Year,
                            Color,
                            Engine,
                            TransmissionTypeID,
                            UserIDCreated,
                            DateOfCreated
                           )
                     VALUES
                           (@CarTypeID,
                            @Model,
                            @Year,
                            @Color,
                            @Engine,
                            @TransmissionTypeID,
                            @UserIDCreated,
                            @DateOfCreated
                        )
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@Engine", Engine);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);
            command.Parameters.AddWithValue("@UserIDCreated", UserIDCreated);
            command.Parameters.AddWithValue("@DateOfCreated", DateOfCreated);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    CarID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return CarID;
        }


        public static bool UpdateCar(int CarID,int CarTypeID, string Model, DateTime Year, string Color,
            string Engine, int TransmissionTypeID, int UserIDCreated, DateTime DateOfCreated)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Cars  
                                SET CarTypeID = @CarTypeID  
                                , Model = @Model  
                                , Year = @Year  
                                , Color = @Color  
                                , Engine = @Engine  
                                , TransmissionTypeID = @TransmissionTypeID                                  
                                , UserIDCreated = @UserIDCreated  
                                , DateOfCreated = @DateOfCreated  
                            
                            WHERE CarID=@CarID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@Engine", Engine);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);
            command.Parameters.AddWithValue("@UserIDCreated", UserIDCreated);
            command.Parameters.AddWithValue("@DateOfCreated", DateOfCreated);
          


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



        public static bool GetCarInfoByID(int CarID,ref int CarTypeID, ref string Model, ref DateTime Year, 
            ref string Color,
           ref string Engine, ref int TransmissionTypeID,  ref int UserIDCreated, ref DateTime DateOfCreated)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Cars 
                            where CarID =@CarID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@Engine", Engine);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);
            command.Parameters.AddWithValue("@UserIDCreated", UserIDCreated);
            command.Parameters.AddWithValue("@DateOfCreated", DateOfCreated);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarTypeID = (int)reader["CarTypeID"];
                    Model = (string)reader["Model"];
                    Year = (DateTime)reader["Year"];
                    Color = (string)reader["Color"];
                    Engine = (string)reader["Engine"];
                    TransmissionTypeID = (int)reader["TransmissionTypeID"];
                    UserIDCreated = (int)reader["UserIDCreated"];
                    DateOfCreated = (DateTime)reader["DateOfCreated"];


                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }



        public static bool GetCarInfoByModel(ref int CarID, ref int CarTypeID,  string Model, ref DateTime Year,
    ref string Color,
   ref string Engine, ref int TransmissionTypeID,  ref int UserIDCreated, ref DateTime DateOfCreated)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Cars 
                            where Model =@Model ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@Engine", Engine);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);
            command.Parameters.AddWithValue("@UserIDCreated", UserIDCreated);
            command.Parameters.AddWithValue("@DateOfCreated", DateOfCreated);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarID = (int)reader["CarID"];
                    CarTypeID = (int)reader["CarTypeID"];
                    Year = (DateTime)reader["Year"];
                    Color = (string)reader["Color"];
                    Engine = (string)reader["Engine"];
                    TransmissionTypeID = (int)reader["TransmissionTypeID"];
                    UserIDCreated = (int)reader["UserIDCreated"];
                    DateOfCreated = (DateTime)reader["DateOfCreated"];


                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }


       


    }
    }
