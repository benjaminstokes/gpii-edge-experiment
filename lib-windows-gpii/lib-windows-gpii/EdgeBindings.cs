using System;
using System.Threading.Tasks;
using GPII.SystemSettings;
using System.IO;

namespace GPII.edge
{
    class EdgeBindings
    {
        public async Task<object> TurnOnStickyKeys(object input)
        {
            var stickyKeys = new StickyKeys();
            stickyKeys.TurnOn();
            return true;
        }

        public async Task<object> TurnOffStickyKeys(object input)
        {
            var stickyKeys = new StickyKeys();
            stickyKeys.TurnOff();
            return true;
        }

        public async Task<object> TurnOnHighContrast(object input)
        {
            var highContrast = new HighContrast();
            highContrast.IsHighContrastOn = true;
            highContrast.Theme = HighContrast.ColorSchemes.HighContrastBlack;
            highContrast.Apply();
            return true;
        }

        public async Task<object> TurnOffHighContrast(object input)
        {
            var highContrast = new HighContrast();
            highContrast.IsHighContrastOn = false;
            highContrast.Apply();
            return true;
        }

        public async Task<object> SendAndReturnJSON(object input)
        {
            return input;
        }
    }
}
