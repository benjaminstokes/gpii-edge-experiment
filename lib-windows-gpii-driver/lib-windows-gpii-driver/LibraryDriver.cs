

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
            drivers.Add(new ProcessDriver());
            drivers.Add(new StickyKeysDriver());
            drivers.Add(new HighContrastDriver());

            foreach (var driver in drivers)
            {
                driver.DoTests();
            }

            Logger.Debug("tests completed");
            Console.ReadLine();
        }
    }
}
