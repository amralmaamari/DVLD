using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace DvldDataAccessLayer
{
    public class clsQuestionLevelData
    {
        public static DataTable GetAllQuestionLevels()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from QuestionLevels";

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


        public static bool GetQuestionLevelInfoByID(int QuestionLevelID,ref string LevelName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
       
            string query = @"select * from QuestionLevels
                                where QuestionLevelID=@QuestionLevelID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@QuestionLevelID", QuestionLevelID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LevelName =(string) reader["LevelName"];
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }
    }
}
