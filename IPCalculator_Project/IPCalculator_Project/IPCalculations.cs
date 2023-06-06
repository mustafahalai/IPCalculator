using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace IPCalculator_Project
{
    class IPCalculations
    {

       public string ClassName;
    

        public string ClassType(int value)
        {
            if (value >=0 && value <= 127 )
            {
                ClassName = "A";
            }
            else if (value >= 128 && value <=191)
            {
                ClassName = "B";
            }
            else if (value >=192 && value <= 223)
            {
                ClassName = "C";
            }
            else if (value >= 224 && value <= 239)
            {
                ClassName = "D";
            }
            else if (value >= 240 && value <= 255)
            {
                ClassName = "E";
                
            }
            else
            {
                Thread.Sleep(500);
                MessageBox.Show("Value out of Range \n Limit is of 255","Error..!!!",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }

            return this.ClassName;
        }


        string[] array = new string[9];
        string data = "";
        public string Binary(int octet)
        {
         
            data = "";
            int pos = 8;
            int i = 0;

            while (i < 8)
            {
                if ((octet & (1 << i)) != 0)
                {
                    array[pos] = "1";
                }
                else
                {
                    array[pos] = "0";
                }
                pos--;
                i++;

            }

            for (int i2 = 0; i2 < array.Length; i2++)
            {
                data = data + array[i2];
            }
            Array.Clear(array, 0, array.Length);
            return data;
        }


        public string SubnetMask(int mask)
        {
            string[] maskarray = new string[mask];
            data = "";
            string hbData = "";

            int hostbit = 32 - mask;
            for (int i2 = 0; i2 < mask; i2++)
            {
                data = data + "1";
            }

            for (int i1 = 0; i1 < hostbit; i1++)
            {
                hbData = hbData + "0";
            }
            data = data + hbData;

            return data;
        }

        public string ConvertInteger(string value)
        {
            value = Convert.ToString(Convert.ToInt32(value, 2), 10);
            return value;
        }


        public string ChangeBitsZero(int Octet, int octetValue)
        {
            string dataOctet = Binary(Octet);

            data = "";
            dataOctet = dataOctet.Substring(0, dataOctet.Length - octetValue);

            for (int i = 0; i < octetValue; i++)
            {
                dataOctet = dataOctet + "0";
            }
            data = ConvertInteger(dataOctet);
            dataOctet = "";
            
            return data;
        }

        public string ChangeBitsOne(int Octet, int octetValue)
        {
            data = "";
            string dataOctet = Binary(Octet);
            dataOctet = dataOctet.Substring(0, dataOctet.Length - octetValue);

            for (int i = 0; i < octetValue; i++)
            {
                dataOctet = dataOctet + "1";
            }
            data = ConvertInteger(dataOctet);
            dataOctet = "";
            return data;
        }


        public int HostBit(int mask)
        {
            int hostbit = 32 - mask;
            return hostbit;
        }
          
        public int NumberofAddress(int mask)
        {
            int sum;
            sum = 32 - mask;
        
            sum = Convert.ToInt32(Math.Pow(2,sum));
           sum = sum - 2;
            return sum;

        }

        public string IPCombine(string octet1, string octet2, string octet3, string octet4)
        {
            data = octet1 + "." + octet2 + "." + octet3 + "." + octet4;
            return data;
        }

        int hostbit = 0;
        public string FirstAddress_ClassC(int Octet1,int Octet2,int Octet3,int Octet4, int mask)
        {
            data = "";
            hostbit = 32 - mask;
            dataForthOctet = ChangeBitsZero(Octet4, hostbit);

            data = IPCombine(Octet1.ToString(),Octet2.ToString(),Octet3.ToString(),dataForthOctet);
            return data;
        }


        public string LastAddress_ClassC(int Octet1,int Octet2,int Octet3,int Octet4, int mask)
        {
            data = "";
            hostbit = 32 - mask;
            dataForthOctet = ChangeBitsOne(Octet4, hostbit);

            data = IPCombine(Octet1.ToString(), Octet2.ToString(), Octet3.ToString(), dataForthOctet);
            return data;
        }



        string dataForthOctet = "";
        string dataThridOctet = "";
        string dataSecondOctet = "";

        int FourthOctet = 0;
        int ThirdOctet = 0;
        int SecondOctet = 0;
     
        public string FirstAddress_ClassB(int Octet1,int Octet2,int Octet3, int Octet4, int mask)
        {
            dataForthOctet = "";
            dataThridOctet = "";
            data = "";
            if (mask >=24)
            {
                hostbit = 32 - mask;
                FourthOctet = hostbit;
                dataForthOctet = ChangeBitsZero(Octet4, FourthOctet);

                data = IPCombine(Octet1.ToString(),Octet2.ToString(),Octet3.ToString(),dataForthOctet);
            }
            else
            {
                hostbit = 32 - mask;
                int breakvalue = hostbit;
                FourthOctet = 8;

                ThirdOctet = breakvalue - 8;

                dataThridOctet = ChangeBitsZero(Octet3, ThirdOctet);
                dataForthOctet = ChangeBitsZero(Octet4, FourthOctet);

                data = IPCombine(Octet1.ToString(),Octet2.ToString(),dataThridOctet,dataForthOctet);
            }
          
            return data;

        }


        public string LastAddress_ClassB(int Octet1,int Octet2,int Octet3, int Octet4, int mask)
        {

            data = "";

            if (mask >= 24)
            {
                hostbit = 32 - mask;
                FourthOctet = hostbit;
                dataForthOctet = ChangeBitsOne(Octet4, FourthOctet);
                data = IPCombine(Octet1.ToString(), Octet2.ToString(), Octet3.ToString(), dataForthOctet);

            }
            else
            {
                hostbit = 32 - mask;
                int breakvalue = hostbit;
                FourthOctet = 8;
                ThirdOctet = breakvalue - 8;

                dataThridOctet = ChangeBitsOne(Octet3, ThirdOctet);
                dataForthOctet = ChangeBitsOne(Octet4, FourthOctet);
                data = IPCombine(Octet1.ToString(), Octet2.ToString(), dataThridOctet, dataForthOctet);
            }

          
            return data;

        }

       
        public string FirstAddress_ClassA(int octet1,int Octet2, int Octet3, int Octet4, int mask)
        {
            data = "";
            dataForthOctet = "";
            dataThridOctet = "";
            dataSecondOctet = "";
    
            if (mask >16 && mask <= 23)
            {
                FourthOctet = 8;
                hostbit = 32 - mask;
                ThirdOctet = hostbit - 8;
 
                dataForthOctet = ChangeBitsZero(Octet4, FourthOctet);
                dataThridOctet = ChangeBitsZero(Octet3, ThirdOctet);
                data = IPCombine(octet1.ToString(),Octet2.ToString(),dataThridOctet,dataForthOctet);
            }
            else if (mask >= 24 && mask <=32 )
            {
                hostbit = 32 - mask;
                FourthOctet = hostbit;

                dataForthOctet = ChangeBitsZero(Octet4, FourthOctet);
                data = IPCombine(octet1.ToString(),Octet2.ToString(),Octet3.ToString(),dataForthOctet);
            }
            else
            {

                hostbit = 32 - mask;
                FourthOctet = 8;
                ThirdOctet = 8;
                hostbit = hostbit - 8;
                SecondOctet = hostbit - 8;

                dataForthOctet = ChangeBitsZero(Octet4, FourthOctet);
                dataThridOctet = ChangeBitsZero(Octet3, ThirdOctet);
                dataSecondOctet = ChangeBitsZero(Octet2, SecondOctet);

                data = IPCombine(octet1.ToString(),dataSecondOctet,dataThridOctet,dataForthOctet);

            }

            return data;
        }


        public string LastAddress_ClassA(int Octet1,int Octet2, int Octet3, int Octet4, int mask)
        {

            data = "";
            dataForthOctet = "";
            dataThridOctet = "";
            dataSecondOctet = "";


            if (mask > 16 && mask <= 23)
            {
                FourthOctet = 8;
                hostbit = 32 - mask;
                ThirdOctet = hostbit - 8;

                dataForthOctet = ChangeBitsOne(Octet4, FourthOctet);
                dataThridOctet = ChangeBitsOne(Octet3, ThirdOctet);

                data =IPCombine(Octet1.ToString(), Octet2.ToString(),dataThridOctet,dataForthOctet);
            }
            else if (mask >= 24 && mask <= 32)
            {
                hostbit = 32 - mask;
                FourthOctet = hostbit;
               
                dataForthOctet = ChangeBitsOne(Octet4, FourthOctet);
                data =IPCombine(Octet1.ToString(),Octet2.ToString(),Octet3.ToString(),dataForthOctet);

            }
            else
            {
                hostbit = 32 - mask;
                FourthOctet = 8;
                ThirdOctet = 8;
                hostbit = hostbit - 8;
                SecondOctet = hostbit - 8;

                dataForthOctet = ChangeBitsOne(Octet4, FourthOctet);
                dataThridOctet = ChangeBitsOne(Octet3, ThirdOctet);
                dataSecondOctet = ChangeBitsOne(Octet2, SecondOctet);

                data =IPCombine(Octet1.ToString(),dataSecondOctet,dataThridOctet,dataForthOctet);

            }

            return data;
        
        }


        public string ClassFull_FirstAddressC(int octet1, int octet2, int octet3, int octet4)
        {
            data = "";
         
            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            int val4 = 0;
            if (octet1 >= 0 && octet1 <= 127)
            {
        
                val1 = octet1 & 255;
                val2 = octet2 & 0;
                val3 = octet3 & 0;
                val4 = octet4 & 0;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            
            }
            else if (octet1 >= 128 && octet1 <= 191)
            {

                val1 = octet1 & 255;
                val2 = octet2 & 255;
                val3 = octet3 & 0;
                val4 = octet4 & 0;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();

            }
            else if (octet1 >= 192 && octet1 <= 223)
            {
               
                val1 = octet1 & 255;
                val2 = octet2 & 255;
                val3 = octet3 & 255;
                val4 = octet4 & 0;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            }
            else if (octet1 >= 224 && octet1 <= 239)
            {
                val1 = octet1 & 255;
                val2 = octet2 & 255;
                val3 = octet3 & 255;
                val4 = octet4 & 255;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            }
            else
            {
                MessageBox.Show("Something Went Wrong in First Address", "Error..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
            return data;
        }


        public string ClassFull_LastAddressC(int octet1, int octet2, int octet3, int octet4)
        {
            data = "";

            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            int val4 = 0;
            if (octet1 >= 0 && octet1 <= 127)
            {
                
                val1 = octet1 | 0;
                val2 = octet2 | 255;
                val3 = octet3 | 255;
                val4 = octet4 | 255;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();

            }
            else if (octet1 >= 128 && octet1 <= 191)
            { 
              
                val1 = octet1 | 0;
                val2 = octet2 | 0;
                val3 = octet3 | 255;
                val4 = octet4 | 255;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            }
            else if (octet1 >= 192 && octet1 <= 223)
            {
               
                val1 = octet1 | 0;
                val2 = octet2 | 0;
                val3 = octet3 | 0;
                val4 = octet4 | 255;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            }
            else if (octet1 >= 224 && octet1 <= 239)
            {
                
                val1 = octet1 | 0;
                val2 = octet2 | 0;
                val3 = octet3 | 0;
                val4 = octet4 | 0;
                data = val1.ToString() + "." + val2.ToString() + "." + val3.ToString() + "." + val4.ToString();
            }
            else
            {
                MessageBox.Show("Something Went Wrong in Last Address", "Error..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
            return data;
        }

    }
}
