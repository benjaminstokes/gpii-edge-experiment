

using System;

namespace GPII.drivers
{
    class LibraryDriver
    {
        [STAThread]
        static void Main(string[] args)
        {

            //HighContrastDriver.ToggleHighContrast();
            //HighContrastDriver.TestMultipleInstantionations();
            //StickyKeysDriver.ToggleStickyKeys();

            //HighContrastDriver.TestEmptyStringTheme();
            //HighContrastDriver.TurnOnDefaultHighContrast();

            ProcessDriver.DoTests();

            Console.ReadLine();
        }
    }
}
