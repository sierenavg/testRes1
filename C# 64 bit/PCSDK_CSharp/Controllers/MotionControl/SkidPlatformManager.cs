using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.MotionControl
{
    class SkidPlatformManager
    {
        //internal bool getBarcodeId_Unloader(bool p, out string carId)
        //{
        //    throw new NotImplementedException();
        //}
        bool _IsByPass = true;
        internal bool GetBarcodeId_Unloader(bool isLeft, out string barcodeId, out int frameSize)
        {
            if (_IsByPass)
            {
                frameSize = 5;
                barcodeId = "ByPassResult";
                return true;
            }
            throw new NotImplementedException();
        }

        internal bool GetBarcodeId_Loader(bool p, out string barcodeId, out int frameSize)
        {
            if (_IsByPass)
            {
                frameSize = 5;
                barcodeId = "ByPassResult";
                return true;
            }
            throw new NotImplementedException();
        }
    }
}
