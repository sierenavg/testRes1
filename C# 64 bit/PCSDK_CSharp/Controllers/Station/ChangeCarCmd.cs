using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.Station
{
    class ChangeCarCmd
    {
         public bool isLeft, isLoader;
        public ChangeCarStep requestStep;
        
        public ChangeCarCmd(bool isLeft, bool isLoader,ChangeCarStep requestStep)
        {
            this.isLeft = isLeft;
            this.isLoader = isLoader;
            this.requestStep = requestStep;
        }
    }
}
