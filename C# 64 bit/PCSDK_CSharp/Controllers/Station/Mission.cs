using MIC.Giant.Controllers;
using MIC.Giant.Controllers.Communiction;
using MIC.Giant.Controllers.FileManagement;
using MIC.Giant.Data;
using MIC.Giant.Data.Arm;
using MIC.Giant.Data.Reciepe;
using MIC.Giant.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.Station
{
 
    abstract class Mission
    {
        
        //abstract bool start()
        //{
        //    throw new NotImplementedException();
        //}
        //protected LogManager logmanager;
        //protected CommunicationManager communicationManager;
        ////public List<CRobot> robots;
        //protected int processID;
        protected bool isBusy;
        //protected CRecipeGroup curRecipeGroup;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }
        public abstract MissionResult Run(object recG);
        //{
        //    throw new NotImplementedException();
        //}
        public abstract MissionResult ReRun(CRecipeGroup recG, int stepID);
        //{
        //    throw new NotImplementedException();
        //}
        public abstract MissionResult Initial();
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
