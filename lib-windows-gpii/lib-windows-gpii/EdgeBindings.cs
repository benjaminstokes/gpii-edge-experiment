using System;
using System.Threading.Tasks;
using GPII.SystemSettings;

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

        public async Task<object> JSONPassTest(object input)
        {
            Console.WriteLine(input.ToString());
            return true;
        }

        public async Task<object> JSONReturnTest(object input)
        {
            var anonymousReturnSample = new
            {
                error = false,
                data = new
                {
                    key1 = "some data1",
                    key2 = "some data2",
                    ids = new[]
                    {
                        new {
                            item1 = "item1",
                            item2 = "item2"
                        }

                    }
                }
            };

            return anonymousReturnSample;
        }
    }
}
