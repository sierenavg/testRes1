using MIC.Giant.Data.Arm;
using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers
{
    //public enum RobotStatus
    //{
    //    busy,free,alarm,finish,auto,nonAuto
    //}
    class RobotManager
    {
        public ABBrobot _RobotStation;

        //public bool isLoaderRunning()
        //{

        //}
        public bool IsLoaderMoving()
        {
            //this.robotStation
            string result = "?";
            this._RobotStation.readLoaderString("isLoaderMoving", out result);
            if (result == "\"y\"")
            {
                return true;
            }
            else
                return false;
        }
        public bool NextStep_Loader()
        {
            this._RobotStation.writeLoaderString("isLoaderMoving", "y");
            return true;
        }
        public bool IsUnloaderMoving()
        {
            //this.robotStation
            string result = "?";
            this._RobotStation.readUnLoaderString("isUnloaderMoving", out result);
            if (result == "\"y\"")
            {
                return true;
            }
            else
                return false;
        }
        public bool NextStep_Unloader()
        {
            this._RobotStation.writeUnLoaderString("isUnloaderMoving", "y");
            return true;
        }
        public RobotManager()
        {
            _RobotStation = new ABBrobot();
        }
        RobotStatus loadLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        RobotStatus loadUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        RobotStatus runUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        RobotStatus runLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        RobotStatus initialUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        RobotStatus initialLoaderRecipe()
        {
            throw new NotImplementedException();
        }


        internal bool IsRobotAllOk()
        {
            return this._RobotStation.IsRobotAllOk();
            //private ABB.Robotics.Controllers.RapidDomain.Task[] tasks  = this._controller.Rapid.GetTasks();
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
            //return true;
            throw new NotImplementedException();
        }

        internal void SetRec_Loader(Data.Reciepe.CRecipe rec)
        {
            this._RobotStation.writeLoaderString("loaderCustomFunc", rec.targetFunc);
            
        }
        internal void SetRec_Unloader(Data.Reciepe.CRecipe rec)
        {
            this._RobotStation.writeUnLoaderString("unloaderCustomFunc", rec.targetFunc);

        }
    }
}
