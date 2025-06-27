using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;

namespace DvldDataAccessLayer
{
    public class clsPeopleData
    {
       public static bool GetPersonInfoByID(int PersonID,ref string NationalNo, ref string FirstName, ref string SecondName
                                            , ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor
                                            , ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID
                                            , ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select * From People
                             Where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string) reader["NationalNo"];
                    FirstName = (string) reader["FirstName"];
                    SecondName = (string) reader["SecondName"];

                    if(reader["ThirdName"] != DBNull.Value)
                    {
                    ThirdName = (string) reader["ThirdName"];
                    }
                    else
                    {

                    ThirdName = "";
                    }
                    
                    LastName = (string) reader["LastName"];
                    DateOfBirth = (DateTime) reader["DateOfBirth"];
                    Gendor = (byte) reader["Gendor"];
                    Address = (string) reader["Address"];
                    Phone = (string) reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                    {
                            Email = (string) reader["Email"];
                    }
                    else
                    {

                        Email = "";
                    }
                    NationalityCountryID = (int) reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                         ImagePath = (string) reader["ImagePath"];
                    }
                    else
                    {

                        ImagePath = "";
                    }



                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(ref int PersonID,  string NationalNo, ref string FirstName, ref string SecondName
                                          , ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor
                                          , ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID
                                          , ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select * From People
                             Where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if(reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];

                    }
                    else
                        Email = "";
                    {
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                  

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {

                        ImagePath = "";
                    }

                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }


        public static int AddNewPerson( string NationalNo,  string FirstName,  string SecondName
                                         ,  string ThirdName,  string LastName,  DateTime DateOfBirth,  byte Gendor
                                         ,  string Address,  string Phone,  string Email,  int NationalityCountryID
                                         ,  string ImagePath)
        {

            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                 VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath)
                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if(ThirdName != "" && ThirdName != null)
            {
            command.Parameters.AddWithValue("@ThirdName", ThirdName);

            }
            else
            {
            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);

            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)            
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
               else
            command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    PersonID = result;
                }
     
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return PersonID;
        }


        public static bool UpdatePerson( int PersonID,string NationalNo, string FirstName, string SecondName
                                       , string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor
                                       , string Address, string Phone, string Email, int NationalityCountryID
                                       , string ImagePath)
        {

            bool isUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE People
                 SET NationalNo = @NationalNo,
                    FirstName = @FirstName,
                     SecondName = @SecondName,
                     ThirdName = @ThirdName,
                     LastName = @LastName,
                     DateOfBirth = @DateOfBirth,
                     Gendor = @Gendor,
                     Address = @Address,
                     Phone = @Phone,
                     Email = @Email,
                     NationalityCountryID = @NationalityCountryID,
                     ImagePath = @ImagePath
                 WHERE PersonID = @PersonID"; ;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);

            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);

            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                connection.Open();
                int rowAffected = command.ExecuteNonQuery();
               if(rowAffected > 0)
                {
                    isUpdate = true;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isUpdate;
        }

        public static DataTable GetAllPeople()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select People.PersonID,People.NationalNo,People.FirstName,People.SecondName,People.ThirdName,People.LastName,
                            CASE WHEN People.Gendor = 0 THEN 'Male' Else 'Famle' End as Gendor,
                            People.DateOfBirth,
                            Countries.CountryName as Nationality
                            ,People.Phone,People.Email
                            from People 
                            Inner Join Countries On Countries.CountryID = People.NationalityCountryID;
                                        ";
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
            catch (Exception ex) {  }
            finally { connection.Close(); }
            return dataTable;

        }

        public static int GetAllPeopleCount()
        {
            int RecoredCount = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "Select Count(*) From People";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if(scalar != null && int.TryParse(scalar.ToString(),out int result))
                {
                    RecoredCount = result;
                }
               
            }
            catch (Exception ex) {  }
            finally { connection.Close(); }
            return RecoredCount;
        }

        public static bool IsPersonExist(int PersonID) {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 From People
                              Where PersonID =@PersonID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    if (result > 0)
                        IsExist = true;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsExist;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 From People
                              Where NationalNo =@NationalNo ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                object scalar = command.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int result))
                {
                    if (result > 0)
                        IsExist = true;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsExist;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowAffeccted = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM People
                              Where PersonID =@PersonID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                rowAffeccted = command.ExecuteNonQuery();
               

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffeccted > 0);
        }
    }
}
