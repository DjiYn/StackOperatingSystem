using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Utilities;
using System.Text.RegularExpressions;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMachine
    {
        VirtualMemory vMemory;
        VirtualProcessor vProcessor;
        int processIndex;

        public VirtualMachine(PagingMechanism pagingMechanism, int processIndex)
        {
            this.vMemory = new VirtualMemory(pagingMechanism, processIndex);
            this.vProcessor = new VirtualProcessor();
            this.processIndex = processIndex;
        }

        public VirtualMemory getMemory()
        {
            return vMemory;
        }

        public VirtualProcessor getProcessor()
        {
            return vProcessor;
        }

        public void run()
        {
            
            Console.WriteLine("Virtual Machine starts!");

            for(int i = 0; i < 255; i++)
            {
                char[] data = vMemory.readWord("0000".ToCharArray());

                foreach(char c in data)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            
        }


    }
}