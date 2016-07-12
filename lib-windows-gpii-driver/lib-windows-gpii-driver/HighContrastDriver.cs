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
    class HighContrastDriver : ITestDriver
    {
        public void DoTests()
        {
            Logger.Debug("Running HighContrast tests");
            TestMultipleInstantionations();
            TestEmptyStringTheme();
            ToggleHighContrast();
        }


        public void PollHighConttrastUntilSettingIs(bool until)
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
        private void TestMultipleInstantionations()
        {
            Logger.Debug("Entering TestMultipleInstantionations");
            var hc3 = new HighContrast();
            for (int i = 0; i < 10; i++)
            {
                Logger.Debug("creating HighContrast #" + i.ToString());
                new HighContrast();
            }
        }

        private void TestEmptyStringTheme()
        {
            var highContrast = new HighContrast();

            highContrast.Theme = HighContrast.ColorSchemes.HighContrastBlack;
            highContrast.Apply();
            Assert(new HighContrast().Theme == HighContrast.ColorSchemes.HighContrastBlack);

            highContrast.Theme = HighContrast.ColorSchemes.HighContrastWhite;
            highContrast.Apply();
            Assert(new HighContrast().Theme == HighContrast.ColorSchemes.HighContrastWhite);

            highContrast.Theme = HighContrast.ColorSchemes.Default;  // value of Default is empty string
            highContrast.Apply();
            Assert(new HighContrast().Theme == HighContrast.ColorSchemes.Default);  // This WILL fail because the Theme value is still HighContrastWhite.
        }

        /*
        private static void TurnOnDefaultHighContrast()
        {
            var highContrast = new HighContrast();
            highContrast.IsHighContrastOn = true;
            highContrast.Theme = HighContrast.ColorSchemes.HighContrastBlack;
            highContrast.Apply();
        }
        */

        private void ToggleHighContrast()
        {
            Logger.Debug("Entering ToggleHighContrast");

            HighContrast highContrast = new HighContrast();
            highContrast.IsHighContrastOn = true;
            highContrast.Theme = HighContrast.ColorSchemes.HighContrastBlack;
            highContrast.Apply();
            Logger.Debug("High contrast should be ON: HighContrastBlack");
            Assert(new HighContrast().IsHighContrastOn == true);
            PollHighConttrastUntilSettingIs(true);

            WaitForContrastChange();
            highContrast.IsHighContrastOn = false;
            highContrast.Apply();
            PollHighConttrastUntilSettingIs(false);
            Assert(new HighContrast().IsHighContrastOn == false);
            Logger.Debug("High contrast should be OFF");

            WaitForContrastChange();

            highContrast.IsHighContrastOn = true;
            highContrast.Theme = HighContrast.ColorSchemes.HighContrastWhite;
            highContrast.Apply();
            PollHighConttrastUntilSettingIs(true);
            Assert(new HighContrast().IsHighContrastOn);
            Logger.Debug("High contrast should be ON: HighContrastWhite");

            WaitForContrastChange();

            highContrast.IsHighContrastOn = false;
            highContrast.Apply();
            PollHighConttrastUntilSettingIs(false);
            Assert(new HighContrast().IsHighContrastOn == false);
            Logger.Debug("High contrast should be OFF");
            WaitForContrastChange();
        }

        private void WaitForContrastChange()
        {
            int sleepTimeInSeconds = 10;
            Thread.Sleep(sleepTimeInSeconds * 1000);
        }
    }
}
