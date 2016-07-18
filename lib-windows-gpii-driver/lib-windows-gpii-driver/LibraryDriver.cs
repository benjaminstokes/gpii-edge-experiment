

using System;
using System.Collections.Generic;

namespace GPII.drivers
{
    class LibraryDriver
    {
        [STAThread]
        static void Main(string[] args)
        {
            var drivers = new List<ITestDriver>();
            // drivers.Add(new ProcessDriver());
            //  drivers.Add(new StickyKeysDriver());
            //  drivers.Add(new HighContrastDriver());
            drivers.Add(new NonClientMetricsDriver());

            foreach (var driver in drivers)
            {
                driver.DoTests();
            }

            Logger.Debug("Tests completed. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
