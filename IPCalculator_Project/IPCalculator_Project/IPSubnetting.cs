using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IPCalculator_Project
{
    class IPSubnetting
    {
       public DataGridView datagridview = new DataGridView();
        string data;
        IPCalculations ip = new IPCalculations();
        ArrayList List2 = new ArrayList();
        ArrayList list = new ArrayList();
        public DataTable dt = new DataTable();

        int[] networkNumber;
        int arryLength;
        public int bitborrow;
        public int hostbit;
        public int maskbit;
        public int Bitscount;

        int a = 0;
        int b = 0;
        int andVal = 0;

        int andVal4 = 0;
        int andVal3 = 0;
        int andVal2 = 0;


        int Orval2 = 0;
        int Orval3 = 0;
        int Orval4 = 0;
        

        string subnetNormal4;
        string subnetNormal3;
        string subnetNormal2;


        int store = 0;
        int Hbit = 0;


        public string[] NetworkA;
        public string[] BroadcastA;
        public string[] HostA;

        private int NetworkCheck(int input)
        {
            int val = 0;
            int count = 0;
            while (val <= input)
            {
                count++;
                val = Convert.ToInt32(Math.Pow(2, count));
                if (val >= input)
                {
                    break;
                }
            }
            return count;
        }

        public void subnetting_classC(int octet1, int octet2, int octet3,int octet4, int input)
        {
            Bitscount = 0;
            hostbit = 0;
            maskbit = 0;
         
            Bitscount = NetworkCheck(input);
            bitborrow = 0;
            bitborrow = Bitscount;

            maskbit = 24 + Bitscount;

            Hbit = 8 - bitborrow;
            andVal = octet4 & 0;

            if (maskbit <=31)
            {
                a = Breakconvert(andVal, 0, Bitscount);
                b = Breakconvert(andVal, Bitscount, Hbit);
    
            }
            else
            {
                a = Breakconvert(andVal, 0, Bitscount);
            }

            

          
            arryLength = input;
            networkNumber = new int[arryLength];

            for (int i = 0; i < arryLength; i++)
            {
                networkNumber[i] = i + a;
            }

            if (maskbit <=31)
            {
                while (networkNumber.Length != bitborrow)
                {

                    for (int i = 0; i < networkNumber.Length; i++)
                    {
                        list.Add(BinarySubnetConvert(networkNumber[i], bitborrow, Hbit));
                    }

                    if (networkNumber.Length >= bitborrow)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i2 = 0; i2 < networkNumber.Length; i2++)
                {
                    list.Add(networkNumber[i2]);
                }

            }

            dt.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Network Address", typeof(string));
            dt.Columns.Add("Host Address", typeof(string));
            dt.Columns.Add("Broadcast Address", typeof(string));

            for (int i = 0; i < list.Count; i++)
            {
                string Network = "";
                string broadcast = "";
                string hostFA = "";
                string hostLA = "";

                Network = list[i].ToString();
                broadcast = list[i].ToString();

                int val4 = 0;

               if (maskbit > 24)
                {
                    val4 = Int32.Parse(list[i].ToString());
                    Network = octet1 + "." + octet2 + "." + octet3 + "." + Network;
                    broadcast = BroadCast_Address(octet1, octet2, octet3, Int32.Parse(broadcast), maskbit);
                    if (maskbit < 31)
                    {
                        val4 = val4 + 1;
                        hostFA = octet1 + "." + octet2 + "." + octet3 + "." + val4;
                        Orval4 = Orval4 - 1;
                        hostLA = octet1 + "." + octet2 + "." + octet3 + "." + Orval4;
                    }
                    else
                    {
                        hostFA = octet1 + "." + octet2 + "." + octet3 + "." + val4;
                        hostLA = octet1 + "." + octet2 + "." + octet3 + "." + Orval4;
                    }
                  

                    dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

                }

            }

            networkNumber = null;
            list.Clear();
            List2.Clear();
         
        }

        public void subnetting_classB(int octet1,int octet2,int octet3, int octet4, int input)
        {

            Bitscount = NetworkCheck(input);
            bitborrow = 0;
            bitborrow = Bitscount;

            maskbit = 16 + Bitscount;
            int remainbit = 0;
            if (maskbit > 16 && maskbit <= 24)
            {
                Hbit = 8 - bitborrow;
                andVal4 = octet4 & 0;
                andVal3 = octet3 & 0;

                if (maskbit == 24)
                {
                    a = 0;
                }
                else
                {
                    a = Breakconvert(andVal3, 0, Bitscount);
                    b = Breakconvert(andVal3, Bitscount, Hbit);
                    remainbit = 8 - Bitscount;
                }  
            }

            else if (maskbit > 24)
            {
                Hbit = 16 - bitborrow;
                bitborrow = 8 - Hbit;
                andVal4 = octet4 & 0;

                if (maskbit <= 31)
                {
                    a = Breakconvert(andVal4, 0, bitborrow);
                    b = Breakconvert(andVal4, bitborrow, Hbit);
                    remainbit = 16 - Bitscount;
                }
                else
                {
                    a = Breakconvert(andVal, 0, bitborrow);
                }

            }


            arryLength = input;
            networkNumber = new int[arryLength];

            for (int i = 0; i < arryLength; i++)
            {
                networkNumber[i] = a;
                a++;
                if (networkNumber[i] == 255)
                {
                    a = 0;
                }
            }

            if (maskbit <24 || maskbit <32)
            {
                while (networkNumber.Length != bitborrow)
                {
                    for (int i = 0; i < networkNumber.Length; i++)
                    {
                        list.Add(BinarySubnetConvert(networkNumber[i], bitborrow, Hbit));
                    }


                    if (networkNumber.Length >= bitborrow)
                    {
                        break;
                    }

            }
     
           }
           else if (maskbit == 24 || maskbit == 32)
            {
                for (int i = 0; i < networkNumber.Length; i++)
                {
                    list.Add(networkNumber[i]);
                }
            }

                dt.Clear();
                dt.Columns.Add("Network Address", typeof(string));
                dt.Columns.Add("Host Address", typeof(string));
                dt.Columns.Add("Broadcast Address", typeof(string));

  
        
            for (int i = 0; i < list.Count; i++)
            {
                string Network = "";
                string broadcast = "";
                string hostFA = "";
                string hostLA = "";

                Network = list[i].ToString();
                broadcast = list[i].ToString();

                int val4 = 0;


                if (maskbit > 16 && maskbit <= 24)
                {
                    val4 = andVal4;
             
                    int val3 = Convert.ToInt32(list[i]);
                    Network = octet1 + "." + octet2 + "." + Network + "." + andVal4;
                    val4 = val4 + 1;
                    hostFA = octet1 + "." + octet2 + "." + list[i] + "." + val4;

                    broadcast = BroadCast_Address(octet1, octet2, Int32.Parse(broadcast), andVal4, maskbit);
                    Orval4 = Orval4 - 1;
                    hostLA = octet1 + "." + octet2 + "." + Orval3 + "." + Orval4;
   
                    dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

                }
                else if (maskbit > 24)
                {

                        if (Orval4 == 254 || Orval4 == 255)
                        {

                            if (andVal3 == 255)
                            {
                                if (andVal4!= andVal3)
                                {
                                    andVal3 = 0;
                                }
                            }
                            andVal3++;
                        }

                        val4 = Int32.Parse(list[i].ToString());

                        Network = octet1 + "." + octet2 + "." + andVal3 + "." + Network;
                        broadcast = BroadCast_Address(octet1, octet2, andVal3, Int32.Parse(broadcast), maskbit);
                 
                        if (maskbit <31)
                        {
                            val4 = val4 + 1;
                            hostFA = octet1 + "." + octet2 + "." + andVal3 + "." + val4;

                            Orval4 = Orval4 - 1;
                            hostLA = octet1 + "." + octet2 + "." + Orval3 + "." + Orval4;
                        }
                        else
                        {
                            hostFA = octet1 + "." + octet2 + "." + andVal3 + "." + val4;
                            hostLA = octet1 + "." + octet2 + "." + Orval3 + "." + Orval4;
                        }
                        dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

                }

              }

            networkNumber = null;
            list.Clear();
            List2.Clear();
        }


     

        public void subnetting_classA(int octet1, int octet2, int octet3, int octet4, int input)
        {
            Bitscount = NetworkCheck(input);
            bitborrow = 0;
            bitborrow = Bitscount;

            maskbit = 8 + Bitscount;
            int remainbit = 0;
            if (maskbit <= 16)
            {
                Hbit = 8 - bitborrow;
      
                andVal4 = octet4 & 0;
                andVal3 = octet3 & 0;

                if (maskbit == 16)
                {
                    a = 0;
                }
                else
                {
                    a = Breakconvert(andVal3, 0, Bitscount);
                    b = Breakconvert(andVal3, Bitscount, Hbit);
                    remainbit = 8 - Bitscount;
                }

            }

            else if (maskbit > 16 && maskbit < 24)
            {
                Hbit = 16 - bitborrow;

                andVal4 = octet4 & 0;
                andVal3 = octet3 & 0;

                if (maskbit == 24)
                {
                    a = 0;
                }
                else
                {
                    int nw = 8 - Hbit;
                    a = Breakconvert(andVal3, 0, nw);
                    b = Breakconvert(andVal3, nw, Hbit);
                    bitborrow = nw;
                }

                remainbit = 16 - Bitscount;
            }
            else if (maskbit > 24)
            {
                Hbit = 24 - bitborrow;
                bitborrow = 8 - Hbit;
                andVal4 = octet4 & 0;
                if (maskbit == 32)
                {
                    a = 0;
                }
                else
                {
                    a = Breakconvert(andVal4, 0, bitborrow);
                    b = Breakconvert(andVal4, bitborrow, Hbit);
                    remainbit = 24 - Bitscount;
                }
               
            }


            arryLength = input;
            networkNumber = new int[arryLength];

            for (int i = 0; i < arryLength; i++)
            {
                networkNumber[i] = a;
                a++;
                if (networkNumber[i] == 255)
                {
                    a = 0;
                }

            }
            if (maskbit < 16 || maskbit < 24 || maskbit < 32)
            {
                while (networkNumber.Length != bitborrow)
                {

                    for (int i = 0; i < networkNumber.Length; i++)
                    {
                        list.Add(BinarySubnetConvert(networkNumber[i], bitborrow, Hbit));
                    }
                    if (networkNumber.Length >= bitborrow)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < networkNumber.Length; i++)
                {
                    list.Add(networkNumber[i]);
                }
            }
         
      
            dt.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Network Address", typeof(string));
            dt.Columns.Add("Host Address", typeof(string));
            dt.Columns.Add("Broadcast Address", typeof(string));
            bool breakloop = false;
            int j = 0;
            //while (list.Count - 1 !=0)
            //{
            //    string Network = "";
            //    string broadcast = "";
            //    string hostFA = "";
            //    string hostLA = "";

            //    Network = list[j].ToString();
            //    broadcast = list[j].ToString();

            //    int val4 = 0;

            //    if (maskbit > 8 && maskbit <= 16)
            //    {
            //        val4 = andVal4;

            //        Network = octet1 + "." + Network + "." + andVal3 + "." + andVal4;
            //        val4 = val4 + 1;
            //        hostFA = octet1 + "." + list[j] + "." + andVal3 + "." + val4;

            //        broadcast = BroadCast_Address(octet1, Int32.Parse(broadcast), andVal3, andVal4, maskbit);
            //        Orval4 = Orval4 - 1;
            //        hostLA = octet1 + "." + Orval2 + "." + Orval3 + "." + Orval4;
            //        if (Orval3 == 255)
            //        {
            //            andVal2++;
            //        }

            //        dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

            //    }

            //    else if (maskbit > 16 && maskbit <= 24)
            //    {
            //        val4 = andVal4;

            //        int val3 = Convert.ToInt32(list[j]);
            //        Network = octet1 + "." + andVal2 + "." + Network + "." + andVal4;
            //        val4 = val4 + 1;
            //        hostFA = octet1 + "." + andVal2 + "." + list[j] + "." + val4;

            //        broadcast = BroadCast_Address(octet1, andVal2, Int32.Parse(broadcast), andVal4, maskbit);
            //        Orval4 = Orval4 - 1;
            //        hostLA = octet1 + "." + andVal2 + "." + Orval3 + "." + Orval4;
            //        if (val3 == 255 || Orval3 == 255)
            //        {
            //            andVal2++;
            //        }

            //        dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);
            //    }
            //    else if (maskbit > 24)
            //    {

            //        if (breakloop == false)
            //        {
            //            if (Orval4 == 254)
            //            {
            //                if (andVal3 == 255)
            //                {
            //                    andVal3 = 0;

            //                    andVal2++;

            //                }
            //                andVal3++;
            //            }
            //            store = 0;
            //            val4 = Int32.Parse(list[j].ToString());

            //            Network = octet1 + "." + andVal2 + "." + andVal3 + "." + Network;
            //            val4 = val4 + 1;
            //            hostFA = octet1 + "." + andVal2 + "." + andVal3 + "." + val4;
            //            broadcast = BroadCast_Address(octet1, andVal2, andVal3, Int32.Parse(broadcast), maskbit);
            //            store = Orval4;
            //            Orval4 = Orval4 - 1;

            //            hostLA = octet1 + "." + Orval2 + "." + Orval3 + "." + Orval4;
            //            dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

            //            if (andVal2 == 255 && andVal3 == 255 && store == 255)
            //            {
            //                breakloop = true;
            //                break;

            //            }
            //        }



            //        //   kk[h] = Network;
            //        //   h++;
            //        //   kk[h] = hostFA;
            //        //   h++;
            //        //   kk[h] = hostLA;
            //        //   h++;
            //        //kk[h] =   broadcast;
            //        //h++;
            //    }

            //    j++;
            //    if (j == list.Count)
            //    {
            //        break;
            //    }
            //}
            datagridview.ColumnCount = 3;
            datagridview.Columns[0].Name = "NETWORK";
            datagridview.Columns[1].Name = "HOST";
            datagridview.Columns[2].Name = "BROADCAST";  
            NetworkA = new string[list.Count];
            BroadcastA = new string[list.Count];
            HostA = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                string Network = "";
                string broadcast = "";
                string hostFA = "";
                string hostLA = "";

                Network = list[i].ToString();
                broadcast = list[i].ToString();

                int val4 = 0;

                if (maskbit > 8 && maskbit <= 16)
                {
                    val4 = andVal4;

                    Network = octet1 + "." + Network + "." + andVal3 + "." + andVal4;
                    val4 = val4 + 1;
                    hostFA = octet1 + "." + list[i] + "." + andVal3 + "." + val4;

                    broadcast = BroadCast_Address(octet1, Int32.Parse(broadcast), andVal3, andVal4, maskbit);
                    Orval4 = Orval4 - 1;
                    hostLA = octet1 + "." + Orval2 + "." + Orval3 + "." + Orval4;
                    if (Orval3 == 255)
                    {
                        andVal2++;
                    }

                    dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

                }

                else if (maskbit > 16 && maskbit <= 24)
                {
                    val4 = andVal4;

                    int val3 = Convert.ToInt32(list[i]);
                    Network = octet1 + "." + andVal2 + "." + Network + "." + andVal4;
                    val4 = val4 + 1;
                    hostFA = octet1 + "." + andVal2 + "." + list[i] + "." + val4;

                    broadcast = BroadCast_Address(octet1, andVal2, Int32.Parse(broadcast), andVal4, maskbit);
                    Orval4 = Orval4 - 1;
                    hostLA = octet1 + "." + andVal2 + "." + Orval3 + "." + Orval4;
                    if (val3 == 255 || Orval3 == 255)
                    {
                        andVal2++;
                    }

                    dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);
                }
                else if (maskbit > 24)
                {

                    if (breakloop == false)
                    {
                        if (Orval4 == 254)
                        {
                            if (andVal3 == 255)
                            {
                                andVal3 = 0;

                                andVal2++;

                            }
                            andVal3++;
                        }
                        
                        store = 0;
                        val4 = Int32.Parse(list[i].ToString());

                        Network = octet1 + "." + andVal2 + "." + andVal3 + "." + Network;
                        val4 = val4 + 1;
                        hostFA = octet1 + "." + andVal2 + "." + andVal3 + "." + val4;
                        broadcast = BroadCast_Address(octet1, andVal2, andVal3, Int32.Parse(broadcast), maskbit);
                        store = Orval4;
                        Orval4 = Orval4 - 1;

                        hostLA = octet1 + "." + Orval2 + "." + Orval3 + "." + Orval4;

                        //if (maskbit <28)
                        //{
                        //    dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);
                            
                        //}
                        //else
                        //{
                        //    //NetworkA[i] = Network;
                        //    //HostA[i] = hostFA + "  to  " + hostLA;
                        //    //BroadcastA[i] = broadcast;
                        //    datagridview.Rows.Add(Network,hostFA + " to "+ hostLA,broadcast);

                        //}
                        dt.Rows.Add(Network, hostFA + " to " + hostLA, broadcast);

                        if (andVal2 == 255 && andVal3 == 255 && store == 255)
                        {
                            breakloop = true;
                            break;

                        }
                    }



                    //   kk[h] = Network;
                    //   h++;
                    //   kk[h] = hostFA;
                    //   h++;
                    //   kk[h] = hostLA;
                    //   h++;
                    //kk[h] =   broadcast;
                    //h++;
                }
            }
            list.Clear();
            List2.Clear();
            networkNumber = null;

        }







        public string BroadCast_Address(int octet1, int octet2, int octet3, int octet4, int mask)
        {
            Orval2 = 0;
            Orval3 = 0;
            Orval4 = 0;
            data = "";
            if (mask > 16 && mask <= 24)
            {
                Orval4 = octet4 | 255;

                subnetNormal3 = Complement_SubnetMask(mask);
                subnetNormal3 = subnetNormal3.Substring(16, 8);
                Orval3 = Convert.ToInt32(ip.ConvertInteger(subnetNormal3));
                Orval3 = octet3 | Orval3;


                data = octet1.ToString() + "." + octet2.ToString() + "." + Orval3.ToString() + "." + Orval4.ToString();

            }
            else if (mask > 8 && mask <= 16)
            {
                subnetNormal2 = Complement_SubnetMask(mask);
                subnetNormal2 = subnetNormal2.Substring(8, 8);
                Orval2 = Convert.ToInt32(ip.ConvertInteger(subnetNormal2));
                
                Orval2 = octet2 | Orval2;
                Orval3 = octet3 | 255;
                Orval4 = octet4 | 255;

                data = octet1.ToString() + "." + Orval2.ToString() + "." + Orval3.ToString() + "." + Orval4.ToString();

            }


            else if (mask > 24)
            {

                Orval2 = octet2 | 0;
                Orval3 = octet3 | 0;

                subnetNormal4 = Complement_SubnetMask(mask);
                subnetNormal4 = subnetNormal4.Substring(24, 8);
                Orval4 = Convert.ToInt32(ip.ConvertInteger(subnetNormal4));
                Orval4 = octet4 | Orval4;

                data = octet1.ToString() + "." + Orval2.ToString() + "." + Orval3.ToString() + "." + Orval4.ToString();


            }

            return data;
        }

        public int Breakconvert(int octet, int startindex, int length)
        {
          
            string data = ip.Binary(octet);

            data = data.Substring(startindex, length);

            data = ip.ConvertInteger(data);
            return Convert.ToInt32(data);
        }

        public string BinarySubnetConvert(int octet, int Networkbits,int HostBits)
        {
            int arryLength = Networkbits;
            arryLength = arryLength + 1;
            string[] b = new string[arryLength];
            Array.Clear(b, 0, b.Length);
            data = "";

            int pos = Networkbits;
            int i = 0;

            while (i < Networkbits)
            {
                if ((octet & (1 << i)) != 0)
                {
                    b[pos] = "1";
                }
                else
                {
                    b[pos] = "0";
                }
                pos--;
                i++;

            }

            for (int i2 = 0; i2 < b.Length; i2++)
            {
                data = data + b[i2];
            }



            data =   ChangeBitsZero(data,HostBits);
            return data;
        }

        public string ChangeBitsZero(string Octet, int octetValue)
        {
            string dataOctet = Octet;
            data = "";
            for (int i = 0; i < octetValue; i++)
            {
                dataOctet = dataOctet + "0";
            }

            data = dataOctet;
            dataOctet = "";
            data= ip.ConvertInteger(data);
            return data;
        }

        public string Complement_SubnetMask(int mask)
        {
            string[] maskarray = new string[mask];
            data = "";
            string hbData = "";

            int hostbit = 32 - mask;
            for (int i2 = 0; i2 < mask; i2++)
            {
                data = data + "0";
            }

            for (int i1 = 0; i1 < hostbit; i1++)
            {
                hbData = hbData + "1";
            }
            data = data + hbData;
            return data;

        }


    }
}
