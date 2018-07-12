using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using  MIC.Giant.Data.Arm;
using MIC.Giant.Controllers;
using System.IO;
using MIC.Giant.Data.Reciepe;
using System.Threading;
using MIC.Giant.Controllers.Station;
using MIC.Giant.Enum;
namespace MIC.Giant
{
    public partial class Form1 : Form
    {
       // private NetworkScanner scanner = null;
       // private Controller _controller = null;

        private RapidData NumIndex1 = null;
        ABBrobot tempRobot; //= new CABB();
        //private ABB.Robotics.Controllers.IOSystemDomain.Signal di01 = null;
        //private ABB.Robotics.Controllers.IOSystemDomain.Signal di02 = null;
        //private ABB.Robotics.Controllers.IOSystemDomain.Signal do01 = null;
        //private ABB.Robotics.Controllers.IOSystemDomain.Signal doRobotRunning = null;

        //ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDI01 = null;
        //ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDI02 = null;
        //int cs_di01;
        //ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDO01 = null;
        //int cs_do01;
        //ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDORobotRunning = null;
        //int cs_DORobotRunning;
        private Task[] tasks = null;
        List<string> testCmds;
        mainFunc mainFunction;
        MainController mainController;
        public Form1()
        {  mainController = new MainController(this);
            this.tempRobot = (ABBrobot)mainController._RobotManager._RobotStation;
            InitializeComponent();
          
            mainFunction = new mainFunc(this);
            testCmds = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                testCmds.Add("");
            }
            loadCmd();
            initialPage();
        }
        private void initialPage()
        {
            this.dataGridView3.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                this.dataGridView3.Rows.Add();
            }
        }
        private void loadCmd()
        {
            //testCmds[0] = " [[364,0,490],[1.2168E-08,1,0,0],[0,-1,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]";
            //testCmds[1] = " [[364,-300,594],[1.2168E-08,1,0,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]";
            //testCmds[2] = " [[304,-300,594],[1.2168E-08,1,0,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]";
            //testCmds[3] = " [[364,-300,594],[1.2168E-08,1,0,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]";
            //testCmds[4] = " [[364,0,490],[1.2168E-08,1,0,0],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]";
            //for(int i=0;i<10;i++)
            //{
            //    this.dataGridView1.Rows.Add();
            //    this.dataGridView1.Rows[i].Cells[0].Value = i + 1;
            //    this.dataGridView2.Rows.Add();
            //    this.dataGridView2.Rows[i].Cells[0].Value = i + 1;
            //}
            //this.dataGridView1.Rows[10].Cells[0].Value = 11;
            //this.dataGridView2.Rows[10].Cells[0].Value = 11;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            DisableControllerFunctionality();
            listView1.Items.Clear();
            //this.tempRobot.scanner = new NetworkScanner();
            //this.tempRobot.scanner.Scan();
            //ControllerInfoCollection controllers = tempRobot.scanner.Controllers;
           
            //List<ListViewItem> target = tempRobot.getConnectedRobot();
            List<ListViewItem> target = this.mainController.RunScanRobot();
            foreach (ListViewItem a in target)
            {
                this.listView1.Items.Add(a);
            }
            //foreach (ControllerInfo controllerInfo in controllers)
            //{
            //    item = new ListViewItem(controllerInfo.IPAddress.ToString());
            //    item.SubItems.Add(controllerInfo.Availability.ToString());
            //    item.SubItems.Add(controllerInfo.IsVirtual.ToString());
            //    item.SubItems.Add(controllerInfo.SystemName);
            //    item.SubItems.Add(controllerInfo.Version.ToString());
            //    item.SubItems.Add(controllerInfo.ControllerName);
            //    this.listView1.Items.Add(item);
            //    item.Tag = controllerInfo;
            //}
            //DisableControllerFunctionality();
            //listView1.Items.Clear();
            //this.scanner = new NetworkScanner();
            //this.scanner.Scan();
            //ControllerInfoCollection controllers = scanner.Controllers;
            //ListViewItem item = null;
            //foreach (ControllerInfo controllerInfo in controllers)
            //{
            //    item = new ListViewItem(controllerInfo.IPAddress.ToString());
            //    item.SubItems.Add(controllerInfo.Availability.ToString());
            //    item.SubItems.Add(controllerInfo.IsVirtual.ToString());
            //    item.SubItems.Add(controllerInfo.SystemName);
            //    item.SubItems.Add(controllerInfo.Version.ToString());
            //    item.SubItems.Add(controllerInfo.ControllerName);
            //    this.listView1.Items.Add(item);
            //    item.Tag = controllerInfo;
            //}
        }

        private void EnableControllerFunctionality()
        {
            // put all the enable and disable functionality in the same place so that it is easy to reuse
            label1.Visible = false;
            listView1.Enabled = false;
            gbxControllerSelected.Visible = true;
        }
        private void DisableControllerFunctionality()
        {
            // put all the enable and disable functionality in the same place so that it is easy to reuse
            label1.Visible = true;
            listView1.Enabled = true;
            gbxControllerSelected.Visible = false;
            if (this.tempRobot._controller != null) //if selecting a new controller
            {
                this.tempRobot._controller.Logoff();
                this.tempRobot._controller.Dispose();
                this.tempRobot._controller = null;
            }
        }
        
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            if (item.Tag != null)
            {
                int status = this.mainController.InitialRobotCnnection(item);//tempRobot.initialConnection(item);
                if (status == 1)
                {
                    listView1.Items.Clear();
                    listView1.Items.Add(item);
                    EnableControllerFunctionality();
                }
             
            }
            controller_State_Setting();
        }
        //private void listView1_DoubleClick(object sender, EventArgs e)
        //{
        //    ListViewItem item = this.listView1.SelectedItems[0]; if (item.Tag != null)
        //    {
        //        ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
        //        if (controllerInfo.Availability == Availability.Available)
        //        {
        //            if (controllerInfo.IsVirtual)
        //            {
        //                this._controller = ControllerFactory.CreateFrom(controllerInfo); 
        //                this._controller.Logon(UserInfo.DefaultUser);
        //                listView1.Items.Clear();
        //                listView1.Items.Add(item);
        //                EnableControllerFunctionality();
        //            }
        //            else //real controller
        //            {
        //                if (MessageBox.Show("This is NOT a virtual controller, do you really want to connect to that?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
        //                {
        //                    this._controller = ControllerFactory.CreateFrom(controllerInfo);
        //                    this._controller.Logon(UserInfo.DefaultUser);
        //                    listView1.Items.Clear();
        //                    listView1.Items.Add(item);
        //                    EnableControllerFunctionality();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Selected controller not available.");
        //        }
        //    }
        //    controller_State_Setting();
        //}

        private void btnProduction_Click(object sender, EventArgs e)
        {
            if (this.tempRobot._controller.IsVirtual)
            {
                this.tempRobot.StartProduction();
              //  StartProduction();
            }
            else
            {
                if (MessageBox.Show("Do you want to start production for the selected controller?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.tempRobot.StartProduction();
                    //StartProduction();
                }
            }


        }
        private void StartProduction()
        {
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        //tasks[0].ResetProgramPointer();
                        this.tempRobot._controller.Rapid.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }

        private void brnRead_Click(object sender, EventArgs e)
        {
            string result;
           // this.tempRobot.readLoaderString("TestStr", out reult);
            try
            {
                //readString("T_ROB1", "PC_Test", variable, out result);
                if (this.tempRobot.readString("T_ROB1", "PC_Test", "TestStr", out result) != RobotStatus.finish)
               // if (this.tempRobot.readUnLoaderString("TestStr", out reult) != robotStatus.finish)
                //if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                //{
                //    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                //    {
                //        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "TestStr");
                //        textBoxString.Text = rd.Value.ToString();
                //    }
                //}
                //else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
                else
                {
                    textBoxString.Text = result;
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }

        }

        private void changeCmdType(string cmd)
        {
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "cmdType");
                        ABB.Robotics.Controllers.RapidDomain.String rapidString;
                        rapidString.FillFromString(cmd);
                        rd.Value = rapidString;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {

            try
            {
                //if (this.tempRobot.writeLoaderString("TestStr", textBoxString.Text) != robotStatus.finish)
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "TestStr");
                        ABB.Robotics.Controllers.RapidDomain.String rapidString;
                        rapidString.FillFromString(textBoxString.Text);
                        rd.Value = rapidString;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }




        private void WriteBtn_PX_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData CS_InputType = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "InputType");
                        RapidData CS_Point1 = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "PXArray");
                        RapidData CS_Euler1 = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "EulerArray");
                        
                        int PxSelect = (int)numericUpDown1.Value-1;
                        RobTarget CS_p1 = (RobTarget)CS_Point1.ReadItem(PxSelect); // index start from "0"

                        Num CS_EulerRx = (Num)CS_Euler1.ReadItem(PxSelect, 0);
                        Num CS_EulerRy = (Num)CS_Euler1.ReadItem(PxSelect, 1);
                        Num CS_EulerRz = (Num)CS_Euler1.ReadItem(PxSelect, 2);

                        CS_p1.Trans.X = Convert.ToSingle(textBox_X.Text);
                        CS_p1.Trans.Y = Convert.ToSingle(textBox_Y.Text);
                        CS_p1.Trans.Z = Convert.ToSingle(textBox_Z.Text);

                        CS_EulerRx.FillFromString(textBox_Rx.Text);
                        CS_EulerRy.FillFromString(textBox_Ry.Text);
                        CS_EulerRz.FillFromString(textBox_Rz.Text);

                        CS_Point1.WriteItem(CS_p1, PxSelect);


                        CS_Euler1.WriteItem(CS_EulerRx, PxSelect, 0);
                        CS_Euler1.WriteItem(CS_EulerRy, PxSelect, 1);
                        CS_Euler1.WriteItem(CS_EulerRz, PxSelect, 2);

                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }


        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData CS_InputType = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "InputType");
                        RapidData CS_Point1 = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "PXArray");
                        RapidData CS_Euler1 = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "EulerArray");

                        int PxSelect = (int)numericUpDown1.Value - 1;
                        RobTarget CS_p1 = (RobTarget)CS_Point1.ReadItem(PxSelect);

                        Num CS_EulerRx = (Num)CS_Euler1.ReadItem(PxSelect, 0); // index start from "0"
                        Num CS_EulerRy = (Num)CS_Euler1.ReadItem(PxSelect, 1);
                        Num CS_EulerRz = (Num)CS_Euler1.ReadItem(PxSelect, 2);
                        
                        textBox_X.Text = CS_p1.Trans.X.ToString();
                        textBox_Y.Text = CS_p1.Trans.Y.ToString();
                        textBox_Z.Text = CS_p1.Trans.Z.ToString();

                        textBox_Rx.Text = CS_EulerRx.ToString();
                        textBox_Ry.Text = CS_EulerRy.ToString();
                        textBox_Rz.Text = CS_EulerRz.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }

        }
        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                RobotStatus result=this.tempRobot.Stop_Click();
                if (result != RobotStatus.finish)
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
                //if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                //{
                //    Mastership m = Mastership.Request(this.tempRobot._controller.Rapid);
                //    tasks[0].Stop(StopMode.Immediate);
                //    m.Dispose();
                //}
                //else
                //{
                //    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                //}
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }
        //private void Stop_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        tasks = this.tempRobot._controller.Rapid.GetTasks();
        //        if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
        //        {
        //            Mastership m = Mastership.Request(this.tempRobot._controller.Rapid);
        //            tasks[0].Stop(StopMode.Immediate);
        //            m.Dispose();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Automatic mode is required to start execution from a remote client.");
        //        }
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        MessageBox.Show("Mastership is held by another client. " + ex.Message);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("Unexpected error occurred: " + ex.Message);
        //    }
        //}


        private void UpdateGUI(object sender, System.EventArgs e)
        {


            try
            {

                this.tempRobot.NumIndex1 = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "User", "reg1");
                this.tempRobot.di01 = this.tempRobot._controller.IOSystem.GetSignal("DI10_1");
                this.tempRobot.di02 = this.tempRobot._controller.IOSystem.GetSignal("DI10_2");
               //this.tempRobot.di01 = this.tempRobot._controller.IOSystem.GetSignal("DI10_1");
                this.tempRobot.do01 = this.tempRobot._controller.IOSystem.GetSignal("DO10_1");
                this.tempRobot.do02 = this.tempRobot._controller.IOSystem.GetSignal("DO10_2");

               this.tempRobot.doRobotRunning = this.tempRobot._controller.IOSystem.GetSignal("DO10_16");

               this.tempRobot.sigDI01 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)this.tempRobot.di01;
               this.tempRobot.sigDI02 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)this.tempRobot.di02;
               //this.tempRobot.sigDI02 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)di02;
               this.tempRobot.cs_di01 = this.tempRobot.sigDI01.Get();
               this.tempRobot.sigDO01 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)this.tempRobot.do01;
               this.tempRobot.sigDO02 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)this.tempRobot.do02;
               this.tempRobot.cs_do01 = this.tempRobot.sigDO01.Get();
               this.tempRobot.cs_do02 = this.tempRobot.sigDO02.Get();

               this.tempRobot.sigDORobotRunning = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)this.tempRobot.doRobotRunning;
               this.tempRobot.cs_DORobotRunning = this.tempRobot.sigDORobotRunning.Get();






                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    label14.ForeColor = Color.Green;
                    label14.Text = "Auto Mode";

                }
                else if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.ManualReducedSpeed)
                {
                    label14.ForeColor = Color.YellowGreen;
                    label14.Text = "Manual Mode";
                }


                if (this.tempRobot.cs_DORobotRunning == 1)
                {
                    //robot is running
                   label_Running.Text = "Robot Running...";

                }
                else
                {
                    //robot is stopped
                    label_Running.Text = "Robot Stopped...";

                }

                if (this.tempRobot.cs_di01 == 0)
                {
                    button_DI1.BackColor = Color.Gray;
                }
                else
                {
                    button_DI1.BackColor = Color.Green;
                }

                if (this.tempRobot.cs_do01 == 0)
                {
                    button_DO1.BackColor = Color.Gray;
                }
                else
                {
                    button_DO1.BackColor = Color.Green;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }


        private void controller_State_Setting()
        {

            this.tempRobot._controller.OperatingModeChanged += new EventHandler<ABB.Robotics.Controllers.OperatingModeChangeEventArgs>(controller_OperatingModeChanged);

            this.tempRobot.di01 = this.tempRobot._controller.IOSystem.GetSignal("DI10_1");
            this.tempRobot.di01.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);

            this.tempRobot.di02 = this.tempRobot._controller.IOSystem.GetSignal("DI10_2");
            this.tempRobot.di02.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);
          //  this.tempRobot.di02 = this.tempRobot._controller.IOSystem.GetSignal("DI10_1");
           // this.tempRobot.di02.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);

            this.tempRobot.do01 = this.tempRobot._controller.IOSystem.GetSignal("DO10_1");
            this.tempRobot.do01.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);

            this.tempRobot.do02 = this.tempRobot._controller.IOSystem.GetSignal("DO10_2");
            this.tempRobot.do02.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);

            this.tempRobot.doRobotRunning = this.tempRobot._controller.IOSystem.GetSignal("DO10_16");
            this.tempRobot.doRobotRunning.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);
        }
        private void controller_OperatingModeChanged(object sender, OperatingModeChangeEventArgs e)
        {
            this.Invoke(new EventHandler(UpdateGUI), sender, e);
        }


        private void reg1_ValueChanged(object sender, DataValueChangedEventArgs e)
        {
            this.Invoke(new EventHandler(UpdateGUI), sender, e);
        }

        private void sig_Changed(object sender, ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs e)
        {
            this.Invoke(new EventHandler(UpdateGUI), sender, e);
        }

        private void button_DO1_Click(object sender, EventArgs e)
        {
            if (this.tempRobot.cs_do01 == 0)
            {
                this.tempRobot.sigDO01.Set();
            }
            else
            {
                this.tempRobot.sigDO01.Reset();
            }
            UpdateGUI(sender, e);
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
          
        }

        private void up_down_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btn_start_Click(object sender, EventArgs e)
        {

        }

        private void btn_change_Click(object sender, EventArgs e)
        {

        }

        private void btn_start_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        public void savePosition(string varName, ABB.Robotics.Controllers.RapidDomain.RobTarget newTarget)
        {
           
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", varName);
                        //ABB.Robotics.Controllers.RapidDomain.RobTarget newTarget = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
                        
                        //ABB.Robotics.Controllers.RapidDomain.String rapidString;
                        
                        //rapidString.FillFromString(cmd);
                        rd.Value = newTarget;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }
        public void saveInt(string varName,int intValue)
        {

            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", varName);
                        //ABB.Robotics.Controllers.RapidDomain.RobTarget newTarget = new ABB.Robotics.Controllers.RapidDomain.RobTarget();

                        ABB.Robotics.Controllers.RapidDomain.Num rapidNum=new  ABB.Robotics.Controllers.RapidDomain.Num(intValue);

        //                rapidNum.FillFromString2(value.ToString());
                        rd.Value = rapidNum;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            //abbRecipe temp = new abbRecipe("tester1", this.testCmds);
            //this.mainFunction.addRecipe(temp);
            //this.mainFunction.loadCurrentRecipeToRob(0);

            //if (this.tempRobot._controller.IsVirtual)
            //{

            //    StartProduction();
            //}
            //else
            //{
            //    if (MessageBox.Show("Do you want to start production for the selected controller?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        StartProduction();
            //    }
            //}
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BTN_READSTRING_MINE_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tempRobot._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    string varName = TXTBOX_NAME.Text;
                    using (Mastership m = Mastership.Request(this.tempRobot._controller.Rapid))
                    {
                       // RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", "TestStr");
                        RapidData rd = this.tempRobot._controller.Rapid.GetRapidData("T_ROB1", "PC_Test", varName);
                        TXTBOX_VALUE.Text = rd.Value.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client. " + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }

        private void btn_autoStart_Click(object sender, EventArgs e)
        {
            this.mainController.AutoStart();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.mainController.Load(1);
        }

        private void btn_loadRec_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"c:\MyFolder\Default\"; // 檔案對話方框開啟的預設資料夾

            // 設定可以選擇的檔案類型

            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";

            openFileDialog1.CheckFileExists = true;             // 若檔案/路徑 不存在是否顯示錯誤訊息

            openFileDialog1.CheckPathExists = false;

            DialogResult result = openFileDialog1.ShowDialog();     // 顯示檔案對話方框並回傳狀態（DialogResult.OK、DialogResult.Cancel）

            if (result == DialogResult.OK)
            {

                // 操作檔案 openFileDialog1.FileName
                string name1 = openFileDialog1.FileName;
                this.dataGridView3.Rows.Clear();
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                int index = 0;
                while (!sr.EndOfStream)
                {               // 每次讀取一行，直到檔尾

                    string line = sr.ReadLine();            // 讀取文字到 line 變數
                    
                    string[] results = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (results.Length > 1)
                    {
                        this.dataGridView3.Rows.Add();
                        this.dataGridView3.Rows[index].Cells[0].Value = results[0];
                        this.dataGridView3.Rows[index].Cells[1].Value = results[1];
                        index++;
                    }
                }

                sr.Close();
            }
        }

        private void btn_readRec_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save an TXT File";
           

            DialogResult result = saveFileDialog1.ShowDialog();     // 顯示檔案對話方框並回傳狀態（DialogResult.OK、DialogResult.Cancel）

            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < this.dataGridView3.Rows.Count; i++)
                {
                    string line = this.dataGridView3.Rows[i].Cells[0].Value + " " + this.dataGridView3.Rows[i].Cells[1].Value;
                    sw.WriteLine(line);
                }
              

                sw.Close();
            }
        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"c:\MyFolder\Default\"; // 檔案對話方框開啟的預設資料夾

            // 設定可以選擇的檔案類型

            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";

            openFileDialog1.CheckFileExists = true;             // 若檔案/路徑 不存在是否顯示錯誤訊息

            openFileDialog1.CheckPathExists = false;

            DialogResult result = openFileDialog1.ShowDialog();     // 顯示檔案對話方框並回傳狀態（DialogResult.OK、DialogResult.Cancel）

            if (result == DialogResult.OK)
            {    
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                int index = 0;
                List<string> ip=new List<string>();
               
                while (!sr.EndOfStream)
                {               // 每次讀取一行，直到檔尾

                    string line = sr.ReadLine();            // 讀取文字到 line 變數
                    ip.Add(line);
                    //ip[index] = line;
                    index += 1;
                    if (index >= 21)
                        break;
                }
                CRecipeGroup curData = new CRecipeGroup(ip);
                this.txt_recID.Text = curData.recName;
                this.txt_Loader_L1.Text = curData.loaderRecipes[0].targetFunc;
                this.txt_Loader_L2.Text = curData.loaderRecipes[1].targetFunc;
                this.txt_Loader_L3.Text = curData.loaderRecipes[2].targetFunc;
                this.txt_Loader_L4.Text = curData.loaderRecipes[3].targetFunc;
                this.txt_Loader_L5.Text = curData.loaderRecipes[4].targetFunc;

                this.txt_Loader_R1.Text = curData.loaderRecipes[5].targetFunc;
                this.txt_Loader_R2.Text = curData.loaderRecipes[6].targetFunc;
                this.txt_Loader_R3.Text = curData.loaderRecipes[7].targetFunc;
                this.txt_Loader_R4.Text = curData.loaderRecipes[8].targetFunc;
                this.txt_Loader_R5.Text = curData.loaderRecipes[9].targetFunc;

                this.txt_Unloader_L1.Text = curData.unloaderRecipes[0].targetFunc;
                this.txt_Unloader_L2.Text = curData.unloaderRecipes[1].targetFunc;
                this.txt_Unloader_L3.Text = curData.unloaderRecipes[2].targetFunc;
                this.txt_Unloader_L4.Text = curData.unloaderRecipes[3].targetFunc;
                this.txt_Unloader_L5.Text = curData.unloaderRecipes[4].targetFunc;

                this.txt_Unloader_R1.Text = curData.unloaderRecipes[5].targetFunc;
                this.txt_Unloader_R2.Text = curData.unloaderRecipes[6].targetFunc;
                this.txt_Unloader_R3.Text = curData.unloaderRecipes[7].targetFunc;
                this.txt_Unloader_R4.Text = curData.unloaderRecipes[8].targetFunc;
                this.txt_Unloader_R5.Text = curData.unloaderRecipes[9].targetFunc;

                sr.Close();
            }
        }

        private void btn_saveRec_Click(object sender, EventArgs e)
        {
            //List<string> result = new List<string>();
            CRecipeGroup curData = new CRecipeGroup();
            curData.recName = this.txt_recID.Text;
            curData.loaderRecipes[0].targetFunc = this.txt_Loader_L1.Text;// = curData.loaderRecipes[0].targetFunc;
            curData.loaderRecipes[1].targetFunc = this.txt_Loader_L2.Text;
            curData.loaderRecipes[2].targetFunc = this.txt_Loader_L3.Text;
            curData.loaderRecipes[3].targetFunc = this.txt_Loader_L4.Text;
            curData.loaderRecipes[4].targetFunc = this.txt_Loader_L5.Text;

            curData.loaderRecipes[5].targetFunc = this.txt_Loader_R1.Text;
            curData.loaderRecipes[6].targetFunc = this.txt_Loader_R2.Text;
            curData.loaderRecipes[7].targetFunc = this.txt_Loader_R3.Text;
            curData.loaderRecipes[8].targetFunc = this.txt_Loader_R4.Text;
            curData.loaderRecipes[9].targetFunc = this.txt_Loader_R5.Text;

            curData.unloaderRecipes[0].targetFunc = this.txt_Unloader_L1.Text;
            curData.unloaderRecipes[1].targetFunc = this.txt_Unloader_L2.Text;
            curData.unloaderRecipes[2].targetFunc = this.txt_Unloader_L3.Text;
            curData.unloaderRecipes[3].targetFunc = this.txt_Unloader_L4.Text;
            curData.unloaderRecipes[4].targetFunc = this.txt_Unloader_L5.Text;

            curData.unloaderRecipes[5].targetFunc = this.txt_Unloader_R1.Text;
            curData.unloaderRecipes[6].targetFunc = this.txt_Unloader_R2.Text;
            curData.unloaderRecipes[7].targetFunc = this.txt_Unloader_R3.Text;
            curData.unloaderRecipes[8].targetFunc = this.txt_Unloader_R4.Text;
            curData.unloaderRecipes[9].targetFunc = this.txt_Unloader_R5.Text;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save an TXT File";


            DialogResult result = saveFileDialog1.ShowDialog();     // 顯示檔案對話方框並回傳狀態（DialogResult.OK、DialogResult.Cancel）

            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.WriteLine(curData.recName);
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine(curData.loaderRecipes[i].targetFunc);
                }
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine(curData.unloaderRecipes[i].targetFunc);
                }
                


                sw.Close();
            }
        }

        private void btn_clearRec_Click(object sender, EventArgs e)
        {
            this.txt_recID.Text = "";
            this.txt_Loader_L1.Text = "";
            this.txt_Loader_L2.Text = "";
            this.txt_Loader_L3.Text = "";
            this.txt_Loader_L4.Text = "";
            this.txt_Loader_L5.Text = "";

            this.txt_Loader_R1.Text = "";
            this.txt_Loader_R2.Text = "";
            this.txt_Loader_R3.Text = "";
            this.txt_Loader_R4.Text = "";
            this.txt_Loader_R5.Text = "";

            this.txt_Unloader_L1.Text = "";
            this.txt_Unloader_L2.Text = "";
            this.txt_Unloader_L3.Text = "";
            this.txt_Unloader_L4.Text = "";
            this.txt_Unloader_L5.Text = "";

            this.txt_Unloader_R1.Text = "";
            this.txt_Unloader_R2.Text = "";
            this.txt_Unloader_R3.Text = "";
            this.txt_Unloader_R4.Text = "";
            this.txt_Unloader_R5.Text = "";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.dataGridView3.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                this.dataGridView3.Rows.Add();
            }
        }

        private void btn_addRow_Click(object sender, EventArgs e)
        {
            this.dataGridView3.Rows.Add();
        }

        private void TAB_MAIN_Click(object sender, EventArgs e)
        {

        }

        private void btn_changeLoader_left_Click(object sender, EventArgs e)
        {
            
            int frameSize ;//= int.Parse(txt_frameSize.Text);
            bool isValid= int.TryParse(txt_frameSize_Loader2.Text, out frameSize);
            if (isValid)
                this.mainController.SetLoader(txt_barcodeID_Loader2.Text, frameSize, true);
            else
                MessageBox.Show("error");
        }

        private void btn_changeLoader_right_Click(object sender, EventArgs e)
        {
            int frameSize;//= int.Parse(txt_frameSize.Text);
            bool isValid = int.TryParse(txt_frameSize_Loader2.Text, out frameSize);
            if (isValid)
            {
                this.mainController.SetLoader(txt_barcodeID_Loader2.Text, frameSize, false);

            }
            else
                MessageBox.Show("error");
        }

        public void updateLoaderFrameSize(int size, bool isLeft)
        {
            if (isLeft)
            {
                this.lb_LoaderLeftFrameSize.Text = size.ToString();
                this.lb_LoaderLeftFrameSize2.Text = size.ToString();
            }
            else
            {
                this.lb_LoaderRightFrameSize.Text = size.ToString();
                this.lb_LoaderRightFrameSize2.Text = size.ToString();
            }

        }
        public void updateLoaderFrameBarcode(string barcode, bool isLeft)
        {
            if (isLeft)
            {
                this.lb_LoaderLeftFrameBarcode.Text = barcode;
                this.lb_LoaderLeftFrameBarcode2.Text = barcode;
            }
            else
            {
                this.lb_LoaderRightFrameBarcode.Text = barcode;
                this.lb_LoaderRightFrameBarcode2.Text = barcode;
            }
        }
        public void updateUnloaderFrameSize(int size, bool isLeft)
        {
            if (isLeft)
            {
                this.lb_UnloaderLeftFrameSize.Text = size.ToString();
                this.lb_UnloaderLeftFrameSize2.Text = size.ToString();
            }
            else
            {
                this.lb_UnloaderRightFrameSize.Text = size.ToString();
                this.lb_UnloaderRightFrameSize2.Text = size.ToString();
            }

        }
        public void updateUnloaderFrameBarcode(string barcode, bool isLeft)
        {
            if (isLeft)
            {
                this.lb_UnloaderLeftFrameBarcode.Text = barcode;
                this.lb_UnloaderLeftFrameBarcode2.Text = barcode;
            }
            else
            {
                this.lb_UnloaderRightFrameBarcode.Text = barcode;
                this.lb_UnloaderRightFrameBarcode2.Text = barcode;
            }
        }

        public void setLoaderStatuss(LoaderStatus curSt)
        {
            this.lb_Loader_status.Text = curSt.ToString();
        }
        public void setUnloaderStatuss(UnloaderStatus curSt)
        {
            this.lb_Unloader_status.Text = curSt.ToString();
        }

        private void SimulationDirectRobotMoving(object sender, EventArgs e)
        {
            int selectID =0;
            int.TryParse(txt_runPos.Text, out selectID);
            selectID -= 1;
            if (selectID < 0)
                selectID = 0;
            if (selectID >= 10)
                selectID = 9;
            if (this.chk_simuRunLoader.Checked)
            {
                this.mainController.Load(selectID);
                Thread.Sleep(500);
            }
            if (this.chk_simuRunUnloader.Checked)
            {
                this.mainController.Unload(selectID);
                Thread.Sleep(500);
            }
            //txt_runPos.Text = (selectID + 1).ToString();
            //this.mainController.unload(selectID);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.mainController.UpdateInterface();
        }

        private void btn_leftCarOut_Click(object sender, EventArgs e)
        {

            this.mainController.ChangeCar_Loader(0, ChangeCarStep.CAR_OUT);
        }

        private void btn_leftCarIn_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Loader(0, ChangeCarStep.CAR_IN);
        }

        private void btn_leftCarInFinish_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Loader(0, ChangeCarStep.CAR_IN_FINISH);
        }

        private void btn_rightCarOut_Loader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Loader(1, ChangeCarStep.CAR_OUT);
        }
        private void btn_rightCarIn_Loader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Loader(1, ChangeCarStep.CAR_IN);
        }
        private void btn_rightCarInFinish_Loader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Loader(1, ChangeCarStep.CAR_IN_FINISH);
        }


        private void btn_rightCarOut_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(1, ChangeCarStep.CAR_OUT);
        }

        private void btn_rightCarIn_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(1, ChangeCarStep.CAR_IN);
        }

        private void btn_rightCarInFinish_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(1, ChangeCarStep.CAR_IN_FINISH);
        }

        private void btn_leftCarOut_Unloader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(0, ChangeCarStep.CAR_OUT);
        }

        private void btn_leftCarIn_Unloader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(0, ChangeCarStep.CAR_IN);
        }

        private void btn_leftCarInFinish_Unloader_Click(object sender, EventArgs e)
        {
            this.mainController.ChangeCar_Unloader(0, ChangeCarStep.CAR_IN_FINISH);
        }

        private void btn_changeUnloader_left_Click(object sender, EventArgs e)
        {
            int frameSize;//= int.Parse(txt_frameSize.Text);
            bool isValid = int.TryParse(txt_frameSize_Loader2.Text, out frameSize);
            if (isValid)
                this.mainController.SetUnloader(txt_barcodeID_Loader2.Text, frameSize, true);
            else
                MessageBox.Show("error");
        }

        private void btn_changeUnloader_right_Click(object sender, EventArgs e)
        {
            int frameSize;//= int.Parse(txt_frameSize.Text);
            bool isValid = int.TryParse(txt_frameSize_Loader2.Text, out frameSize);
            if (isValid)
                this.mainController.SetUnloader(txt_barcodeID_Loader2.Text, frameSize, false);
            else
                MessageBox.Show("error");
        }

        private void btn_loadRecFull_Click_1(object sender, EventArgs e)
        {
          
            {

                // 操作檔案 openFileDialog1.FileName
                string name1 = @"C:\Users\erichou\Desktop\資料庫\menu.txt";
                this.dataGridView3.Rows.Clear();
                StreamReader sr = new StreamReader(name1);
                int index = 0;
                while (!sr.EndOfStream)
                {               // 每次讀取一行，直到檔尾

                    string line = sr.ReadLine();            // 讀取文字到 line 變數

                    string[] results = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (results.Length > 1)
                    {
                        this.dataGridView3.Rows.Add();
                        this.dataGridView3.Rows[index].Cells[0].Value = results[0];
                        this.dataGridView3.Rows[index].Cells[1].Value = results[1];
                        index++;
                    }
                }

                sr.Close();
            }
        }

        private void btn__cmdRunLeft_loader_Click(object sender, EventArgs e)
        {
            bool isLeft = true;
            this.mainController.LoadBySide(isLeft);
            Thread.Sleep(500);
        }

        private void btn__cmdRunRight_loader_Click(object sender, EventArgs e)
        {
            bool isLeft = false;
            this.mainController.LoadBySide(isLeft);
            Thread.Sleep(500);
        }

        private void btn__cmdRunLeft_unloader_Click(object sender, EventArgs e)
        {
            bool isLeft = true;
            this.mainController.UnloadBySide(isLeft);
            Thread.Sleep(500);
        }

        private void btn__cmdRunRight_unloader_Click(object sender, EventArgs e)
        {
            bool isLeft = false;
            this.mainController.UnloadBySide(isLeft);
            Thread.Sleep(500);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            this.mainController.SetLoader(6, true);
            this.mainController.SetLoader(6, false);
            this.mainController.SetUnloader(6, true);
            this.mainController.SetUnloader(6, false);
        }

        private void btn_saveRecFull_Click(object sender, EventArgs e)
        {
            {
                string name1 = @"C:\Users\erichou\Desktop\資料庫\menu.txt";
                StreamWriter sw = new StreamWriter(name1);
                for (int i = 0; i < this.dataGridView3.Rows.Count; i++)
                {
                    string line = this.dataGridView3.Rows[i].Cells[0].Value + " " + this.dataGridView3.Rows[i].Cells[1].Value;
                    sw.WriteLine(line);
                }


                sw.Close();
                int k = 11;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormWarningSign warningSign = new FormWarningSign();
           warningSign.Show();
         
        }
     


        //private void btn_changeUnloader_right_Click(object sender, EventArgs e)
        //{
        //    int frameSize;//= int.Parse(txt_frameSize.Text);
        //    bool isValid = int.TryParse(txt_frameSize.Text, out frameSize);
        //    if (isValid)
        //    {
        //        this.mainController.setLoader(txt_barcodeID.Text, frameSize, false);

        //    }
        //    else
        //        MessageBox.Show("error");
        //}
      

       

    
    }

}
