using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID,ref int ApplicationID,ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications
                            where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                reader.Close();



            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }



        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(ref int LocalDrivingLicenseApplicationID,  int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications
                            where ApplicationID =@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                //here is problem
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }



        //You Have to do the below query without the view for the practice only for me only
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications_View  
                            order by ApplicationDate Desc";

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


        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications
                           (ApplicationID
                           ,LicenseClassID)
                     VALUES
                           (@ApplicationID
                           ,@LicenseClassID)
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    ApplicationTypeID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return ApplicationTypeID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications
                               SET ApplicationID = @ApplicationID
                                  ,LicenseClassID= @LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                 rowAffected= command.ExecuteNonQuery();

                
               

               
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }


        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM LocalDrivingLicenseApplications
                            WHERE 
                            LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);



            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();





            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }


        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result=false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select top 1 TestResult FROM LocalDrivingLicenseApplications
                            Inner Join TestAppointments ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            Inner Join Tests ON TestAppointments.TestAppointmentID =Tests.TestAppointmentID
                            where (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID 
                            and TestAppointments.TestTypeID=@TestTypeID )
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && bool.TryParse(scalar.ToString(), out bool result))
                {
                    Result= result ; 
                }



            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return Result;
        }


        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsAttend = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select top 1 TestResult FROM LocalDrivingLicenseApplications
                            Inner Join TestAppointments ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            Inner Join Tests ON TestAppointments.TestAppointmentID =Tests.TestAppointmentID
                            where (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestAppointments.TestTypeID=@TestTypeID )
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if(scalar != null)
                {
                    IsAttend = true;
                }
              



            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsAttend;
        }


        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte TotalTrialsPerTest = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select  TotalTrialsPerTest = count(TestID) FROM LocalDrivingLicenseApplications
                            Inner Join TestAppointments ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            Inner Join Tests ON TestAppointments.TestAppointmentID =Tests.TestAppointmentID
                            where (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestAppointments.TestTypeID=@TestTypeID )


                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && byte.TryParse(scalar.ToString(), out byte result))
                {
                    TotalTrialsPerTest = result;
                }




            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return TotalTrialsPerTest;
        }


        //هذا مختلف عن حل الاستاذ شوف وقارن كيف الحل
        //is there any test is (islock=0 false) if 0 mean active
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

    
            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    IsActive = (result > 0);
                }




            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsActive;
        }


        //DeleteLocalDrivingLicenseApplication
        //DoesPassTestType
        //DoesAttendTestType
        //TotalTrialsPerTest
        //IsThereAnActiveScheduledTest


    }
}
