using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsCountryData
    {
        public static DataTable GetAllCountries()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = "Select * From Countries";
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
            catch (Exception ex) { }
            finally { connection.Close(); }
            return dataTable;

        }

   


        public static bool GetCountryInfoByName(ref int CountryID,  string CountryName)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select CountryID From Countries
                             Where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(),out int result))
                {
                    isFound = true;
                    CountryID = result;
                }



            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select CountryName From Countries
                             Where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if(scalar != null)
                {
                    isFound = true;
                    CountryName =scalar.ToString();
                }



            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }


    }
}
