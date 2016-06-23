using System;
using System.Runtime.InteropServices;
using GPII.SystemSettings;

namespace GPII.WindowsAPI
{
    public class user32
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uiActions uiAction, uint uiParam, ref StickyKeys.Win32Struct pvParam, SPIFlags fWinIni);

        public enum uiActions : uint
        {
            SPI_GETHIGHCONTRAST = 0x0042,
            SPI_GETSHOWSOUNDS = 0x0038,
            SPI_GETSTICKYKEYS = 0x003A,

            SPI_SETHIGHCONTRAST = 0x0043,
            SPI_SETSHOWSOUNDS = 0x0039,
            SPI_SETSTICKYKEYS = 0x003B
        }

        [Flags]
        public enum SPIFlags
        {
            None = 0x00,
            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,
            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,
            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        }
    }
}
