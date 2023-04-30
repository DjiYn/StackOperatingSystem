using StackOperatingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualProcessor
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

        public char[] getSP()
        {
            return regSP;
        }

        public void setSP(char[] sp)
        {
            if (sp.Length > Settings.vSPSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regSP = sp;
            }
        }

        public char[] getIC()
        {
            return regIC;
        }

        public void setIC(char[] ic)
        {
            if (ic.Length > Settings.vICSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regIC = ic;
            }
        }

    }
}
