using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    class RealMemory
    {
        char[] rMemory;

        public RealMemory(int userMemorySize)
        {
            rMemory = new char[userMemorySize];

            for(int i = 0; i < userMemorySize; i++) // For raw memory test
            {
                rMemory[i] = '?';
            }
        }

        public char readByte(int realMemoryAddress) 
        { 
            return rMemory[realMemoryAddress];
        }

        public void writeByte(int realMemoryAddress, char data)
        {
            rMemory[realMemoryAddress] = data;
        }


        public int getBlockIndex(int index)
        {
            return Settings.rWORDSINBLOCK * Settings.rWORDSIZE * index;
        }

        public int getWordInBlockIndex(int indexBlock, int wordIndex)
        {
            return getBlockIndex(indexBlock) + wordIndex;
        }

        public int getByteInWordInBlockIndex(int indexBlock, int wordIndex, int byteIndex)
        {
            return getWordInBlockIndex(indexBlock, wordIndex) + byteIndex;
        }
    }
}
