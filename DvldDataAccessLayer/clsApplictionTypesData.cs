using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsApplictionTypesData
    {
        public static DataTable GetAllApplicationTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable =new DataTable();
            string query = "select * from ApplicationTypes;";
            SqlCommand command = new SqlCommand(query,connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }

                reader.Close();

            }catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dataTable;



        }


        public static int AddNewApplicationType(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO ApplicationTypes
                           (ApplicationTypeTitle
                           ,ApplicationFees)
                     VALUES
                           (@ApplicationTypeTitle,
                           ,@ApplicationFees)
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


            try
            {
                connection.Open();

                object scalar = command.ExecuteScalar();

                if (scalar != null && int.TryParse(scalar.ToString(),out int result) )
                {
                    ApplicationTypeID = result;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return ApplicationTypeID;
        }

        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            bool IsUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE ApplicationTypes  SET ApplicationTypeTitle = @ApplicationTypeTitle  ,
                            ApplicationFees = @ApplicationFees 
                            WHERE ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

               int rowAffected = command.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    IsUpdate=true;
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }

            return IsUpdate;
        }


        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,ref string ApplicationTypeTitle,ref float ApplicationFees)
        {
            bool IsFind = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from ApplicationTypes 
                            where ApplicationTypeID =@ApplicationTypeID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFind = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle (reader["ApplicationFees"]);
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsFind;
        }

    }
}
