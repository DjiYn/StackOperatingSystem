using StackOperatingSystem.Devices;
using StackOperatingSystem.Utilities;
using StackOperatingSystem.VirtualMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class RealProcessor
    {
        // REGISTERS

        char[] regPTR;
        char[] regIC;
        char[] regSP;


        // SUPERVISOR REGISTERS

        Boolean isSupervisor;
        char[] regPI;
        char[] regSI;
        char[] regTI;

        // Channel Device

        ChannelDevice channelDevice;

        public RealProcessor(ChannelDevice channelDevice) 
        {
            regPTR = new char[Settings.rPTRSIZE];
            regIC = new char[Settings.rICSIZE];
            regSP = new char[Settings.rSPSIZE];

            isSupervisor = false;

            regPI = new char[Settings.sPISIZE];
            regSI = new char[Settings.sSISIZE];
            regTI = new char[Settings.sTISIZE];

            this.channelDevice = channelDevice;
        }

        public void test()
        {
            try
            {
                //channelDevice.setST(0x3); // HD -> Swap
                //channelDevice.setDT(0x4);
                //channelDevice.setSB(0x0);
                //channelDevice.setDB(0x15);
                //channelDevice.setBC(0xFF);
                //channelDevice.setOS(0x10);
                //channelDevice.XCHG();

                //channelDevice.setST(0x5); // Swap -> User mem
                //channelDevice.setDT(0x1);
                //channelDevice.setSB(0x15);
                //channelDevice.setDB(0x30);
                //channelDevice.setBC(0xFF);
                //channelDevice.setOS(0x10);
                //channelDevice.XCHG();


                //channelDevice.setST(0x1); // User -> Supervisor
                //channelDevice.setDT(0x2);
                //channelDevice.setSB(0x30);
                //channelDevice.setDB(Settings.sSUPERVISORMEMORYSTARTSATBLOCK);
                //channelDevice.setBC(0xFF);
                //channelDevice.setOS(0x0);
                //channelDevice.XCHG();


                //channelDevice.setST(0x2); // supervisor -> Output
                //channelDevice.setDT(0x3);
                //channelDevice.setSB(Settings.sSUPERVISORMEMORYSTARTSATBLOCK);
                //channelDevice.setDB(0);
                //channelDevice.setBC(0xFF);
                //channelDevice.setOS(0x0);
                //channelDevice.XCHG();


                //channelDevice.setST(0x4); // User input -> user mem
                //channelDevice.setDT(0x1);
                //channelDevice.setSB(0);
                //channelDevice.setDB(55);
                //channelDevice.setBC(0xF);
                //channelDevice.setOS(0x0);
                //channelDevice.XCHG();

                channelDevice.setST(0x1); //  user mem -> output
                channelDevice.setDT(0x3);
                channelDevice.setSB(0);
                channelDevice.setDB(0);
                channelDevice.setBC(0xF);
                channelDevice.setOS(0x0);
                channelDevice.XCHG();

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            


            //channelDevice.test();
            //char[] regST; // Object to get data from. 1 - User memory, 2 - Supervizor memory, 3 - Outside memory (Hard Drives), 4 - Input device, 5 - Swap memory;
            //char[] regDT; // Object to write data to. 1 - User memory, 2 - Supervizor memory, 3 - Output device, 4 - Swap memory;
            //char[] regSB; // Block number to get data from.
            //char[] regDB; // Block number to write data to.
            //char[] regBC; // Number of bytes to copy.
            //char[] regOS; // Offset of the position in the block for writing block.
        }

        public long getPTR()
        {
            return Conversion.convertHexToLong(regPTR);
        }

        public int getIC()
        {
            return Conversion.convertHexToInt(regIC);
        }

        public long getSP()
        {
            return Conversion.convertHexToLong(regSP);
        }

        public void setPTR(long ptr)
        {
            char[] ptrToSetTo = Conversion.convertIntToHex(ptr);
            if (ptrToSetTo.Length > Settings.rPTRSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regPTR = ptrToSetTo;
            }
        }

        public void setIC(int ic)
        {
            char[] icToSetTo = Conversion.convertIntToHex(ic);
            if (icToSetTo.Length > Settings.rICSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regIC = icToSetTo;
            }
        }

        public void setSP(long sp)
        {
            char[] spToSetTo = Conversion.convertIntToHex(sp);
            if (spToSetTo.Length > Settings.rSPSIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regSP = spToSetTo;
            }
        }

        public bool getIsSupervisor()
        {
            return isSupervisor;
        }

        public void setIsSupervisor(bool isSupervisor)
        { 
            this.isSupervisor = isSupervisor;
        }

        public int getPI()
        {
            return Conversion.convertHexToInt(regPI);
        }

        public int getSI()
        {
            return Conversion.convertHexToInt(regSI);
        }

        public int getTI()
        {
            return Conversion.convertHexToInt(regTI);
        }

        public void setPI(int pi)
        {
            char[] piToSetTo = Conversion.convertIntToHex(pi);
            if (piToSetTo.Length > Settings.sPISIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regPI = piToSetTo;
            }
        }

        public void setSI(int si)
        {
            char[] siToSetTo = Conversion.convertIntToHex(si);
            if (siToSetTo.Length > Settings.sSISIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regSI = siToSetTo;
            }
        }

        public void setTI(int ti)
        {
            char[] tiToSetTo = Conversion.convertIntToHex(ti);
            if (tiToSetTo.Length > Settings.sPISIZE)
            {
                throw new Exception("Overflowing register");
            }
            else
            {
                regTI = tiToSetTo;
            }
        }

    }
}
