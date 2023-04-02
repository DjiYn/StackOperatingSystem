using StackOperatingSystem.Devices;
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

        public RealMachine() 
        {
            rPagingMechanism = new PagingMechanism();
            rChannelDevice = new ChannelDevice();

            hardDrive = new HardDrive();
            inputDevice = new InputDevice();
            outputDevice = new OutputDevice();

            rMemory = new RealMemory(Settings.rMEMORYSIZE);
            rSwapMemory = new SwapMemory(Settings.ssSWAPMEMORYSIZE);

            rProcessor = new RealProcessor(rChannelDevice);

            ArrayList objects = new ArrayList();
            objects.Add(rMemory);
            objects.Add(hardDrive);
            objects.Add(rSwapMemory);
            objects.Add(inputDevice);
            objects.Add(outputDevice);

            rChannelDevice.setDevices(objects);
        }

        public void test()
        {
            rProcessor.test();
        }
    }
}
