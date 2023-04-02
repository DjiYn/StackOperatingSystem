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


        //public void writeword(int index, char[] data)
        //{
        //    for (int i = 0; i < 4; i++)
        //        rMemory[index + i] = data[i];
        //}

        //public char[] readword(int index)
        //{
        //    char[] word = new char[4];
        //    for (int i = 0; i < 4; i++)
        //        word[i] = rMemory[index + i];
        //    return word;
        //}
    }
}
