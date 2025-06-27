using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Diagnostics;

namespace DvldDataAccessLayer
{
    public class clsUserData
    {


        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password
                                        , ref int Permission, ref bool IsActive)
        {
            bool isFound = false;
            try
            {
                
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"Select * From Users
                                 Where UserID=@UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                Permission = (int)reader["Permission"];
                                IsActive = (bool)reader["IsActive"];


                            }
                        }
                    }



                }
            }







            catch (Exception ex) { }
             
                return isFound;

            
            

            
 }

        public static bool GetUserInfoByPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password
                                     , ref int Permission, ref bool IsActive)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    string query = @"Select * From Users
                             Where PersonID=@PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                Permission = (int)reader["Permission"];
                                IsActive = (bool)reader["IsActive"];




                            }
                        }

                    }


                    
                   
                }
                
            }
            catch (Exception ex) { }            
            return isFound;
        }


        public static bool GetUserInfoByUsernameAndPassword(ref int UserID, ref int PersonID, string UserName, string Password
                                    , ref int Permission, ref bool IsActive)
        {
            bool isFound = false;
            try
            {
                 using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    string query = @"Select * From Users
                                     Where UserName=@UserName and Password=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            UserID = (int)reader["UserID"];
                            PersonID = (int)reader["PersonID"];
                            Permission = (int)reader["Permission"];
                            IsActive = (bool)reader["IsActive"];




                        }
      

                    }
                }
;

          

               
            }
            catch (Exception ex) { }
            finally { }
            return isFound;
        }



        public static int AddNewUser(int PersonID, string UserName, string Password
                                         ,int Permission, bool IsActive)
        {
            int UserID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO Users (PersonID, UserName, Password,Permission, IsActive)
                 VALUES (@PersonID, @UserName, @Password,@Permission, @IsActive)
                  SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@IsActive", IsActive);


                    object scalar = command.ExecuteScalar();
                    
                        if (scalar != null && int.TryParse(scalar.ToString(), out int insertedID))
                        {
                            if (insertedID > 0)
                            {
                                UserID = insertedID;
                            }

                        }

                    


                }



            
                
            }
            catch (Exception ex) { }
            finally {  }
            return UserID;
        }


        public static bool UpdateUser( int UserID,  int PersonID, string UserName, string Password
                                          ,int Permission,  bool IsActive)
        {

            int rowAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();
                string query = @"UPDATE Users
                 SET 
                     PersonID = @PersonID,
                     UserName = @UserName,
                     Password = @Password 
                     Permission = @Permission 
                     IsActive = @IsActive,
                    WHERE UserID = @UserID ;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                 rowAffected = command.ExecuteNonQuery();
                }


            }
               
           

            try
            {
              

            }
            catch (Exception ex) { }
            finally {}
            return (rowAffected > 0);
        }

        // the teacher have different Query check it 
        public static DataTable GetAllUser()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            DataTable dataTable = new DataTable();
            string query = @"select Users.UserID,Users.PersonID ,
                            (People.FirstName +' '+ People.SecondName +' '+ IsNULL(People.ThirdName,'')  +' '+ People.LastName  ) as FullName
                            ,Users.UserName,Users.IsActive 
                            from Users
                            INNER JOIN People ON People.PersonID = Users.PersonID";

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

        public static bool DeleteUser(int UserID)
        {
            int rowAffeccted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM Users
                              Where UserID =@UserID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowAffeccted = command.ExecuteNonQuery();


            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowAffeccted > 0);
        }



        public static bool IsUserExist(int UserID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 From Users
                              Where UserID =@UserID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();

                IsExist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return IsExist;
        }

        public static bool IsUserExist(string UserName)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 From Users
                              Where UserName =@UserName ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

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



     

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select Found=1 From Users
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




        public static bool ChangePassword(int UserID, string NewPassword)
        {

            bool isUpdate = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Users
                 SET 
                     Password = @NewPassword,
                 WHERE UserID = @UserID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@NewPassword", NewPassword);



            try
            {
                connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    isUpdate = true;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isUpdate;
        }








 
    }
}
