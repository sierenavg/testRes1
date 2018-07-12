using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCSDK_Csharp_BasicScanApp_514.Data;
using System.Windows.Forms;
namespace PCSDK_Csharp_BasicScanApp_514.Controllers
{
    class CControler
    {
       public CLogManager m_logManager;
       public CRobotManager m_robotManager;
       public CReaderManager m_readerManager;
       public CcommunicationManager m_communicationManager;
       public CSensorManager m_sensorManager;
       public CAlarmManager m_alarmManager;
       public CMissionManager m_missionManager;

        public CControler()
        {
            initial();
        }
        void initial()
        {
            m_logManager = new CLogManager();
            m_robotManager = new CRobotManager();
            m_readerManager = new CReaderManager();
            m_communicationManager = new CcommunicationManager();
            m_sensorManager = new CSensorManager();
            m_alarmManager = new CAlarmManager();
            m_missionManager = new CMissionManager(m_robotManager);
        }

        bool runLoadArmRecipe()
        {
            throw new NotImplementedException();
        }
        bool RunUnloadArmRecipe()
        {
            throw new NotImplementedException();
        }
        bool ScanLeft()
        {
            throw new NotImplementedException();
        }
        bool ScanRight()
        {
            throw new NotImplementedException();
        }
        //SendMobus SaveLog SendTrollyInfoByInternet ChangeCar Start SendAlarm  SetRecipe
        bool SendMobus()
        {
            throw new NotImplementedException();
        }
        bool SaveLog()
        {
            throw new NotImplementedException();
        }
        bool SendTrollyInfoByInternet()
        {
            throw new NotImplementedException();
        }
        bool ChangeCar()
        {
            throw new NotImplementedException();
        }
        bool Start()
        {
            throw new NotImplementedException();
        }
         bool SendAlarm()
        {
            throw new NotImplementedException();
        }
         bool SetRecipe()
        {
            throw new NotImplementedException();
        }
        public List<ListViewItem> runScanRobot()
         {
             return this.m_robotManager.robotStation.scanRobot();
            
         }
        public int initialRobotCnnection(ListViewItem item)
        {
            return this.m_robotManager.robotStation.initialRobotConnection(item);
            //ListViewItem item = this.listView1.SelectedItems[0];
            //if (item.Tag != null)
            //{
            //    int status = tempRobot.initialConnection(item);
            //    if (status == 1)
            //    {
            //        listView1.Items.Clear();
            //        listView1.Items.Add(item);
            //        EnableControllerFunctionality();
            //    }

            //}
            //controller_State_Setting();
        }

        public int autoStart()
        {
            //檢查barcode reader 滑台 io 網路
            //selfCheckAll();
            if (MessageBox.Show("Do you want to start production for the selected controller?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.m_robotManager.robotStation.start();
                //StartProduction();

                //if (MessageBox.Show("是否補更換車架?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                //{
                //    ChangeCar();
                //}
                //ScanLeft();
                //ScanRight();
                //updateFile();
            }
            return 0;
        }

        public int load(int side,int index)
        {
            this.m_missionManager.runLoader(new Data.Reciepe.CRecipeGroup());
            return 1;
            //return this.m_processManager.
            //throw new NotImplementedException();
        }

        private void updateFile()
        {
            throw new NotImplementedException();
        }

        private void selfCheckAll()
        {
            throw new NotImplementedException();
        }

        
    }
}
