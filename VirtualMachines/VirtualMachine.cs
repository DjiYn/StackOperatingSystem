namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMachine
    {
        VirtualMemory vRAM;
        VirtualProcessor vProcessor;

        char[] regSP; // SP - Stack Pointer register
        char[] regIC; // IC - Program Counter register


        public VirtualMachine()
        {
            this.regSP = new char[Settings.SPSIZE];
            this.regIC = new char[Settings.ICSIZE];
            this.regSP[0] = '0';
            this.regIC[0] = '0';

            this.vRAM = new VirtualMemory();
            this.vProcessor = new VirtualProcessor();
        }

        public VirtualMachine(char[] SP, char[] IC, char[] memory)
        {
            // Can make a test if SP, IC registers are bigger size than usual.

            this.regSP = SP;
            this.regIC = IC;

            this.vRAM = new VirtualMemory(memory);
            this.vProcessor = new VirtualProcessor();
        }

        public void setSP(char[] SP) 
        {
            this.regSP = SP;
        }

        public void setIC(char[] IC)
        {
            this.regIC = IC;
        }

        public void loadToMemory(char[] vRAM)
        {
            //this.vRAM.loadMemory(vRAM);
        }

        public void startVirtualMachine()
        {
            
        }

        public void loadFromFile(string fileName)
        {
            fileName = @"..\..\..\Devices\HardDrive.txt";
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                Console.WriteLine(text);
            } else
            {
                Console.WriteLine("File did not load!");
            }
        }

    }
}