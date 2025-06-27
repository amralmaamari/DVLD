using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsTestAppointmentData
    {


        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,ref int TestTypeID,
            ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID
            , ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from TestAppointments
                            where TestAppointmentID =@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle (reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if(reader["RetakeTestApplicationID"] == DBNull.Value){

                    RetakeTestApplicationID = -1;
                    }
                    else
                    {

                    RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    }
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }



        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID,  int TestTypeID, ref int TestAppointmentID,
     ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID
     , ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select Top(1) * from TestAppointments
                    where TestTypeID=@TestTypeID 
                    AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                    order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    IsFound = true;
            
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {

                        RetakeTestApplicationID = -1;
                    }
                    else
                    {

                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    }
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }




        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE
                        (TestTypeID = @TestTypeID)
                        AND(LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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


        // i have to do the same as the view by my self Do IT♥
        public static DataTable GetAllTestAppointments()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from TestAppointments_View";

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


        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID
           , int RetakeTestApplicationID)
        {
            int NewTestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO TestAppointments
                           (TestTypeID
                           ,LocalDrivingLicenseApplicationID
                           ,AppointmentDate
                           ,PaidFees
                           ,CreatedByUserID
                           ,IsLocked
                           ,RetakeTestApplicationID)
                    VALUES
                           (@TestTypeID,
                           @LocalDrivingLicenseApplicationID,
                           @AppointmentDate,
                           @PaidFees,
                           @CreatedByUserID,
                           0,
                           @RetakeTestApplicationID)

                     SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            if (RetakeTestApplicationID == -1)
            {

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            }


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    NewTestAppointmentID = result;
                }

            }

            catch (Exception ex) { }
            finally { connection.Close(); }

            return NewTestAppointmentID;
        }





        public static bool UpdateTestAppointment(int TestAppointmentID ,int TestTypeID, int LocalDrivingLicenseApplicationID, 
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID
            , bool IsLocked, int RetakeTestApplicationID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE TestAppointments
                               SET TestTypeID = @TestTypeID
                                  ,LocalDrivingLicenseApplicationID= @LocalDrivingLicenseApplicationID
                                  ,AppointmentDate= @AppointmentDate
                                  ,PaidFees= @PaidFees
                                  ,CreatedByUserID= @CreatedByUserID
                                  ,IsLocked= @IsLocked
                                  ,RetakeTestApplicationID= @RetakeTestApplicationID
                             WHERE TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if(RetakeTestApplicationID == -1)
            {
            command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);

            }
            else
            {
            command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            }


            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();





            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }



        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM TestAppointments
                            WHERE 
                            TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);



            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();





            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffected > 0);
        }


       //checked if the appoinentment NOT Locked 
        public static bool IsAppointmentLocked(int TestAppointmentID)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select IsLocked=1 from TestAppointments
                            where TestAppointmentID=@TestAppointmentID and  IsLocked=1 ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);




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



        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select TestID from Tests
                            where TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    TestID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return TestID;
        }



       

      
    }
}
