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
        {

            if (ComboMode)
            {
                Combo(Target);
            }


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
                    Q.Cast(t, UsePackets, true);
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
}
