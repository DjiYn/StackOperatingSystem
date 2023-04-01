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
        
        char[] vMemory;

        public VirtualMemory(int size) 
        { 
            this.vMemory = new char[size];
        }

        public char readByte(int address)
        {
            return this.vMemory[address];
        }

        public void writeByte(int address, char data)
        {
            this.vMemory[address]= data;
        }

        public char[] readMemory()
        {
            return this.vMemory;
        }

        public void writeMemory(char[] memory)
        {
            this.vMemory = memory;
        }

        public char[] readWordFromStack(char[] SP)
        {
            int SPDecimalValue = Conversion.convertHexToInt(SP);
            char[] data = new char[Settings.vWORDSIZE];
            
            for(int i = 0; i < Settings.vWORDSIZE; i++)
            {
                data[i] = this.vMemory[SPDecimalValue + i];
            }

            return data;
        }

        public void writeWordToStack(char[] SP, char[] word)
        {
            int SPDecimalValue = Conversion.convertHexToInt(SP);

            for (int i = 0; i < Settings.vWORDSIZE; i++)
            {
                this.vMemory[SPDecimalValue + i] = word[i];
            }
        }

    }
}
