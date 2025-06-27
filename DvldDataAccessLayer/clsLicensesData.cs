using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsLicensesData
    {

        public static DataTable GetAllLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Licenses";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }catch (Exception ex) { }
            finally { connection.Close(); }

            return dt;
        }


        // Do the Query by your self
        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return dt;
        }


        // u have to write this FUN
        //AddNewLicense
        //UpdateLicense

        public static int AddNewLicense( int ApplicationID,  int DriverID,
               int LicenseClass,  DateTime IssueDate,  DateTime ExpirationDate,
               String Notes, double PaidFees,  bool IsActive,
               byte IssueReason,  int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO Licenses
                           (ApplicationID
                           ,DriverID
                           ,LicenseClass
                           ,IssueDate
                           ,ExpirationDate
                           ,Notes
                           ,PaidFees
                           ,IsActive
                           ,IssueReason
                           ,CreatedByUserID)
                     VALUES
                           (@ApplicationID,
                           @DriverID, 
                           @LicenseClass,
                           @IssueDate,
                           @ExpirationDate, 
                           @Notes, 
                           @PaidFees,
                           @IsActive,
                           @IssueReason, 
                           @CreatedByUserID)
            select  SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", @ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();

                if(scalar != null && int.TryParse(scalar.ToString(),out int result)) {
                    LicenseID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return LicenseID;



        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID,
             int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
             String Notes, float PaidFees, bool IsActive,
             byte IssueReason, int CreatedByUserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE  Licenses
                            SET
                           ApplicationID=@ApplicationID
                           ,DriverID=@DriverID
                           ,LicenseClass=@LicenseClass
                           ,IssueDate=@IssueDate
                           ,ExpirationDate=@ExpirationDate
                           ,Notes=@Notes
                           ,PaidFees=@PaidFees
                           ,IsActive=@IsActive
                           ,IssueReason=@IssueReason
                           ,CreatedByUserID=@CreatedByUserID
                     
                    Where LicenseID=@LicenseID
                                        ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", @ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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




        public static bool GetLicenseInfoByID(int LicenseID,ref int ApplicationID,ref int DriverID,
                ref int LicenseClass,ref DateTime IssueDate,ref DateTime ExpirationDate,
                ref String Notes,ref float PaidFees,ref bool IsActive,
                ref byte IssueReason,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from  Licenses
                            where LicenseID= @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID =(int) reader["ApplicationID"];
                    DriverID = (int) reader["DriverID"];
                    LicenseClass = (int) reader["LicenseClass"];
                    IssueDate = (DateTime) reader["IssueDate"];
                    ExpirationDate = (DateTime) reader["ExpirationDate"];

                    if(reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string) reader["Notes"];

                    }else
                    {
                        Notes = "";
                    }

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                  
                    IsActive = (bool) reader["IsActive"];
                    IssueReason = (byte) reader["IssueReason"];
                    CreatedByUserID = (int) reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFound;


        }



        //the query differnt from the teachrer but the same result
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int ActiveLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Licenses
                        Inner Join Applications  ON Licenses.ApplicationID = Applications.ApplicationID
                        Inner Join People  ON People.PersonID = Applications.ApplicantPersonID
                        where Licenses.LicenseClass=@LicenseClassID and People.PersonID=@PersonID and IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    ActiveLicenseID = result;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return ActiveLicenseID;
        }





        public static bool DeactivateLicense(int LicenseClassID)
        {
            int rowsAffected=0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Licenses
                           SET IsActive =0
                         WHERE LicenseID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                 rowsAffected = command.ExecuteNonQuery();
               

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }
    }
}
