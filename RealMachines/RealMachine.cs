using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class RealMachine
    {

        // REGISTERS

        char[] regPTR;
        char[] regIC;
        char[] regSP;


        // SUPERVISOR REGISTERS

        Boolean isSupervisor;
        char[] regPI;
        char[] regSI;
        char[] regTI;

        RealMemory rMemory;
        RealProcessor rProcessor;
        SwapMemory rSwapMemory;


        PagingMechanism rPagingMechanism;
        ChannelDevice rChannelDevice;



        public RealMachine() 
        {
            regPTR = new char[Settings.rPTRSIZE];
            regIC = new char[Settings.rICSIZE];
            regSP = new char[Settings.rSPSIZE];

            isSupervisor = false;

            regPI = new char[Settings.sPISIZE];
            regSI = new char[Settings.sSISIZE];
            regTI = new char[Settings.sTISIZE];

            rMemory = new RealMemory(Settings.rMEMORYSIZE);
            rProcessor = new RealProcessor();
            rSwapMemory = new SwapMemory(Settings.sSWAPMEMORYSIZE);
        }
    }
}
