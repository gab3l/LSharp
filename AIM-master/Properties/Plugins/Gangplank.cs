using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Plugins
{
    public class Gangplank : PluginBase
    {
        public Gangplank()
        {
            //Spell
            Q = new Spell(SpellSlot.Q, 625);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R, 25000);
        }

        public override void OnUpdate(EventArgs args)
        {
            KS();


            if (ComboMode)
            {
                Combo(Target);
                if (Player.HasBuffOfType(BuffType.Taunt) || Player.HasBuffOfType(BuffType.Stun) ||
                    Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Polymorph) ||
                    Player.HasBuffOfType(BuffType.Blind) || Player.HasBuffOfType(BuffType.Fear) ||
                    Player.HasBuffOfType(BuffType.Silence) || Player.HealthPercentage() < 30)
                {
                    if (W.IsReady() && Player.HealthPercentage() < 70)
                    {
                        W.Cast();
                    }
                }
            }
        }

        private void Combo(Obj_AI_Hero Target)
        {
            if (E.IsReady())
            {
                E.Cast();
            }
            if (Q.IsReady() && Target.IsValidTarget(Q.Range))
            {
                Q.CastOnUnit(Target);
            }

            if (R.IsReady() && Target.IsValidTarget(R.Range))
            {
                R.CastIfWillHit(Target, 2);
            }
			var allminions = MinionManager.GetMinions(ObjectManager.Player.Position, Q.Range, MinionTypes.All, MinionTeam.NotAlly);
			foreach(var minion in allminions)
			{
				Q.CastOnUnit(minion);
				return;
			}
        }

        private void KS()
        {
            foreach (var target in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (!target.IsDead && Q.IsReady() && !target.IsAlly && Player.Distance(target.Position) < Q.Range &&
                    Player.GetSpellDamage(target, SpellSlot.Q) > (target.Health + 20))
                {
                    Q.CastOnUnit(target);
                }
                if (R.IsReady() && !target.IsDead && !target.IsAlly && Player.Distance(target.Position) < R.Range &&
                    Player.GetSpellDamage(target, SpellSlot.R) > (target.Health))
                {
                    R.Cast(target);
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