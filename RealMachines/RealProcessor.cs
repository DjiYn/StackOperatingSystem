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

            isSupervisor = false; // 1 baito registras, kurio reikšmė nusako procesoriaus darbo režimą (vartotojas, supervizorius)

            regPI = new char[Settings.sPISIZE]; // 1 baito programinių pertraukimų registras

            //1.neteisingas adresas(pi = 1)
            //2.neteisingas operacijos kodas(pi = 2)
            //3.neteisingas priskyrimas(pi = 3)
            //4.neužtenka atminties(overflow)(pi = 4)
            //5.dalyba iš nulio(pi= 5)

            regSI = new char[Settings.sSISIZE]; // 1 baito supervizorinių pertraukimų registras

            //1. GD(SI = 1) – Iš įvedimo srauto perskaito žodžių, kiek nurodyta stack’e ir įrašo į SP atminties vietą(adresą) iš kurios nuskaitė.
            //2. PD(SI = 2) – Išsiunčia išvedimui žodžių, kiek nurodyta stack’e, iš stack’o atminties vietos.
            //3. HALT(SI = 3) – Vartotojo programos vykdymo pabaiga.

            regTI = new char[Settings.sTISIZE]; // 1 baito taimerio registras

            this.channelDevice = channelDevice;
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
