using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Plugins
{
	public class DrMundo : PluginBase
	{
		public DrMundo()
		{
			Q = new Spell(SpellSlot.Q, 930);
			W = new Spell(SpellSlot.W, 320);
			E = new Spell(SpellSlot.E, 225);
			R = new Spell(SpellSlot.R, 0);

			Q.SetSkillshot(0.50f, 75f, 1500f, true, SkillshotType.SkillshotLine);
		}

		public override void OnUpdate(EventArgs args)
		{
							var target1 = TargetSelector.GetTarget(1000, TargetSelector.DamageType.Magical);
			if (target1==null) return;
            if (ComboMode)
            {
                if (Player.HealthPercentage() < 50 && R.IsReady())
                {
                    R.Cast();
                }
                Combo(target1);
            }

                if (Q.CastCheck(Target, "ComboQ"))
                {
                    Q.CastIfHitchanceEquals(target1, HitChance.High);
                }
            
        }

        //from mundo TheKushStyle
        private void Combo(Obj_AI_Hero target)
        {
            var ActiveW = false;
            if ()
            {
                ActiveW = true;
            }
            else
            {
                ActiveW = false;
            }

            if (Q.CastCheck(target, "ComboQ"))
            {
                Q.CastIfHitchanceEquals(Target, HitChance.High);
            }

            if (target.IsValidTarget() && W.IsReady() && Player.Distance(target) <= 500 && !Player.HasBuff("BurningAgony"))
            {
                W.Cast();
            }
            if (target.IsValidTarget() && Player.Distance(target) > 500 && Player.HasBuff("BurningAgony"))
            {
                W.Cast();
            }

            if (E.IsReady() && Player.Distance(target) <= 700)
            {
                E.Cast();
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("ComboQ", "Use Q", true);
            config.AddBool("ComboW", "Use W", true);
            config.AddBool("ComboE", "Use E", true);
            config.AddBool("ComboR", "Use R", true);
        }
    }
}