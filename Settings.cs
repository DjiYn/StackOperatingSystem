using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem
{
    static class Settings
    {
        // Real Machine


        // Registers
        public static int rPTRSIZE = 0x4;
        public static int rICSIZE  = 0x2;
        public static int rSPSIZE  = 0x4;

        // Supervizor Registers
        public static int sPISIZE = 0x1;
        public static int sSISIZE = 0x1;
        public static int sTISIZE = 0x1;

        // Memory

        public static int rBLOCKS       = 0x12C;
        public static int rWORDSINBLOCK = 0xFF;
        public static int rWORDSIZE     = 0x4;
        public static int rMEMORYSIZE = rBLOCKS*rWORDSINBLOCK*rWORDSIZE; // Total RAM in real memory.

        //------------------------------------------------------------------------------

        // Swap Memory

        public static int SBLOCKS = 0xFF;
        public static int sWORDSINBLOCK = 0xFF;
        public static int sWORDSIZE = 0x4;
        public static int sSWAPMEMORYSIZE = SBLOCKS*sWORDSINBLOCK*sWORDSIZE; // Total SWAP memory.


        //------------------------------------------------------------------------------
        // Virtual Machine


        // Registers
        public static int vSPSIZE = 0x4;
        public static int vICSIZE = 0x2;
    }
}
