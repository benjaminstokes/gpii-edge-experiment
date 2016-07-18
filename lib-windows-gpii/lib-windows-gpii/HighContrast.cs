using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GPII.WindowsAPI.NativeMethods;

namespace GPII.Settings
{
    public class HighContrast
    {
        /// <summary>
        /// The high contrast feature is available.
        /// </summary>
        public bool IsHighContrastAvailable { get; }
        /// <summary>
        /// A confirmation dialog appears when the high contrast feature is activated by using the hot key.
        /// </summary>
        public bool IsConfirmDialogEnabled { get; set; }
        /// <summary>
        /// The high contrast feature is on.
        /// </summary>
        public bool IsHighContrastOn { get; set; }
        /// <summary>
        /// The user can turn the high contrast feature on and off by simultaneously pressing the left ALT, left SHIFT, and PRINT SCREEN keys.
        /// </summary>
        public bool IsHotKeyActive { get; set; }
        /// <summary>
        ///  The hot key associated with the high contrast feature can be enabled. An application can retrieve this value, but cannot set it.
        /// </summary>
        public bool IsHotKeyAvailable { get; }
        /// <summary>
        /// A siren is played when the user turns the high contrast feature on or off by using the hot key.
        /// </summary>
        public bool IsOnOffSirenEnabled { get; set; }
        /// <summary>
        /// A visual indicator is displayed when the high contrast feature is on.This value is not currently used and is ignored.
        /// </summary>
        //public bool IsVisualIndicatorEnabled { get; set; }
        public ColorSchemes Theme { get; set; }

        public HighContrast()
        {
            var structure = GetSystemParameters();
            var flags = (Flags)structure.dwFlags;

            this.IsHighContrastAvailable = flags.HasFlag(Flags.HCF_AVAILABILE);
            this.IsConfirmDialogEnabled = flags.HasFlag(Flags.HCF_CONFIRMHOTKEY);
            this.IsHighContrastOn = flags.HasFlag(Flags.HCF_HIGHCONTRASTON);
            this.IsHotKeyActive = flags.HasFlag(Flags.HCF_HOTKEYACTIVE);
            this.IsHotKeyAvailable = flags.HasFlag(Flags.HCF_HOTKEYAVAILABLE);
            this.IsOnOffSirenEnabled = flags.HasFlag(Flags.HCF_HOTKEYSOUND);

            this.Theme = GetColorSchemeEnum(Marshal.PtrToStringAuto(structure.lpszDefaultScheme));

        }

        private Flags BuildFlags()
        {
            Flags flags = 0x00;
            if (IsHighContrastAvailable)
                flags |= Flags.HCF_AVAILABILE;
            if (IsConfirmDialogEnabled)
                flags |= Flags.HCF_CONFIRMHOTKEY;
            if (IsHighContrastOn)
                flags |= Flags.HCF_HIGHCONTRASTON;
            if (IsHotKeyActive)
                flags |= Flags.HCF_HOTKEYACTIVE;
            if (IsHotKeyAvailable)
                flags |= Flags.HCF_HOTKEYAVAILABLE;
            if (IsOnOffSirenEnabled)
                flags |= Flags.HCF_HOTKEYSOUND;

            return flags;
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

        private ColorSchemes GetColorSchemeEnum(string scheme)
        {
            if (scheme == "High Contrast Black") return ColorSchemes.HighContrastBlack;
            if (scheme == "High Contrast White") return ColorSchemes.HighContrastWhite;
            return ColorSchemes.Default;
        }


        /// <summary>
        /// Applies the HighContrast instance's current state to the system
        /// </summary>
        /// <example>
        /// // turn high contrast on
        /// var hc = new HighContrast();
        /// hc.IsHighContrastOn = true;
        /// hc.Apply();
        /// </example>
        public void Apply()
        {
            Win32Struct win32Struct = new Win32Struct();
            string colorSchemeText = GetColorSchemeText(this.Theme);
            win32Struct.lpszDefaultScheme = Marshal.StringToHGlobalUni(colorSchemeText);
            win32Struct.dwFlags = (int)BuildFlags();
            win32Struct.cbSize = Marshal.SizeOf(win32Struct);
            bool result = SystemParametersInfo(uiActions.SPI_SETHIGHCONTRAST, win32Struct.cbSize, ref win32Struct, 0);
            Marshal.FreeHGlobal(win32Struct.lpszDefaultScheme); // TODO: Ensure this memory is freed via try/catch/finally
            if (!result) throw new System.ComponentModel.Win32Exception();
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
