using GPII.drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPII.Settings;
using static System.Diagnostics.Debug;
using System.Reflection;

namespace GPII
{
    class NonClientMetricsDriver : ITestDriver
    {
        public void DoTests()
        {
            Logger.Debug("Running NonClientMetricsDriver");
            TestProfile();
            TestBooleanLogFontStyles();
            TestAllInt32Properties();
        }

        // Tests a profile matching the GPII unit test for NonClientMetrics
        public void TestProfile()
        {
            Logger.Debug("Testing a sample profile");
            // Save original settings
            var ncmOriginal = new NonClientMetrics();

            // Change settings
            var ncm = new NonClientMetrics();
            ncm.ScrollWidth = 30;
            ncm.ScrollHeight = 30;
            ncm.CaptionFont.IsItalic = true;
            ncm.SmallCaptionFont.IsUnderline = true;
            ncm.MenuFont.IsItalic = true;
            ncm.MenuFont.IsUnderline = true;
            ncm.Apply();

            // Test settings were saved in system. We use the ncmAssert instance for assertions            
            var ncmAssert = new NonClientMetrics();
            Assert(ncmAssert.ScrollWidth == 30, "ScrollWidth should have been set");
            Assert(ncmAssert.ScrollHeight == 30, "ScrollHeight should have been set");
            Assert(ncmAssert.CaptionFont.IsItalic == true, "CaptionFont should be Italic");
            Assert(ncmAssert.SmallCaptionFont.IsUnderline == true, "SmallCaptionFont should be Underlined");
            Assert(ncmAssert.MenuFont.IsItalic == true, "MenuFont should be Italic");
            Assert(ncmAssert.MenuFont.IsUnderline == true, "MenuFont should be underlined");

            // Restore settings and verify
            ncmOriginal.Apply();
            ncmAssert = new NonClientMetrics();
            Assert(ncmAssert.ScrollHeight == ncmOriginal.ScrollHeight, "ScrollHeight should have been restored");
            Assert(ncmAssert.ScrollWidth == ncmOriginal.ScrollWidth, "ScrollWidth should have been restored");
            Assert(ncmAssert.CaptionFont.IsItalic == ncmOriginal.CaptionFont.IsItalic, "CaptionFont should have been restored");
            Assert(ncmAssert.SmallCaptionFont.IsUnderline == ncmOriginal.SmallCaptionFont.IsUnderline, "SmallCaptionFont should have been restored");
            Assert(ncmAssert.MenuFont.IsItalic == ncmOriginal.MenuFont.IsItalic, "MenuFont should have been restored (italics)");
            Assert(ncmAssert.MenuFont.IsUnderline == ncmOriginal.MenuFont.IsUnderline, "MenuFont should have been restored (underline)");
            // TODO: Should the equality logic be implemented in NonClientMetrics class so we can write (ncm1 == ncm2) in test code?
        }

        // Uses reflection to test each LogFont and Boolean style (italic, underline, etc) combinaton. Each style is toggled to true then false.
        public void TestBooleanLogFontStyles()
        {
            foreach (var logFontProperty in typeof(NonClientMetrics).GetProperties())
            {
                if (logFontProperty.PropertyType.Name != "LogFont")
                    continue;

                foreach (var booleanStyleProperty in typeof(LogFont).GetProperties())
                {
                    if (booleanStyleProperty.PropertyType.Name != "Boolean")
                        continue;

                    Logger.Debug("Testing NonClientMetrics." + logFontProperty.Name + "." + booleanStyleProperty.Name);
                    var ncm = new NonClientMetrics();
                    booleanStyleProperty.SetValue(((LogFont)logFontProperty.GetValue(ncm)), true);
                    ncm.Apply();
                    Assert((bool)booleanStyleProperty.GetValue(((LogFont)logFontProperty.GetValue(new NonClientMetrics()))) == true);
                    booleanStyleProperty.SetValue(((LogFont)logFontProperty.GetValue(ncm)), false);
                    ncm.Apply();
                    Assert((bool)booleanStyleProperty.GetValue(((LogFont)logFontProperty.GetValue(new NonClientMetrics()))) == false);
                }
            }
        }

        /// <summary>
        /// NonClientMetrics has so many integer properties that we're going to use reflection to generate the tests for each property
        /// with a common pattern of get the value, change it, restore the original
        /// </summary>
        public void TestAllInt32Properties()
        {
            // We hold property names and test values in this dict so we can feed the test code. Keys are property names. Values are ints that the
            // property will be set to during the test.
            Dictionary<string, int> properties = new Dictionary<string, int>();
            properties.Add("BorderWidth", 5);
            properties.Add("ScrollWidth", 16); // 8 is minimum
            properties.Add("ScrollHeight", 8); // 8 is minimum
            properties.Add("CaptionWidth", 28); // 28 is default
            properties.Add("CaptionHeight", 17); // 17 is default
            properties.Add("SmallCaptionWidth", 25); // 17 is default
            properties.Add("SmallCaptionHeight", 25); // 17 is default
            properties.Add("MenuWidth", 15); // 19 is default, 8 is minimum
            properties.Add("MenuHeight", 25); // 19 is default, 17 is minimum
            properties.Add("PaddedBorderWidth", 5); // 0 is default 

            Type type = typeof(NonClientMetrics);
            foreach (var keyValuePair in properties)
            {
                PropertyInfo p = type.GetProperty(keyValuePair.Key);
                Logger.Debug("Testing NonClientMetrics set & restore of " + p.Name);
                TestInt32Property(p, keyValuePair.Value);
            }
        }

        /// <summary>
        /// Sets a Int32 property of a NonClientMetrics instance to a value, verifies, and restores original value
        /// </summary>
        /// <param name="p">PropertyInfo instance to test (must be a Int32 type)</param>
        /// <param name="newValue">Value the property will be set to</param>
        private void TestInt32Property(PropertyInfo p, int newValue)
        {
            // Obtain a NCM instance and record the original value
            var ncm = new NonClientMetrics();
            var originalValue = (int)p.GetValue(ncm);

            // Set the new value
            p.SetValue(ncm, newValue);
            ncm.Apply();

            // Assert the new value was set
            Assert((int)p.GetValue(new NonClientMetrics()) == newValue);

            // Restore the original
            p.SetValue(ncm, originalValue);
            ncm.Apply();

            // Assert the original setting was restored
            Assert((int)p.GetValue(new NonClientMetrics()) == originalValue);
        }
    }
}
