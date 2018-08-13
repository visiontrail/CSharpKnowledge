using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.TryAnyCSharp
{
    enum TimeTicked
    {
        NeverTicked = 0,
        PressedStart = 1,
        PressedEnd = 3
    }

    class TwoTimeSpan
    {
        public DateTime time_one { get; set; }
        public DateTime time_two { get; set; }

        private TimeSpan time_span
        {
            get
            {
                return time_two - time_one;
            }
            set
            {
                value = time_two - time_one;
            }
        }

        public int TimeDelaySecond
        {
            get
            {
                return time_span.Seconds;
            }
            set
            {
                value = time_span.Seconds;
            }
        }
    }
}
