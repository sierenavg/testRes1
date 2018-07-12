using MIC.Giant.Data.Arm;
using MIC.Giant.Data.Reciepe;
using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.Station
{
    class MissionLoader : Mission
    {
        //CRobotStation loader=new CABB();
        private RobotManager _RobotManager;
        private LoaderStatus _Status;

        public LoaderStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public MissionLoader(RobotManager robotManager)
        {
            // TODO: Complete member initialization
            this._RobotManager = robotManager;
            this._Status = LoaderStatus.HOLDING;
        }

        bool _IsByPass = true;
     



        public override MissionResult Run(object ip)
        {
            CRecipe rec = (CRecipe)ip;
            MissionResult statues = MissionResult.NORMAL;
            statues = this.selfCheck();
            if(statues!=MissionResult.NORMAL)
                return statues;
            
            statues = this.loadRecipe();
            if (statues != MissionResult.NORMAL)
                return statues;

            statues = this.checkRGVstatus();
            if (statues != MissionResult.NORMAL)
                return statues;

            statues = this.RunRecipe(rec);
            return statues;

        }

        private MissionResult RunRecipe(CRecipe rec)
        {
            this._RobotManager.SetRec_Loader(rec);
            Thread.Sleep(500);
            this._RobotManager.NextStep_Loader();
            Thread.Sleep(4000);
            //to 1///
            while (this._RobotManager.IsLoaderMoving())
            {
                Thread.Sleep(500);
            }
            Thread.Sleep(2000);
            this._Status = LoaderStatus.HOLDING;
            //this.closeClaw();
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_Loader();
            ////to 2
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_Loader();
            ////to 3
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //this.openClaw();
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_Loader();
            ////to 4
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //Thread.Sleep(5000);
       //     this.robotManager.nextStep_Loader();
            return MissionResult.NORMAL;
           // throw new NotImplementedException();
        }

        private void closeClaw()
        {

            //throw new NotImplementedException();
        }

        private void openClaw()
        {
           // throw new NotImplementedException();
        }

        private MissionResult checkRGVstatus()
        {
            if(_IsByPass)
              return MissionResult.NORMAL;
            throw new NotImplementedException();
        }

        private MissionResult loadRecipe()
        {
            if (_IsByPass)
                  return MissionResult.NORMAL;
                    throw new NotImplementedException();
        }

        private MissionResult selfCheck()
        {
            if (_IsByPass)
            return MissionResult.NORMAL;
            throw new NotImplementedException();
        }

        public override MissionResult ReRun(CRecipeGroup recG, int stepID)
        {
            if (_IsByPass)
            return MissionResult.NORMAL;
            throw new NotImplementedException();
        }

        public override MissionResult Initial()
        {
            if (_IsByPass)
            return MissionResult.NORMAL;
            throw new NotImplementedException();
        }
    }
}
