using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.Devices
{
    class OutputDevice
    {
        string buffer;
        public OutputDevice() 
        {
            buffer = "";
        }

        public void writeByte(char data)
        {
            buffer += data;
        }

        public void run()
        {
            Console.WriteLine(buffer);
            buffer = "";
        }
    }
}
