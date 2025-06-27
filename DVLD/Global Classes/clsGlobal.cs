
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using static DvldDataBusinessLayer.clsGlobal;
namespace DvldDataBusinessLayer
{
    public class clsGlobal

    {

        public static clsUser CurrentUser;
        const string keyName = @"HKEY_CURRENT_USER\Software\DVLD";



        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                //this will get the current project directory folder.

                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                string filePath = currentDirectory + "\\data.txt";

                if (Username == "" &&  File.Exists(filePath)) {
                    File.Delete(filePath);
                    return true;
                }
            
                string dataToSave = Username + "#//#" + Password;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }


                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"[RememberUsernameAndPassword] An error occurred: {ex.Message}");
                return false;
            }
        }



        public static  bool RememberUsernameAndPasswordToRegistry(string Username, string Password)
        {


            try
            {

                if (Username == "" && DoesRegistryKeyExist())
                {
                    return DelteExisitRegistry();
                     
                }

                Registry.SetValue(keyName, "Username", Username, RegistryValueKind.String);
                Registry.SetValue(keyName, "Password", Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

           


        }


        public static bool GetStoredCredentialFromRegistry(ref string Username, ref string Password)
        {
            try

            {
                if (!DoesRegistryKeyExist())
                {
                    Console.WriteLine($"The registry key '{keyName}' does not exist.");
                    return false;
                }


                Username = Registry.GetValue(keyName, "Username",null).ToString();
                Password = Registry.GetValue(keyName, "Password", null).ToString();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[GetStoredCredential] An error occurred: {ex.Message}");
                return false;
            }
        }


        static private bool DoesRegistryKeyExist()
        {
            try { 
                using (RegistryKey baseKey = Registry.CurrentUser)
                {
                    using (RegistryKey subKey = baseKey.OpenSubKey("SOFTWARE\\DVLD"))
                    {
                        if (subKey == null)
                            return false;

                        object usernameValue = subKey.GetValue("Username");
                        if (usernameValue == null)
                        {
                            // The "Username" value does not exist
                            Console.WriteLine("The 'Username' value does not exist in the registry key.");
                            return false;
                        }
                        return true;
                    }
                }
            }catch (Exception ex)
            {
                return false;
            }
        }

        static bool DelteExisitRegistry()
        {
          


            try
            {
                using (RegistryKey baseKey = Registry.CurrentUser)
                {
                    using (RegistryKey subKey = baseKey.OpenSubKey("SOFTWARE\\DVLD", true))
                    {
                        if(subKey != null) { 
                            subKey.DeleteValue("Username");
                            subKey.DeleteValue("Password");
                            return true;
                        }else
                        {
                            return false;   
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("UnauthorizedAccessException: Run the program with administrative privileges.");
                return false;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();


            string filePath = currentDirectory + "\\data.txt";
            try
            {
                if (File.Exists(filePath)) { 
                    using (StreamReader reader =new StreamReader(filePath))
                    {
                        string line;
                        //it will copy first line to line ♥
                        while((line =reader.ReadLine())!= null)
                        {
                            //the below code i did't understand
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            Username= result[0];
                            Password= result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show($"[GetStoredCredential] An error occurred: {ex.Message}");
                return false;
            }
        }

    }
}
