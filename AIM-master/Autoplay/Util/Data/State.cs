//WIP, removed from release.


using AIM.Autoplay.Util.Helpers;
using AIM.Autoplay.Util.Objects;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util.Data
{
    public class State
    {
        private static readonly Constants Constants = new Constants();

        public static bool IsBotSafe()
        {
            var map = Constants.Map;
            if (map != null && map.Type == Utility.Map.MapType.HowlingAbyss)
            {
                return true;
            }
            if (Variables.TookRecallDecision)
            {
                return false;
            }
            if (Heroes.Me.InFountain())
            {
                return (Heroes.Me.Health > Heroes.Me.MaxHealth * 0.9f) && (Heroes.Me.Mana > Heroes.Me.MaxMana * 0.8f);
            }
            if (Heroes.Me.Mana < Heroes.Me.MaxMana * Constants.LowManaRatio)
            {
                return Heroes.Me.Health > Heroes.Me.MaxHealth * Constants.LowHealthIfLowManaRatio &&
                       !Heroes.Me.IsRecalling() &&
                       !(Heroes.Me.Gold > Randoms.NeededGoldToBack && !MetaHandler.HasSixItems());
            }
            return (Heroes.Me.Health > Heroes.Me.MaxHealth * Constants.LowHealthRatio) && !Heroes.Me.IsRecalling() &&
                   !(Heroes.Me.Gold > Randoms.NeededGoldToBack && !MetaHandler.HasSixItems());
        }
    }
}