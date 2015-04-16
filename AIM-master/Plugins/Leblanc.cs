using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using AIM.Util;

namespace AIM.Plugins
{
	public class Leblanc : PluginBase
	{
		private bool firstW = false;
		public Leblanc()
		{
			Q = new Spell(SpellSlot.Q, 720);
			Q.SetTargetted(0.5f, 1500f);

			W = new Spell(SpellSlot.W, 670);
			W.SetSkillshot(0.6f, 220f, 1900f, false, SkillshotType.SkillshotCircle);

			E = new Spell(SpellSlot.E, 900);
			E.SetSkillshot(0.3f, 80f, 1650f, true, SkillshotType.SkillshotLine);

			R = new Spell(SpellSlot.R, 720);
		}

		public override void OnUpdate(EventArgs args)
		{
				            var target = TargetSelector.GetTarget(1400, TargetSelector.DamageType.Magical);


            if (ComboMode)
            {

                if (Q.IsReady() && R.IsReady() && target.IsValidTarget(Q.Range))
                {
                    Q.CastOnUnit(target);
                    Utility.DelayAction.Add(100, () => R.CastOnUnit(target));
                }

                if (W.IsReady() && target.IsValidTarget(W.Range) && !firstW && (Player.HealthPercentage() > 30 || W.IsKillable(target)))
                {
                    W.Cast(target);
                    firstW = true;
                }
                if (Q.IsReady() && target.IsValidTarget(Q.Range))
                {
                    Q.CastOnUnit(target);
                }
                if (R.IsReady() && target.IsValidTarget(Q.Range))
                {
                    R.CastOnUnit(target);
                }
                if (E.IsReady() && target.IsValidTarget(700))
                {
                    E.CastIfHitchanceEquals(Target, HitChance.High);
                }
                if (W.IsReady() && firstW && Player.HealthPercentage() < 60)
                {
                    W.Cast();
                    firstW = false;
                }

                if (W.Instance.State == SpellState.Cooldown)
                {
                    firstW = false;
                }
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
