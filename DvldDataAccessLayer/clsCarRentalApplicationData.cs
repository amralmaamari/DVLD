using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsCarRentalApplicationData
    {


        //You Have to do the below query without the view for the practice only for me only
        public static DataTable GetAllCarRentalApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            // string query = @"select * from LocalDrivingLicenseApplications_View  
            //order by ApplicationDate Desc";

             string query = @"select * from CarRentalApplications_View
                                order by ApplicationDate desc";

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


        public static bool GetCarRentalApplicationInfoByID(int CarRentalApplicationID, ref int RentalApplicationID,
            ref int CreatedByUserID,ref string Notes,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from CarRentalApplications
                            where CarRentalApplicationID =@CarRentalApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CarRentalApplicationID", CarRentalApplicationID);
            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    RentalApplicationID = (int)reader["RentalApplicationID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    Notes = (string)reader["Notes"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();



            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }


        public static bool UpdateCarRentalApplication(int CarRentalApplicationID,  int RentalApplicationID,
             int CreatedByUserID,  string Notes,  bool IsActive)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE CarRentalApplications
                               SET RentalApplicationID = @RentalApplicationID
                                  ,CreatedByUserID= @CreatedByUserID
                                  ,Notes= @Notes
                                  ,IsActive= @IsActive
                             WHERE CarRentalApplicationID=@CarRentalApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarRentalApplicationID", CarRentalApplicationID);
            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();





            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }


        public static bool DeleteCarRentalApplication(int CarRentalApplicationID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM CarRentalApplications
                            WHERE 
                            CarRentalApplicationID=@CarRentalApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CarRentalApplicationID", CarRentalApplicationID);



            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();





            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }

        public static int AddNewCarRentalApplications(int RentalApplicationID,
             int CreatedByUserID, string Notes, bool IsActive)
        {
            int CarRentalApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            INSERT INTO CarRentalApplications
                                       (RentalApplicationID
                                       ,CreatedByUserID
                                       ,Notes
                                       ,IsActive
                                       )
                                 VALUES
                                       (@RentalApplicationID
                                       ,@CreatedByUserID
                                       ,@Notes
                                       ,@IsActive
                                       )
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentalApplicationID", RentalApplicationID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@IsActive", IsActive);




            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    CarRentalApplicationID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return CarRentalApplicationID;
        }


        //DeleteLocalDrivingLicenseApplication
        //DoesPassTestType
        //DoesAttendTestType
        //TotalTrialsPerTest
        //IsThereAnActiveScheduledTest
    }
}
