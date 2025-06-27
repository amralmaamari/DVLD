using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsCarsTypesData
    {


        public static DataTable GetAllCarTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = "select * from CarTypes;";
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


        public static int AddNewCarType(string CarTypeName)
        {
            int CarTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO CarTypes
                           (Name
                           )
                     VALUES
                           (@CarTypeName
                        )
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarTypeName", CarTypeName);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    CarTypeID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return CarTypeID;
        }

        public static bool UpdateCarType(int CarTypeID, string CarTypeName)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE CarTypes  SET Name = @CarTypeName  
                            
                            WHERE CarTypeID=@CarTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
            command.Parameters.AddWithValue("@CarTypeName", CarTypeName);


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





        public static bool GetCarTypeInfoByID(int CarTypeID, ref string CarTypeName)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from CarTypes 
                            where CarTypeID =@CarTypeID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarTypeID", CarTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarTypeName = (string)reader["Name"];


                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }


        public static bool GetCarTypeInfoByName(ref int CarTypeID,  string CarTypeName)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from CarTypes 
                            where Name =@CarTypeName ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarTypeName", CarTypeName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    CarTypeID = (int)reader["CarTypeID"];


                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }

    }
}
