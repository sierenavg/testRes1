using MIC.Giant.Data;
using MIC.Giant.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.FileManagement
{
    class RecipeManager
    {
        string _RecipeAddress;
        List<CRecipeGroup> _RecipeGroups;
        RecipeEditorManager _RecipeEditorManager;
        Dictionary<string, string> _RecMenu;
       

        public RecipeManager()
        {
            _RecipeAddress = @"C:\Users\erichou\Desktop\資料庫\";
            Initial();
        }
        public RecipeManager(string baseAddress)
        {
            _RecipeAddress = baseAddress;
            Initial();
        }
        private bool Initial()
        {
            this._RecipeEditorManager = new RecipeEditorManager();
            this._RecMenu = new Dictionary<string, string>();
            LoadRecMenu(_RecipeAddress+"menu.txt");
            return true;
        }
        public void LoadRecMenu(string address)
        {

            StreamReader sr = new StreamReader(address);

            while (!sr.EndOfStream)
            {               // 每次讀取一行，直到檔尾
                string line = sr.ReadLine();            // 讀取文字到 line 變數

                string[] results = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (results.Length > 1)
                {
                    if(!_RecMenu.ContainsKey(results[0]))
                        _RecMenu.Add(results[0], results[1]);


                }
            }
            sr.Close();
        }
        public CRecipeGroup LoadRec(string address)
        {
            StreamReader sr = new StreamReader(this._RecipeAddress+address+".txt");
            int index = 0;
            List<string> ip = new List<string>();

            while (!sr.EndOfStream)
            {               // 每次讀取一行，直到檔尾

                string line = sr.ReadLine();            // 讀取文字到 line 變數
                ip.Add(line);
                //ip[index] = line;
                index += 1;
                if (index >= 21)
                    break;
            }
            CRecipeGroup curData = new CRecipeGroup(ip);
           

            sr.Close();
            return curData;
        }
       
        public bool InitialRecipeGroup()
        {
            this._RecipeGroups.Clear();
            string targetDirectory;

            string[] fileEntries = Directory.GetFiles(this._RecipeAddress);
            int id = 0;
            foreach (string fileName in fileEntries)
            {
                string filePath = this._RecipeAddress + fileName;
                this._RecipeGroups[id]=this._RecipeEditorManager.LoadRecipeGroup(filePath);
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
      

        

        internal CRecipeGroup getRecipeGroupByBarcodeID(string barcodeID)
        {
            if (_RecMenu.ContainsKey(barcodeID))
            {
                return LoadRec(_RecMenu[barcodeID]);
            }
            else
                return null;

            return new CRecipeGroup();
            throw new NotImplementedException();
        }
    }
}
