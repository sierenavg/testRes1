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
    class MissionUnloader : Mission
    {
        private RobotManager _RobotManager;
        private UnloaderStatus _Status;

        public UnloaderStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public MissionUnloader(RobotManager robotManager)
        {
            // TODO: Complete member initialization
            this._RobotManager = robotManager;
            this._Status = UnloaderStatus.HOLDING;
        }
        
        public override MissionResult Run(object ip)
        {
            CRecipe rec = (CRecipe)ip;
            MissionResult statues = MissionResult.NORMAL;
            statues = this.SelfCheck();
            if (statues != MissionResult.NORMAL)
                return statues;

            statues = this.LoadRecipe();
            if (statues != MissionResult.NORMAL)
                return statues;

            statues = this.CheckRGVstatus();
            if (statues != MissionResult.NORMAL)
                return statues;

            statues = this.RunRecipe(rec);
            return statues;
        }

        private MissionResult RunRecipe(CRecipe rec)
        {
            this._RobotManager.SetRec_Unloader(rec);
            Thread.Sleep(500);
            this._RobotManager.NextStep_Unloader();
            Thread.Sleep(4000);
            //to 1///
            while (this._RobotManager.IsUnloaderMoving())
            {
                Thread.Sleep(500);
            }
            Thread.Sleep(2000);
            this._Status = UnloaderStatus.HOLDING;
            return MissionResult.NORMAL;
        }

        private MissionResult CheckRGVstatus()
        {
            return MissionResult.NORMAL;
            throw new NotImplementedException();
        }

        private MissionResult LoadRecipe()
        {
            return MissionResult.NORMAL;
            throw new NotImplementedException();
        }

        private MissionResult SelfCheck()
        {
            return MissionResult.NORMAL;
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
    }
}
