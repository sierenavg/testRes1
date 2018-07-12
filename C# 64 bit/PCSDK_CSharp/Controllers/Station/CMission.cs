using PCSDK_Csharp_BasicScanApp_514.Controllers;
using PCSDK_Csharp_BasicScanApp_514.Data;
using PCSDK_Csharp_BasicScanApp_514.Data.Arm;
using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers.Station
{
    enum stationStatus
    {
        normal,robotError,mainTakeControl
    }
    abstract class CMission
    {
        
        //abstract bool start()
        //{
        //    throw new NotImplementedException();
        //}
        protected CLogManager logmanager;
        protected CcommunicationManager communicationManager;
        //public List<CRobot> robots;
        protected int processID;
        protected bool isBusy;
        protected CRecipeGroup curRecipeGroup;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }
        public abstract stationStatus run(object recG);
        //{
        //    throw new NotImplementedException();
        //}
        public abstract stationStatus reRun(CRecipeGroup recG, int stepID);
        //{
        //    throw new NotImplementedException();
        //}
        public abstract stationStatus initial();
       // {
       // }

        //public bool decode(CRecipe ip)
        //{
        //    List<CCommand> nextCmd=new List<CCommand>();
        //    for(int i=0;i<ip.commands.Count();i++)
        //    {
        //        if (ip.commands[i].type == 0)
        //        {
        //            nextCmd.Add(ip.commands[i]);
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
    }
}
