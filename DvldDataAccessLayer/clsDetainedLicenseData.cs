using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DvldDataAccessLayer
{
    public class clsDetainedLicenseData
    {

        public static bool GetDetainedLicenseInfoByID( int DetainID,
      ref int LicenseID,
     ref DateTime DetainDate, ref float FineFees,
    ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
     ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from  DetainedLicenses
                            where DetainID= @DetainID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle (reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    else
                        ReleaseDate = DateTime.Now;
                        

                    if (reader["ReleasedByUserID"] != DBNull.Value)                    
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    else                    
                        ReleasedByUserID = -1;



                    if (reader["ReleaseApplicationID"] != DBNull.Value)                    
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    else
                        ReleaseApplicationID = -1;
                    

                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFound;


        }


        public static bool GetDetainedLicenseInfoByLicenseID(ref int DetainID,
      int LicenseID,
     ref DateTime DetainDate, ref float FineFees,
    ref  int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
     ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select TOP 1 * from  DetainedLicenses
                            where LicenseID= @LicenseID order by DetainID desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    else
                        ReleaseDate = DateTime.Now;


                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    else
                        ReleasedByUserID = -1;



                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    else
                        ReleaseApplicationID = -1;


                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFound;


        }
        public static DataTable GetAllDetainedLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from DetainedLicenses_View order by IsReleased,DetainID";
            SqlCommand command = new SqlCommand(query, connection);

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


        public static int AddNewDetainedLicense( int LicenseID,
        DateTime DetainDate, float FineFees,
       int CreatedByUserID)
        {
            int DetainID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO DetainedLicenses
                           (LicenseID
                           ,DetainDate
                           ,FineFees
                           ,CreatedByUserID,
                            IsReleased
                           
                         
                           )
                     VALUES
                           (@LicenseID,
                           @DetainDate, 
                           @FineFees,
                           @CreatedByUserID,
                            0
                           
                                                   
                          )
            select  SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
      
        

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    DetainID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return DetainID;



        }



        public static bool UpdateDetainedLicense(int DetainID, int LicenseID,
        DateTime DetainDate, float FineFees,
       int CreatedByUserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE  DetainedLicenses
                            SET
                           LicenseID=@LicenseID
                           ,DetainDate=@DetainDate
                           ,FineFees=@FineFees
                           ,CreatedByUserID=@CreatedByUserID
                           
                       
                     
                    Where DetainID=@DetainID
                                        ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
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
        public static bool ReleaseDetainedLicense(int DetainID,
                int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE  DetainedLicenses
                            SET
                            IsReleased=1,
                           ReleasedByUserID=@ReleasedByUserID
                           ,ReleaseApplicationID=@ReleaseApplicationID
                            ,ReleaseDate=@ReleaseDate
                           
                    Where DetainID=@DetainID
                                        ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);



            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowAffected > 0);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select IsDetained=1 from DetainedLicenses 
                            where LicenseID = @LicenseID and IsReleased=0;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();

                if(scalar != null )
                {
                    IsDetained = Convert.ToBoolean(scalar);
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsDetained ;
        }

    }
}
