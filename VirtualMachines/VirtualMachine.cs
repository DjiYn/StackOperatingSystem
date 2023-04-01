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
            regSP = new char[Settings.vSPSIZE];
            regIC = new char[Settings.vICSIZE];

            for (int i = 0;  i < regSP.Length; i++)
                this.regSP[i] = '0';

            for (int i = 0; i < this.regIC.Length; i++)
                this.regIC[i] = '0';

            this.vRAM = new VirtualMemory(Settings.vMEMORYSIZE);
            this.vProcessor = new VirtualProcessor();
        }

        public void increaseByOneSP()
        {
            int addOne = Conversion.convertHexToInt(this.regSP);
            addOne = addOne + 1;
            this.regSP = Conversion.convertIntToHex(addOne);
        }

        public void increaseByOneIC()
        {
            int addOne = Conversion.convertHexToInt(this.regIC);
            addOne = addOne + 1;
            this.regIC = Conversion.convertIntToHex(addOne);
        }

        public void writeMemory(char[] memory)
        {
            vRAM.writeMemory(memory);
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

                for (int i = 0; i < loadedProgram.Length; i = i + Settings.wordSize)
                {
                    char[] wordToWrite = new char[Settings.wordSize];
                    for (int j = 0; j < Settings.wordSize; j++)
                    {
                        if (i + j < loadedProgram.Length)
                            wordToWrite[j] = loadedProgram[i + j];
                    }
                    vRAM.writeWordToStack(regSP, wordToWrite);
                    this.increaseByOneSP();
                }
            } else
            {
                Console.WriteLine("File did not load!");
            }
        }
    }
}