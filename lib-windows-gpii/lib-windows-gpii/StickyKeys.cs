using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GPII.WindowsAPI.NativeMethods;

namespace GPII.Settings
{
    public class StickyKeys
    {
        public bool IsAudibleFeedbackEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_AUDIBLEFEEDBACK); } }
        public bool IsAvailable { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_AVAILABLE); } }
        public bool IsConfirmHotkeyEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_CONFIRMHOTKEY); } }
        public bool IsHotkeyActiveEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_HOTKEYACTIVE); } }
        public bool IsHotkeySoundEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_HOTKEYSOUND); } }
        public bool IsVisualIndicatorEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_INDICATOR); } }
        public bool IsOn { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_STICKYKEYSON); } }
        public bool IsTriStateEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_TRISTATE); } }
        public bool IsTwoKeysOffEnabled { get { return this.flags.HasFlag(StickyKeys.Flags.SKF_TWOKEYSOFF); } }
        public StickyKeyModifier RightALT { get { return this.rightALT; } }
        public StickyKeyModifier LeftALT { get { return this.leftALT; } }
        public StickyKeyModifier RightCTL { get { return this.rightCTL; } }
        public StickyKeyModifier LeftCTL { get { return this.leftCTL; } }
        public StickyKeyModifier RightWIN { get { return this.rightWIN; } }
        public StickyKeyModifier LeftWIN { get { return this.leftWIN; } }
        public StickyKeyModifier RightSHIFT { get { return this.rightSHIFT; } }
        public StickyKeyModifier LeftSHIFT { get { return this.leftSHIFT; } }


        private StickyKeyModifier rightALT, leftALT;
        private StickyKeyModifier rightCTL, leftCTL;
        private StickyKeyModifier rightWIN, leftWIN;
        private StickyKeyModifier rightSHIFT, leftSHIFT;
        private StickyKeys.Flags flags;

        public StickyKeys()
        {
            Refresh();
        }

        private void Refresh()
        {
            this.flags = GetStickyKeysFromSPI().dwFlags;
            this.rightALT = new StickyKeyModifier(flags.HasFlag(Flags.SKF_RALTLATCHED), flags.HasFlag(Flags.SKF_RALTLOCKED));
            this.leftALT = new StickyKeyModifier(flags.HasFlag(Flags.SKF_LALTLATCHED), flags.HasFlag(Flags.SKF_RALTLOCKED));
            this.rightCTL = new StickyKeyModifier(flags.HasFlag(Flags.SKF_RCTLLATCHED), flags.HasFlag(Flags.SKF_RCTLLOCKED));
            this.leftCTL = new StickyKeyModifier(flags.HasFlag(Flags.SKF_LCTLLATCHED), flags.HasFlag(Flags.SKF_RCTLLOCKED));
            this.rightWIN = new StickyKeyModifier(flags.HasFlag(Flags.SKF_RWINLATCHED), flags.HasFlag(Flags.SKF_RWINLOCKED));
            this.leftWIN = new StickyKeyModifier(flags.HasFlag(Flags.SKF_LWINLATCHED), flags.HasFlag(Flags.SKF_RWINLOCKED));
            this.rightSHIFT = new StickyKeyModifier(flags.HasFlag(Flags.SKF_RSHIFTLATCHED), flags.HasFlag(Flags.SKF_RSHIFTLOCKED));
            this.leftSHIFT = new StickyKeyModifier(flags.HasFlag(Flags.SKF_LSHIFTLATCHED), flags.HasFlag(Flags.SKF_RSHIFTLOCKED));
        }

        private Win32Struct GetStickyKeysFromSPI()
        {
            uint stickyKeysSize = (uint)Marshal.SizeOf(typeof(Win32Struct));
            Win32Struct stickyKeys = new Win32Struct { cbSize = stickyKeysSize, dwFlags = 0 };
            bool result = SystemParametersInfo(uiActions.SPI_GETSTICKYKEYS, stickyKeysSize, ref stickyKeys, 0);
            if (!result) throw new System.ComponentModel.Win32Exception();

            return stickyKeys;
        }

        private void SetStickKeysFlags(StickyKeys.Flags flags)
        {
            uint stickyKeysSize = (uint)Marshal.SizeOf(typeof(Win32Struct));
            Win32Struct stickyKeys = new Win32Struct { cbSize = stickyKeysSize, dwFlags = flags };
            bool result = SystemParametersInfo(uiActions.SPI_SETSTICKYKEYS, stickyKeysSize, ref stickyKeys, 0);
            if (!result) throw new System.ComponentModel.Win32Exception();
        }

        public void TurnOn()
        {
            if (!flags.HasFlag(StickyKeys.Flags.SKF_STICKYKEYSON))
            {
                flags |= StickyKeys.Flags.SKF_STICKYKEYSON;
                SetStickKeysFlags(flags);
            }
        }

        public void TurnOff()
        {
            flags &= ~StickyKeys.Flags.SKF_STICKYKEYSON; // remove the ON bit if set
            SetStickKeysFlags(flags);
        }

        /// <summary>
        /// Structure used in Windows API to get/set Sticky Keys settings
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd373652%28v=vs.85%29.aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Struct
        {
            public uint cbSize;
            public StickyKeys.Flags dwFlags;
        }


        /// <summary>
        /// Bit-flags used in Windows API for Sticky Keys settings 
        /// </summary>
        [Flags]
        public enum Flags : uint
        {
            None = 0x00,
            SKF_AUDIBLEFEEDBACK = 0x00000040,
            SKF_AVAILABLE = 0x00000002,
            SKF_CONFIRMHOTKEY = 0x00000008,
            SKF_HOTKEYACTIVE = 0x00000004,
            SKF_HOTKEYSOUND = 0x00000010,
            SKF_INDICATOR = 0x00000020,
            SKF_STICKYKEYSON = 0x00000001,
            SKF_TRISTATE = 0x00000080,
            SKF_TWOKEYSOFF = 0x00000100,
            SKF_LALTLATCHED = 0x10000000,
            SKF_LCTLLATCHED = 0x04000000,
            SKF_LSHIFTLATCHED = 0x01000000,
            SKF_RALTLATCHED = 0x20000000,
            SKF_RCTLLATCHED = 0x08000000,
            SKF_RSHIFTLATCHED = 0x02000000,
            SKF_LALTLOCKED = 0x00100000,
            SKF_LCTLLOCKED = 0x00040000,
            SKF_LSHIFTLOCKED = 0x00010000,
            SKF_RALTLOCKED = 0x00200000,
            SKF_RCTLLOCKED = 0x00080000,
            SKF_RSHIFTLOCKED = 0x00020000,
            SKF_LWINLATCHED = 0x40000000,
            SKF_RWINLATCHED = 0x80000000,
            SKF_LWINLOCKED = 0x00400000,
            SKF_RWINLOCKED = 0x00800000
        }
    }
}
