using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace AIM.Autoplay.Util.Objects
{
    public class Minions
    {
        public List<Obj_AI_Minion> AllMinions;
        public List<Obj_AI_Minion> AllyMinions;
        public List<Obj_AI_Minion> EnemyMinions;

        public Minions()
        {
            UpdateMinions();
        }

        public void UpdateMinions()
        {
            AllMinions = ObjectHandler.Get<Obj_AI_Minion>().ToList();
            AllyMinions =
                AllMinions.FindAll(
                    minion => minion.IsValid && !minion.IsDead && minion.IsAlly && MinionManager.IsMinion(minion));
            EnemyMinions =
                AllMinions.FindAll(
                    minion => minion.IsValid && !minion.IsDead && minion.IsEnemy && MinionManager.IsMinion(minion));
        }

        public Obj_AI_Minion GetLeadMinion(Vector3? position = null)
        {
            var pos = position ?? ObjectHandler.Player.ServerPosition;
            var closestTurret = Turrets.EnemyTurrets.OrderBy(t => t.Distance(pos, true)).FirstOrDefault();

            if (closestTurret == null)
            {
                // need to add more logic here
                // like minions closest to enemy nexus or inhib
                return null;
            }

            return AllyMinions.OrderBy(x => x.Distance(closestTurret.Position, true)).FirstOrDefault();
        }

        public Obj_AI_Minion GetClosestEnemyMinion(Vector3? position = null)
        {
            var pos = position ?? ObjectHandler.Player.ServerPosition;

            return
                EnemyMinions.OrderBy(x => x.Distance(pos, true))
                    .FirstOrDefault(
                        minion => minion.IsValid && !minion.IsDead && minion.IsEnemy && MinionManager.IsMinion(minion));
        }
    }
}