using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class RealMemory
    {
        char[] rMemory;

        public RealMemory(int userMemorySize)
        {
            rMemory = new char[userMemorySize];

            for(int i = 0; i < userMemorySize; i++) 
            {
                rMemory[i] = '?'; // For raw memory test
            }
        }

        public char readByte(int index) 
        { 
            return rMemory[index];
        }

        public void writeByte(int index, char data)
        {
            rMemory[index] = data;
        }


        public int getBlockIndex(int index)
        {
            return Settings.rWORDSINBLOCK * Settings.rWORDSIZE * index;
        }

        public int getWordInBlockIndex(int indexBlock, int wordIndex)
        {
            return getBlockIndex(indexBlock) + wordIndex * Settings.rWORDSIZE;
        }

        public int getByteInWordInBlockIndex(int indexBlock, int wordIndex, int byteIndex)
        {
            return getWordInBlockIndex(indexBlock, wordIndex) + byteIndex;
        }
    }
}
