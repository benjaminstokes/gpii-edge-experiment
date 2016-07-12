using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GPII.SystemSettings;
using static System.Diagnostics.Debug;

namespace GPII.drivers
{
    class StickyKeysDriver : ITestDriver
    {
        private void ToggleStickyKeys()
        {
            StickyKeys stickyKeys = new StickyKeys();

            stickyKeys.TurnOn();
            Assert(new StickyKeys().IsOn, "Sticky Keys should be on");

            stickyKeys.TurnOff();
            Assert(new StickyKeys().IsOn == false, "Sticky keys should be off");
        }

        public void DoTests()
        {
            Logger.Debug("Running StickyKeys tests");
            ToggleStickyKeys();
        }
    }
}
