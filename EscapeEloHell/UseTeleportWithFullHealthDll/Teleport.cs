namespace UseTeleportWithFullHealthDll
{
    public class Teleport
    {
        public static int TeleportTimeInMilliseconds = 3500;
        public static double FountainRegenerationInPercentPer250Milliseconds = 2.1;

        public static bool IsStart(double percentageHealth, double percentagegMana, bool homeguardActive = false)
        {
            var mssingHp = 100 - percentageHealth;
            var mssingMana = 100 - percentagegMana;

            var startCallbackPercent = FountainRegenerationInPercentPer250Milliseconds * 4 * TeleportTimeInMilliseconds /
                                       1000;

            return mssingHp <= startCallbackPercent || mssingMana <= startCallbackPercent;
        }
    }
}