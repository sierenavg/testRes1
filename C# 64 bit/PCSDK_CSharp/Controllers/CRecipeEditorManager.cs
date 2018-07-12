using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCSDK_Csharp_BasicScanApp_514.Data;

using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers
{
    class CRecipeEditorManager
    {
        CRecipeGroup curRecipeGroup;

        internal CRecipeGroup CurRecipeGroup
        {
            get { return curRecipeGroup; }
            set { curRecipeGroup = value; }
        }
        public CRecipeEditorManager()
        {
            curRecipeGroup = new CRecipeGroup();
        }
    
      
        public CRecipeGroup loadRecipeGroup(string filePath)
        {
            curRecipeGroup = new CRecipeGroup();
            throw new NotImplementedException();
            //return curRecipeGroup.loadRecipeFromFile(baseAddress + fileName);
        }
        public bool saveRecipeGroup(string filePath)
        {
            curRecipeGroup = new CRecipeGroup();
            throw new NotImplementedException();
            //return curRecipeGroup.saveRecipeFromFile(baseAddress + fileName);
        }
    }
}
