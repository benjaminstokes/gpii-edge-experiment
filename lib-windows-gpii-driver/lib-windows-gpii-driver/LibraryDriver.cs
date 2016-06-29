

using System;

namespace GPII.drivers
{
    class LibraryDriver
    {
        [STAThread]
        static void Main(string[] args)
        {

            HighContrastDriver.ToggleHighContrast();
            HighContrastDriver.TestMultipleInstantionations();
            // StickyKeysDriver.ToggleStickyKeys();

            Console.ReadLine();
        }
    }
}
