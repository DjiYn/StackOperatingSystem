using StackOperatingSystem.Devices;
using StackOperatingSystem.VirtualMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.RealMachines
{
    public class RealMachine
    {
        PagingMechanism rPagingMechanism;
        ChannelDevice rChannelDevice;

        HardDrive hardDrive;
        InputDevice inputDevice;
        OutputDevice outputDevice;

        RealMemory rMemory;
        SwapMemory rSwapMemory;

        RealProcessor rProcessor;

        ArrayList virtualMachines;

        public RealMachine() 
        {
            
            rChannelDevice = new ChannelDevice();

            hardDrive = new HardDrive();
            inputDevice = new InputDevice();
            outputDevice = new OutputDevice();

            rMemory = new RealMemory(Settings.rMEMORYSIZE);
            rSwapMemory = new SwapMemory(Settings.ssSWAPMEMORYSIZE);

            rPagingMechanism = new PagingMechanism(rMemory);

            rProcessor = new RealProcessor(rChannelDevice);

            ArrayList objects = new ArrayList();
            objects.Add(rMemory);
            objects.Add(hardDrive);
            objects.Add(rSwapMemory);
            objects.Add(inputDevice);
            objects.Add(outputDevice);

            rChannelDevice.setDevices(objects);

            virtualMachines = new ArrayList();
        }

        public void StartStop()
        {
            loadHardDriveDataToSuperVisor();
            while(true)
            {
                int programToLaunch = operatingSystemMenu();
                Console.WriteLine("Launching the application... PRESS ENTER TO CONTINUE");
                runApplication(programToLaunch);
                createVirutualMachine();
                //createVirutualMachine();
                Console.ReadLine();
            }
        }

        public int operatingSystemMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Stack Operating System!");
                Console.WriteLine("********************************");
                Console.WriteLine("1 - Run application: 'SUDEK10PLIUS20'");
                Console.WriteLine("2 - Run application: 'PRG2'");
                Console.WriteLine("0 - Turn off computer!");
                Console.WriteLine("********************************");
                Console.WriteLine("Your selection: ");
                string userInput = Console.ReadLine();
                

                if(userInput == "0")
                {
                    Console.WriteLine("Shutting down the computer...");
                    Environment.Exit(0);
                }
                    

                if (userInput == "1")
                {
                    return 1;
                }
                    

                if (userInput == "2")
                {
                    return 2;
                }
            }
        }

        internal void runApplication(int v)
        {
            //string buffer = "";
            //for(int i = 0; i < 0xFF; i=i+4)
            //{
            //    buffer = rMemory.readByte(Settings.sSUPERVISORMEMORYSTARTS + i).ToString()
            //           + rMemory.readByte(Settings.sSUPERVISORMEMORYSTARTS + i + 1).ToString()
            //           + rMemory.readByte(Settings.sSUPERVISORMEMORYSTARTS + i + 2).ToString()
            //           + rMemory.readByte(Settings.sSUPERVISORMEMORYSTARTS + i + 3).ToString();
            //    Console.WriteLine(buffer);
            //}



        }

        internal void loadHardDriveDataToSuperVisor()
        {
            // LOAD THE APPLICATION TO SUPERVISOR MEMORY
            rChannelDevice.setST(0x3); // HD -> Supervizor memory
            rChannelDevice.setDT(0x2);
            rChannelDevice.setSB(0x0);
            rChannelDevice.setDB(Settings.sSUPERVISORMEMORYSTARTSATBLOCK);
            rChannelDevice.setBC(0x2FF);
            rChannelDevice.setOS(0x0);
            rChannelDevice.XCHG();
        }

        public void test()
        {
            rProcessor.test();
            virtualMachines.Add(new VirtualMachine(rPagingMechanism));
            ((VirtualMachine)virtualMachines[0]).run();
        }

       public void createVirutualMachine()
       {
            rPagingMechanism.allocateMemoryForVirtualMachine();

            rChannelDevice.setST(0x1); //  user mem -> output
            rChannelDevice.setDT(0x3);
            rChannelDevice.setSB(255);
            rChannelDevice.setDB(0);
            rChannelDevice.setBC(0x3FC);
            rChannelDevice.setOS(0x0);
            rChannelDevice.XCHG();

            Console.WriteLine();
            rChannelDevice.setST(0x2); // Supervizor memoryuser mem -> output
            rChannelDevice.setDT(0x1);
            rChannelDevice.setSB(Settings.sSUPERVISORMEMORYSTARTSATBLOCK);
            rChannelDevice.setDB(0x0);
            rChannelDevice.setBC(0x2FF);
            rChannelDevice.setOS(0x0);
            rChannelDevice.XCHG();

            Console.WriteLine();
            rChannelDevice.setST(0x1); //  user mem -> output
            rChannelDevice.setDT(0x3);
            rChannelDevice.setSB(0);
            rChannelDevice.setDB(0);
            rChannelDevice.setBC(0x3FF);
            rChannelDevice.setOS(0x0);
            rChannelDevice.XCHG();

            Console.WriteLine();


            rPagingMechanism.getRealAddress(0xF12);
            rPagingMechanism.readByte(0);
        }
    }
}
