using MIC.Giant.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Giant.Controllers.FileManagement
{
    class LogManager
    {
        //internal bool runCmd(CCommand ip)
        //{
        //    throw new NotImplementedException();
        //}
        public LogManager()
        {
            
        }
        object _Thislock = new object();
        public string _FolderAd = @"C:\Users\erichou\Desktop\資料庫\";
        //public string fileName=@"
        public bool SaveProcessLog(string message,bool isLoader)
        {
            lock (_Thislock)
            {
                DateTime curTime=DateTime.Now;
                string adr;
                if (isLoader)
                {
                    adr = _FolderAd + "loader_" + curTime.Year + "_" + curTime.Month + "_" + curTime.Day + "_log.txt";
                    //File.AppendAllText(adr, message);
                }
                else
                {
                    adr = _FolderAd + "unloader_" + curTime.Year + "_" + curTime.Month + "_" + curTime.Day + "_log.txt";
                }
                File.AppendAllText(adr, message);
                return true;
            }
        }
    }
}
