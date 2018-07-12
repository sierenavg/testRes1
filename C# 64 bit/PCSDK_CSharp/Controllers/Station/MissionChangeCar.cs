using MIC.Giant.Controllers.MotionControl;
using MIC.Giant.Controllers.SensorAndAlarm;
using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.Station
{
    //public enum changeCarStep
    //{
    //    HOLDING,CAR_OUT,CAR_IN,CAR_IN_FINISH
    //}

   
    class MissionChangeCar : Mission
    {
        private ChangeCarStatus _Status = ChangeCarStatus.HOLDING;
        ChangeCarStep _CurStep;

        public ChangeCarStep CurStep
        {
          get { return _CurStep; }
          set { _CurStep = value; }
}
        public ChangeCarStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
       // CMissionLoader m_Loader;
        //CMissionUnloader m_Unloader;
        private AlarmManager _AlarmManager;
  
        private SkidPlatformManager _SkidPlatformManager;
       
      
        private MotionControl.ExernalAxisManager _ExernalAxisManager;
        MissionManagerLibrary _MissionManagerPartialCmd;
       
     

        public MissionChangeCar( AlarmManager cAlarmManager, 
            SkidPlatformManager cSkidPlatformManager, MotionControl.ExernalAxisManager cExernalAxisManager,MissionManagerLibrary cMissionManagerPartialCmd)
        {
            // TODO: Complete member initialization
            //this.m_Loader = m_Loader;
            //this.m_Unloader = m_Unloader;
            this._AlarmManager = cAlarmManager;
            this._SkidPlatformManager = cSkidPlatformManager;
            this._ExernalAxisManager = cExernalAxisManager;
            this._MissionManagerPartialCmd = cMissionManagerPartialCmd;
        }
        public override MissionResult Run(object recG)
        {
            throw new NotImplementedException();
        }

        public override MissionResult ReRun(Data.Reciepe.CRecipeGroup recG, int stepID)
        {
            throw new NotImplementedException();
        }

        public override MissionResult Initial()
        {
            throw new NotImplementedException();
        }
        public MissionResult RunChangeCar_Loader(object arg)
        {
            ChangeCarCmd cmd = (ChangeCarCmd)arg;
            MissionResult result;
            this._CurStep = cmd.requestStep; //changeCarStep.CAR_IN;
            switch (cmd.requestStep)
            {
                case ChangeCarStep.CAR_OUT:
                    result = this.RunCarOut_Loader(cmd);
                    break;
                case ChangeCarStep.CAR_IN:
                    result = this.RunCarIn_Loader (cmd);
                    break;
                case ChangeCarStep.CAR_IN_FINISH:
                    result = this.RunCarInFinish_Loader(cmd);
                    this._CurStep = ChangeCarStep.HOLDING;
                    break;
                default:
                    result = MissionResult.CMDERROR;
                    break;
            }
            this._Status = ChangeCarStatus.HOLDING;
            //this.curStep =
            return result;
         

        }
        internal MissionResult RunCarOut_Loader(ChangeCarCmd cmd)
        {
            LoaderStatus faultStatus;
            //this.curStep= changeCarStep.CAR_IN;
            //bool isLeft = (bool)arg;
            if (cmd.isLeft)
                faultStatus = LoaderStatus.WORKING_LEFT;
            else
                faultStatus = LoaderStatus.WORKING_RIGHT;
            while (this._MissionManagerPartialCmd.GetStatus_Loader() == faultStatus)
            {
                Thread.Sleep(800);
            }
            //TODO
            this._AlarmManager.EnableRaster(cmd.isLeft, true,false);
            this._AlarmManager.EnableWarningSign(cmd.isLeft, true,true);
            this._ExernalAxisManager.EnableCarHolder_Loader(cmd.isLeft, false);
            //this.status = changeCarStatus.HOLDING;
            return MissionResult.FINISH;

        }
        internal MissionResult RunCarIn_Loader(ChangeCarCmd cmd)
        {
          
            this._ExernalAxisManager.EnableCarHolder_Loader(cmd.isLeft, true);
            
            //this.status = changeCarStatus.HOLDING;
            return MissionResult.FINISH;

        }
        internal MissionResult RunCarInFinish_Loader(ChangeCarCmd cmd)
        {
           
            //TODO
            this._AlarmManager.EnableRaster(cmd.isLeft,true, true);
            this._AlarmManager.EnableWarningSign(cmd.isLeft,true, false);
            string barcodeId = "testBarcodeID";
            int frameSize = -1;
            bool isPass = this._SkidPlatformManager.GetBarcodeId_Loader(cmd.isLeft, out barcodeId, out frameSize);
            if (isPass)
            {
                this._MissionManagerPartialCmd.SetBarcodeByForce_Loader(barcodeId, frameSize, cmd.isLeft);
            }
            return MissionResult.FINISH;

        }

        public MissionResult Run_changeCar_Unloader(object arg)
        {
            ChangeCarCmd cmd = (ChangeCarCmd)arg;
            MissionResult result;
            this._CurStep = cmd.requestStep; //changeCarStep.CAR_IN;
            switch (cmd.requestStep)
            {
                case ChangeCarStep.CAR_OUT:
                    result = this.RunCarOut_Unloader(cmd);
                    break;
                case ChangeCarStep.CAR_IN:
                    result = this.RunCarIn_Unloader(cmd);
                    break;
                case ChangeCarStep.CAR_IN_FINISH:
                    result = this.RunCarInFinish_Unloader(cmd);
                    this._CurStep = ChangeCarStep.HOLDING;
                    break;
                default:
                    result = MissionResult.CMDERROR;
                    break;
            }
            this._Status = ChangeCarStatus.HOLDING;
            //this.curStep =
            return result;


        }
        internal MissionResult RunCarOut_Unloader(ChangeCarCmd cmd)
        {
            UnloaderStatus faultStatus;
            //this.curStep= changeCarStep.CAR_IN;
            //bool isLeft = (bool)arg;
            if (cmd.isLeft)
                faultStatus = UnloaderStatus.WORKING_LEFT;
            else
                faultStatus = UnloaderStatus.WORKING_RIGHT;
            while (this._MissionManagerPartialCmd.GetStatus_Unloader() == faultStatus)
            {
                Thread.Sleep(800);
            }
            //TODO
            this._AlarmManager.EnableRaster(cmd.isLeft, false,false);
            this._AlarmManager.EnableWarningSign(cmd.isLeft,false, true);
            this._ExernalAxisManager.EnableCarHolder_Unloader(cmd.isLeft, false);
            //this.status = changeCarStatus.HOLDING;
            return MissionResult.FINISH;

        }
        internal MissionResult RunCarIn_Unloader(ChangeCarCmd cmd)
        {

            this._ExernalAxisManager.EnableCarHolder_Loader(cmd.isLeft, true);

            //this.status = changeCarStatus.HOLDING;
            return MissionResult.FINISH;

        }
        internal MissionResult RunCarInFinish_Unloader(ChangeCarCmd cmd)
        {

            //TODO
            this._AlarmManager.EnableRaster(cmd.isLeft, false,true);
            this._AlarmManager.EnableWarningSign(cmd.isLeft,false, false);
            string barcodeId="testBarcodeID";
            int frameSize = -1;
            bool isPass=this._SkidPlatformManager.GetBarcodeId_Unloader(cmd.isLeft, out barcodeId,out frameSize);
            if (isPass)
            {
                this._MissionManagerPartialCmd.SetBarcodeByForce_Unloader(barcodeId, frameSize, cmd.isLeft);
            }
            return MissionResult.FINISH;

        }
    }
}
