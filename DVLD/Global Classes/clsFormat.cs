using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD
{
    internal class clsFormat
    {
        public static string DateToShort(DateTime date1)
        {
            return date1.ToString("dd/MMM/yyyy");
        }
    }
}
