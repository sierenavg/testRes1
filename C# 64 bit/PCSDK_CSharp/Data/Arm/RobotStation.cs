using MIC.Giant.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIC.Giant.Data.Arm
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
