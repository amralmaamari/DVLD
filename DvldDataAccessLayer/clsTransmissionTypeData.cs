using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsTransmissionTypeData
    {
        public static DataTable GetAllTransmissionTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = "select * from TransmissionTypes;";
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


        public static int AddNewTransmissionType(string TransmissionTypeName)
        {
            int TransmissionTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO TransmissionTypes
                           (Name
                           )
                     VALUES
                           (@TransmissionTypeName
                        )
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TransmissionTypeName", TransmissionTypeName);
           


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    TransmissionTypeID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return TransmissionTypeID;
        }

        public static bool UpdateTransmissionType(int TransmissionTypeID, string TransmissionTypeName)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE TransmissionTypes  SET Name = @TransmissionTypeName  
                            
                            WHERE TransmissionTypeID=@TransmissionTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);
            command.Parameters.AddWithValue("@TransmissionTypeName", TransmissionTypeName);
         

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





        public static bool GetTransmissionTypeInfoByID(int TransmissionTypeID,ref string TransmissionTypeName)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from TransmissionTypes 
                            where TransmissionTypeID =@TransmissionTypeID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TransmissionTypeID", TransmissionTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    TransmissionTypeName = (string)reader["Name"];
                   
             
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }


        public static bool GetTransmissionTypeInfoByName(ref int TransmissionTypeID,  string TransmissionTypeName)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from TransmissionTypes 
                            where Name =@TransmissionTypeName ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TransmissionTypeName", TransmissionTypeName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    TransmissionTypeID = (int)reader["TransmissionTypeID"];


                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }
    }
}
