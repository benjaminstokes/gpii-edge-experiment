using static System.Diagnostics.Debug;
using GPII.SystemSettings;

namespace GPII
{
    class LibraryDriver
    {
        static void Main(string[] args)
        {
            ToggleStickyKeys();
        }

        static void ToggleStickyKeys()
        {
            StickyKeys stickyKeys = new StickyKeys();

            stickyKeys.TurnOn();
            Assert(new StickyKeys().IsOn);

            stickyKeys.TurnOff();
            Assert(new StickyKeys().IsOn == false);
        }
    }
}
