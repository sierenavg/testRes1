using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.Station
{
    class MissionManagerLibrary
    {
        private MissionManager _MissionManager;

        public MissionManagerLibrary(MissionManager cMissionManager)
        {
            // TODO: Complete member initialization
            this._MissionManager = cMissionManager;
        }
        public void SetBarcodeByForce_Loader(string barcodeID1, int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Loader(barcodeID1, frameSize, isLeft);
        }
        public void SetBarcodeByForce_Unloader(string barcodeID1, int frameSize, bool isLeft)
        {
            this._MissionManager.SetBarcodeByForce_Unloader(barcodeID1, frameSize, isLeft);
        }
        public LoaderStatus GetStatus_Loader()
        {
            return this._MissionManager.GetStatus_Loader();
        }
        public UnloaderStatus GetStatus_Unloader()
        {
            return this._MissionManager.GetStatus_Unloader();
        }
    }
}
