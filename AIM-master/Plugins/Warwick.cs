using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Plugins
{
	public class Warwick : PluginBase
	{
		public Warwick()
		{
			Q = new Spell(SpellSlot.Q, 400);
			W = new Spell(SpellSlot.W, 1000);
			E = new Spell(SpellSlot.E, 1500);
			R = new Spell(SpellSlot.R, 700);
		}

		public override void OnUpdate(EventArgs args)
		{
		//	if (ComboMode)
		//	{
					 if (Target.HasBuffOfType(BuffType.Invulnerability))
            {
                return;
            }

                if (Q.CastCheck(Target, "ComboQ"))
                {
                    Q.Cast(Target);
                }
                if (R.CastCheck(Target, "ComboR") && R.IsKillable(Target))
                {
                    R.Cast(Target);
                }
                if (Player.HealthPercentage() > 20 && Player.Distance(Target) < 1000)
                {
                    if (W.IsReady())
                    {
                        W.Cast();
                    }
                }
				var allminions = MinionManager.GetMinions(ObjectManager.Player.Position, Q.Range, MinionTypes.All, MinionTeam.NotAlly);
				foreach(var minion in allminions)
                {
                    if (minion.Health < Player.GetSpellDamage(minion, SpellSlot.Q)) Q.CastOnUnit(minion);
                    return;
                }
          //  }
        }

        public override void OnPossibleToInterrupt(Obj_AI_Base unit, InterruptableSpell spell)
        {
            if (spell.DangerLevel < InterruptableDangerLevel.High || unit.IsAlly)
            {
                return;
            }
            if (R.CastCheck(unit, "Interrupt.R"))
            {
                R.Cast(unit);
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("ComboQ", "Use Q", true);
            config.AddBool("ComboW", "Use W", true);
            config.AddBool("ComboE", "Use E", true);
            config.AddBool("ComboR", "Use R", true);
        }

        public override void InterruptMenu(Menu config)
        {
            config.AddBool("Interrupt.R", "Use R to Interrupt Spells", true);
        }
    }
}