﻿#region LICENSE

// Copyright 2014-2015 Support
// FiddleSticks.cs is part of Support.
// 
// Support is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Support is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Support. If not, see <http://www.gnu.org/licenses/>.
// 
// Filename: Support/Support/FiddleSticks.cs
// Created:  01/10/2014
// Date:     20/01/2015/11:20
// Author:   h3h3

#endregion

using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;
using ActiveGapcloser = AIM.Util.ActiveGapcloser;

namespace AIM.Plugins
{
    #region

    

    #endregion

    public class FiddleSticks : PluginBase
    {
        public FiddleSticks()
        {
            Q = new Spell(SpellSlot.Q, 575);
            W = new Spell(SpellSlot.W, 575);
            E = new Spell(SpellSlot.E, 750);
            R = new Spell(SpellSlot.R, 800);
        }

        public override void OnUpdate(EventArgs args)
        {
			var target = TargetSelector.GetTarget(900, TargetSelector.DamageType.Magical);

            if (ComboMode)
            {
				
				if (target == null || Player.IsChannelingImportantSpell()) // Check if there is a target
				{
					return;
				}
				 if (R.IsReady()  && Player.CountEnemiesInRange(R.Range) > 1)
				{
					 R.Cast(target.ServerPosition);
				}
                if (Q.IsReady())
                {
                    Q.CastOnUnit(target);
                }

                if (E.IsReady())
                {
                    E.CastOnUnit(target);
					return;
                }
				if (W.IsReady())
				{
					W.CastOnUnit(target);
				}
            }

            if (HarassMode)
            {
                if (E.CastCheck(target, "Harass.E"))
                {
                    E.CastOnUnit(target);
                }
            }
        }

        public override void OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender.IsAlly)
            {
                return;
            }

            if (Q.CastCheck(gapcloser.Sender, "Gapcloser.Q"))
            {
                Q.CastOnUnit(gapcloser.Sender);
            }
        }

        public override void OnPossibleToInterrupt(Obj_AI_Base unit, InterruptableSpell spell)
        {
            if (spell.DangerLevel < InterruptableDangerLevel.High || unit.IsAlly)
            {
                return;
            }

            if (Q.CastCheck(unit, "Interrupt.Q"))
            {
                Q.CastOnUnit(unit);
                return;
            }

            if (E.CastCheck(unit, "Interrupt.E"))
            {
                E.CastOnUnit(unit);
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("Combo.Q", "Use Q", true);
            config.AddBool("Combo.E", "Use E", true);
        }

        public override void HarassMenu(Menu config)
        {
            config.AddBool("Harass.E", "Use E", true);
        }

        public override void InterruptMenu(Menu config)
        {
            config.AddBool("Gapcloser.Q", "Use Q to Interrupt Gapcloser", true);

            config.AddBool("Interrupt.Q", "Use Q to Interrupt Spells", true);
            config.AddBool("Interrupt.E", "Use E to Interrupt Spells", true);
        }
    }
}