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
        char[] sf;//Object to write flags. 1- CF flag, 2- PF parity flag, 3- ZF zero flag, 4- SF (sign flag) , 5- OF overflow.   
         char[] test;
        public VirtualMachine(PagingMechanism pagingMechanism, int processIndex)
        {
            this.vProcessor = new VirtualProcessor();
            this.processIndex = processIndex;
            this.vMemory = new VirtualMemory(pagingMechanism, processIndex, vProcessor);
            sf = new char [Settings.vICSIZE];
            
            

            char[] stackStartAddress = new char[Settings.vSPSIZE];
            char[] blockNumber =  Conversion.convertIntToHex(Settings.vSTACKSTARTSATBLOCK);
            //sf = new char[Settings.vICSIZE];
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
          
            int ICIndex = Conversion.convertHexToInt(vProcessor.getIC());
            int SPIndex = Conversion.convertHexToInt(vProcessor.getSP());

               //vProcessor.setSP(Conversion.convertIntToHex(5));
            //char[] daata = vMemory.readWord(vProcessor.getSP());


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
                else if ((new string(data)) == "PUSH")
                {

                   // vMemory.pushToStack(Conversion.convertIntToHex(5));
                }
                else if ((new string(data)) == "ADDD")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;
                    
                    addd(SPIndex);
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;
                    vProcessor.setSP(Conversion.convertIntToHex(SPIndex));
                    char[] daata = vMemory.readWord(vProcessor.getSP());
                    Console.WriteLine(daata);

                }
                else if ((new string(data)) == "SUBB")
                {            
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;
                    subb(SPIndex);
                }
                else if ((new string(data)) == "MULL")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    mull(SPIndex);
                }
                else if ((new string(data)) == "DIVV")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    divv(SPIndex);
                }
                else if ((new string(data)) == "NEGG")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    negg(SPIndex);
                }
                else if ((new string(data)) == "ANDD")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    andd(SPIndex);
                }
                 else if ((new string(data)) == "ORRR")
                {
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    orrr(SPIndex);
                }
                else if ((new string(data)) == "NOTT")
                {   
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    nott(SPIndex);
                }
                else if ((new string(data)) == "JMPP")
                {                    
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;

                    jmpp(SPIndex);
                }
                else if ((new string(data)) == "CMPP")
                { 
                    SPIndex = Conversion.convertHexToInt(vProcessor.getSP())-1;
                    cmpp(SPIndex);
                }

                
                ICIndex = Conversion.convertHexToInt(vProcessor.getIC()) + 1;
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

 
        public void flagCheker(int index,int byte_size1,int byte_size2)
        {
            //if(byte_size1>=0)
          //  {
            //if( byt_size1 <  sizeof(regSP [index - 1 ]) &&  byt_size2 <  sizeof(regSP [index - 1 ]) ) 
           //{sf[0] = 1; }//CF
           //if(0 == regSP [index - 1 ])//ZF
           //{ZFF=1}
           // }  
           //if(sizeof(regSP [index - 1 ] ) > 4)//OF
           //{sf[4]=1}
           //if(regSP [index - 1] < 0)//SF
           //{sf[3] = 1}
           //if(sizeof(regSP[index - 1])%2 > 0 )//PF
           //{sf[1] = 1}
            //else{SF[1] = 0}
        }

        public void addd(int index)
        {   
            vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);
            char[] data2 = vMemory.popFromStack();
            Console.WriteLine(data2);
            int data1int = int.Parse(new string(data1));
            int data2int = int.Parse(new string(data2));
 
            int answer = data1int + data2int;
            char[] charArray = answer.ToString().ToCharArray();
            vMemory.pushToStack(Conversion.convertToWord(charArray));
 
           //flagCheker(index,byt_size1,byt_size2);
         
        }
        public void subb(int index)
        {
           vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);
            char[] data2 = vMemory.popFromStack();
            Console.WriteLine(data2);
            int data1int = int.Parse(new string(data1));
            int data2int = int.Parse(new string(data2));
 
            int answer = data1int - data2int;
            char[] charArray = answer.ToString().PadLeft(4, '0').ToCharArray();
            vMemory.pushToStack(charArray);
 
          //flagCheker(index,byt_size1,byt_size2);
    
        }
        public void mull(int index)
        {
            vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);
            char[] data2 = vMemory.popFromStack();
            Console.WriteLine(data2);
            int data1int = int.Parse(new string(data1));
            int data2int = int.Parse(new string(data2));
 
            int answer = data1int * data2int;
            char[] charArray = answer.ToString().PadLeft(4, '0').ToCharArray();
            vMemory.pushToStack(charArray);
             //flagCheker(index,-1,-1);
        }
        public void divv(int index)
        {
             vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);
            char[] data2 = vMemory.popFromStack();
            Console.WriteLine(data2);
            int data1int = int.Parse(new string(data1));
            int data2int = int.Parse(new string(data2));
 
            int answer = data1int / data2int;
            char[] charArray = answer.ToString().PadLeft(4, '0').ToCharArray();
            vMemory.pushToStack(charArray);
             //flagCheker(index,-1,-1); 
        }
        public void negg(int index)
        {
             vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);

            int data1int = int.Parse(new string(data1));
   
 
            int answer = -data1int ;
            char[] charArray = answer.ToString().PadLeft(4, '0').ToCharArray();
            vMemory.pushToStack(charArray);
             //flagCheker(index,-1,-1);
        }
        public void andd(int index)
        {
            vProcessor.setSP(Conversion.convertIntToHex(index));
          // index.ToString().ToCharArray() Console.WriteLine(data1);
            char[] data1 = vMemory.popFromStack();

            char[] data2 = vMemory.popFromStack();

            char[] answer= new char[Settings.vWORDSIZE];
            for(int i = 0 ; i < Settings.vWORDSIZE; ++i)
            {
              answer[i] = (char)(data1[i] & data2[i]);  
            }

            vMemory.pushToStack(answer);
            //flagCheker(index,-1,-1);          
        }
        public void orrr(int index)
        {
       
            //flagCheker(index,-1,-1);
         
        }
        public void nott(int index)//Spinversija
        {
        
            //flagCheker(index,-1,-1);
         
        }
        public void jmpp(int index)
        {
            //vProcessor.setIC(Conversion.convertIntToHex(index));
        }

        public void cmpp(int index)
        { 
            vProcessor.setSP(Conversion.convertIntToHex(index));
            char[] data1 = vMemory.popFromStack();
            
            Console.WriteLine(data1);
            char[] data2 = vMemory.popFromStack();
            Console.WriteLine(data2);
            int data1int = int.Parse(new string(data1));
            int data2int = int.Parse(new string(data2));
 


            if(data1int == data2int)
            {
                sf[2] = '1';
            }

            else if(data1int != data2int)
            {
                sf[2] = '0';
            }
            
            else if(data1int > data2int)
            {
                sf[4] = '0';
                sf[5]='0';
                 Console.WriteLine('>');
            }
             else if(data1int < data2int)
            {
                sf[4] = '1';
                sf[5] = '1';
                 Console.WriteLine('<');
            }
              vProcessor.setIC(Conversion.convertIntToHex(index+1));
        }



    }
}