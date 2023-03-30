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

        public VirtualMemory() 
        { 
            this.vMemory = new char[Settings.blockSize*Settings.wordSize];
        }
        public VirtualMemory(char[] progMemory) 
        { 
            this.vMemory = new char[Settings.blockSize*Settings.wordSize];
            this.vMemory = progMemory;
        }

        public char readByte(int address)
        {

            return this.vMemory[address];
        }

        public void writeByte(int address, char simbole)
        {
            this.vMemory[address]= simbole;
        }

        public char[] readMemory()
        {
            return this.vMemory;
        }

        // public void writeMemory(char[] text)
        // {
        //     if(text.Length < Settings.blockSize*Settings.wordSize)
        //     {
        //        this.vMemory = text; 
        //     }
        // }

        public char[] readWordFromStack(char[] SP)
        {
            int SPDecimalValue = Conversion.convertHexToInt(SP);
            char[] data = new char[Settings.wordSize];
            
            for(int i = 0; i < Settings.wordSize; i++)
            {
                data[i] = this.vMemory[SPDecimalValue + i];
            }

            return data;
        }

        public void writeWordToStack(char[] SP, char[] word)
        {
            int SPDecimalValue = Conversion.convertHexToInt(SP);
            
            for (int i = 0; i < Settings.wordSize; i++)
            {
                this.vMemory[SPDecimalValue + i] = word[i];
            }
        }

    }
}
