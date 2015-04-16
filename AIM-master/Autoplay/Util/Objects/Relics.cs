using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util.Objects
{
    public class Relics
    {
        public static Obj_AI_Base ClosestRelic()
        {
            var hprelics =
                ObjectHandler.Get<Obj_AI_Base>()
                    .FindAll(r => r.IsValid && r.Name.Contains("HealthPack"))
                    .ToList()
                    .OrderBy(r => Heroes.Me.Distance(r, true));
            return hprelics.First();
        }
    }
}