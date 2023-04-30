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

        public char[] getRealAddress(char[] vAddress) 
        {
            char[] realAddress = new char[Settings.rPTRSIZE];

            // Get Page table Index to look through assigned pages word by word.
            int pageTableAddressIndex = rMemory.getBlockIndex(pageTables[0]); // TO DO - change so it uses multiple page tables.

            // Extracting the block address part from virtual address
            char[] virtualBlockIndex = getBlockAddressFromAddress(vAddress);

            // Extracting block address part from virtual address
            char[] virtualWordIndex = getWordAddressFromAddress(vAddress);

            // Extracting byte in word address part from virtual address
            char[] virtualByteInWordIndex = getByteAddressFromAddress(vAddress);


            // Gets a real memory block index from the page table.
            char[] realBlockIndex = new char[Settings.rWORDSIZE];

            for(int i = 0; i < Settings.rWORDSIZE; i++)
                realBlockIndex[i] = rMemory.readByte(pageTableAddressIndex + Conversion.convertHexToInt(virtualBlockIndex) * Settings.rWORDSIZE + i);


            // Create Real Address to return. // TO DO - make it so it dinamically assigns addresses accourding to register size.

            // Assing Block address
            realAddress[0] = realBlockIndex[0];
            realAddress[1] = realBlockIndex[1];
            realAddress[2] = realBlockIndex[2];
            realAddress[3] = realBlockIndex[3];

            // Assign Word Address
            realAddress[4] = virtualWordIndex[0];
            realAddress[5] = virtualWordIndex[1];

            // Assign Byte in Word address
            realAddress[6] = virtualByteInWordIndex[0];
            realAddress[7] = virtualByteInWordIndex[1];


            ////==============================================================
            //// DELETE
            //Console.WriteLine("Virtual address:  ");
            //foreach (char c in vAddress)
            //{
            //    Console.Write(c);
            //}
            //Console.WriteLine();

            //// DELETE
            //Console.WriteLine("Real Block address:  ");
            //foreach (char c in realBlockIndex)
            //{
            //    Console.Write(c);
            //}
            //Console.WriteLine();

            //Console.WriteLine("Real word address:  ");
            //foreach (char c in virtualWordIndex)
            //{
            //    Console.Write(c);
            //}
            //Console.WriteLine();

            //Console.WriteLine("Real byte address:  ");
            //foreach (char c in virtualByteInWordIndex)
            //{
            //    Console.Write(c);
            //}
            //Console.WriteLine();

            //// DELETE
            //Console.WriteLine("Real address:  ");
            //foreach (char c in realAddress)
            //{
            //    Console.Write(c);
            //}
            //Console.WriteLine();

            //// DELETE
            ////==============================================================

            return realAddress; 
        }

        public void writeByte(char[] vAddress, char data)
        {
            char[] realAddress = getRealAddress(vAddress);

            if (realAddress[0] == '1')
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!"); // TO DO, implement swap operation.
            else if (realAddress[0] == '0')
            {
                rMemory.writeByte(getRealAddressIndex(realAddress), data);
            }
                
        }

        public char readByte(char[] vAddress) 
        {
            char[] realAddress = getRealAddress(vAddress);

            if (realAddress[0] == '1')
            {
                Console.WriteLine("NEEDS TO SWAP FIRST AND THEN WRITE!"); // TO DO, implement swap operation.
                return '?';
            }
            else if (realAddress[0] == '0')
            {
                return rMemory.readByte(getRealAddressIndex(realAddress));
            }
            else
                return '?';
        }

        public void writeWord(char[] vAddress, char[] data) // [FF - BLOCKS][FF - WORDS]
        {
            vAddress = fixAddress(vAddress);

            char[] word = new char[Settings.rWORDSIZE];

            for (int i = 0; i < Settings.rWORDSIZE; i++)
            {
                vAddress[vAddress.Length - 1] = i.ToString().ToCharArray()[0];
                writeByte(vAddress, data[i]);
            }
        }

        public char[] readWord(char[] vAddress) // [FF - BLOCKS][FF - WORDS]
        {
            vAddress = fixAddress(vAddress);

            char[] word = new char[Settings.rWORDSIZE];

            for (int i = 0; i < Settings.rWORDSIZE; i++)
            {
                vAddress[vAddress.Length - 1] = i.ToString().ToCharArray()[0];
                word[i] = readByte(vAddress);
            }    
                
            return word;
        }


        // Utilities for Paging mechanism
        public char[] fixAddress(char[] vAddress)
        {
            char[] vAddressFormated = new char[Settings.vSPSIZE];
            vAddressFormated[Settings.vSPSIZE - 1] = '0';
            vAddressFormated[Settings.vSPSIZE - 2] = '0';

            int j = vAddress.Length - 1;
            for (int i = Settings.vSPSIZE - 3; i >= 0; i--)
            {
                if(j >= 0)
                {
                    vAddressFormated[i] = vAddress[j];
                    j--;
                }
                else
                {
                    vAddressFormated[i] = '0';
                }
            }

            return vAddressFormated;
        }

        int getRealAddressIndex(char[] rAddress)
        {
            char[] realBlock = getBlockAddressFromAddress(rAddress);
            char[] realWord = getWordAddressFromAddress(rAddress);
            char[] realByte = getByteAddressFromAddress(rAddress);

            int realBlockIndex = Conversion.convertHexToInt(realBlock);
            int realWordIndex = Conversion.convertHexToInt(realWord);
            int realByteIndex = Conversion.convertHexToInt(realByte);

            int realAddressIndex = rMemory.getByteInWordInBlockIndex(realBlockIndex, realWordIndex, realByteIndex);

            return realAddressIndex;
        }

        char[] getBlockAddressFromAddress(char[] rAddress)
        {
            char[] rBlockAddress = new char[4];
            rBlockAddress[0] = rAddress[0];
            rBlockAddress[1] = rAddress[1];
            rBlockAddress[2] = rAddress[2];
            rBlockAddress[3] = rAddress[3];

            return rBlockAddress;
        }

        char[] getWordAddressFromAddress(char[] rAddress)
        {
            char[] rWordAddress = new char[2];
            rWordAddress[0] = rAddress[4];
            rWordAddress[1] = rAddress[5];

            return rWordAddress;
        }

        char[] getByteAddressFromAddress(char[] rAddress)
        {
            char[] rByteAddress = new char[2];
            rByteAddress[0] = rAddress[6];
            rByteAddress[1] = rAddress[7];

            return rByteAddress;
        }
    }
}
