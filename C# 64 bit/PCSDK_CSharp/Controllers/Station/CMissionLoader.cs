using PCSDK_Csharp_BasicScanApp_514.Data.Arm;
using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers.Station
{
    class CMissionLoader : CMission
    {
        //CRobotStation loader=new CABB();
        private CRobotManager robotManager;

        public CMissionLoader(CRobotManager robotManager)
        {
            // TODO: Complete member initialization
            this.robotManager = robotManager;
        }
        
     



        public override stationStatus run(object ip)
        {
            CRecipeGroup recG = (CRecipeGroup)ip;
            stationStatus statues = stationStatus.normal;
            statues = this.selfCheck();
            if(statues!=stationStatus.normal)
                return statues;
            
            statues = this.loadRecipe();
            if (statues != stationStatus.normal)
                return statues;

            statues = this.checkRGVstatus();
            if (statues != stationStatus.normal)
                return statues;

            statues = this.runRecipe();
            return statues;

        }

        private stationStatus runRecipe()
        {
            this.robotManager.nextStep_loader();
            //to 1
            while (this.robotManager.isLoaderMoving())
            {
                Thread.Sleep(500);
            }
         
            //this.closeClaw();
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_loader();
            ////to 2
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_loader();
            ////to 3
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //this.openClaw();
            //Thread.Sleep(5000);
            //this.robotManager.nextStep_loader();
            ////to 4
            //while (this.robotManager.isLoaderMoving())
            //{
            //    Thread.Sleep(500);
            //}
            //Thread.Sleep(5000);
       //     this.robotManager.nextStep_loader();
            return stationStatus.normal;
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

        private stationStatus checkRGVstatus()
        {
            return stationStatus.normal;
            throw new NotImplementedException();
        }

        private stationStatus loadRecipe()
        {
            return stationStatus.normal;
            throw new NotImplementedException();
        }

        private stationStatus selfCheck()
        {
            return stationStatus.normal;
            throw new NotImplementedException();
        }

        public override stationStatus reRun(CRecipeGroup recG, int stepID)
        {
            return stationStatus.normal;
            throw new NotImplementedException();
        }

        public override stationStatus initial()
        {
            return stationStatus.normal;
            throw new NotImplementedException();
        }
    }
}
