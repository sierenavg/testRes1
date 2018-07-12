using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIC.Giant.Data;
using System.Windows.Forms;
using MIC.Giant.Controllers.MotionControl;
using MIC.Giant.Controllers.Station;
using MIC.Giant.Controllers.Communiction;
using MIC.Giant.Controllers.FileManagement;
using MIC.Giant.Controllers.SensorAndAlarm;
using MIC.Giant.Enum;
namespace MIC.Giant.Controllers
{
    class MainController
    {
       public LogManager _LogManager;
       public RobotManager _RobotManager;
       public ReaderManager _ReaderManager;
       public CommunicationManager _CommunicationManager;
       public SensorManager _SensorManager;
       public AlarmManager _AlarmManager;
       public MissionManager _MissionManager;
       public RecipeManager _RecipeManager;
       public SkidPlatformManager _SkidPlatformManager;
       public ExernalAxisManager _ExernalAxisManager;
        public Form1 _View;
        public MainController(Form1 view)
        {
            this._View = view;
            Initial();
        }
        void Initial()
        {
            _LogManager = new LogManager();
            _RobotManager = new RobotManager();
            _ReaderManager = new ReaderManager();
            _CommunicationManager = new CommunicationManager();
            _SensorManager = new SensorManager();
            _AlarmManager = new AlarmManager();
            _RecipeManager = new RecipeManager();
            _SkidPlatformManager = new SkidPlatformManager();
            _ExernalAxisManager = new ExernalAxisManager();
            _MissionManager = new MissionManager(_RobotManager, _RecipeManager, _AlarmManager
                , _CommunicationManager, _SkidPlatformManager, _ExernalAxisManager,_LogManager);
        }

        bool RunLoadArmRecipe()
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
        public List<ListViewItem> RunScanRobot()
         {
             return this._RobotManager._RobotStation.scanRobot();
            
         }
        public int InitialRobotCnnection(ListViewItem item)
        {
            return this._RobotManager._RobotStation.initialRobotConnection(item);
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

        public int AutoStart()
        {
            //檢查barcode reader 滑台 io 網路
            //selfCheckAll();
            if (MessageBox.Show("Do you want to start production for the selected controller?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this._RobotManager._RobotStation.start();
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
        public int LoadBySide(bool isLeft)
        {
            MissionResult r = this._MissionManager.RunLoader(isLeft);
            switch (r)
            {
                case MissionResult.START:
                    // MessageBox.Show("Mission Start");
                    break;
                default:
                    MessageBox.Show("loader Error " + r.ToString());
                    break;
            }
            // MessageBox.Show("finish");
            return 1;
            //return this.m_processManager.
            //throw new NotImplementedException();
        }
        public int Load(int posId)
        {
            MissionResult r= this._MissionManager.RunLoader(posId);
            switch (r)
            {
                case MissionResult.START:
                   // MessageBox.Show("Mission Start");
                    break;
                default:
                    MessageBox.Show("loader Error "+r.ToString());
                    break;
            }
           // MessageBox.Show("finish");
            return 1;
            //return this.m_processManager.
            //throw new NotImplementedException();
        }
        public int UnloadBySide(bool isLeft)
        {
            MissionResult r = this._MissionManager.RunUnloader(isLeft);
            switch (r)
            {
                case MissionResult.START:
                    // MessageBox.Show("Mission Start");
                    break;
                default:
                    MessageBox.Show("unloader Error " + r.ToString());
                    break;
            }
            // MessageBox.Show("finish");
            return 1;
            //return this.m_processManager.
            //throw new NotImplementedException();
        }
        public int Unload(int posId)
        {
            MissionResult r = this._MissionManager.RunUnloader(posId);
            switch (r)
            {
                case MissionResult.START:
                    // MessageBox.Show("Mission Start");
                    break;
                default:
                    MessageBox.Show("unloader Error " + r.ToString());
                    break;
            }
            // MessageBox.Show("finish");
            return 1;
            //return this.m_processManager.
            //throw new NotImplementedException();
        }
        private void UpdateFile()
        {
            throw new NotImplementedException();
        }

        private void SelfCheckAll()
        {
            throw new NotImplementedException();
        }

        internal void SetLoader( int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Loader( frameSize, isLeft);
            this._View.updateLoaderFrameSize(frameSize, isLeft);
           // this._View.updateLoaderFrameBarcode(barcodeID, isLeft);
            // if(isLeft)
            //   this.view.lb_
        }

        internal void SetLoader(string barcodeID, int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Loader(barcodeID, frameSize, isLeft);
            this._View.updateLoaderFrameSize(frameSize, isLeft);
            this._View.updateLoaderFrameBarcode(barcodeID, isLeft);
            // if(isLeft)
            //   this.view.lb_
        }
        internal void SetUnloader( int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Unloader( frameSize, isLeft);
            this._View.updateUnloaderFrameSize(frameSize, isLeft);
            
           
        }
        internal void SetUnloader(string barcodeID, int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Unloader(barcodeID, frameSize, isLeft);
            this._View.updateUnloaderFrameSize(frameSize, isLeft);
            this._View.updateUnloaderFrameBarcode(barcodeID, isLeft);
            // if(isLeft)
            //   this.view.lb_
        }

        internal void UpdateInterface()
        {
            this._View.updateLoaderFrameSize(this._MissionManager.LoaderItemNum_left, true);
            this._View.updateLoaderFrameSize(this._MissionManager.LoaderItemNum_right, false);
            this._View.updateLoaderFrameBarcode(this._MissionManager.LoaderItemID_left, true);
            this._View.updateLoaderFrameBarcode(this._MissionManager.LoaderItemID_right, false);

            this._View.updateUnloaderFrameSize(this._MissionManager.UnloaderItemNum_left, true);
            this._View.updateUnloaderFrameSize(this._MissionManager.UnloaderItemNum_right, false);
            this._View.updateUnloaderFrameBarcode(this._MissionManager.UnloaderItemID_left, true);
            this._View.updateUnloaderFrameBarcode(this._MissionManager.UnloaderItemID_right, false);

            this._View.setLoaderStatuss(this._MissionManager.GetStatus_Loader());
            this._View.setUnloaderStatuss(this._MissionManager.GetStatus_Unloader());
        }

        internal void ChangeCar(int p)
        {
            throw new NotImplementedException();
        }

        internal void ChangeCar_Loader(int carSideId, ChangeCarStep curStep)
        {
            //throw new NotImplementedException();
            MissionResult r = this._MissionManager.RunChangeCar_Loader(carSideId, curStep);
        }

        internal void ChangeCar_Unloader(int carSideId, ChangeCarStep curStep)
        {
            MissionResult r = this._MissionManager.RunChangeCar_Unloader(carSideId, curStep);
            //throw new NotImplementedException();
        }
        
    }
}
