using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.Utilities
{
    static class Conversion
    {
        public static int convertHexToInt(char[] text)
        {
            int value = Convert.ToInt32(text.ToString(), 16);

            return value;
        }

        public static char[] convertIntToHex(int number)
        {
            char[] value = string.Format("{0:x}", number).ToUpper().ToCharArray();

            return value;
        }
    }
}
