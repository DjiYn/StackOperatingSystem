using StackOperatingSystem.VirtualMachines;
using System.Runtime.Intrinsics.X86;

namespace StackOperatingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stack Operating System!");
            VirtualMachine vm = new VirtualMachine();
            vm.loadFromFile("");
        }
    }
}