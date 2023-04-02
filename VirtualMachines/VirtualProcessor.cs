using StackOperatingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackOperatingSystem.VirtualMachines
{
    internal class VirtualProcessor
    {
        char[] regSP; // SP - Stack Pointer register
        char[] regIC; // IC - Program Counter register

        public VirtualProcessor() 
        {
            regSP = new char[Settings.vSPSIZE];
            regIC = new char[Settings.vICSIZE];

            for (int i = 0; i < regSP.Length; i++)
                this.regSP[i] = '0';

            for (int i = 0; i < this.regIC.Length; i++)
                this.regIC[i] = '0';
        }

        public long getSP()
        {
            return Conversion.convertHexToLong(regSP);
        }

        public void setSP(long sp)
        {
            char[] spToSetTo = Conversion.convertIntToHex(sp);
            if (spToSetTo.Length > Settings.rPTRSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regSP = spToSetTo;
            }
        }

        public int getIC()
        {
            return Conversion.convertHexToInt(regIC);
        }

        public void setIC(int ic)
        {
            char[] icToSetTo = Conversion.convertIntToHex(ic);
            if (icToSetTo.Length > Settings.rICSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regIC = icToSetTo;
            }
        }

    }
}
