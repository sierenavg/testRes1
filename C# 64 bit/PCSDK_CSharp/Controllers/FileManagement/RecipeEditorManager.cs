using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIC.Giant.Data;

using MIC.Giant.Data.Reciepe;

namespace MIC.Giant.Controllers.FileManagement
{
    class RecipeEditorManager
    {
        CRecipeGroup curRecipeGroup;

        internal CRecipeGroup CurRecipeGroup
        {
            get { return curRecipeGroup; }
            set { curRecipeGroup = value; }
        }
        public RecipeEditorManager()
        {
            curRecipeGroup = new CRecipeGroup();
        }
    
      
        public CRecipeGroup LoadRecipeGroup(string filePath)
        {
            curRecipeGroup = new CRecipeGroup();
            throw new NotImplementedException();
            //return curRecipeGroup.loadRecipeFromFile(baseAddress + fileName);
        }
        public bool SaveRecipeGroup(string filePath)
        {
            curRecipeGroup = new CRecipeGroup();
            throw new NotImplementedException();
            //return curRecipeGroup.saveRecipeFromFile(baseAddress + fileName);
        }
    }
}
