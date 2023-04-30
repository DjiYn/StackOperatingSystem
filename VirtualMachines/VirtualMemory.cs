using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.VirtualMachines
{
    internal class VirtualMemory
    {

        PagingMechanism pagingMechanism;

        public VirtualMemory(PagingMechanism pagingMechanism) 
        {
            this.pagingMechanism = pagingMechanism;
        }

        public char readByte(int index)
        {
            return pagingMechanism.readByte(Conversion.convertIntToHex(index));
        }

        public void writeByte(int index, char data)
        {
            pagingMechanism.writeByte(Conversion.convertIntToHex(index), data);
        }

    }
}
