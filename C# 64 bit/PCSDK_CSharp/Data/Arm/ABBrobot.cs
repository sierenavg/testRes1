using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using MIC.Giant.Controllers;
using MIC.Giant.Enum;
namespace MIC.Giant.Data.Arm
{
    class ABBrobot : CRobotStation
    {
        public NetworkScanner scanner = null;
        public Controller _controller = null;

        public RapidData NumIndex1 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.Signal di01 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.Signal di02 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.Signal do01 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.Signal do02 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.Signal doRobotRunning = null;
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDI01 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDI02 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDI0 = null;
     
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDO01 = null;
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDO02 = null;
        public int cs_di01;
        public int cs_do01;
        public int cs_do02;
        public ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal sigDORobotRunning = null;
        public int cs_DORobotRunning;

        private ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        List<string> testCmds;

        public List<ListViewItem> getConnectedRobot()
        {
           
            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            ListViewItem item = null;
            List<ListViewItem> itemS = new List<ListViewItem>();
            foreach (ControllerInfo controllerInfo in controllers)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                itemS.Add(item);
                item.Tag = controllerInfo;
            }
            return itemS;
        }
        public int initialConnection(ListViewItem item)
        {
            //ListViewItem item = this.listView1.SelectedItems[0];
            int result = 0;
            if (item.Tag != null)
            {
                ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                if (controllerInfo.Availability == Availability.Available)
                {
                    if (controllerInfo.IsVirtual)
                    {
                        this._controller = ControllerFactory.CreateFrom(controllerInfo);
                        this._controller.Logon(UserInfo.DefaultUser);
                        result= 1;
                        //listView1.Items.Clear();
                        //listView1.Items.Add(item);
                        //EnableControllerFunctionality();
                    }
                    else //real controller
                    {
                        if (MessageBox.Show("This is NOT a virtual controller, do you really want to connect to that?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            this._controller = ControllerFactory.CreateFrom(controllerInfo);
                            this._controller.Logon(UserInfo.DefaultUser);
                            result= 1;
                            //listView1.Items.Clear();
                            //listView1.Items.Add(item);
                            //EnableControllerFunctionality();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected controller not available.");
                    result= 0;
                }
            }
            //controller_State_Setting();
            return result;
        }
        private void controller_State_Setting()
        {

            //_controller.OperatingModeChanged += new EventHandler<ABB.Robotics.Controllers.OperatingModeChangeEventArgs>(controller_OperatingModeChanged);


            //di01 = _controller.IOSystem.GetSignal("DI10_1");
            //di01.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);
            //do01 = _controller.IOSystem.GetSignal("DO10_1");
            //do01.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);

            //doRobotRunning = _controller.IOSystem.GetSignal("DO10_16");
            //doRobotRunning.Changed += new EventHandler<ABB.Robotics.Controllers.IOSystemDomain.SignalChangedEventArgs>(sig_Changed);
        }
   
        //public void UpdateGUI()
        //{


        //    try
        //    {

        //        NumIndex1 = _controller.Rapid.GetRapidData("T_ROB1", "User", "reg1");
        //        di01 = _controller.IOSystem.GetSignal("DI10_1");
        //        do01 = _controller.IOSystem.GetSignal("DO10_1");
        //        doRobotRunning = _controller.IOSystem.GetSignal("DO10_16");

        //        sigDI01 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)di01;
        //        cs_di01 = sigDI01.Get();
        //        sigDO01 = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)do01;
        //        cs_do01 = sigDO01.Get();

        //        sigDORobotRunning = (ABB.Robotics.Controllers.IOSystemDomain.DigitalSignal)doRobotRunning;
        //        cs_DORobotRunning = sigDORobotRunning.Get();


        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("Error occurred: " + ex.Message);
        //    }
        //}


        public void StartProduction()
        {
            try
            {
                if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    using (Mastership m = Mastership.Request(this._controller.Rapid))
                    {
                        //tasks[0].ResetProgramPointer();
                        this._controller.Rapid.Start();
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
        //IsRobotAllOk
        public bool IsRobotAllOk()
        {
            tasks = this._controller.Rapid.GetTasks();
            if (tasks[0].ExecutionStatus == TaskExecutionStatus.Running)
                return true;
            else
                return false;
            //if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
            //{
            //    Mastership m = Mastership.Request(this._controller.Rapid);
            //    tasks[0].Stop(StopMode.Immediate);
            //    //tasks[0].ExecutionStatus = TaskExecutionStatus.
            //    m.Dispose();
            //    return RobotStatus.finish;
            //}
            //else
            //{
            //    return RobotStatus.nonAuto;
            //    //MessageBox.Show("Automatic mode is required to start execution from a remote client.");
            //}
        }
        public RobotStatus Stop_Click()
        {
            tasks = this._controller.Rapid.GetTasks();
            if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                Mastership m = Mastership.Request(this._controller.Rapid);
                tasks[0].Stop(StopMode.Immediate);
                //tasks[0].ExecutionStatus = TaskExecutionStatus.
                m.Dispose();
                return RobotStatus.finish;
            }
            else
            {
                return RobotStatus.nonAuto;
                //MessageBox.Show("Automatic mode is required to start execution from a remote client.");
            }
            //try
            //{
            //    tasks = this._controller.Rapid.GetTasks();
            //    if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
            //    {
            //        Mastership m = Mastership.Request(this._controller.Rapid);
            //        tasks[0].Stop(StopMode.Immediate);
            //        m.Dispose();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Automatic mode is required to start execution from a remote client.");
            //    }
            //}
            //catch (System.InvalidOperationException ex)
            //{
            //    MessageBox.Show("Mastership is held by another client. " + ex.Message);
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("Unexpected error occurred: " + ex.Message);
            //}
        }

        //public string readLoaderString
        public RobotStatus readString(string controller, string modual, string variable, out string result)
        {
            if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(this._controller.Rapid))
                {
                    RapidData rd = this._controller.Rapid.GetRapidData(controller, modual, variable);
                    result = rd.Value.ToString();
                }
                return RobotStatus.finish;
            }
            else
            {
                result = "fail";
                return RobotStatus.nonAuto;
            }
         

        }
        public RobotStatus readLoaderString( string variable, out string result)
        {
            return readString("T_ROB1", "loader", variable, out result);
        }
        public RobotStatus readUnLoaderString(string variable, out string result)
        {
            return readString("T_ROB2", "unloader", variable, out result);
        }
        private RobotStatus writeString(string controller, string modual, string variableName,  string ipValue)
        {
            if (this._controller.OperatingMode == ControllerOperatingMode.Auto)
            {
                using (Mastership m = Mastership.Request(this._controller.Rapid))
                {
                    RapidData rd = this._controller.Rapid.GetRapidData(controller, modual, variableName);
                    ABB.Robotics.Controllers.RapidDomain.String rapidString;
                    rapidString.FillFromString(ipValue);
                    rd.Value = rapidString;
                }
                return RobotStatus.finish;
            }
            else
            {
                return RobotStatus.nonAuto;
            }
           
        }
        public RobotStatus writeLoaderString(string variable, string ipValue)
        {
            return writeString("T_ROB1", "loader", variable, ipValue);
        }
        public RobotStatus writeUnLoaderString(string variable, string ipValue)
        {
            return writeString("T_ROB2", "unloader", variable, ipValue);
        }
       public override List<ListViewItem> scanRobot()
       {
           this.scanner = new NetworkScanner();
           this.scanner.Scan();
           ControllerInfoCollection controllers = this.scanner.Controllers;

           List<ListViewItem> target = this.getConnectedRobot();
           return target;
       }
       public override int initialRobotConnection(ListViewItem item)
       {
           int status = this.initialConnection(item);
           return status;
           //if (item.Tag != null)
           //{
           //    int status = this.initialConnection(item);
           //    if (status == 1)
           //    {
           //        listView1.Items.Clear();
           //        listView1.Items.Add(item);
           //        EnableControllerFunctionality();
           //    }

           //}
           //controller_State_Setting();
       }


       public override int start()
       {
           this.StartProduction();
           return 0;
       }
    }
}
