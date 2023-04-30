using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class SwapMemory
    {
        char[] sMemory;
        public SwapMemory(int swapMemorySize) 
        { 
            sMemory = new char[swapMemorySize];

            for (int i = 0; i < swapMemorySize; i++) // For raw memory test
            {
                sMemory[i] = '?';
            }
        }

        public char readByte(int index)
        {
            return sMemory[index];
        }

        public void writeByte(int index, char data)
        {
            sMemory[index] = data;
        }

        public int getBlockIndex(int index)
        {
            return Settings.ssWORDSINBLOCK * Settings.ssWORDSIZE * index;
        }

        public int getWordInBlockIndex(int indexBlock, int wordIndex)
        {
            return getBlockIndex(indexBlock) + wordIndex * Settings.ssWORDSIZE;
        }

        public int getByteInWordInBlockIndex(int indexBlock, int wordIndex, int byteIndex)
        {
            return getWordInBlockIndex(indexBlock, wordIndex) + byteIndex;
        }
    }
}
