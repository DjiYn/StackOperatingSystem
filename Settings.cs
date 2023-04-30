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
        public static int rCONTAINSCHARS = 2;
        public static int rPTRSIZE = 0x4 * rCONTAINSCHARS;
        public static int rICSIZE  = 0x2 * rCONTAINSCHARS;
        public static int rSPSIZE  = 0x4 * rCONTAINSCHARS;

        // Supervizor Registers
        public static int sCONTAINSCHARS = 2;
        public static int sPISIZE = 0x1 * sCONTAINSCHARS;
        public static int sSISIZE = 0x1 * sCONTAINSCHARS;
        public static int sTISIZE = 0x1 * sCONTAINSCHARS;

        // Memory

        public static int rBLOCKS       = 0x12E;
        public static int rWORDSINBLOCK = 0xFF;
        public static int rWORDSIZE     = 0x4;
        public static int rMEMORYSIZE = rBLOCKS * rWORDSINBLOCK * rWORDSIZE; // Total RAM in real memory.

        public static int sSUPERVISORMEMORYSTARTSATBLOCK = 0x12C;
        public static int sSUPERVISORMEMORYSTARTS = sSUPERVISORMEMORYSTARTSATBLOCK * rWORDSINBLOCK * rWORDSIZE;

        //------------------------------------------------------------------------------

        // Swap Memory

        public static int ssBLOCKS = 0xFF;
        public static int ssWORDSINBLOCK = 0xFF;
        public static int ssWORDSIZE = 0x4;
        public static int ssSWAPMEMORYSIZE = ssBLOCKS * ssWORDSINBLOCK * ssWORDSIZE; // Total SWAP memory.


        //------------------------------------------------------------------------------
        // Virtual Machine


        // Registers
        public static int vCONTAINSCHARS = 2;
        public static int vSPSIZE = 0x4 * vCONTAINSCHARS;
        public static int vICSIZE = 0x2 * vCONTAINSCHARS;

        // Memory

        public static int vBLOCKS = 0xFF;
        public static int vWORDSINBLOCK = 0xFF;
        public static int vWORDSIZE = 0x4;
        public static int vMEMORYSIZE = vBLOCKS * vWORDSINBLOCK * vWORDSIZE; // Total memory that 1 VM can have.
        public static int vSTACKSTARTSATBLOCK = 0xC8;

        public static int vBLOCKSWITHPAGETABLE = vBLOCKS + 1;

        //------------------------------------------------------------------------------
        // Channel Device

        // Registers
        public static int cCONTAINSCHARS = 2;
        public static int cSTSIZE = 0x1 * cCONTAINSCHARS;
        public static int cDTSIZE = 0x1 * cCONTAINSCHARS;
        public static int cSBSIZE = 0x2 * cCONTAINSCHARS;
        public static int cDBSIZE = 0x2 * cCONTAINSCHARS;
        public static int cBCSIZE = 0x2 * cCONTAINSCHARS;
        public static int cOSSIZE = 0x1 * cCONTAINSCHARS;

        //------------------------------------------------------------------------------
        // Paging Mechanism

        public static int pPAGESIZE = 0xFF;
        public static int pNUMBEROFPAGES = vBLOCKS;
    }
}
