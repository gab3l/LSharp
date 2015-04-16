using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace AIM.Plugins
{
	public class Teemo : PluginBase
	{
		private readonly Random Rand = new Random((42 / 13 * DateTime.Now.Millisecond) + DateTime.Now.Second);
		private Vector2 pos;

		public Teemo()
		{
			Q = new Spell(SpellSlot.Q, 680);
			W = new Spell(SpellSlot.W);
			R = new Spell(SpellSlot.R, 230);
			Q.SetTargetted(0f, 2000f);
			R.SetSkillshot(0.1f, 75f, float.MaxValue, false, SkillshotType.SkillshotCircle);
		}

		public override void OnUpdate(EventArgs args)
		{
		var targetteemo = TargetSelector.GetTarget(900, TargetSelector.DamageType.Magical);
		if (targetteemo==null) return;
            if (ComboMode)
            {
                if (Q.CastCheck(targetteemo, "ComboQ"))
                {
                    Q.Cast(targetteemo);
                }

                if (R.CastCheck(targetteemo, "ComboR"))
                {
                    R.Cast(targetteemo);
                }
                if (R.IsReady())
                {
                    var _randRange = Rand.Next(-100, 100);

                    pos.X = Player.Position.X + _randRange;
                    pos.Y = Player.Position.Y + _randRange;
                    R.Cast(pos.To3D());
                }
                if (Orbwalking.InAutoAttackRange(Target) && Player.HealthPercentage() > 30)
                {
                    if (W.IsReady())
                    {
                        W.Cast();
                    }
                    Player.IssueOrder(GameObjectOrder.AttackUnit, Target);
                }
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("ComboQ", "Use Q", true);
            config.AddBool("ComboW", "Use W", true);
            config.AddBool("ComboR", "Use R", true);
        }
    }
}