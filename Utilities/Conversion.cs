using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.Utilities
{
    static class Conversion
    {
        public static int convertHexToInt(char[] text)
        {
            string formated = new string(text);

            int value = int.Parse(formated, System.Globalization.NumberStyles.HexNumber);

            return 0; 
        }

        public static char[] convertIntToHex(int number)
        {
            char[] value = string.Format("{0:x}", number).ToUpper().ToCharArray();

            return value;
        }

        internal static int convertHexToInt(string v)
        {
            int value = int.Parse(v, System.Globalization.NumberStyles.HexNumber);
            return value;
        }
    }
}
