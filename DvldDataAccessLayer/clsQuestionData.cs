using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataAccessLayer
{
    public class clsQuestionData
    {
        public static DataTable GetAllQuestion()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select * from Questions
                            order by QuestionID asc";

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

        public static int GetAllQuestionCount()
        {
            int RecoredCount = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = "Select Count(*) From Questions";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    RecoredCount = result;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return RecoredCount;
        }
    }
}
