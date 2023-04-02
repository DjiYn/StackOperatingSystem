﻿using StackOperatingSystem.Utilities;
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
            //channelDevice.setST(0x3); // Hard drive -> User mem
            //channelDevice.setDT(0x1);
            //channelDevice.setSB(0x0);
            //channelDevice.setBC(0xFF);
            //channelDevice.setDB(0x0);
            //channelDevice.setOS(255);
            //channelDevice.XCHG();

            //channelDevice.setST(0x1); // User mem -> Output
            //channelDevice.setDT(0x3);
            //channelDevice.setSB(0x0);
            //channelDevice.setDB(0x0);
            //channelDevice.setBC(0x50);
            //channelDevice.setOS(0x0);
            //channelDevice.XCHG();

            //channelDevice.setST(0x1); // User mem -> Super mem
            //channelDevice.setDT(0x2);
            //channelDevice.setSB(0x0);
            //channelDevice.setDB(0x25);
            //channelDevice.setBC(0x50);
            //channelDevice.setOS(255);
            //channelDevice.XCHG();


            //channelDevice.setST(0x2); // Super mem -> Output
            //channelDevice.setDT(0x3);
            //channelDevice.setSB(0x25);
            //channelDevice.setDB(0x0);
            //channelDevice.setBC(0x30);
            //channelDevice.setOS(0x0);
            //channelDevice.XCHG();

            channelDevice.setST(0x3); // HD -> Swap
            channelDevice.setDT(0x4);
            channelDevice.setSB(0x0);
            channelDevice.setDB(0x15);
            channelDevice.setBC(0xFF);
            channelDevice.setOS(0x0);
            channelDevice.XCHG();

            channelDevice.setST(0x5); // Swap -> User mem
            channelDevice.setDT(0x1);
            channelDevice.setSB(0x15);
            channelDevice.setDB(0x0);
            channelDevice.setBC(0xFF);
            channelDevice.setOS(0x0);
            channelDevice.XCHG();


            channelDevice.setST(0x1); // User -> Output
            channelDevice.setDT(0x3);
            channelDevice.setSB(0x0);
            channelDevice.setDB(0x0);
            channelDevice.setBC(0xFF);
            channelDevice.setOS(0x0);
            channelDevice.XCHG();

            //char[] regST; // Object to get data from. 1 - User memory, 2 - Supervizor memory, 3 - Outside memory (Hard Drives), 4 - Input device, 5 - Swap memory;
            //char[] regDT; // Object to write data to. 1 - User memory, 2 - Supervizor memory, 3 - Output device, 4 - Swap memory;
            //char[] regSB; // Block number to get data from.
            //char[] regDB; // Block number to write data to.
            //char[] regBC; // Number of bytes to copy.
            //char[] regOS; // Offset of the position in the block for writing block.
        }

        long getPTR()
        {
            return Conversion.convertHexToLong(regPTR);
        }

        int getIC()
        {
            return Conversion.convertHexToInt(regIC);
        }

        long getSP()
        {
            return Conversion.convertHexToLong(regSP);
        }

        void setPTR(long ptr)
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

        void setIC(int ic)
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

        void setSP(long sp)
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

        bool getIsSupervisor()
        {
            return isSupervisor;
        }

        void setIsSupervisor(bool isSupervisor)
        { 
            this.isSupervisor = isSupervisor;
        }

        int getPI()
        {
            return Conversion.convertHexToInt(regPI);
        }

        int getSI()
        {
            return Conversion.convertHexToInt(regSI);
        }

        int getTI()
        {
            return Conversion.convertHexToInt(regTI);
        }

        void setPI(int pi)
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

        void setSI(int si)
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

        void setTI(int ti)
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
