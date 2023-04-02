using StackOperatingSystem.RealMachines;
using StackOperatingSystem.VirtualMachines;
using System.Runtime.Intrinsics.X86;

namespace StackOperatingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stack Operating System!");
            RealMachine machine = new RealMachine();
            machine.test();
            
        }
    }
}