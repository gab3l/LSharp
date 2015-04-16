using System;
using System.Linq;
using AIM.Autoplay.Util;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Helpers;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using Orbwalking = AIM.Autoplay.Util.Orbwalking;

namespace AIM.Autoplay.Modes
{
	public abstract class Base
	{
		private static readonly Obj_AI_Hero Player = ObjectManager.Player;
		public static int LastMove;
		public static int mvmntDelay = 200;
		public static Menu Menu;
		public static Menu Orbwalker;
		public static Obj_AI_Minion LeadingMinion;
		public static Obj_AI_Minion ClosestEnemyMinion;

		protected Base()
		{
			ObjConstants = new Constants();
			ObjHeroes = new Heroes();
			ObjMinions = new Minions();
			ObjTurrets = new Turrets();
			Menu = new Menu("AIM", "AIM", true);

			//AIM Settings
            Menu.AddItem(new MenuItem("Enabled", "Enabled").SetValue(new KeyBind(32, KeyBindType.Toggle)));
			Menu.AddItem(new MenuItem("LowHealth", "Self Low Health %").SetValue(new Slider(20, 10, 50)));
			Menu.AddItem(new MenuItem("autobounds", "AUTO SET MIN MAX(turn off to manually set)").SetValue(true));
			Menu.AddItem(new MenuItem("MinDist", "Min Dist ").SetValue(new Slider(100, -500, 1000)));
			Menu.AddItem(new MenuItem("MaxDist", "Max Dist ").SetValue(new Slider(300, -500, 1000)));

			//Humanizer
            var move = Menu.AddSubMenu(new Menu("Humanizer", "humanizer"));
			move.AddItem(new MenuItem("MovementEnabled", "Enabled").SetValue(true));
			
			move.AddItem(new MenuItem("MovementDelay", "Movement Delay")).SetValue(new Slider(400, 0, 5000));
			Menu.AddToMainMenu();

			Console.WriteLine("Menu Init Success!");

			ObjHQ = new HQ();
			Orbwalker = Menu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
			OrbW = new Orbwalking.Orbwalker(Orbwalker);

			Obj_AI_Base.OnIssueOrder += Obj_AI_Base_OnIssueOrder;
		}

		public static Constants ObjConstants { get; protected set; }
		public static Heroes ObjHeroes { get; protected set; }
		public static Minions ObjMinions { get; protected set; }
		public static Turrets ObjTurrets { get; protected set; }
		public static HQ ObjHQ { get; protected set; }
		public static Orbwalking.Orbwalker OrbW { get; set; }
		public virtual void OnGameLoad(EventArgs args) { }
		public virtual void OnGameUpdate(EventArgs args) { }

		#region Humanizer
        private static void Obj_AI_Base_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args)
		{
			if (sender == null || !sender.IsValid || !sender.IsMe)
			{
				return;
			}

			int mvmtDelay = Randoms.Rand.Next(200, 770);
			if (args.Order == GameObjectOrder.MoveTo)
			{
				if (ObjectManager.Player.HasBuff("SionR"))
					return;
				if (Environment.TickCount - LastMove < mvmtDelay)
				{
					args.Process = false;
					//	LastMove = Environment.TickCount;
					return;
				}

				LastMove = Environment.TickCount;
			}

			if (args.Target == null)
			{
				return;
			}

			if (args.Target.IsEnemy && args.Target is Obj_AI_Hero
			&& sender.UnderTurret(true)
				&& (args.Order == GameObjectOrder.AutoAttack || args.Order == GameObjectOrder.AttackUnit))
            {
                args.Process = false;
            }
        }
        #endregion

        #region Minions

        public void RefreshMinions()
        {
            ObjMinions.UpdateMinions();
            LeadingMinion = Utility.Map.GetMap().Type == Utility.Map.MapType.SummonersRift
                ? ObjMinions.GetLeadMinion(SummonersRift.BottomLane.Bottom_Zone.CenterOfPolygone().To3D())
                : ObjMinions.GetLeadMinion();
            ClosestEnemyMinion = ObjMinions.GetClosestEnemyMinion();
        }

        public static bool InDangerUnderEnemyTurret()
        {
            var nearestTurret = Turrets.EnemyTurrets.FirstOrDefault(t => t.Distance(Player) < 800);
            if (nearestTurret != null)
            {
                return
                    ObjectManager.Get<Obj_AI_Minion>()	
                        .Count(minion => minion.IsAlly && !minion.IsDead && minion.Distance(nearestTurret) < 650) <= 2;
            }
            return false;
        }

        #endregion Minions
    }
}