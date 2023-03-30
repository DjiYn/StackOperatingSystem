using StackOperatingSystem.Utilities;
using System.Text.RegularExpressions;

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

            for(int i = 0;  i < this.regSP.Length; i++)
                this.regSP[i] = 'A';

            for (int i = 0; i < this.regIC.Length; i++)
                this.regIC[i] = '0';

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

        public string getSP()
        {
            return new string(this.regSP);
        }

        public void setIC(char[] IC)
        {
            this.regIC = IC;
        }

        public char[] getIC()
        {
            return this.regIC;
        }

        public void increaseByOneSP()
        {
            int addOne = Conversion.convertHexToInt(this.regSP);
            addOne = addOne + 1;
            Console.WriteLine(addOne);
            this.regSP = Conversion.convertIntToHex(addOne);
        }

        public void loadToMemory(char[] vRAM)
        {
            //this.vRAM.loadMemory(vRAM);
        }

        public void startVirtualMachine()
        {
            
        }

        public void loadFromHardDrive()
        {
            
            string fileName = @"..\..\..\Devices\HardDrive.txt";
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                int endingOfProgram = text.IndexOf("$END");
                string loadedProgram = text.Substring(0, endingOfProgram);

                // Fixing syntax to prepare to load to memory
                loadedProgram = loadedProgram.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                Console.WriteLine(loadedProgram);

                for (int i = 0; i < loadedProgram.Length; i = i + Settings.wordSize)
                {
                    char[] wordToWrite = new char[Settings.wordSize];
                    for (int j = 0; j < Settings.wordSize; j++)
                    {
                        if (i + j < loadedProgram.Length)
                            wordToWrite[j] = loadedProgram[i + j];
                    }
                    Console.WriteLine(Conversion.convertHexToInt(this.regSP));
                    vRAM.writeWordToStack(this.regSP, wordToWrite);
                    this.increaseByOneSP();
                }

                
                Console.WriteLine(vRAM.readMemory());
            } else
            {
                Console.WriteLine("File did not load!");
            }
        }

    }
}