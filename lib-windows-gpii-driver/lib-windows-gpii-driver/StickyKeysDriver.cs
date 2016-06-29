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
    class StickyKeysDriver
    {
        public static void ToggleStickyKeys()
        {
            StickyKeys stickyKeys = new StickyKeys();

            stickyKeys.TurnOn();
            Assert(new StickyKeys().IsOn);

            stickyKeys.TurnOff();
            Assert(new StickyKeys().IsOn == false);
        }
    }
}
