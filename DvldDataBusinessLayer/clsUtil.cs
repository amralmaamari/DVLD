using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (Directory.Exists(FolderPath))
            {
                return true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string FileName = sourceFile;
            FileInfo fi= new FileInfo(FileName);
            string extn=fi.Extension;
            return GenerateGUID() + extn;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string DestinationFolder = @"D:\programmingadvices\2\19\DVLD Project\DVLD-People-Images\";

            if (!CreateFolderIfDoesNotExist(DestinationFolder)) {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);

            try{
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                return false;
            }

            sourceFile=destinationFile;
            return true;


        }


        public static string SaveRememberMe(string Username,string Password)
        {
            string Path = "D:\\programmingadvices\\2\\19\\DVLD Project\\currentUser.txt";
            string Delim = "#//#";
            string FullText = Username + Delim + Password;

            return FullText;
            
          
        }
    }
}
