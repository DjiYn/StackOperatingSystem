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
        int processIndex;

        public VirtualMemory(PagingMechanism pagingMechanism, int processIndex) 
        {
            this.pagingMechanism = pagingMechanism;
            this.processIndex = processIndex;
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

        public void pushToStack(char[] SP, char[] data)
        {
            //char[] 
            //pagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(processIndex);
            //pagingMechanism.writeWord(vAddress, data);
        }

    }
}
