using StackOperatingSystem.RealMachines;
using StackOperatingSystem.Utilities;
using System.Text.RegularExpressions;

namespace StackOperatingSystem.VirtualMachines
{
    public class VirtualMachine
    {
        VirtualMemory vMemory;
        VirtualProcessor vProcessor;
        int processIndex;

        public VirtualMachine(PagingMechanism pagingMechanism, int processIndex)
        {
            this.vProcessor = new VirtualProcessor();
            this.processIndex = processIndex;
            this.vMemory = new VirtualMemory(pagingMechanism, processIndex, vProcessor);
            

            char[] stackStartAddress = new char[Settings.vSPSIZE];
            char[] blockNumber =  Conversion.convertIntToHex(Settings.vSTACKSTARTSATBLOCK);
            for(int i = 0; i < Settings.vSPSIZE; i++)
                stackStartAddress[i] = '0';
            stackStartAddress[Settings.vSPSIZE - 4] = blockNumber[0];
            stackStartAddress[Settings.vSPSIZE - 3] = blockNumber[1];

            vProcessor.setSP(stackStartAddress); // Stack will be from 200 block
        }

        public VirtualMemory getMemory()
        {
            return vMemory;
        }

        public VirtualProcessor getProcessor()
        {
            return vProcessor;
        }

        public void run()
        {
            loadDataSegment();
            loadCodeSegment(); // TO DO - if inicialized second time from SWAP does not need to reload everything, just load registers.
            Console.WriteLine("Virtual Machine starts!");

            while(true)
            {
                char[] data = vMemory.readWord(vProcessor.getIC());

                // TO DO - write operations with commands here!

                Console.WriteLine("IC POINTS TO THIS COMMAND:  ");
                foreach (char c in data)
                {
                    Console.Write(c);
                }
                Console.WriteLine();

                if ((new string(data)) == "HALT")
                {
                    break;
                }

                int ICIndex = Conversion.convertHexToInt(vProcessor.getIC()) + 1;
                vProcessor.setIC(Conversion.convertIntToHex(ICIndex));
            }
            
        }

        public void loadCodeSegment()
        {
            while (true)
            {
                char[] data = vMemory.readWord(vProcessor.getIC());

                int ICIndex = Conversion.convertHexToInt(vProcessor.getIC()) + 1;
                vProcessor.setIC(Conversion.convertIntToHex(ICIndex));

                if ((new string(data)) == "CODE")
                {
                    ICIndex++;
                    vProcessor.setIC(Conversion.convertIntToHex(ICIndex));
                    break;
                }
            }
        }

        public void loadDataSegment()
        {
            char[] vAddress = Conversion.convertIntToHex(0); // Starts from begining.
            string dataSegmentData = "";

            while (true)
            {
                char[] data = vMemory.readWord(vAddress);

                int addressIndex = Conversion.convertHexToInt(vAddress) + 1;
                vAddress = Conversion.convertIntToHex(addressIndex);

                
                if ((new string(data)) == "DATA")
                {
                    addressIndex++;
                    vAddress = Conversion.convertIntToHex(addressIndex);
                    while (true)
                    {
                        data = vMemory.readWord(vAddress);

                        if ((new string(data)) == "CODE")
                        {
                            break;
                        }
                        foreach (char c in data)
                        {
                            dataSegmentData += c;
                        }
                        

                        addressIndex = Conversion.convertHexToInt(vAddress) + 1;
                        vAddress = Conversion.convertIntToHex(addressIndex);

                    }
                }

                if ((new string(data)) == "CODE")
                {
                    break;
                }
            }
            pushDataSegmentToStack(dataSegmentData.ToCharArray());
        }

        public void pushDataSegmentToStack(char[] data)
        {
            if (data.Length % 4 != 0)
                throw new Exception("Data is not in words!");

            for (int i = 0; i < data.Length; i = i + 4)
            {
                char[] wordBuffer = new char[4];
                for (int j = 0; j < 4; j++)
                    wordBuffer[j] = data[i + j];
                vMemory.pushToStack(wordBuffer);
            }
        }
    }
}