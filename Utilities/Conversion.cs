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
        public static int convertHexToInt(char[] array)
        {
            string formated = new string(array);

            int value = int.Parse(formated, System.Globalization.NumberStyles.HexNumber);

            return value; 
        }

        public static long convertHexToLong(char[] array)
        {
            string formated = new string(array);

            long value = long.Parse(formated, System.Globalization.NumberStyles.HexNumber);

            return value;
        }

        public static int convertHexToInt(string array)
        {
            int value = int.Parse(array, System.Globalization.NumberStyles.HexNumber);
            return value;
        }

        public static char[] convertIntToHex(int number)
        {
            char[] value = string.Format("{0:x}", number).ToUpper().ToCharArray();

            return value;
        }

        public static char[] convertIntToHex(long number)
        {
            char[] value = string.Format("{0:x}", number).ToUpper().ToCharArray();

            return value;
        }

    }
}
