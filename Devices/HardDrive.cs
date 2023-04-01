using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackOperatingSystem.Devices
{
    public class HardDrive
    {
        public HardDrive() { }


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

                for (int i = 0; i < loadedProgram.Length; i = i + Settings.rWORDSIZE)
                {
                    char[] wordToWrite = new char[Settings.rWORDSIZE];
                    for (int j = 0; j < Settings.rWORDSIZE; j++)
                    {
                        if (i + j < loadedProgram.Length)
                            wordToWrite[j] = loadedProgram[i + j];
                    }
                }
            }
            else
            {
                Console.WriteLine("File did not load!");
            }
        }
    }
}
