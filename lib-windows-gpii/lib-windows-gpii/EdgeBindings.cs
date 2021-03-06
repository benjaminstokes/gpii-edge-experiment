﻿using System;
using System.Threading.Tasks;
using GPII.Settings;
using GPII.SystemProcess;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GPII.edge
{
    /// <summary>
    /// Provides entry points to lib-windows-gpii functionality that can be called by Edge.js.
    /// </summary>
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

        public async Task<object> GetNonClientMetrics(dynamic input)
        {
            return new NonClientMetrics();
        }

        public async Task<object> SetNonClientMetrics(dynamic input)
        {
            var ncm = new NonClientMetrics();
            ncm.UseSettings(input);
            ncm.Apply();
            return new NonClientMetrics();
        }

        public async Task<object> GetScrollWidth(dynamic input)
        {
            return new NonClientMetrics().ScrollWidth;
        }

        public async Task<object> SetScrollWidth(dynamic input)
        {
            int scrollWidth = (int)input.scrollWidth;
            NonClientMetrics ncm = new NonClientMetrics();
            ncm.ScrollWidth = scrollWidth;
            ncm.Apply();
            return new NonClientMetrics().ScrollWidth;
        }

        public async Task<object> DoDotNetException(dynamic input)
        {
            throw new NotImplementedException("what happens in edgejs?");
        }
    }
}
