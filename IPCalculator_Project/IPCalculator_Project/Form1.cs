using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
                                               

namespace IPCalculator_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     

        IPCalculations ip = new IPCalculations();
        string Networkaddress;
        string Broadcastaddress;
        int FirstAdd;
        int LastAdd;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int octet1Limit = Convert.ToInt32(OctetFirst_TxtBox.Text);
                int octet2Limit = Convert.ToInt32(OctetSecond_TxtBox.Text);
                int octet3Limit = Convert.ToInt32(OctetThird_TxtBox.Text);
                int octet4Limit = Convert.ToInt32(OctetFourth_TxtBox.Text);
                int maskbitLimit = Convert.ToInt32(Mask_TextBox.Text);


                if (octet1Limit > 255 || octet2Limit > 255 || octet3Limit > 255 || octet4Limit > 255 || maskbitLimit > 32)
                {
                    MessageBox.Show("Please Enter IP \n According to its Range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (OctetFirst_TxtBox.Text == "" || OctetSecond_TxtBox.Text == "" || OctetThird_TxtBox.Text == "" || OctetFourth_TxtBox.Text == "" || Mask_TextBox.Text == "")
                {
                    MessageBox.Show(" Please Fill All the Octet Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    IPCalculations ip = new IPCalculations();
                    ip.ClassType(Convert.ToInt32(OctetFirst_TxtBox.Text));
                    NetworkClass_TxtBox.Text = ip.ClassName;    

                    IPFirstAddress_Label.Visible = true;
                    IPLastAdress_Label.Visible = true;
                    FirstAddress2_Label.Visible = true;
                    LastAddress2_Label.Visible = true;


                    if (octet1Limit >= 0 && octet1Limit <= 127 && maskbitLimit >= 8)
                    {
                        
                        Rangeip2_Label.Text = "Range: 0 to 127";
                        
                        
                        Networkaddress = ip.FirstAddress_ClassA(octet1Limit,octet2Limit,octet3Limit,octet4Limit, maskbitLimit);
                        IPFirstAddress_Label.Text = Networkaddress;
           
                        
                        Broadcastaddress = ip.LastAddress_ClassA(octet1Limit, octet2Limit, octet3Limit, octet4Limit, maskbitLimit);
                        IPLastAdress_Label.Text = Broadcastaddress;

                        if (maskbitLimit >=31)
                        {
                            FirstAddress2_Label.Text = Networkaddress;
                            LastAddress2_Label.Text = Broadcastaddress;              
                        }
                        else
                        {
                            FirstAdd = Int32.Parse(IPAddressSplit(IPFirstAddress_Label.Text, 3)) + 1;
                            FirstAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPFirstAddress_Label.Text, 0), IPAddressSplit(IPFirstAddress_Label.Text, 1), IPAddressSplit(IPFirstAddress_Label.Text, 2), FirstAdd.ToString());

                            LastAdd = Int32.Parse(IPAddressSplit(IPLastAdress_Label.Text, 3)) - 1;
                            LastAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPLastAdress_Label.Text, 0), IPAddressSplit(IPLastAdress_Label.Text, 1), IPAddressSplit(IPLastAdress_Label.Text, 2), LastAdd.ToString());

                        }

                    }
                    else if (octet1Limit >= 128 && octet1Limit <= 191 && maskbitLimit >= 16)
                    {    
                       
                        Rangeip2_Label.Text = "Range: 128 to 191";
                       
                        Networkaddress = ip.FirstAddress_ClassB(octet1Limit, octet2Limit, octet3Limit, octet4Limit, maskbitLimit);
                        Broadcastaddress = ip.LastAddress_ClassB(octet1Limit, octet2Limit, octet3Limit, octet4Limit,maskbitLimit);
                        
                     
                        IPFirstAddress_Label.Text = Networkaddress;
                        IPLastAdress_Label.Text = Broadcastaddress;
                        if (maskbitLimit >=31)
                        {
                            FirstAddress2_Label.Text = Networkaddress;
                            LastAddress2_Label.Text = Broadcastaddress;

                        }
                        else
                        {
                            FirstAdd = Int32.Parse(IPAddressSplit(IPFirstAddress_Label.Text, 3)) + 1;
                            FirstAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPFirstAddress_Label.Text, 0), IPAddressSplit(IPFirstAddress_Label.Text, 1), IPAddressSplit(IPFirstAddress_Label.Text, 2), FirstAdd.ToString());

                            LastAdd = Int32.Parse(IPAddressSplit(IPLastAdress_Label.Text, 3)) - 1;
                            LastAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPLastAdress_Label.Text, 0), IPAddressSplit(IPLastAdress_Label.Text, 1), IPAddressSplit(IPLastAdress_Label.Text, 2), LastAdd.ToString());
 
                        }
                

                    }

                    else if (octet1Limit >= 192 && octet1Limit <= 223 && maskbitLimit >= 24)
                    {
                     
                        Rangeip2_Label.Text = "Range: 192 to 223";

                        
                        Networkaddress = ip.FirstAddress_ClassC(octet1Limit, octet2Limit, octet3Limit, octet4Limit, maskbitLimit);
                        IPFirstAddress_Label.Text =Networkaddress;

                        Broadcastaddress = ip.LastAddress_ClassC(octet1Limit, octet2Limit, octet3Limit, octet4Limit, maskbitLimit);
                        IPLastAdress_Label.Text = Broadcastaddress;

                        FirstAdd = Int32.Parse(IPAddressSplit(IPFirstAddress_Label.Text, 3)) + 1;
                        FirstAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPFirstAddress_Label.Text, 0), IPAddressSplit(IPFirstAddress_Label.Text, 1), IPAddressSplit(IPFirstAddress_Label.Text, 2), FirstAdd.ToString());

                        LastAdd = Int32.Parse(IPAddressSplit(IPLastAdress_Label.Text, 3)) - 1;
                        LastAddress2_Label.Text = ipaddressCombine(IPAddressSplit(IPLastAdress_Label.Text, 0), IPAddressSplit(IPLastAdress_Label.Text, 1), IPAddressSplit(IPLastAdress_Label.Text, 2), LastAdd.ToString());


                    }
                    else if (octet1Limit >= 240 && octet1Limit <= 255 || maskbitLimit < 32)
                    {
                        MessageBox.Show("IP Address like 240.XXX.XXX.XXX is not Valid.\n Host Range of IP Addresses from '240.0.0.0 to 255.255.255.255' belong to Class E and Reserved.\n Please Insert IP Between 0.0.0.0 to 239.255.255.255 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        MessageBox.Show("Please Give Mask Number \n According to its Range ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    Networkaddress = "";
                    Broadcastaddress = "";
                    FirstAdd = 0;
                    LastAdd = 0;

                    Octet1_Label.Visible = true;
                    Octet2_Label.Visible = true;
                    Octet3_Label.Visible = true;
                    Octet4_Label.Visible = true;
                    SubnetMaskCIDR_Label.Visible = true;


               
                    int hb = ip.HostBit(maskbitLimit);
                    HostBit_TxtBox.Text = hb.ToString();

                    
                    int totalAddress = ip.NumberofAddress(maskbitLimit);
                    NoOfAddresses_TxtBox.Text = totalAddress.ToString();


                
                    string subnetmask = ip.SubnetMask(maskbitLimit);
                    string octet1 = subnetmask;
                    string octet2 = subnetmask;
                    string octet3 = subnetmask;
                    string octet4 = subnetmask;
                    octet1 = ip.ConvertInteger(octet1.Substring(0, 8));
                    octet2 = ip.ConvertInteger(octet2.Substring(8, 8));
                    octet3 = ip.ConvertInteger(octet3.Substring(16, 8));
                    octet4 = ip.ConvertInteger(octet4.Substring(24, 8));
                    SubnetMaskCIDR_Label.Text = ipaddressCombine(octet1, octet2, octet3, octet4);



                    Octet1_Label.Text = ip.Binary(octet1Limit);
                    Octet2_Label.Text = ip.Binary(octet2Limit);
                    Octet3_Label.Text = ip.Binary(octet3Limit);
                    Octet4_Label.Text = ip.Binary(octet4Limit);



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    
        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void OctetFirst_TxtBox_TextChanged(object sender, EventArgs e)
        {
            OctetFirst_TxtBox.MaxLength = 3;
        }
        private void OctetSecond_TxtBox_TextChanged(object sender, EventArgs e)
        {
            OctetSecond_TxtBox.MaxLength = 3;
        }
        private void OctetThird_TxtBox_TextChanged(object sender, EventArgs e)
        {
            OctetThird_TxtBox.MaxLength = 3;
        }
        private void OctetFourth_TxtBox_TextChanged(object sender, EventArgs e)
        {
            OctetFourth_TxtBox.MaxLength = 3;
        }
        private void Mask_TextBox_TextChanged(object sender, EventArgs e)
        { 
            Mask_TextBox.MaxLength = 2;
        }
        private void Clear_Btn_Click(object sender, EventArgs e)
        {

                 OctetFirst_TxtBox.Text = "";
                 OctetSecond_TxtBox.Text = "";
                 OctetThird_TxtBox.Text = "";
                 OctetFourth_TxtBox.Text = "";
                 Mask_TextBox.Text = "";
                 Rangeip2_Label.Text = "";

                 IPFirstAddress_Label.Text = "";
                 IPLastAdress_Label.Text = "";
                 NetworkClass_TxtBox.Text = "";
                 NoOfAddresses_TxtBox.Text = "";
         
                 HostBit_TxtBox.Text = "";
                 SubnetMaskCIDR_Label.Text = "";

                 Octet1_Label.Text = "";
                 Octet2_Label.Text = "";
                 Octet3_Label.Text = "";
                 Octet4_Label.Text = "";
                 FirstAddress2_Label.Text = "";
                 LastAddress2_Label.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IPFirstAddress_Label.Visible = false;
            IPLastAdress_Label.Visible = false;
            Octet1_Label.Visible = false;
            Octet2_Label.Visible = false;
            Octet3_Label.Visible = false;
            Octet4_Label.Visible = false;
            SubnetMaskCIDR_Label.Visible = false;


            FirstAddress1_Label.Visible = false;
            LastAddress1_Label.Visible = false;
            FirstAddress2_Label.Visible = false;
            LastAddress2_Label.Visible = false;

            OctetOne_Label.Visible = false;
            OctetTwo_Label.Visible = false;
            OctetThree_Label.Visible = false;
            OctetFour_Label.Visible = false;

            SubnetMask_Lbl.Visible = false;
            IPFirstA_Label.Visible = false;
            IPLastA_Label.Visible = false;
            Rangeip_Label.Visible = false;

            //subnet tab
            RangeSN_Label.Visible = false;
            Octet1_LblSN.Visible = false;
            Octet2_LblSN.Visible = false;
            Octet3_LblSN.Visible = false;
            Octet4_LblSN.Visible = false;
            SubnetMaskSN_Label.Visible = false;

        }
       
        public string ipaddressCombine(string firstOctet, string secondOctet, string thirdOctet, string fourthOctet)
        {

            string combine = "";
            combine = firstOctet + "." + secondOctet + "." + thirdOctet + "." + fourthOctet;
            return combine;

        }



        private void Calculate2_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                int octet1Limit = Convert.ToInt32(OctetOne_Tb.Text);
                int octet2Limit = Convert.ToInt32(OctetTwo_Tb.Text);
                int octet3Limit = Convert.ToInt32(OctetThree_Tb.Text);
                int octet4Limit = Convert.ToInt32(OctetFour_Tb.Text);

                if (octet1Limit <= 255 && octet2Limit <= 255 && octet3Limit <= 255 && octet4Limit <= 255)
                {

                    ip.ClassType(Convert.ToInt32(OctetOne_Tb.Text));
                    NetworkClass_Tb.Text = ip.ClassName;

                    IPFirstA_Label.Text = ip.ClassFull_FirstAddressC(octet1Limit, octet2Limit, octet3Limit, octet4Limit);
                    IPLastA_Label.Text = ip.ClassFull_LastAddressC(octet1Limit, octet2Limit, octet3Limit, octet4Limit);

            
                    OctetOne_Label.Visible = true;
                    OctetTwo_Label.Visible = true;
                    OctetThree_Label.Visible = true;
                    OctetFour_Label.Visible = true;
                    SubnetMask_Lbl.Visible = true;
                    IPFirstA_Label.Visible = true;
                    IPLastA_Label.Visible = true;
                    Rangeip_Label.Visible = true;
                    FirstAddress1_Label.Visible = true;
                    LastAddress1_Label.Visible = true;

                    
                    if (octet1Limit >= 0 && octet1Limit <= 127)
                    {
                             

                        SubnetMask_Lbl.Text = "255.0.0.0";
                        Rangeip_Label.Text = "Range: 0  to  127";
                        MaskBit_tb.Text = "8";
                        HostBit_Tb.Text = ip.HostBit(8).ToString();
                        NoOfAddres_Tb.Text = ip.NumberofAddress(8).ToString();


                    }
                    else if (octet1Limit >= 128 && octet1Limit <= 191)
                    {
                       

                        SubnetMask_Lbl.Text = "255.255.0.0";
                        Rangeip_Label.Text = "Range: 128  to  191";
                        MaskBit_tb.Text = "16";
                        HostBit_Tb.Text = ip.HostBit(16).ToString();
                        NoOfAddres_Tb.Text = ip.NumberofAddress(16).ToString();

                    }
                    else if (octet1Limit >= 192 && octet1Limit <= 223)
                    {
                     

                        SubnetMask_Lbl.Text = "255.255.255.0";
                        Rangeip_Label.Text = "Range: 192  to  223";

                        MaskBit_tb.Text = "24";
                        HostBit_Tb.Text = ip.HostBit(24).ToString();
                        NoOfAddres_Tb.Text = ip.NumberofAddress(24).ToString();
                    }
                    else if (octet1Limit >= 224 && octet1Limit <= 239)
                    {

                        SubnetMask_Lbl.Text = "255.255.255.255";
                        Rangeip_Label.Text = "Range: 224  to  339";

                        MaskBit_tb.Text = "32";
                        HostBit_Tb.Text = ip.HostBit(32).ToString();
                        NoOfAddres_Tb.Text = ip.NumberofAddress(32).ToString();

                    }


                    else if (octet1Limit >= 240 && octet1Limit <= 255)
                    {
                        MessageBox.Show("IP Address like 240.XXX.XXX.XXX is not Valid.\n Host Range of IP Addresses from '240.0.0.0 to 255.255.255.255' belong to Class E and Reserved.\n Please Insert IP Between 0.0.0.0 to 239.255.255.255 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (MaskBit_tb.Text == "32")
                    {
                        FirstAddress1_Label.Text = IPFirstA_Label.Text;
                        LastAddress1_Label.Text = IPLastA_Label.Text;                   
                    }
                    else
                    {
                        FirstAdd = Int32.Parse(IPAddressSplit(IPFirstA_Label.Text, 3)) + 1;
                        FirstAddress1_Label.Text = ipaddressCombine(IPAddressSplit(IPFirstA_Label.Text, 0), IPAddressSplit(IPFirstA_Label.Text, 1), IPAddressSplit(IPFirstA_Label.Text, 2), FirstAdd.ToString());

                        LastAdd = Int32.Parse(IPAddressSplit(IPLastA_Label.Text, 3)) - 1;
                        LastAddress1_Label.Text = ipaddressCombine(IPAddressSplit(IPLastA_Label.Text, 0), IPAddressSplit(IPLastA_Label.Text, 1), IPAddressSplit(IPLastA_Label.Text, 2), LastAdd.ToString());

                    }
        
                 
                    OctetOne_Label.Text = ip.Binary(octet1Limit);
                    OctetTwo_Label.Text = ip.Binary(octet2Limit);
                    OctetThree_Label.Text = ip.Binary(octet3Limit);
                    OctetFour_Label.Text = ip.Binary(octet4Limit);
                    
                    FirstAdd = 0;
                    LastAdd = 0;
                }

                else if (OctetOne_Tb.Text == "" || OctetTwo_Tb.Text == "" || OctetThree_Tb.Text == "" || OctetFour_Tb.Text == "")
                {
                    MessageBox.Show("Please Fill Octet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Please Enter IP \n According to its Range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error in ClassFull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }






        private void Clear2_Btn_Click(object sender, EventArgs e)
        {

            OctetOne_Tb.Text ="";
            OctetTwo_Tb.Text="";
            OctetThree_Tb.Text="";
            OctetFour_Tb.Text="";

            NetworkClass_Tb.Text = "";
            OctetOne_Label.Text = "";
            OctetTwo_Label.Text = "";
            OctetThree_Label.Text = "";
            OctetFour_Label.Text = "";

            SubnetMask_Lbl.Text = "";
            MaskBit_tb.Text = "";
            HostBit_Tb.Text = "";
            NoOfAddres_Tb.Text = "";
            Rangeip_Label.Text = "";
            IPFirstA_Label.Text = "";
            IPLastA_Label.Text = "";


            FirstAddress1_Label.Text = "";
            LastAddress1_Label.Text = "";

        }

        private void Exit2_Btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OctetOne_Tb_TextChanged(object sender, EventArgs e)
        {
            OctetOne_Tb.MaxLength = 3;
        }

        private void OctetTwo_Tb_TextChanged(object sender, EventArgs e)
        {
            OctetTwo_Tb.MaxLength = 3;
        }

        private void OctetThree_Tb_TextChanged(object sender, EventArgs e)
        {
            OctetThree_Tb.MaxLength = 3;
        }

        private void OctetFour_Tb_TextChanged(object sender, EventArgs e)
        {
            OctetFour_Tb.MaxLength = 3;
        }

        private void Exit3_Btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clear3_Btn_Click(object sender, EventArgs e)
        {
            RangeSN_Label.Text = "";
            Octet1_LblSN.Text = "";
            Octet2_LblSN.Text = "";
            Octet3_LblSN.Text = "";
            Octet4_LblSN.Text = "";


            NumbrofNetwork_TB.Text = "";
            OctetOne_TbSN.Text = "";
            OctetTwo_TbSN.Text = "";
            OctetThree_TbSN.Text = "";
            OctetFour_TbSN.Text = "";
            HostBitSN_TB.Text = "";
            NetworkbitBorrow_TB.Text = "";
            NetworkClassSN_TB.Text = "";
            MaskBitSN_TB.Text = "";
            SubnetMaskSN_Label.Text = "";

            HostNetwork_SNtb.Text = "";
            NetworkNumber_TxtBox.Text = "";


            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null;
            }
            else
            {
                dataGridView1.Rows.Clear();
            }

            dataGridView1.Refresh();

        }





        private void Calclulate3_Btn_Click(object sender, EventArgs e)
        {
           
            try
            {
                IPSubnetting sub = new IPSubnetting();
                int octet1Limit = Convert.ToInt32(OctetOne_TbSN.Text);
                int octet2Limit = Convert.ToInt32(OctetTwo_TbSN.Text);
                int octet3Limit = Convert.ToInt32(OctetThree_TbSN.Text);
                int octet4Limit = Convert.ToInt32(OctetFour_TbSN.Text);
               
                if (octet1Limit > 255 || octet2Limit > 255 || octet3Limit > 255 || octet4Limit > 255)
                {
                    MessageBox.Show("Please Enter IP \n According to its Range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (OctetOne_TbSN.Text == "" || OctetTwo_TbSN.Text == "" || OctetThree_TbSN.Text == "" || OctetFour_TbSN.Text == "")
                {
                    MessageBox.Show(" Please Fill All the Octet Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (octet1Limit >= 0 && octet1Limit <= 127)
                {
                    sub.subnetting_classA(octet1Limit, octet2Limit, octet3Limit, octet4Limit, Convert.ToInt32(NumbrofNetwork_TB.Text));

               
                  //  DataTable dt = new DataTable();
                    //dt.Columns.Add("Result");
                    //dataGridView1.ColumnCount = 3;
                    //dataGridView1.Columns[0].Name = "Network";
                    //dataGridView1.Columns[1].Name = "Host";
                    //dataGridView1.Columns[2].Name = "Broadcast";


                   //for (int i = 0; i < sub.NetworkAd.Length; i++)
                   //{
                   //    dataGridView1.Update();
                   //    //dataGridView1.Rows.Add(sub.kk[i]);
                   //    dataGridView1.RefreshEdit();
                   //    dataGridView1.Rows.Add(sub.NetworkAd[i],sub.HostAd[i],i);

                   //    //dataGridView1.Rows.Add(sub.NetworkAd[i]);
                   //    //dataGridView1.Rows.Add(sub.HostAd[i]);
                   //    //dataGridView1.Rows.Add(sub.BroadcasAd[i]);

                   //    // dt.Rows.Add(sub.kk[i]);
                   //    // dataGridView1.DataSource = sub.kk[i];
                   //    //dataGridView1.Refresh();

                   //}
          
                    //richTextBox1.Text = sub.NetworkA[i] + "-" + sub.HostA[i] + "-" + sub.BroadcastA[i];
                    //dataGridView1 = sub.datagridview;
                    //for (int i = 0; i < sub.NetworkA.Length; i++)
                    //{
                    //    //richTextBox1.AppendText(sub.NetworkA[i] + "-" + sub.HostA[i] + "-" + sub.BroadcastA[i]);
                     
                    //}
                    dataGridView1.Refresh();
                    dataGridView1.DataSource = sub.dt;
                    dataGridView1.Update();
                    RangeSN_Label.Text = "Range: 0  to  127";

                }
                else if (octet1Limit >= 128 && octet1Limit <= 191)
                {
         
                    sub.subnetting_classB(octet1Limit, octet2Limit, octet3Limit, octet4Limit, Convert.ToInt32(NumbrofNetwork_TB.Text));

                    dataGridView1.Refresh();
                    dataGridView1.DataSource = sub.dt;
                    RangeSN_Label.Text = "Range: 128  to  191";
                    
                }


            
                else if (octet1Limit >= 192 && octet1Limit <= 223)
                {
                   
              
                   sub.subnetting_classC(octet1Limit,octet2Limit,octet3Limit, octet4Limit, Convert.ToInt32(NumbrofNetwork_TB.Text));

                    dataGridView1.Refresh();
                    dataGridView1.DataSource = sub.dt;
                    RangeSN_Label.Text = "Range: 192  to  223";
                   
                }
                else if (octet1Limit >= 240 && octet1Limit <= 255)
                {
                  
                    MessageBox.Show("IP Address like 240.XXX.XXX.XXX is not Valid.\n Host Range of IP Addresses from '240.0.0.0 to 255.255.255.255' belong to Class E and Reserved.\n Please Insert IP Between 0.0.0.0 to 239.255.255.255 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Please Enter IP \n According to its Range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RangeSN_Label.Visible = true;
                Octet1_LblSN.Visible = true;
                Octet2_LblSN.Visible = true;
                Octet3_LblSN.Visible = true;
                Octet4_LblSN.Visible = true;
                SubnetMaskSN_Label.Visible = true;


                ip.ClassType(octet1Limit);
                NetworkClassSN_TB.Text = ip.ClassName;



                Octet1_LblSN.Text = ip.Binary(octet1Limit);
                Octet2_LblSN.Text = ip.Binary(octet2Limit);
                Octet3_LblSN.Text = ip.Binary(octet3Limit);
                Octet4_LblSN.Text = ip.Binary(octet4Limit);



                NetworkbitBorrow_TB.Text = sub.Bitscount.ToString();
                MaskBitSN_TB.Text = sub.maskbit.ToString();


                int numofNetwork = Convert.ToInt32(Math.Pow(2, sub.Bitscount));
                NetworkNumber_TxtBox.Text = numofNetwork.ToString();


                int hb = ip.HostBit(sub.maskbit);
                HostBitSN_TB.Text = hb.ToString();

                int hostNetwork = ip.NumberofAddress(sub.maskbit);
                HostNetwork_SNtb.Text = hostNetwork.ToString();

             
                //int totalAddress = ip.NumberofAddress(Convert.ToInt32(Mask_TextBox.Text));
                //NoOfAddresses_TxtBox.Text = totalAddress.ToString();

        
                string subnetmask = ip.SubnetMask(sub.maskbit);
                string octet1 = subnetmask;
                string octet2 = subnetmask;
                string octet3 = subnetmask;
                string octet4 = subnetmask;
                octet1 = ip.ConvertInteger(octet1.Substring(0, 8));
                octet2 = ip.ConvertInteger(octet2.Substring(8, 8));
                octet3 = ip.ConvertInteger(octet3.Substring(16, 8));
                octet4 = ip.ConvertInteger(octet4.Substring(24, 8));
                SubnetMaskSN_Label.Text = ipaddressCombine(octet1, octet2, octet3, octet4);
             
   
	        }
	        catch (Exception ex)
	        {
		
                    MessageBox.Show(ex.Message, "Error in Subnetting..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
	        }
        }




        private void OctetOne_TbSN_TextChanged(object sender, EventArgs e)
        {
            OctetOne_TbSN.MaxLength = 3;
        }

        private void OctetTwo_TbSN_TextChanged(object sender, EventArgs e)
        {
            OctetTwo_TbSN.MaxLength = 3;
        }

        private void OctetThree_TbSN_TextChanged(object sender, EventArgs e)
        {
            OctetThree_TbSN.MaxLength = 3;
        }

        private void OctetFour_TbSN_TextChanged(object sender, EventArgs e)
        {
            OctetFour_TbSN.MaxLength = 3;
        }

        private void NumbrofNetwork_TB_TextChanged(object sender, EventArgs e)
        {
            NumbrofNetwork_TB.MaxLength = 7;
        }


        public string IPAddressSplit(string IP,int index)
        {
          
            string[] arr = IP.Split('.');
            IP = "";
            IP = arr[index];
            return IP;
        }

        public string ChangeColor(int maskbit, Label label)
        { 
            string data = "";
          //  Label1.Text.ForeColor = System.Drawing.Color.Red;

            //string letters = "Hello World";
            string[] maskarray = new string[maskbit];
            data = "";
            string hbData = "";

            int hostbit = 32 - maskbit;
            for (int i2 = 0; i2 < maskbit; i2++)
            {
                data = data + "n";
            }

            for (int i1 = 0; i1 < hostbit; i1++)
            {
                hbData = hbData + "h";
            }
            data = data + hbData;
            Char[] array = data.ToCharArray();

            foreach (Char c in array)
            {
                if (c == 'n')
                {
                    //Console.ForegroundColor = System.ConsoleColor.Red;
                    //Console.Write(c);
             
                }
                else
                {
                    //Console.ForegroundColor = System.ConsoleColor.White;
                    //Console.Write(c);
                }
            }
            //Console.WriteLine();
            //Console.Read();

            return data;
        }
    }
}
