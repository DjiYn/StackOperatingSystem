using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.VirtualMachines
{
    internal class VirtualProcessor
    {
        char[] regSP; // SP - Stack pointer register
        char[] regIC; // IC - Program counter register


        public VirtualProcessor(char[] SP, char[] IC) 
        {
            this.regSP = SP;
            this.regIC = IC;
        }

        public void setSP(char[] SP)
        {
            this.regSP = SP;
        }

        public void setIC(char[] IC)
        {   
            this.regIC = IC;
        }

    }
}
