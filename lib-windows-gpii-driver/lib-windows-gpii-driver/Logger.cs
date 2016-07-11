using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPII.drivers
{
    class Logger
    {
        private static string datetimeFormat = "yyyy-MM-dd:HHmm.ss.fff";

        public static void Debug(string msg)
        {
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString(datetimeFormat), msg));
        }

        public static void LogBitnessInfo()
        {
            Debug("Operating system bitness: " + (Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit"));
            Debug("Process bitness: " + (Environment.Is64BitProcess ? "64-bit" : "32-bit"));
        }
    }
}
