using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsCustomerData
    {

        //find the the customer by ID
   
        public static DataTable GetAllCustomer()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select * from Customers_View";
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

        public static int AddNewCustomer(int LicenseID, int CreatedByUserID)
        {

            int CustomerID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Customers (LicenseID, CreatedByUserID, IsActive)
                 VALUES (@LicenseID, @CreatedByUserID, 1)
                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
           
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    CustomerID = result;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return CustomerID;
        }


        public static bool GetCustomerInfoByID(int CustomerID, ref int LicenseID,
            ref int CreatedByUserID,ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Customers
                                Where CustomerID=@CustomerID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            




            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;                    
                    LicenseID =(int) reader["LicenseID"];
                    CreatedByUserID = (int) reader["CreatedByUserID"];
                    IsActive = (bool) reader["IsActive"];
                }
                reader.Close();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }


        //License It should be to-one Customer
        public static bool IsThereCustomerActiveByLicenseID(int LicenseID)
        {
            object scalar=null;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select Found=1 from Customers
                    where LicenseID=@LicenseID and IsActive=1
                    order   by CustomerID desc



                    ";

            SqlCommand command = new SqlCommand(query, connection);
       
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
           




            try
            {
                connection.Open();
                 scalar = command.ExecuteScalar();
                
                

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (scalar != null);
        }


        public static bool DeleteCustomer(int CustomerID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM   Customers
                            
                     
                    Where CustomerID=@CustomerID
                                        ";
            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@CustomerID", CustomerID);


            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowAffected > 0);



        }

        public static bool UpdateCustomer(int CustomerID, bool IsActive)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE  Customers
                            SET
                           
                           IsActive=@IsActive
                           
                     
                    Where CustomerID=@CustomerID
                                        ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);


            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowAffected > 0);



        }
    }
}
