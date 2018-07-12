using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using   MIC.Giant.Data;
namespace MIC.Giant.Controllers.SensorAndAlarm
{
    class AlarmManager
    {
        public bool _IsByPass=true;
        internal bool ShowFrameEmpty_Loader(bool isLeft)
        {
            if (_IsByPass)
                return true;
            throw new NotImplementedException();
        }

        //internal bool showLoaderRightFrameEmpty()
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}

        //internal bool LedCarOut(bool isLeft,bool isLoader)
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}

        //internal bool disableRaster_Loader(bool isLeft)
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}

        //internal bool raster_Loader(bool isLeft, bool p)
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}

        internal bool EnableRaster(bool isLeft, bool isLoader,bool isEnable)
        {
            if (_IsByPass)
                return true;
            throw new NotImplementedException();
        }

        internal bool EnableWarningSign(bool isLeft, bool isLoader ,bool isEnable)
        {
            if (_IsByPass)
                return true;
            throw new NotImplementedException();
        }

        //internal bool showRaster_Unloader(bool p1, bool p2)
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}

        //internal bool showWarningSign_Unloader(bool p1, bool p2)
        //{
        //    if (_IsByPass)
        //        return true;
        //    throw new NotImplementedException();
        //}
    }
}
