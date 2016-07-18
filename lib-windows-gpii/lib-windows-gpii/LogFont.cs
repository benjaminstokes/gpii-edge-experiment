using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GPII.Settings
{
    /// <summary>
    /// Provides a constrained API via Properties to a LOGFONT structure
    /// </summary>
    public class LogFont
    {
        // TODO: Do we need a full implementation of LogFont or are italics, underline, strikeout sufficient?
        // I suppose it depends on how many preference values will need to be implemented by LogFont members of NonClientMetrics.
        public bool IsItalic { get { return Convert.ToBoolean(lfStruct.lfItalic); } set { lfStruct.lfItalic = Convert.ToByte(value); } }
        public bool IsUnderline { get { return Convert.ToBoolean(lfStruct.lfUnderline); } set { lfStruct.lfUnderline = Convert.ToByte(value); } }
        public bool IsStrikeout { get { return Convert.ToBoolean(lfStruct.lfStrikeOut); } set { lfStruct.lfStrikeOut = Convert.ToByte(value); } }

        private LOGFONT lfStruct;
        public LogFont(LOGFONT lfStruct)
        {
            this.lfStruct = lfStruct;
        }

        public LOGFONT GetUnderlyingStruct()
        {
            return this.lfStruct;
        }

        // Font face names in the LOGFONT structure can only be 32 characters including a terminator
        public const int LF_FACESIZE = 32;

        /// <summary>
        /// The LOGFONT structure used for NonClientMetrics SPI calls
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string lfFaceName;
        }
    }
}
