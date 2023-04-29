using StackOperatingSystem.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class PagingMechanism
    {
        RealMemory rMemory;

        List<bool> rOccupiedMemoryBlocks;
        List<int> pageTables;
        int totalNumberVirtualMachines;

        public PagingMechanism(RealMemory rMemory)
        {
            rOccupiedMemoryBlocks = new List<bool>();
            pageTables = new List<int>();
            totalNumberVirtualMachines = 0;

            if (rMemory != null)
            {
                this.rMemory = rMemory;
                
                for(int i = 0; i < Settings.rBLOCKS; i++)
                {
                    rOccupiedMemoryBlocks.Add(false);
                } 
            } 
            else
            {
                this.rMemory = new RealMemory(0);
            }
        }

        public void allocateMemoryForVirtualMachine()
        {
            totalNumberVirtualMachines++;

            List<int> blockIndexToAllocate = new List<int>();

            // Allocating real memory blocks
            for (int i = 0; i < Settings.rBLOCKS && i < Settings.sSUPERVISORMEMORYSTARTSATBLOCK; i++)
            {
                if (rOccupiedMemoryBlocks[i] == false)
                {
                    blockIndexToAllocate.Add(i);
                }
                if (blockIndexToAllocate.Count == Settings.vBLOCKSWITHPAGETABLE)
                    break;
            }

            // TODO - Insert SWAP memory allocation if no RAM left.


            // Filling in paging table and marking blocks as taken.
            if (blockIndexToAllocate.Count == Settings.vBLOCKSWITHPAGETABLE)
            {
                // Adding real address of new VM paging table
                pageTables.Add(blockIndexToAllocate[blockIndexToAllocate.Count - 1]);
                int newPageTableAddress = rMemory.getBlockIndex(pageTables[totalNumberVirtualMachines - 1]);

                for (int i = 0; i < Settings.vBLOCKS; i++)
                {
                    rOccupiedMemoryBlocks[blockIndexToAllocate[i]] = true;

                    char[] address = Conversion.convertIntToHex(blockIndexToAllocate[i]);
                    address = Conversion.convertToWord(address);

                    // Filling in the paging table with allocated block addresses
                    rMemory.writeByte(newPageTableAddress + i * Settings.rWORDSIZE + 0, address[0]);
                    rMemory.writeByte(newPageTableAddress + i * Settings.rWORDSIZE + 1, address[1]);
                    rMemory.writeByte(newPageTableAddress + i * Settings.rWORDSIZE + 2, address[2]);
                    rMemory.writeByte(newPageTableAddress + i * Settings.rWORDSIZE + 3, address[3]);
                }

                // TO DO, implement swap operation.
            }
        }

        public char[] getRealAddress(int index) // TO DO, only writes to block and not to block -> at word -> at byte (here).
        {
            char[] realAddress = new char[Settings.rPTRSIZE];

            char[] virtualAddress = Conversion.convertIntToHex(index);
            virtualAddress = Conversion.convertToSPWord(virtualAddress);

            int pageTableAddress = rMemory.getBlockIndex(pageTables[0]); // TO DO - change so it uses multiple page tables.

            
            char[] virtualBlockIndex = new char[2];
            virtualBlockIndex[0] = virtualAddress[4];
            virtualBlockIndex[1] = virtualAddress[5];


            // Gets a real memory block index from the page table.
            char[] realBlockIndex = new char[Settings.rWORDSIZE];

            for(int i = 0; i < Settings.rWORDSIZE; i++)
                realBlockIndex[i] = rMemory.readByte(pageTableAddress + Conversion.convertHexToInt(virtualBlockIndex) * Settings.rWORDSIZE + i);


            char[] virtualWordIndex = new char[2];
            virtualWordIndex[0] = virtualAddress[6];
            virtualWordIndex[1] = virtualAddress[7];

            if (realBlockIndex[0] == '1')
                realAddress[0] = '1';
            else
                realAddress[0] = '0';

            realAddress[2] = '0';
            realAddress[3] = realBlockIndex[1];
            realAddress[4] = realBlockIndex[2];
            realAddress[5] = realBlockIndex[3];

            realAddress[6] = virtualWordIndex[0];
            realAddress[7] = virtualWordIndex[1];



            // DELETE
            Console.WriteLine("Virtual address:  ");
            foreach (char c in virtualAddress)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            // DELETE
            Console.WriteLine("Real Block address:  ");
            foreach (char c in realBlockIndex)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            Console.WriteLine("Virtual word address:  ");
            foreach (char c in virtualWordIndex)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            // DELETE
            Console.WriteLine("Real address:  ");
            foreach (char c in realAddress)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            return realAddress;
        }

        public void writeByte(int index, char data)
        {

            char[] realAddress = getRealAddress(index);
            if (realAddress[0] == '1')
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!"); // TO DO, implement swap operation.
            else if (realAddress[0] == '0')
            {
                //rMemory.getByteInWordInBlockIndex();
                rMemory.writeByte(Conversion.convertHexToInt(realAddress), data);
            }
                
        }

        public char readByte(int index) 
        {
            char[] realAddress = getRealAddress(index);
            if (realAddress[0] == '1')
            {
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!"); // TO DO, implement swap operation.
                return '?';
            }
            else if (realAddress[0] == '0')
            {
                char[] realBlock = new char[3];
                realBlock[0] = realAddress[3];
                realBlock[1] = realAddress[4];
                realBlock[1] = realAddress[5];

                char[] realWord = new char[2];
                realWord[0] = realAddress[6];
                realWord[1] = realAddress[7];

                int realBlockIndex = Conversion.convertHexToInt(realBlock);
                int realWordIndex = Conversion.convertHexToInt(realWord);

                int realAddressIndex = rMemory.getWordInBlockIndex(realBlockIndex, realWordIndex);

                return rMemory.readByte(realAddressIndex);
            }
            else
                return '?';
        }
    }
}
