using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCSDK_Csharp_BasicScanApp_514.Data.Arm
{
    abstract class CRobotStation
    {
        //public virtual bool runCmds(List<CCommand> ip)
        //{
        //    throw new NotImplementedException();
        //}
        public abstract List<ListViewItem> scanRobot();
        public abstract int initialRobotConnection(ListViewItem item);
        public abstract int start();
    }
}
