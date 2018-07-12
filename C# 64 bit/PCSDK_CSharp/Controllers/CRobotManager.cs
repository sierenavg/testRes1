using PCSDK_Csharp_BasicScanApp_514.Data.Arm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers
{
    enum robotStatus
    {
        busy,free,alarm,finish,auto,nonAuto
    }
    class CRobotManager
    {
        public CABB robotStation;

        //public bool isLoaderRunning()
        //{

        //}
        public bool isLoaderMoving()
        {
            //this.robotStation
            string result="?";
            this.robotStation.readLoaderString("isLoaderMoving", out result);
            if (result == "y")
            {
                return true;
            }else
                return false;
        }
        public bool nextStep_loader()
        {
            this.robotStation.writeLoaderString("isLoaderMoving", "y");
            return true;
        }
        public CRobotManager()
        {
            robotStation = new CABB();
        }
        robotStatus loadLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        robotStatus loadUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        robotStatus runUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        robotStatus runLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        robotStatus initialUnLoaderRecipe()
        {
            throw new NotImplementedException();
        }
        robotStatus initialLoaderRecipe()
        {
            throw new NotImplementedException();
        }

    }
}
