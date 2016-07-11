using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPII.SystemProcess
{

    /// <summary>
    /// Wrapper class for starting, killing, and checking if processes are running.
    /// </summary>
    /// <remarks>
    /// Some processes, such as the on screen keyboard (osk.exe) in windows appear to spawn
    /// child processes and exit ASAP. So, although visually it look like your osk.exe process
    /// is still running because the OSK is visible, the process you started has actually exited.
    /// This means you cannot reliably stop a OSK process from the process object you started it from. 
    /// For example:
    /// 
    ///     var osk = new Process();
    ///     osk.StartInfo.FileName = "osk";
    ///     osk.Start();
    ///     // the process starts, spawns a child process, and exits
    ///     osk.Kill();    // throws an error because the process has already exited
    ///     osk.HasExited; // which you can verify with this property
    ///
    /// The work around in ProcessController is to query for processes by name when it is time
    /// to try and kill them rather than hold on to any process handles created when a the process
    /// is started. 
    /// </remarks>
    public class ProcessController
    {
        private string name;
        public ProcessController(string name)
        {
            this.name = name;
        }

        public void Start()
        {
            var process = new Process();
            process.StartInfo.FileName = this.name;
            process.Start();
            WaitForProcessToStart();
        }

        private void WaitForProcessToStart()
        {
            while (IsAnyProcessRunning() == false)
            {
                //no op
            }
        }


        private void WaitForProcessToEnd()
        {
            while (IsAnyProcessRunning() == true)
            {
                //no op
            }
        }


        public void KillAll()
        {
            var processes = Process.GetProcessesByName(this.name);
            foreach (var p in processes)
            {
                p.Kill();
            }
            WaitForProcessToEnd();
            /* There is a race condition potential here while it attempts to 
             * block until *all* processes are really gone. Should it be done
             * in a loop that rechecks the condition on each execution ala:
             * 
             * while ( DoAnyProcessesExist() )
             * {
             *   KillAllProcesses();
             * } // repeat until they're all gone
             *  
             */
        }

        public bool IsAnyProcessRunning()
        {
            // True if the array contains more than 1 process.
            return System.Diagnostics.Process.GetProcessesByName(this.name).Length > 0;
        }
    }
}
