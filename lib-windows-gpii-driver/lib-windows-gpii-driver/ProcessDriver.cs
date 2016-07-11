using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;
using GPII.SystemProcess;
using System.Diagnostics;

namespace GPII.drivers
{
    public class ProcessDriver
    {
        public static void DoTests()
        {
            Logger.LogBitnessInfo();

            TestProcessRunningDetection("osk");
            TestProcessRunningDetection("notepad");
            TestProcessRunningDetection("calc");
        }

        private static void TestProcessRunningDetection(string processName = "osk")
        {
            Logger.Debug("TestProcessRunningDetection for " + processName);
            var processController = new ProcessController(processName);
            Assert(processController.IsAnyProcessRunning() == false, "Verify process not running at beginning of test");

            processController.Start();
            Assert(processController.IsAnyProcessRunning(), "Process should be running");

            processController.KillAll();
            Assert(processController.IsAnyProcessRunning() == false, "Process should have been killed");
        }
    }
}
