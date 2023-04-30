using StackOperatingSystem.Devices;
using StackOperatingSystem.VirtualMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                Console.ReadLine();
                runApplication(programToLaunch);
                Console.WriteLine("Application finished running... PRESS ENTER TO CONTINUE");
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

        internal void runApplication(int programIndex)
        {
            
            switch (programIndex)
            {
                case 1:
                    createVirtualMachine();
                    loadProgramToVirtualMachine(programIndex, virtualMachines.Count - 1);
                    deleteVirtualMachine(virtualMachines.Count - 1);
                    break;
                case 2:
                    break;
                default:
                    throw new Exception("No such application exists in HardDrive!");
            }
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

        public void createVirtualMachine()
        {
            virtualMachines.Add(new VirtualMachine(rPagingMechanism, virtualMachines.Count));
            rPagingMechanism.allocateMemoryForVirtualMachine();
        }

        public void deleteVirtualMachine(int index)
        {
            rPagingMechanism.setCurrentlyUsedByVirtualMachineWithIndex(index);
            rPagingMechanism.clearMemoryForVirtualMachine();
            virtualMachines.RemoveAt(index);
        }

        public void loadProgramToVirtualMachine(int programIndex, int virtualMachineIndex)
        {
            VirtualMachine virtualMachine = ((VirtualMachine)virtualMachines[virtualMachineIndex]);
            virtualMachine.run();
            //

        }
        

        void printAllMemory()
        {
            for (int i = 0; i < Settings.rBLOCKS; i++)
            {
                Console.WriteLine("Block number: " + i);
                rChannelDevice.setST(0x1); //  user mem -> output
                rChannelDevice.setDT(0x3);
                rChannelDevice.setSB(i);
                rChannelDevice.setDB(0);
                rChannelDevice.setBC(0x3FC);
                rChannelDevice.setOS(0x0);
                rChannelDevice.XCHG();
                Console.WriteLine();
            }
        }

    }
}
