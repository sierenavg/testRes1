using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant
{
    static class abbComand_translater
    {
        public static ABB.Robotics.Controllers.RapidDomain.RobTarget getRobTarget(double[] trans, double[] rot, double[] robconf)
        {
            string cmd = "[[";
            for (int i = 0; i < 3; i++)
            {
               cmd=cmd+ trans[i].ToString("#0.00");
            }
            cmd += "],[";
            for (int i = 0; i < 4; i++)
            {
                cmd = cmd + rot[i].ToString("#0.00");
            }
            cmd += "],[";
            for (int i = 0; i < 4; i++)
            {
                cmd = cmd + robconf[i].ToString("#0.00");
            }
            cmd += "],[ 9E9, 9E9, 9E9, 9E9, 9E9, 9E9] ]";
            ABB.Robotics.Controllers.RapidDomain.RobTarget temp = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
            temp.FillFromString2(cmd);
            return temp;
        }

    }
    class abbRecipe
    {
        public List<ABB.Robotics.Controllers.RapidDomain.RobTarget> positions;
        public string recName,varName;
        public abbRecipe(string recName,string variableName="currentPath")
        {
            
            positions = new List<ABB.Robotics.Controllers.RapidDomain.RobTarget>();
            this.recName = recName;
            this.varName = variableName;     
        }

        public abbRecipe(string recName, List<string> cmds,string variableName = "currentPath")
        {

            positions = new List<ABB.Robotics.Controllers.RapidDomain.RobTarget>();
            this.recName = recName;
            this.varName = variableName;
            this.addPath(cmds);
        }

       public void setPath (int id,double[] trans, double[] rot, double[] robconf)
       {
           positions[id] = abbComand_translater.getRobTarget(trans, rot, robconf);
       }
       public void setPath(int id, string cmd)
       {
           positions[id] = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
           positions[id].FillFromString2(cmd);
       }
       public void addPath(double[] trans, double[] rot, double[] robconf)
       {
           positions.Add(abbComand_translater.getRobTarget(trans, rot, robconf));
       }
       public void addPath( string cmd)
       {
           ABB.Robotics.Controllers.RapidDomain.RobTarget temp = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
           temp.FillFromString2(cmd);
           positions.Add(temp);
       }
       public void addPath(List<string> cmds)
       {
           for (int i = 0; i < cmds.Count; i++)
           {
               ABB.Robotics.Controllers.RapidDomain.RobTarget temp = new ABB.Robotics.Controllers.RapidDomain.RobTarget();
               temp.FillFromString2(cmds[i]);
               positions.Add(temp);
           }
       }
    }
    class mainFunc
    {
        Form1 mother;
        List<abbRecipe> recipes;
        public mainFunc(Form1 parent)
        {
            this.mother = parent;
            recipes = new List<abbRecipe>();
        }
        public void addRecipe(abbRecipe recipe)
        {
            recipes.Add(recipe);
        }
       
        public void replaceRecipe(int id,abbRecipe recipe)
        {
            recipes[id] = recipe;
            
        }
        public void loadCurrentRecipeToRob(int id)
        {
            for(int pathId=0;pathId<recipes[id].positions.Count;pathId++)
            {
                  mother.savePosition(recipes[id].varName+pathId.ToString(),recipes[id].positions[pathId]);
            }
            mother.saveInt("currentPathCount", recipes[id].positions.Count);
        }
        public void runRecipe(int id)
        {
            loadCurrentRecipeToRob(id);

        }
        public void runChildFunc(int id)
        {

        }
    }
}
