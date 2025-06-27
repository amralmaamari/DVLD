using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Security.Policy;

namespace DvldDataAccessLayer
{
    public class clsApplicationsData
    {
        public static DataTable GetAllApplications()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select * from ApplicationsList_View  order by ApplicationDate desc";

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


        public static int AddNewApplications(int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID ,
                                byte ApplicationStatus,DateTime LastStatusDate, float PaidFees,int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            INSERT INTO Applications
                                       (ApplicantPersonID
                                       ,ApplicationDate
                                       ,ApplicationTypeID
                                       ,ApplicationStatus
                                       ,LastStatusDate
                                       ,PaidFees
                                       ,CreatedByUserID)
                                 VALUES
                                       (@ApplicantPersonID
                                       ,@ApplicationDate
                                       ,@ApplicationTypeID
                                       ,@ApplicationStatus
                                       ,@LastStatusDate
                                       ,@PaidFees
                                       ,@CreatedByUserID)
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    ApplicationID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return ApplicationID;
        }






        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate
                , ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Applications
                                where ApplicationID =@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];




                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }
       
        
        public static bool UpdateApplication(int ApplicationID,  int ApplicantPersonID,  DateTime ApplicationDate
                ,  int ApplicationTypeID,  byte ApplicationStatus,  DateTime LastStatusDate,  float PaidFees,  int CreatedByUserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            UPDATE Applications
                                set
                                       ApplicantPersonID=@ApplicantPersonID
                                       ,ApplicationDate=@ApplicationDate
                                       ,ApplicationTypeID=@ApplicationTypeID
                                       ,ApplicationStatus=@ApplicationStatus
                                       ,LastStatusDate=@LastStatusDate
                                       ,PaidFees=@PaidFees
                                       ,CreatedByUserID=@CreatedByUserID
                                Where ApplicationID =@ApplicationID  ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




            try
            {
                connection.Open();

                 rowAffected = command.ExecuteNonQuery();

               
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }




        public static bool DeleteApplication(int ApplicationID)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM Applications
                         WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowAffected > 0);
        }


        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 FROM Applications
                         WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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



        public static bool DoesPersonHaveActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            //1-New 2-Cancelled 3-Completed

            return (GetActiveApplicationID(ApplicantPersonID, ApplicationTypeID) != 1);
        }




        //this is work if the not Cancelled or Completed
        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)
        {
            //1-New 2-Cancelled 3-Completed
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select ActiveApplicationID= ApplicationID from Applications
                            where ApplicantPersonID =@ApplicantPersonID and
                            ApplicationTypeID= @ApplicationTypeID and
                            ApplicationStatus =1
		                  ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if(scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    ActiveApplicationID = result;
                }
                
               
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ActiveApplicationID;
        }


        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            //1-New 2-Cancelled 3-Completed
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select ActiveApplicationID=Applications.ApplicationID from Applications
                            Inner JOIN  
                            LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID =Applications.ApplicationID
                            where ApplicantPersonID =@ApplicantPersonID and
                            ApplicationTypeID= @ApplicationTypeID and
							LocalDrivingLicenseApplications.LicenseClassID =@LicenseClassID and
                            ApplicationStatus =1
		                  ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    ActiveApplicationID = result;
                }

             


            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ActiveApplicationID;
        }



        public static bool UpdateStatus(int ApplicationID,  byte NewStatus)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"
                            UPDATE Applications
                                set
                                       
                                       ApplicationStatus=@ApplicationStatus
                                       ,LastStatusDate=@LastStatusDate
                                Where ApplicationID =@ApplicationID  ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);        
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
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



       
    }
}
