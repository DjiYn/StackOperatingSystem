using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMemory
    {

        PagingMechanism pagingMechanism;
        VirtualProcessor vProcessor;
        int processIndex;

        public VirtualMemory(PagingMechanism pagingMechanism, int processIndex, VirtualProcessor vProcessor) 
        {
            this.pagingMechanism = pagingMechanism;
            this.processIndex = processIndex;
            this.vProcessor = vProcessor;
        }

        public char readByte(char[] vAddress)
        {
            pagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(processIndex);
            return pagingMechanism.readByte(vAddress);
        }

        public void writeByte(char[] vAddress, char data)
        {
            pagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(processIndex);
            pagingMechanism.writeByte(vAddress, data);
        }

        public char[] readWord(char[] vAddress)
        {
            pagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(processIndex);
            return pagingMechanism.readWord(vAddress);
        }

        public void writeWord(char[] vAddress, char[] data)
        {
            pagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(processIndex);
            pagingMechanism.writeWord(vAddress, data);
        }

        public void pushToStack(char[] data)
        {
            int SPIndex = Conversion.convertHexToInt(vProcessor.getSP()) + 1;

            if (SPIndex == 0xFFFF)
                throw new Exception("Not enougth memory in stack!");

            if (data.Length != Settings.vWORDSIZE)
                throw new Exception("You can only push a word to stack!");

            writeWord(vProcessor.getSP(), data);

            vProcessor.setSP(Conversion.convertIntToHex(SPIndex));
        }

        public char[] popFromStack()
        {
            int SPIndex = Conversion.convertHexToInt(vProcessor.getSP()) -1;

            //if (SPIndex < 0xC800)
              //  throw new Exception("There is nothing inside the Stack!");

            char[] data = readWord(vProcessor.getSP());

            vProcessor.setSP(Conversion.convertIntToHex(SPIndex));

            return data;
        }

    }
}
