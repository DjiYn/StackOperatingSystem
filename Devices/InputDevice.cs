using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.Devices
{
    public class InputDevice
    {
        string buffer;
        private StringReader reader;
        public InputDevice() 
        {
            buffer = "";
            reader = new StringReader(buffer);
        }

        public void getData()
        {
            string userInput = Console.ReadLine();
            buffer = userInput;
            reader = new StringReader(buffer);
        }

        public char readByte()
        {
            if (reader.Peek() != -1)
            {
                return (char)reader.Read();
            }
            return '\0';
        }

        public void run()
        {
            getData();
        }
    }
}
