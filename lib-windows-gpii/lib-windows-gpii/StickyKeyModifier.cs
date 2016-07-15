using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPII.Settings
{
    public class StickyKeyModifier
    {
        private bool isLatched = false;
        private bool isLocked = false;

        public bool IsLatched { get { return isLatched; } }
        public bool IsLocked { get { return isLocked; } }

        public StickyKeyModifier(bool isLatched, bool isLocked)
        {
            this.isLatched = isLatched;
            this.isLocked = isLocked;
        }
    }
}
