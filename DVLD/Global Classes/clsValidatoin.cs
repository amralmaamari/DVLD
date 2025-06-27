using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace DvldDataBusinessLayer
{
    public class clsValidatoin
    {
        public static bool ValidateEmail(string emailAddress)
        {
            var Pattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            Regex regx = new Regex(Pattern);

           return regx.IsMatch(emailAddress);  



           
        }

        public static bool ValidatePhone(string PhoneNumber)
        {
            var Pattern = @"\+?\d[\d -]{8,12}\d";
            Regex regx = new Regex(Pattern);

            return regx.IsMatch(PhoneNumber);




        }



        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }

    }
}
