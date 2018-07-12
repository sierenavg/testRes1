using MIC.Giant.Controllers.Communiction;
using MIC.Giant.Controllers.FileManagement;
using MIC.Giant.Controllers.MotionControl;
using MIC.Giant.Controllers.SensorAndAlarm;
using MIC.Giant.Controllers.Station;
using MIC.Giant.Data.Reciepe;
using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers
{
    //public enum LoaderStatus
    //{
    //    WORKING_LEFT, WORKING_RIGHT, HOLDING
    //}
    //public enum UnloaderStatus
    //{
    //    WORKING_LEFT, WORKING_RIGHT, HOLDING
    //}
    //public enum ChangeCarStatus
    //{
    //    CHANGING_LEFT, CHANGING_RIGHT, HOLDING
    //}
    //enum MissionResult
    //{
    //    NORMAL, ROBOTERROR, ROBOTBUSY,NOFRAME,NOPATH,START,CHANGECARBUSY,FINISH,CMDERROR
    //}
    //public class ChangeCarCmd
    //{
    //    public bool isLeft, isLoader;
    //    public changeCarStep requestStep;

    //    public ChangeCarCmd(bool isLeft, bool isLoader,changeCarStep requestStep)
    //    {
    //        this.isLeft = isLeft;
    //        this.isLoader = isLoader;
    //        this.requestStep = requestStep;
    //    }


    //}
    class MissionManager
    {
        RobotManager _RobotManager;
        Task<MissionResult> _LoaderTask, _UnloaderTask, _ChangeCarTask_Loader, _ChangeCarTask_Unloader;

        MissionLoader _Loader;
        MissionUnloader _Unloader;
        MissionChangeCar _ChangeCar_Loader, _ChangeCar_Unloader;

        string _LoaderItemID_left, _LoaderItemID_right;
        string unloaderItemID_right, unloaderItemID_left;
        int loaderItemNum_left, loaderItemNum_right;
        int unloaderItemNum_left, unloaderItemNum_right;
        public string LoaderItemID_right
        {
            get { return _LoaderItemID_right; }
            set { _LoaderItemID_right = value; }
        }

        public string LoaderItemID_left
        {
            get { return _LoaderItemID_left; }
            set { _LoaderItemID_left = value; }
        }


        public string UnloaderItemID_left
        {
            get { return unloaderItemID_left; }
            set { unloaderItemID_left = value; }
        }

        public string UnloaderItemID_right
        {
            get { return unloaderItemID_right; }
            set { unloaderItemID_right = value; }
        }


        public int LoaderItemNum_right
        {
            get { return loaderItemNum_right; }
            set { loaderItemNum_right = value; }
        }

        public int LoaderItemNum_left
        {
            get { return loaderItemNum_left; }
            set { loaderItemNum_left = value; }
        }


        public int UnloaderItemNum_right
        {
            get { return unloaderItemNum_right; }
            set { unloaderItemNum_right = value; }
        }

        public int UnloaderItemNum_left
        {
            get { return unloaderItemNum_left; }
            set { unloaderItemNum_left = value; }
        }

        //loaderStatus loaderSta;
        // unloaderStatus    unloaderSta;
        RecipeManager _RecipeManager;
        AlarmManager _AlarmManager;
        public CommunicationManager _CommunicationManager;
        SkidPlatformManager _SkidPlatformManager;
        ExernalAxisManager _ExernalAxisManager;// = new CExernalAxisManager();
        MissionManagerLibrary _MissionManagerPartialCmd;
        LogManager _LogManager;
        void Initail()
        {
            _LoaderItemID_left = "Aaa_17_1"; _LoaderItemID_right = "Aaa_17_3";
            unloaderItemID_right = "Aaa_17_2"; unloaderItemID_left = "Aaa_17_4";
            loaderItemNum_left = 5; loaderItemNum_right = 5;
            unloaderItemNum_left = 5; unloaderItemNum_right = 5;
            //loaderTask = new Task<missionResult>();
            //unloaderTask = new Task<missionResult>();
            //loaderSta = loaderStatus.HOLDING;
            // unloaderSta = unloaderStatus.HOLDING;
            //loaderStatus= robotStatus.
        }
        public MissionManager(RobotManager robotManager, RecipeManager recipeManager,
            AlarmManager alarmManager, CommunicationManager communicationManager,
            SkidPlatformManager m_SkidPlatformManager, ExernalAxisManager m_exernalAxisManager
            , LogManager m_LogManager)
        {
            this._AlarmManager = alarmManager;
            this._CommunicationManager = communicationManager;
            this._RobotManager = robotManager;
            this._RecipeManager = recipeManager;
            this._SkidPlatformManager = m_SkidPlatformManager;
            this._ExernalAxisManager = m_exernalAxisManager;
            this._LogManager = m_LogManager;
            _MissionManagerPartialCmd = new MissionManagerLibrary(this);
            _Loader = new MissionLoader(robotManager);
            _Unloader = new MissionUnloader(robotManager);
            _ChangeCar_Loader = new MissionChangeCar(this._AlarmManager, this._SkidPlatformManager, this._ExernalAxisManager, this._MissionManagerPartialCmd);
            _ChangeCar_Unloader = new MissionChangeCar(this._AlarmManager, this._SkidPlatformManager, this._ExernalAxisManager, this._MissionManagerPartialCmd);

            Initail();
        }

        public MissionResult RunLoader(bool isLeft)
        {
            int posId;

            if (isLeft )
                posId = this.loaderItemNum_left-1;
            else
                posId = this.loaderItemNum_right - 1 + 5;

            return RunLoader(posId, isLeft);
        }

        public MissionResult RunLoader(int posId,bool isLeft)
        {
            //bool isLeft;
            //if (posId < 5)
            //    isLeft = true;
            //else
            //    isLeft = false;

            if (this._Loader.Status != LoaderStatus.HOLDING && !_LoaderTask.IsCompleted)
                return MissionResult.ROBOTBUSY;

            if (isLeft)
            {
                if (this.loaderItemNum_left <= 0)
                    return MissionResult.NOFRAME;
            }
            else
            {
                if (this.loaderItemNum_right <= 0)
                    return MissionResult.NOFRAME;
            }
            //還沒設定位置
            string barcodeID;

            if (!this._RobotManager.IsRobotAllOk())
                return MissionResult.ROBOTERROR;

            if (isLeft)

                barcodeID = this._LoaderItemID_left;
            else
                barcodeID = this._LoaderItemID_right;



            CRecipeGroup recG = this._RecipeManager.getRecipeGroupByBarcodeID(barcodeID);
            if (recG != null)
            {
                if (isLeft)
                    this._Loader.Status = LoaderStatus.WORKING_LEFT;
                else
                    this._Loader.Status = LoaderStatus.WORKING_RIGHT;

                if (isLeft)
                {
                    if (this.loaderItemNum_left == 1)
                        this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
                    this.loaderItemNum_left -= 1;
                }
                else
                {
                    if (this.loaderItemNum_right == 1)
                        this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
                    this.loaderItemNum_right -= 1;
                }

                _LoaderTask = new Task<MissionResult>(_Loader.Run, (object)(recG.loaderRecipes[posId]));
                _LoaderTask.Start();
                return MissionResult.START;
                //missionResult r= loaderTask.Result;

                //暫時 by pass
                //this.m_communicationManager.loadFinish(isLeft);

                // this.m_Loader.Status = loaderStatus.HOLDING;

                //return r;

            }
            else
            {
                return MissionResult.NOPATH;
            }
            //var testing = new Task<int>(tt, (object)recG);
            //testing.Start();
            //throw new NotImplementedException();
            // Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_Loader.run, recG);
        }
        
        public MissionResult RunLoader(int posId)
        {
            bool isLeft;
            if (posId < 5)
                isLeft = true;
            else
                isLeft = false;

            return RunLoader(posId, isLeft);

            //if (this._Loader.Status != LoaderStatus.HOLDING && !_LoaderTask.IsCompleted)
            //    return MissionResult.ROBOTBUSY;

            //if (isLeft)
            //{
            //    if (this.loaderItemNum_left <= 0)
            //        return MissionResult.NOFRAME;
            //}
            //else
            //{
            //    if (this.loaderItemNum_right <= 0)
            //        return MissionResult.NOFRAME;
            //}
            ////還沒設定位置
            //string barcodeID;

            //if (!this._RobotManager.IsRobotAllOk())
            //    return MissionResult.ROBOTERROR;

            //if (isLeft)

            //    barcodeID = this._LoaderItemID_left;
            //else
            //    barcodeID = this._LoaderItemID_right;



            //CRecipeGroup recG = this._RecipeManager.getRecipeGroupByBarcodeID(barcodeID);
            //if (recG != null)
            //{
            //    if (isLeft)
            //        this._Loader.Status = LoaderStatus.WORKING_LEFT;
            //    else
            //        this._Loader.Status = LoaderStatus.WORKING_RIGHT;

            //    if (isLeft)
            //    {
            //        if (this.loaderItemNum_left == 1)
            //            this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
            //        this.loaderItemNum_left -= 1;
            //    }
            //    else
            //    {
            //        if (this.loaderItemNum_right == 1)
            //            this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
            //        this.loaderItemNum_right -= 1;
            //    }

            //    _LoaderTask = new Task<MissionResult>(_Loader.Run, (object)(recG.loaderRecipes[posId]));
            //    _LoaderTask.Start();
            //    return MissionResult.START;
            //    //missionResult r= loaderTask.Result;

            //    //暫時 by pass
            //    //this.m_communicationManager.loadFinish(isLeft);

            //    // this.m_Loader.Status = loaderStatus.HOLDING;

            //    //return r;

            //}
            //else
            //{
            //    return MissionResult.NOPATH;
            //}
            ////var testing = new Task<int>(tt, (object)recG);
            ////testing.Start();
            ////throw new NotImplementedException();
            //// Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_Loader.run, recG);
        }

        public MissionResult RunUnloader(int posId,bool isLeft)
        {
            //bool isLeft;
            //if (posId < 5)
            //    isLeft = true;
            //else
            //    isLeft = false;

            if (this._Unloader.Status != UnloaderStatus.HOLDING && !_UnloaderTask.IsCompleted)
                return MissionResult.ROBOTBUSY;

            if (isLeft)
            {
                if (this.unloaderItemNum_left <= 0)
                    return MissionResult.NOFRAME;
            }
            else
            {
                if (this.unloaderItemNum_right <= 0)
                    return MissionResult.NOFRAME;
            }
            //還沒設定位置
            string barcodeID;

            if (!this._RobotManager.IsRobotAllOk())
                return MissionResult.ROBOTERROR;

            if (isLeft)

                barcodeID = this.unloaderItemID_left;
            else
                barcodeID = this.unloaderItemID_right;



            CRecipeGroup recG = this._RecipeManager.getRecipeGroupByBarcodeID(barcodeID);
            if (recG != null)
            {
                if (isLeft)
                    this._Unloader.Status = UnloaderStatus.WORKING_LEFT;
                else
                    this._Unloader.Status = UnloaderStatus.WORKING_RIGHT;

                if (isLeft)
                {
                    if (this.unloaderItemNum_left == 1)
                        this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
                    this.unloaderItemNum_left -= 1;
                }
                else
                {
                    if (this.unloaderItemNum_right == 1)
                        this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
                    this.unloaderItemNum_right -= 1;
                }

                _UnloaderTask = new Task<MissionResult>(_Unloader.Run, (object)(recG.unloaderRecipes[posId]));
                _UnloaderTask.Start();
                return MissionResult.START;


            }
            else
            {
                return MissionResult.NOPATH;
            }

            //var testing = new Task<int>(tt, (object)recG);
            //testing.Start();
            //throw new NotImplementedException();
            // Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_Loader.run, recG);
        }
        public MissionResult RunUnloader(bool isLeft)
        {
            int posId;

            if (isLeft)
                posId = this.unloaderItemNum_left - 1;
            else
                posId = this.unloaderItemNum_right - 1 + 5;

            return RunUnloader(posId, isLeft);
        }
        public MissionResult RunUnloader(int posId)
        {
            bool isLeft;
            if (posId < 5)
                isLeft = true;
            else
                isLeft = false;
            return RunUnloader(posId, isLeft);
            //if (this._Unloader.Status != UnloaderStatus.HOLDING && !_UnloaderTask.IsCompleted)
            //    return MissionResult.ROBOTBUSY;

            //if (isLeft)
            //{
            //    if (this.unloaderItemNum_left <= 0)
            //        return MissionResult.NOFRAME;
            //}
            //else
            //{
            //    if (this.unloaderItemNum_right <= 0)
            //        return MissionResult.NOFRAME;
            //}
            ////還沒設定位置
            //string barcodeID;

            //if (!this._RobotManager.IsRobotAllOk())
            //    return MissionResult.ROBOTERROR;

            //if (isLeft)

            //    barcodeID = this.unloaderItemID_left;
            //else
            //    barcodeID = this.unloaderItemID_right;



            //CRecipeGroup recG = this._RecipeManager.getRecipeGroupByBarcodeID(barcodeID);
            //if (recG != null)
            //{
            //    if (isLeft)
            //        this._Unloader.Status = UnloaderStatus.WORKING_LEFT;
            //    else
            //        this._Unloader.Status = UnloaderStatus.WORKING_RIGHT;

            //    if (isLeft)
            //    {
            //        if (this.unloaderItemNum_left == 1)
            //            this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
            //        this.unloaderItemNum_left -= 1;
            //    }
            //    else
            //    {
            //        if (this.unloaderItemNum_right == 1)
            //            this._AlarmManager.ShowFrameEmpty_Loader(isLeft);
            //        this.unloaderItemNum_right -= 1;
            //    }

            //    _UnloaderTask = new Task<MissionResult>(_Unloader.Run, (object)(recG.unloaderRecipes[posId]));
            //    _UnloaderTask.Start();
            //    return MissionResult.START;


            //}
            //else
            //{
            //    return MissionResult.NOPATH;
            //}

            //var testing = new Task<int>(tt, (object)recG);
            //testing.Start();
            //throw new NotImplementedException();
            // Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_Loader.run, recG);
        }
        internal void SetBarcodeByForce_Loader( int frameSize, bool isLeft)
        {
            if (isLeft)
            {
                
                this.loaderItemNum_left = frameSize;
            }
            else
            {
                
                this.loaderItemNum_right = frameSize;
            }
        }
        internal void SetBarcodeByForce_Loader(string barcodeID1, int frameSize, bool isLeft)
        {
            if (isLeft)
            {
                this._LoaderItemID_left = barcodeID1;
                this.loaderItemNum_left = frameSize;
            }
            else
            {
                this._LoaderItemID_right = barcodeID1;
                this.loaderItemNum_right = frameSize;
            }
        }
        internal void SetBarcodeByForce_Unloader( int frameSize, bool isLeft)
        {
            if (isLeft)
            {
                
                this.unloaderItemNum_left = frameSize;
            }
            else
            {
              
                this.unloaderItemNum_right = frameSize;
            }
        }
        internal void SetBarcodeByForce_Unloader(string barcodeID1, int frameSize, bool isLeft)
        {
            if (isLeft)
            {
                this.unloaderItemID_left = barcodeID1;
                this.unloaderItemNum_left = frameSize;
            }
            else
            {
                this.unloaderItemID_right = barcodeID1;
                this.unloaderItemNum_right = frameSize;
            }
        }

        internal LoaderStatus GetStatus_Loader()
        {
            if (_LoaderTask != null && _LoaderTask.IsCompleted)
            {
                this._Loader.Status = LoaderStatus.HOLDING;
            }
            return this._Loader.Status;
        }
        internal UnloaderStatus GetStatus_Unloader()
        {
            if (_UnloaderTask != null && _UnloaderTask.IsCompleted)
            {
                this._Unloader.Status = UnloaderStatus.HOLDING;
            }
            return this._Unloader.Status;
        }
        MissionResult CheckChangeCarLoader()
        {
            if (this._ChangeCar_Loader.Status != ChangeCarStatus.HOLDING && !this._ChangeCarTask_Loader.IsCompleted)
                return MissionResult.CHANGECARBUSY;
            return MissionResult.NORMAL;
            //TODO

        }
        internal MissionResult RunChangeCar_Loader(int carSideId, ChangeCarStep curStep)
        {
            //  throw new NotImplementedException();



            bool isLeft;
            if (carSideId == 0)
                isLeft = true;
            else
                isLeft = false;

            ChangeCarCmd curCmd = new ChangeCarCmd(isLeft, true, curStep);

            MissionResult result = CheckChangeCarLoader();
            if (result != MissionResult.NORMAL)
                return result;


            if (isLeft)
            {
                this.loaderItemNum_left = 0;

            }
            else
            {
                this.loaderItemNum_right = 0;

            }



            if (isLeft)
                this._ChangeCar_Loader.Status = ChangeCarStatus.CHANGING_LEFT;
            else
                this._ChangeCar_Loader.Status = ChangeCarStatus.CHANGING_RIGHT;

            //  this.m_alarmManager.LedCarOut(isLeft,true);

            this._ChangeCarTask_Loader = new Task<MissionResult>(this._ChangeCar_Loader.RunChangeCar_Loader, (object)(curCmd));
            this._ChangeCarTask_Loader.Start();

            return MissionResult.START;

        }

        internal MissionResult RunChangeCar_Unloader(int carSideId, ChangeCarStep curStep)
        {

            bool isLeft;
            if (carSideId == 0)
                isLeft = true;
            else
                isLeft = false;

            ChangeCarCmd curCmd = new ChangeCarCmd(isLeft, true, curStep);

            MissionResult result = CheckChangeCarUnloader();
            if (result != MissionResult.NORMAL)
                return result;


            if (isLeft)
            {
                this.unloaderItemNum_left = 0;

            }
            else
            {
                this.unloaderItemNum_right = 0;

            }



            if (isLeft)
                this._ChangeCar_Unloader.Status = ChangeCarStatus.CHANGING_LEFT;
            else
                this._ChangeCar_Unloader.Status = ChangeCarStatus.CHANGING_RIGHT;

            //  this.m_alarmManager.LedCarOut(isLeft,true);

            this._ChangeCarTask_Unloader = new Task<MissionResult>(this._ChangeCar_Loader.Run_changeCar_Unloader, (object)(curCmd));
            this._ChangeCarTask_Unloader.Start();
            return MissionResult.START;
        }

        private MissionResult CheckChangeCarUnloader()
        {
            if (this._ChangeCar_Unloader.Status != ChangeCarStatus.HOLDING && !this._ChangeCarTask_Unloader.IsCompleted)
                return MissionResult.CHANGECARBUSY;
            return MissionResult.NORMAL;
        }
    }

    //class MissionManagerLibrary
    //{
    //    private MissionManager m_MissionManager;

    //    public MissionManagerLibrary(MissionManager cMissionManager)
    //    {
    //        // TODO: Complete member initialization
    //        this.m_MissionManager = cMissionManager;
    //    }
    //    public void setLoaderBarcodeByForce(string barcodeID1, int frameSize, bool isLeft)
    //    {
    //        this.m_MissionManager.setLoaderBarcodeByForce(barcodeID1, frameSize, isLeft);
    //    }
    //    public void setUnloaderBarcodeByForce(string barcodeID1, int frameSize, bool isLeft)
    //    {
    //        this.m_MissionManager.setUnloaderBarcodeByForce(barcodeID1, frameSize, isLeft);
    //    }
    //    public LoaderStatus getLoaderStatus()
    //    {
    //        return this.m_MissionManager.getLoaderStatus();
    //    }
    //    public UnloaderStatus getUnloaderStatus()
    //    {
    //        return this.m_MissionManager.getUnloaderStatus();
    //    }

    //}
}
