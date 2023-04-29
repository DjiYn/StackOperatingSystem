using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Utilities;
using System.Text.RegularExpressions;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMachine
    {
        VirtualMemory vRAM;
        VirtualProcessor vProcessor;

        public VirtualMachine(PagingMechanism pagingMechanism)
        {
            this.vRAM = new VirtualMemory(pagingMechanism);
            this.vProcessor = new VirtualProcessor();
        }

        public void run()
        {
            Console.WriteLine("Virtual Machine starts!");

            for(int i = 0; i < 255; i++)
            {
                Console.Write(vRAM.readByte(i));
            }
            
        }


    }
}