using StackOperatingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class PagingMechanism
    {
        RealMemory rMemory;

        int pageTableIndex;

        public PagingMechanism(RealMemory rMemory)
        {
            if (rMemory != null)
            {
                this.rMemory = rMemory;
                pageTableIndex = rMemory.getBlockIndex(0x100);
            } 
            else
            {
                this.rMemory = new RealMemory(0);
                pageTableIndex = -1;
            }
        }

        public char[] getRealAddress(int index) // TO DO, only writes to block and not to block -> at word -> at byte (here).
        {
            char[] realAddress = new char[Settings.rPTRSIZE];

            char[] virtualAddress = Conversion.convertIntToHex(index);



            for(int i = 0; i < Settings.rPTRSIZE; i++) 
            {
                realAddress[i] = rMemory.readByte(pageTableIndex + index + i);
            }
            return realAddress;
        }

        public void writeByte(int index, char data)
        {

            char[] realAddress = getRealAddress(index);
            if (realAddress[0] == '1')
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!");
            else if (realAddress[0] == '0')
                rMemory.writeByte(Conversion.convertHexToInt(realAddress), data);
        }

        public char readByte(int index) 
        {
            char[] realAddress = getRealAddress(index);
            if (realAddress[0] == '1')
            {
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!");
                return '?';
            }
            else if (realAddress[0] == '0')
                return rMemory.readByte(Conversion.convertHexToInt(realAddress));
            else
                return '?';
        }



    }
}
