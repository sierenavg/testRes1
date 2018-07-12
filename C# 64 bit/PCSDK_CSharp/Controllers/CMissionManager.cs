using PCSDK_Csharp_BasicScanApp_514.Controllers.Station;
using PCSDK_Csharp_BasicScanApp_514.Data.Reciepe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCSDK_Csharp_BasicScanApp_514.Controllers
{
    class CMissionManager
    {
        CRobotManager m_robotManager;
        Task<stationStatus> loaderTask, unloaderTask;

        CMissionLoader m_loader;
        CMissionUnloader m_unloader;

        public CMissionManager(CRobotManager robotManager)
        {
            m_robotManager = robotManager;
            m_loader = new CMissionLoader(robotManager);
            m_unloader = new CMissionUnloader(robotManager);
        }
        public  stationStatus runLoader(CRecipeGroup recG)
        {
            //還沒設定位置

            loaderTask = new Task<stationStatus>(m_loader.run, (object)recG);
            loaderTask.Start();
            return loaderTask.Result;
            //var testing = new Task<int>(tt, (object)recG);
            //testing.Start();
            //throw new NotImplementedException();
           // Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_loader.run, recG);
        }
        public stationStatus runUnloader(CRecipeGroup recG)
        {
            unloaderTask = new Task<stationStatus>(m_unloader.run, (object)recG);
            unloaderTask.Start();
            return unloaderTask.Result;
            //var testing = new Task<int>(tt, (object)recG);
            //testing.Start();
            //throw new NotImplementedException();
            // Task<stationStatus> testing = Task.Factory.StartNew<stationStatus>(m_loader.run, recG);
        }
    }
}
