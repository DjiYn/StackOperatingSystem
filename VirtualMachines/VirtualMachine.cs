using StackOperatingSystem.Utilities;
using System.Text.RegularExpressions;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMachine
    {
        VirtualMemory vRAM;
        VirtualProcessor vProcessor;

        char[] regSP; // SP - Stack Pointer register
        char[] regIC; // IC - Program Counter register


        public VirtualMachine()
        {
            regSP = new char[Settings.vSPSIZE];
            regIC = new char[Settings.vICSIZE];

            for (int i = 0;  i < regSP.Length; i++)
                this.regSP[i] = '0';

            for (int i = 0; i < this.regIC.Length; i++)
                this.regIC[i] = '0';

            this.vRAM = new VirtualMemory(Settings.vMEMORYSIZE);
            this.vProcessor = new VirtualProcessor();
        }

        public void increaseByOneSP()
        {
            int addOne = Conversion.convertHexToInt(this.regSP);
            addOne = addOne + 1;
            this.regSP = Conversion.convertIntToHex(addOne);
        }

        public void increaseByOneIC()
        {
            int addOne = Conversion.convertHexToInt(this.regIC);
            addOne = addOne + 1;
            this.regIC = Conversion.convertIntToHex(addOne);
        }

        public void writeMemory(char[] memory)
        {
            vRAM.writeMemory(memory);
        }

    }
}