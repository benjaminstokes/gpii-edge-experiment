﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GPII.Settings.LogFont;
using static GPII.WindowsAPI.NativeMethods;

namespace GPII.Settings
{
    public class NonClientMetrics
    {
        public int BorderWidth { get { return win32Struct.iBorderWidth; } set { win32Struct.iBorderWidth = value; } }
        public int ScrollWidth { get { return win32Struct.iScrollWidth; } set { win32Struct.iScrollWidth = value; } }
        public int ScrollHeight { get { return win32Struct.iScrollHeight; } set { win32Struct.iScrollHeight = value; } }
        public int CaptionWidth { get { return win32Struct.iCaptionWidth; } set { win32Struct.iCaptionWidth = value; } }
        public int CaptionHeight { get { return win32Struct.iCaptionHeight; } set { win32Struct.iCaptionHeight = value; } }
        public int SmallCaptionWidth { get { return win32Struct.iSmCaptionWidth; } set { win32Struct.iSmCaptionWidth = value; } }
        public int SmallCaptionHeight { get { return win32Struct.iSmCaptionHeight; } set { win32Struct.iSmCaptionHeight = value; } }
        public int MenuWidth { get { return win32Struct.iMenuWidth; } set { win32Struct.iMenuWidth = value; } }
        public int MenuHeight { get { return win32Struct.iMenuHeight; } set { win32Struct.iMenuHeight = value; } }
        public int PaddedBorderWidth { get { return win32Struct.iPaddedBorderWidth; } set { win32Struct.iPaddedBorderWidth = value; } }
        public LogFont MenuFont { get; set; }
        public LogFont StatusFont { get; set; }
        public LogFont MessageFont { get; set; }
        public LogFont CaptionFont { get; set; }
        public LogFont SmallCaptionFont { get; set; }

        private NONCLIENTMETRICS win32Struct;

        public NonClientMetrics()
        {
            this.win32Struct = GetSystemParameters();
            this.MenuFont = new LogFont(win32Struct.lfMenuFont);
            this.StatusFont = new LogFont(win32Struct.lfStatusFont);
            this.MessageFont = new LogFont(win32Struct.lfMessageFont);
            this.CaptionFont = new LogFont(win32Struct.lfCaptionFont);
            this.SmallCaptionFont = new LogFont(win32Struct.lfSmCaptionFont);
        }

        private NONCLIENTMETRICS GetSystemParameters()
        {
            NONCLIENTMETRICS win32Struct = new NONCLIENTMETRICS();
            win32Struct.cbSize = Marshal.SizeOf(win32Struct);
            bool result = SystemParametersInfo(uiActions.SPI_GETNONCLIENTMETRICS, win32Struct.cbSize, ref win32Struct, 0);
            if (!result) throw new System.ComponentModel.Win32Exception();

            return win32Struct;
        }

        /// <summary>
        /// Applies the current NonClientMetrics' instance's values to the operating system.
        /// </summary>
        public void Apply()
        {
            // Refresh the LOGFONT structs from their underlying values inside the LogFont instances
            this.win32Struct.lfCaptionFont = CaptionFont.GetUnderlyingStruct();
            this.win32Struct.lfMenuFont = MenuFont.GetUnderlyingStruct();
            this.win32Struct.lfMessageFont = MessageFont.GetUnderlyingStruct();
            this.win32Struct.lfSmCaptionFont = SmallCaptionFont.GetUnderlyingStruct();
            this.win32Struct.lfStatusFont = StatusFont.GetUnderlyingStruct();

            bool result = SystemParametersInfo(uiActions.SPI_SETNONCLIENTMETRICS, win32Struct.cbSize, ref win32Struct, 0);
            if (!result) throw new System.ComponentModel.Win32Exception();
        }

        /// <summary>
        /// Struct used for NONCLIENTMENTRICS in Windows API
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NONCLIENTMETRICS
        {
            public int cbSize;
            public int iBorderWidth;
            public int iScrollWidth;
            public int iScrollHeight;
            public int iCaptionWidth;
            public int iCaptionHeight;
            public LOGFONT lfCaptionFont;
            public int iSmCaptionWidth;
            public int iSmCaptionHeight;
            public LOGFONT lfSmCaptionFont;
            public int iMenuWidth;
            public int iMenuHeight;
            public LOGFONT lfMenuFont;
            public LOGFONT lfStatusFont;
            public LOGFONT lfMessageFont;
            public int iPaddedBorderWidth; // special care needed if we are to support prior to windows 7
        }
    }
}