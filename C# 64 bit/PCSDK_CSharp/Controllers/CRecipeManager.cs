using PCSDK_Csharp_BasicScanApp_514.Data;
using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers
{
    class CRecipeManager
    {
        string recipeAddress;
        List<CRecipeGroup> recipeGroups;
        CRecipeEditorManager recipeEditorManager;

        public  CRecipeGroup getRecipeGroup(int nid)
        {
            return (CRecipeGroup)((recipeGroups[nid]).Clone());
            //CRecipeGroup copy = new CRecipeGroup(recipeGroups[nid]);
            //return copy;
        }

        public CRecipeManager()
        {
            recipeAddress = "xxxx";
            oninitial();
        }
        public CRecipeManager(string baseAddress)
        {
            recipeAddress = baseAddress;
            oninitial();
        }
        private bool oninitial()
        {
            this.recipeEditorManager = new CRecipeEditorManager();
            return true;
        }
       
        public bool initialRecipeGroup()
        {
            this.recipeGroups.Clear();
            string targetDirectory;

            string[] fileEntries = Directory.GetFiles(this.recipeAddress);
            int id = 0;
            foreach (string fileName in fileEntries)
            {
                string filePath = this.recipeAddress + fileName;
                this.recipeGroups[id]=this.recipeEditorManager.loadRecipeGroup(filePath);
                id++;
            }

            //for (int i = 0; i < recipes[id].steps.Size(); i++)
            //{
            //    for (int cmdId = 0; cmdId < 100; cmdId++)
            //    {
            //        StringBuilder dataIp = new StringBuilder(255);
            //        GetPrivateProfileString(Recipes[id].step[i].toString(), cmdId, "NA", data, 255, fileName);
            //        CCommand tempCmd = new CCommand(dataIp.toString());
            //        recipes[id].step[i].Ccommand[cmdId] = tempCmd;
            //    }
            //}
            return false;
        }
        //private bool getRecipe(string fileName, int recipeID)
        //{
        //    for (int cmdId = 0; cmdId < this.recipes[recipeID].; cmdId++)
        //    {
        //        for (int cmdId = 0; cmdId < 100; cmdId++)
        //        {
        //            StringBuilder dataIp = new StringBuilder(255);
        //            GetPrivateProfileString(Recipes[id].step[i].toString(), cmdId, "NA", data, 255, fileName);
        //            CCommand tempCmd = new CCommand(dataIp.toString());
        //            recipes[id].step[i].Ccommand[cmdId] = tempCmd;
        //        }
        //    }
        //}
    }
}
