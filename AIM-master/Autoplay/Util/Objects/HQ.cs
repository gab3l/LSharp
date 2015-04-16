using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util.Objects
{
    public class HQ
    {
        public static Obj_HQ AllyHQ = ObjectHandler.Get<Obj_HQ>().FirstOrDefault(hq => hq.IsAlly);
        public static Obj_HQ EnemyHQ = ObjectHandler.Get<Obj_HQ>().FirstOrDefault(hq => hq.IsEnemy);
    }
}