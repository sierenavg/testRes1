using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.MotionControl
{
    class ExernalAxisManager
    {
        bool _IsByPass = true;
        internal bool EnableCarHolder_Loader(bool isLeft, bool p)
        {
            if (_IsByPass)
                return true;
            throw new NotImplementedException();
        }

        internal bool EnableCarHolder_Unloader(bool p1, bool p2)
        {
            if (_IsByPass)
                return true;
            throw new NotImplementedException();
        }
    }
}
