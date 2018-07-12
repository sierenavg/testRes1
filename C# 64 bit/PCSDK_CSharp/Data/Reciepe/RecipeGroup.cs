using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Data.Reciepe
{
    class CRecipeGroup :ICloneable
    {

        public string recName;
        //List<CCommand> loader, unLoader;
        public List<CRecipe> loaderRecipes = new List<CRecipe>();
        public List<CRecipe> unloaderRecipes = new List<CRecipe>();
        public CRecipeGroup()
        {
            for (int i = 1; i <= 10; i++)
            {
                loaderRecipes.Add(new CRecipe());
            }
            for (int i = 1; i <= 10; i++)
            {
                unloaderRecipes.Add(new CRecipe());
            }
            //loader = new List<CCommand>(10);
            //unLoader = new List<CCommand>(10);
        }
        public CRecipeGroup(List<string> ip)
        {
           
            this.recName = ip[0];

            for (int i = 1; i <= 10; i++)
            {
                loaderRecipes.Add(new CRecipe()); 
                loaderRecipes[i-1].targetFunc = ip[i];
            }
            for (int i = 1; i <= 10; i++)
            {
                unloaderRecipes.Add( new CRecipe()); 
                unloaderRecipes[i-1].targetFunc = ip[i+10];
            }
        }
        //public bool loadRecipeFromFile(string filePath)
        //{
        //        //for (int cmdId = 0; cmdId < this.recipes[recipeID].; cmdId++)
        //        //{
        //        //    for (int cmdId = 0; cmdId < 100; cmdId++)
        //        //    {
        //        //        StringBuilder dataIp = new StringBuilder(255);
        //        //        GetPrivateProfileString(Recipes[id].step[i].toString(), cmdId, "NA", data, 255, fileName);
        //        //        CCommand tempCmd = new CCommand(dataIp.toString());
        //        //        recipes[id].step[i].Ccommand[cmdId] = tempCmd;
        //        //    }
        //        //}
        //    throw new NotImplementedException();
        //}
        //public bool saveRecipeFromFile(string filePath)
        //{
        //    //for (int cmdId = 0; cmdId < this.recipes[recipeID].; cmdId++)
        //    //{
        //    //    for (int cmdId = 0; cmdId < 100; cmdId++)
        //    //    {
        //    //        StringBuilder dataIp = new StringBuilder(255);
        //    //        GetPrivateProfileString(Recipes[id].step[i].toString(), cmdId, "NA", data, 255, fileName);
        //    //        CCommand tempCmd = new CCommand(dataIp.toString());
        //    //        recipes[id].step[i].Ccommand[cmdId] = tempCmd;
        //    //    }
        //    //}
        //    throw new NotImplementedException();
        //}




        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
