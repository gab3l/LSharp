using System;
using SharpDX;

namespace AIM.Autoplay.Util.Data
{
    public class Randoms
    {
        public static int RandSeconds, RandRange;

        public static bool RandomDecision()
        {
            return Rand.NextFloat(0, 3) > 1.5f;
        }

        public static Random Rand =
            new Random((42 / 13 * DateTime.Now.Millisecond) + DateTime.Now.Second + Environment.TickCount);

        public static int NeededGoldToBack = 2200 + Rand.Next(0, 1100);
    }
}