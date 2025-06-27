using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsLicenseClassData
    {

        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription
                                            , ref Byte MinimumAllowedAge, ref Byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LicenseClasses
                            where LicenseClassID =@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (Byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (Byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }


        public static bool GetLicenseClassInfoByName(ref int LicenseClassID, string ClassName, ref string ClassDescription
                                           , ref Byte MinimumAllowedAge, ref Byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LicenseClasses
                            where ClassName =@ClassName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (Byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (Byte)reader["DefaultValidityLength"];
                    ClassFees = (float)reader["ClassFees"];
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }



        public static DataTable GetAllLicenseClasses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from LicenseClasses order by ClassName";

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


        public static int AddNewLicenseClass(string ClassName, string ClassDescription,
                byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO LicenseClasses
                           (ClassName
                           ,ClassDescription
                           ,MinimumAllowedAge
                           ,DefaultValidityLength
                           ,ClassFees)
                     VALUES
                           (@ClassName
                           ,@ClassDescription
                           ,@MinimumAllowedAge
                           ,@DefaultValidityLength
                           ,@ClassFees)

                     select SCOPE_IDENTITY();"               ;

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);



            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    LicenseClassID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return LicenseClassID;
        }




        public static bool UpdateLicenseClass(int LicenseClassID,string ClassName, string ClassDescription,
               byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE LicenseClasses
                       SET ClassName = @ClassName
                          ,ClassDescription =@ClassDescription
                          ,MinimumAllowedAge = @MinimumAllowedAge
                          ,DefaultValidityLength =@DefaultValidityLength
                          ,ClassFees = @ClassFees
                     WHERE LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);



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
