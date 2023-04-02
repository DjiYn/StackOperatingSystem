using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOperatingSystem.Devices
{
    class OutputDevice
    {
        public OutputDevice() { }

        public void writeByte(char data)
        {
            Console.Write(data);
        }
    }
}
