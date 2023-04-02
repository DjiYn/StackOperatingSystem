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
        public SwapMemory(int size) 
        { 
            sMemory = new char[size];
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
            return getBlockIndex(indexBlock) + wordIndex;
        }

        public int getByteInWordInBlockIndex(int indexBlock, int wordIndex, int byteIndex)
        {
            return getWordInBlockIndex(indexBlock, wordIndex) + byteIndex;
        }
    }
}
