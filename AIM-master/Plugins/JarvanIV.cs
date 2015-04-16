using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using AIM.Util;

namespace AIM.Plugins
{
	public class JarvanIV : PluginBase
	{
		public JarvanIV()
		{
			Q = new Spell(SpellSlot.Q, 770f);
			W = new Spell(SpellSlot.W, 300f);
			E = new Spell(SpellSlot.E, 830f);
			R = new Spell(SpellSlot.R, 650f);
		}

		public override void OnUpdate(EventArgs args)
		{sss
			if (ComboMode)
			{
				Combo(Target);
			}
			Clear();
		}

		private void Combo(Obj_AI_Hero t)
		{
			if (E.IsReady() && t.IsValidTarget(Q.Range) && Q.IsReady())
			{
				//xsalice Code
                var vec = t.ServerPosition - Player.ServerPosition;
				var castBehind = E.GetPrediction(t).CastPosition + Vector3.Normalize(vec) * 100;
				E.Cast(castBehind);
				Utility.DelayAction.Add(200, () => Q.Cast(castBehind));
			}

			if (W.IsReady())
			{
				if (t.IsValidTarget(W.Range))
					W.Cast();
			}

			if (Q.IsReady() && !E.IsReady())
			{
				if (t.IsValidTarget(Q.Range))
					Q.Cast(t);
			}

			if (R.IsReady())
			{
				if (R.IsKillable(t))
				{
					R.Cast(t);
				}

				R.CastIfWillHit(t, 2);
			}
		}

		public override void ComboMenu(Menu config)
		{
			config.AddBool("AutoCombo", "AutoCombo", true);
		}
	}
	 private void Clear()
        {
            var minionObj = MinionManager.GetMinions(
                Q.Range, MinionTypes.All, MinionTeam.NotAlly, MinionOrderTypes.MaxHealth);
            if (minionObj.Count == 0)
            {
                return;
            }
            if ( E.IsReady() &&
                (minionObj.Count > 1 || minionObj.Any(i => i.MaxHealth >= 1200)))
            {
                var pos = E.GetCircularFarmLocation(minionObj.FindAll(i => E.IsInRange(i)));
                if (pos.MinionsHit > 0 && pos.Position.IsValid() && E.Cast(pos.Position))
                {
                    if (Q.IsReady())
                    {
                        Q.Cast(pos.Position);
                    }
                    return;
                }
            }
          
        }
}
