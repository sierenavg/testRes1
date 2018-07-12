using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers.Station
{
    class CMissionUnloader : CMission
    {
        private CRobotManager robotManager;

        public CMissionUnloader(CRobotManager robotManager)
        {
            // TODO: Complete member initialization
            this.robotManager = robotManager;
        }
        
        public override stationStatus run(object ip)
        {
            throw new NotImplementedException();
        }

        public override stationStatus reRun(Data.Reciepe.CRecipeGroup recG, int stepID)
        {
            throw new NotImplementedException();
        }

        public override stationStatus initial()
        {
            throw new NotImplementedException();
        }
    }
}
