using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util.Objects
{
    public class Turrets
    {
        public static List<Obj_AI_Turret> AllTurrets;
        public static List<Obj_AI_Turret> AllyTurrets;
        public static List<Obj_AI_Turret> EnemyTurrets;

        public Turrets()
        {
            UpdateTurrets();
            SortTurretsByDistance();
        }

        public void UpdateTurrets()
        {
            AllTurrets = ObjectHandler.Get<Obj_AI_Turret>().ToList();
            AllyTurrets = AllTurrets.FindAll(turret => turret.IsValid && !turret.IsDead && turret.IsAlly);
            EnemyTurrets = AllTurrets.FindAll(turret => turret.IsValid && !turret.IsDead && turret.IsEnemy);
        }

        public void SortTurretsByDistance()
        {
            AllTurrets = AllTurrets.OrderBy(turret => turret.Distance(Heroes.Me, true)).ToList();
            AllyTurrets = AllyTurrets.OrderBy(turret => turret.Distance(Heroes.Me, true)).ToList();
            EnemyTurrets = EnemyTurrets.OrderBy(turret => turret.Distance(Heroes.Me, true)).ToList();
        }
    }
}