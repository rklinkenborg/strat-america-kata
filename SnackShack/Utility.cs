using System;
using System.Collections.Generic;

namespace SnackShack
{
    class Utility
    {
        public static string ConvertSecToTimeString(int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            return string.Format("{0}:{1:D2}", t.Minutes, t.Seconds);
        }
    }
}
