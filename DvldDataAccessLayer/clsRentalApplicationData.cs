using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsRentalApplicationData
    {
        public static DataTable GetAllRentalApplications()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select * from RentalApplications_View order by EndDateTime desc";

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

        public static int AddNewRentalApplications(int CarID, int CustomerID, DateTime StartDateTime, DateTime EndDateTime,
                               string CurrentLocation, int RentTypeID, int CreatedByUserID, DateTime CreatedDate,
                               float PaidFees,DateTime LastStatusDate, byte RentStatus)
        {
            int RentalApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            INSERT INTO RentalApplications
                                       (CarID
                                       ,CustomerID
                                       ,StartDateTime
                                       ,EndDateTime
                                       ,CurrentLocation
                                       ,RentTypeID
                                       ,CreatedByUserID
                                       ,CreatedDate
                                       ,PaidFees
                                       ,LastStatusDate
                                       ,RentStatus)
                                 VALUES
                                       (@CarID
                                       ,@CustomerID
                                       ,@StartDateTime
                                       ,@EndDateTime
                                       ,@CurrentLocation
                                       ,@RentTypeID
                                       ,@CreatedByUserID
                                       ,@CreatedDate
                                       ,@PaidFees
                                       ,@LastStatusDate
                                       ,@RentStatus)
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            command.Parameters.AddWithValue("@StartDateTime", StartDateTime);
            command.Parameters.AddWithValue("@EndDateTime", EndDateTime);
            command.Parameters.AddWithValue("@CurrentLocation", CurrentLocation);
            command.Parameters.AddWithValue("@RentTypeID", RentTypeID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@RentStatus", RentStatus);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    RentalApplicationID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return RentalApplicationID;
        }

        public static bool GetRentalApplicationInfoByID(int RentalApplicationID,ref int CarID, ref int CustomerID,
           ref DateTime StartDateTime, ref DateTime EndDateTime,
            ref string CurrentLocation, ref int RentTypeID, ref int CreatedByUserID, ref DateTime CreatedDate,
               ref float PaidFees, ref DateTime LastStatusDate, ref byte RentStatus)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from RentalApplications
                                where RentalApplicationID =@RentalApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CarID = (int)reader["CarID"];
                    CustomerID = (int)reader["CustomerID"];
                    StartDateTime = (DateTime)reader["StartDateTime"];
                    EndDateTime = (DateTime)reader["EndDateTime"];
                    CurrentLocation = (string)reader["CurrentLocation"];
                    RentTypeID = (int)reader["RentTypeID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    RentStatus = (byte)reader["RentStatus"];





                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }


        public static bool UpdateRentalApplication(int RentalApplicationID, int CarID, int CustomerID, DateTime StartDateTime, DateTime EndDateTime,
                               string CurrentLocation, int RentTypeID, int CreatedByUserID, DateTime CreatedDate,
                               float PaidFees, DateTime LastStatusDate, byte RentStatus)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            UPDATE RentalApplications
                                set
                                       CarID=@CarID
                                       ,CustomerID=@CustomerID
                                       ,StartDateTime=@StartDateTime
                                       ,EndDateTime=@EndDateTime
                                       ,CurrentLocation=@CurrentLocation
                                       ,RentTypeID=@RentTypeID
                                       ,CreatedByUserID=@CreatedByUserID
                                       ,CreatedDate=@CreatedDate
                                       ,PaidFees=@PaidFees
                                       ,LastStatusDate=@LastStatusDate
                                       ,RentStatus=@RentStatus
                                Where RentalApplicationID =@RentalApplicationID  ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            command.Parameters.AddWithValue("@CarID", CarID);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            command.Parameters.AddWithValue("@StartDateTime", StartDateTime);
            command.Parameters.AddWithValue("@EndDateTime", EndDateTime);
            command.Parameters.AddWithValue("@CurrentLocation", CurrentLocation);
            command.Parameters.AddWithValue("@RentTypeID", RentTypeID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@RentStatus", RentStatus);




            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }


        public static bool DeleteRentalApplication(int RentalApplicationID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Delete RentalApplications 
                         WHERE RentalApplicationID = @RentalApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowAffected > 0);
        }


        public static bool IsRentalApplicationExist(int RentalApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 FROM RentalApplications
                         WHERE RentalApplicationID = @RentalApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }


        public static bool UpdateStatus(int RentalApplicationID, byte NewStatus)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            UPDATE RentalApplications
                                set
                                       
                                       RentStatus=@RentStatus
                                       ,LastStatusDate=@LastStatusDate
                                Where RentalApplicationID =@RentalApplicationID  ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            command.Parameters.AddWithValue("@RentStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);




            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }

        //DoesPersonHaveActiveApplication
        //GetActiveApplicationID
        //GetActiveApplicationIDForLicenseClass
    }
}
