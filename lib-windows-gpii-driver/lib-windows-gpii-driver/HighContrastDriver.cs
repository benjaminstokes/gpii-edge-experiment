using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;
using GPII.SystemSettings;
using System.Threading;

namespace GPII.drivers
{
    class HighContrastDriver
    {
        public static void PollHighConttrastUntilSettingIs(bool until)
        {
            Logger.Debug("Entering PollHighConttrastUntilSettingIs");
            HighContrast hc = new HighContrast();
            do
            {
                hc = new HighContrast();
                Logger.Debug(string.Format("HighContrast setting: {0}", hc.IsHighContrastOn));
            } while (until != hc.IsHighContrastOn);
        }

        /// <summary>
        /// A previous marshalling bug caused crashing if multiple HighContrasts were created in rapid succession
        /// </summary>
        public static void TestMultipleInstantionations()
        {
            Logger.Debug("Entering TestMultipleInstantionations");
            var hc3 = new HighContrast();
            for (int i = 0; i < 10; i++)
            {
                Logger.Debug("creating HighContrast #" + i.ToString());
                new HighContrast();
            }
        }

        public static void ToggleHighContrast()
        {
            Logger.Debug("Entering ToggleHighContrast");

            HighContrast highContrast = new HighContrast();
            highContrast.TurnOn(HighContrast.ColorSchemes.HighContrastBlack);
            Logger.Debug("High contrast should be ON: HighContrastBlack");
            Assert(new HighContrast().IsHighContrastOn == true);
            PollHighConttrastUntilSettingIs(true);

            WaitForContrastChange();

            highContrast.TurnOff();
            PollHighConttrastUntilSettingIs(false);
            Assert(new HighContrast().IsHighContrastOn == false);
            Logger.Debug("High contrast should be OFF");

            WaitForContrastChange();

            highContrast.TurnOn(HighContrast.ColorSchemes.HighContrastWhite);
            PollHighConttrastUntilSettingIs(true);
            Assert(new HighContrast().IsHighContrastOn);
            Logger.Debug("High contrast should be ON: HighContrastWhite");

            WaitForContrastChange();

            highContrast.TurnOff();
            PollHighConttrastUntilSettingIs(false);
            Assert(new HighContrast().IsHighContrastOn == false);
            Logger.Debug("High contrast should be OFF");
            WaitForContrastChange();
        }

        private static void WaitForContrastChange()
        {
            int sleepTimeInSeconds = 10;
            Thread.Sleep(sleepTimeInSeconds * 1000);
        }
    }
}
