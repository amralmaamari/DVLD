using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsRentTypeData
    {

        public static DataTable GetAllRentTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = "select * from RentTypes;";
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


        public static int AddNewRentType(string RentTypeName, decimal RentTypeFees ,string RentTypeDescription)
        {
            int RentTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO RentTypes
                           (Name
                           ,Fees
                            ,Description)
                     VALUES
                           (@RentTypeName,
                           ,@RentTypeFees
                           ,@RentTypeDescription)
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentTypeName", RentTypeName);
            command.Parameters.AddWithValue("@RentTypeFees", RentTypeFees);
            command.Parameters.AddWithValue("@RentTypeDescription", RentTypeDescription);


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    RentTypeID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return RentTypeID;
        }

        public static bool UpdateRentType(int RentTypeID, string RentTypeName, decimal RentTypeFees, string RentTypeDescription)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE RentTypes  SET Name = @RentTypeName  ,
                            Fees = @RentTypeFees,
                            Description = @RentTypeDescription
                            WHERE RentTypeID=@RentTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentTypeID", RentTypeID);
            command.Parameters.AddWithValue("@RentTypeName", RentTypeName);
            command.Parameters.AddWithValue("@RentTypeFees", RentTypeFees);
            command.Parameters.AddWithValue("@RentTypeDescription", RentTypeDescription);

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





        public static bool GetRentTypeInfoByID(int RentTypeID, ref string RentTypeName, ref decimal RentTypeFees, ref string RentTypeDescription)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from RentTypes 
                            where RentTypeID =@RentTypeID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentTypeID", RentTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    RentTypeName = (string)reader["Name"];
                    RentTypeFees = Convert.ToInt32(reader["Fees"]);
                    if (reader["Description"] != DBNull.Value)
                    {
                    RentTypeDescription = (string)reader["Description"];

                    }else
                    {
                    RentTypeDescription = "";

                    }
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }


        public static bool GetRentTypeInfoByName(ref int RentTypeID,  string RentTypeName, ref decimal RentTypeFees, ref string RentTypeDescription)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from RentTypes 
                            where Name =@RentTypeName ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentTypeName", RentTypeName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    RentTypeID = (int)reader["RentTypeID"];
                    RentTypeFees = Convert.ToInt32(reader["Fees"]);
                    if (reader["Description"] != DBNull.Value)
                    {
                        RentTypeDescription = (string)reader["Description"];

                    }
                    else
                    {
                        RentTypeDescription = "";

                    }
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }


    }
}
