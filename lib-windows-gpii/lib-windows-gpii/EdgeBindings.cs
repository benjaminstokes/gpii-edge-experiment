using System;
using System.Threading.Tasks;
using GPII.Settings;
using GPII.SystemProcess;
using System.IO;

namespace GPII.edge
{
    class EdgeBindings
    {
        public async Task<object> TurnOnStickyKeys(dynamic input)
        {
            var stickyKeys = new StickyKeys();
            stickyKeys.TurnOn();
            return true;
        }

        public async Task<object> TurnOffStickyKeys(dynamic input)
        {
            var stickyKeys = new StickyKeys();
            stickyKeys.TurnOff();
            return true;
        }

        public async Task<object> TurnOnHighContrast(dynamic input)
        {
            var highContrast = new HighContrast();
            highContrast.IsHighContrastOn = true;
            highContrast.Theme = HighContrast.ColorSchemes.HighContrastBlack;
            highContrast.Apply();
            return true;
        }

        public async Task<object> TurnOffHighContrast(dynamic input)
        {
            var highContrast = new HighContrast();
            highContrast.IsHighContrastOn = false;
            highContrast.Apply();
            return true;
        }

        public async Task<object> SendAndReturnJSON(dynamic input)
        {
            return input;
        }

        public async Task<object> LaunchProcess(dynamic input)
        {
            string processName = (string)input.processName;
            ProcessController process = new ProcessController(processName);
            process.Start();

            return null;
        }

        public async Task<object> IsProcessRunning(dynamic input)
        {
            string processName = (string)input.processName;
            ProcessController process = new ProcessController(processName);
            return process.IsAnyProcessRunning();
        }

        public async Task<object> KillAllProcessesByName(dynamic input)
        {
            string processName = (string)input.processName;
            ProcessController process = new ProcessController(processName);
            process.KillAll();
            return null;
        }
    }
}
