using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOperatingSystem.Utilities;
using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Devices;

namespace StackOperatingSystem.RealMachines
{
    public class ChannelDevice
    {
        char[] regST; // Object to get data from. 1 - User memory, 2 - Supervizor memory, 3 - Outside memory (Hard Drives), 4 - Input device, 5 - Swap memory;
        char[] regDT; // Object to write data to. 1 - User memory, 2 - Supervizor memory, 3 - Output device, 4 - Swap memory;
        char[] regSB; // Block number to get data from.
        char[] regDB; // Block number to write data to.
        char[] regBC; // Number of bytes to copy.
        char[] regOS; // Offset of the position in the block for writing block.

        ArrayList Devices;
        public ChannelDevice() 
        {
            regST = new char[Settings.cSTSIZE];
            regDT = new char[Settings.cDTSIZE];
            regSB = new char[Settings.cSBSIZE];
            regDB = new char[Settings.cDBSIZE];
            regBC = new char[Settings.cBCSIZE];
            regOS = new char[Settings.cOSSIZE];

            Devices = new ArrayList();
        }

        public void XCHG()
        {
            switch(getST())
            {
                case 1: // User memory
                    switch (getDT())
                    {
                        case 1: // User memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 2: // Supervisor memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 3: // Output device
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((OutputDevice)Devices[4]).writeByte(
                                     ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + getOS() + i)
                                );
                            }
                            break;

                        case 4: // Swap memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((SwapMemory)Devices[2]).writeByte(
                                    ((SwapMemory)Devices[2]).getBlockIndex(getDB()) + getOS() + i,
                                    ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;
                    }
                    break;

                case 2: // Supervisor memory
                    switch (getDT())
                    {
                        case 1: // User memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 2: // Supervisor memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 3: // Output device
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((OutputDevice)Devices[4]).writeByte(
                                     ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + getOS() + i)
                                );
                            }
                            break;

                        case 4: // Swap memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((SwapMemory)Devices[2]).writeByte(
                                    ((SwapMemory)Devices[2]).getBlockIndex(getDB()) + getOS() + i,
                                    ((RealMemory)Devices[0]).readByte(((RealMemory)Devices[0]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;
                    }
                    break;

                case 3: // Outside memory
                    switch (getDT())
                    {
                        case 1: // User memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((HardDrive)Devices[1]).readByte()
                                );
                            }
                            break;

                        case 2: // Supervisor memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((HardDrive)Devices[1]).readByte()
                                );
                            }
                            break;

                        case 3: // Output device
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((OutputDevice)Devices[4]).writeByte(
                                    ((HardDrive)Devices[1]).readByte()
                                );
                            }
                            break;

                        case 4: // Swap memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((SwapMemory)Devices[2]).writeByte(
                                    ((SwapMemory)Devices[2]).getBlockIndex(getDB()) + getOS() + i,
                                    ((HardDrive)Devices[1]).readByte()
                                );
                            }
                            break;
                    }
                    break;

                case 4: // Input device
                    break;

                case 5: // Swap memory
                    switch (getDT())
                    {
                        case 1: // User memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((SwapMemory)Devices[2]).readByte(((SwapMemory)Devices[2]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 2: // Supervisor memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((RealMemory)Devices[0]).writeByte(
                                        ((RealMemory)Devices[0]).getBlockIndex(getDB()) + getOS() + i,
                                        ((SwapMemory)Devices[2]).readByte(((SwapMemory)Devices[2]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;

                        case 3: // Output device
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((OutputDevice)Devices[4]).writeByte(
                                     ((SwapMemory)Devices[2]).readByte(((SwapMemory)Devices[2]).getBlockIndex(getSB()) + getOS() + i)
                                );
                            }
                            break;

                        case 4: // Swap memory
                            for (int i = 0; i < getBC(); i++)
                            {
                                ((SwapMemory)Devices[2]).writeByte(
                                    ((SwapMemory)Devices[2]).getBlockIndex(getDB()) + getOS() + i,
                                    ((SwapMemory)Devices[2]).readByte(((SwapMemory)Devices[2]).getBlockIndex(getSB()) + i)
                                );
                            }
                            break;
                    }
                    break;
            }

        }

        public void setDevices(ArrayList objects) 
        { 
            this.Devices = objects;
        }

        int getST()
        {
            return Conversion.convertHexToInt(regST);
        }

        int getDT()
        {
            return Conversion.convertHexToInt(regDT);
        }

        int getSB()
        {
            return Conversion.convertHexToInt(regSB);
        }

        int getDB()
        {
            return Conversion.convertHexToInt(regDB);
        }

        int getBC()
        {
            return Conversion.convertHexToInt(regBC);
        }

        int getOS()
        {
            return Conversion.convertHexToInt(regOS);
        }

        public void setST(int st)
        {
            char[] stToSetTo = Conversion.convertIntToHex(st);
            if (stToSetTo.Length > Settings.cSTSIZE) 
            {
                throw new Exception("Overflowing register");
            } else
            {
                regST = stToSetTo;
            }
        }

        public void setDT(int dt)
        {
            char[] dtToSetTo = Conversion.convertIntToHex(dt);
            if (dtToSetTo.Length > Settings.cDTSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regDT = dtToSetTo;
            }
        }

        public void setSB(int sb)
        {
            char[] sbToSetTo = Conversion.convertIntToHex(sb);
            if (sbToSetTo.Length > Settings.cSBSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regSB = sbToSetTo;
            }
        }

        public void setDB(int db)
        {
            char[] dbToSetTo = Conversion.convertIntToHex(db);
            if (dbToSetTo.Length > Settings.cDBSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regDB = dbToSetTo;
            }
        }

        public void setBC(int bc)
        {
            char[] bcToSetTo = Conversion.convertIntToHex(bc);
            if (bcToSetTo.Length > Settings.cBCSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regBC = bcToSetTo;
            }
        }

        public void setOS(int os)
        {
            char[] osToSetTo = Conversion.convertIntToHex(os);
            if (osToSetTo.Length > Settings.cOSSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regOS = osToSetTo;
            }
        }

    }
}
