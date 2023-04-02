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
        private string data;
        private StringReader reader;
        public HardDrive() 
        {
            data = "";
            loadData();
            reader = new StringReader(data);
        }

        public void loadData()
        {
            string fileName = @"..\..\..\Devices\HardDrive.txt";

            if (File.Exists(fileName))
            {
                string data = File.ReadAllText(fileName);
                //int endingOfProgram = text.IndexOf("$END");
                //string loadedProgram = text.Substring(0, endingOfProgram);

                // Fixing syntax to prepare to load to memory
                this.data = data.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            }
            else
            {
                Console.WriteLine("File did not load!");
            }
        }

        public char readByte()
        {
            if (reader.Peek() != -1)
            {
                return (char)reader.Read();
            }
            return '\0';
        }
    }
}
