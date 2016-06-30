using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GPII.WindowsAPI.user32;

namespace GPII.SystemSettings
{
    public class HighContrast
    {
        /// <summary>
        /// The high contrast feature is available.
        /// </summary>
        public bool IsHighContrastAvailable { get { return this.flags.HasFlag(Flags.HCF_AVAILABILE); } }
        /// <summary>
        /// A confirmation dialog appears when the high contrast feature is activated by using the hot key.
        /// </summary>
        public bool IsConfirmDialogEnabled { get { return this.flags.HasFlag(Flags.HCF_CONFIRMHOTKEY); } }
        /// <summary>
        /// The high contrast feature is on.
        /// </summary>
        public bool IsHighContrastOn { get { return this.flags.HasFlag(Flags.HCF_HIGHCONTRASTON); } }
        /// <summary>
        /// The user can turn the high contrast feature on and off by simultaneously pressing the left ALT, left SHIFT, and PRINT SCREEN keys.
        /// </summary>
        public bool IsHotKeyActive { get { return this.flags.HasFlag(Flags.HCF_HOTKEYACTIVE); } }
        /// <summary>
        ///  The hot key associated with the high contrast feature can be enabled. An application can retrieve this value, but cannot set it.
        /// </summary>
        public bool IsHotKeyAvailable { get { return this.flags.HasFlag(Flags.HCF_HOTKEYAVAILABLE); } }
        /// <summary>
        /// A siren is played when the user turns the high contrast feature on or off by using the hot key.
        /// </summary>
        public bool IsOnOffSirenEnabled { get { return this.flags.HasFlag(Flags.HCF_HOTKEYSOUND); } }
        /// <summary>
        /// A visual indicator is displayed when the high contrast feature is on.This value is not currently used and is ignored.
        /// </summary>
        public bool IsVisualIndicatorEnabled { get { return this.flags.HasFlag(Flags.HCF_INDICATOR); } }
        public string ThemeName { get; }

        private Flags flags;

        public HighContrast()
        {
            var structure = GetSystemParameters();
            this.flags = (Flags)structure.dwFlags;
            this.ThemeName = Marshal.PtrToStringAuto(structure.lpszDefaultScheme);
        }

        private Win32Struct GetSystemParameters()
        {
            Win32Struct win32Struct = new Win32Struct();
            win32Struct.cbSize = Marshal.SizeOf(win32Struct); ;
            bool result = SystemParametersInfo(uiActions.SPI_GETHIGHCONTRAST, win32Struct.cbSize, ref win32Struct, 0);
            if (!result) throw new System.ComponentModel.Win32Exception();

            return win32Struct;
        }

        private string GetColorSchemeText(ColorSchemes scheme)
        {
            var enumType = typeof(ColorSchemes);
            var enumValue = enumType.GetMember(scheme.ToString())[0];
            var descriptionAttributes = enumValue.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string description = ((DescriptionAttribute)descriptionAttributes[0]).Description;

            return description;
        }


        private void SetSystemParameters(Flags flags, ColorSchemes scheme)
        {
            Win32Struct win32Struct = new Win32Struct();
            string colorSchemeText = GetColorSchemeText(scheme);
            win32Struct.lpszDefaultScheme = Marshal.StringToHGlobalUni(colorSchemeText);
            win32Struct.dwFlags = (int)flags;
            win32Struct.cbSize = Marshal.SizeOf(win32Struct);
            bool result = SystemParametersInfo(uiActions.SPI_SETHIGHCONTRAST, win32Struct.cbSize, ref win32Struct, 0);
            Marshal.FreeHGlobal(win32Struct.lpszDefaultScheme); // TODO: Ensure this memory is freed via try/catch/finally
            if (!result) throw new System.ComponentModel.Win32Exception();
        }


        public void TurnOn(ColorSchemes scheme)
        {
            if (!flags.HasFlag(Flags.HCF_HIGHCONTRASTON))
            {
                flags |= Flags.HCF_HIGHCONTRASTON;
                SetSystemParameters(flags, scheme);
            }
        }

        public void TurnOff()
        {
            flags &= ~Flags.HCF_HIGHCONTRASTON; // remove the ON bit if set
            SetSystemParameters(flags, ColorSchemes.Default);
        }


        /// <summary>
        /// Structure used in Windows API to get/set high contrast settings
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd318112%28v=vs.85%29.aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct Win32Struct
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr lpszDefaultScheme;
        }
        /// <summary>
        /// Bit-flags used in Windows API for High Contrast settings 
        /// </summary>
        [Flags]
        public enum Flags : uint
        {
            //None = 0x00,
            HCF_AVAILABILE = 0x00000002,
            HCF_CONFIRMHOTKEY = 0x00000008,
            HCF_HIGHCONTRASTON = 0x00000001,
            HCF_HOTKEYACTIVE = 0x00000004,
            HCF_HOTKEYAVAILABLE = 0x00000040,
            HCF_HOTKEYSOUND = 0x00000010,
            HCF_INDICATOR = 0x00000020
        }

        public enum ColorSchemes
        {
            [Description("High Contrast White")]
            HighContrastWhite,
            [Description("High Contrast Black")]
            HighContrastBlack,
            [Description("")]
            Default
        }
    }
}
