#region LICENSE

// Copyright 2014 - 2014 AIM
// Kayle.cs is part of AIM.
// AIM is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// AIM is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with AIM. If not, see <http://www.gnu.org/licenses/>.

#endregion

#region

using System;
using LeagueSharp;
using LeagueSharp.Common;
using AIM.Util;
using ActiveGapcloser = AIM.Util.ActiveGapcloser;

#endregion

namespace AIM.Plugins
{
	public class Kayle : PluginBase
	{
		public Kayle()
		{
			Q = new Spell(SpellSlot.Q, 650);
			W = new Spell(SpellSlot.W, 900);
			E = new Spell(SpellSlot.E, 525);
			R = new Spell(SpellSlot.R, 900);
		}

		public override void OnUpdate(EventArgs args)
		{
			var target1 = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
			if (target1==null) return;
			if (ComboMode)
			{
				
					Q.Cast(target1);
				

				if ( Player.HealthPercentage() > 30 && Player.CountEnemiesInRange(3000) > 0)
				{
					E.Cast();
					Player.IssueOrder(GameObjectOrder.AttackUnit, target1);
				}

				if (Player.HealthPercentage()<50)
				{
					W.Cast(Player);
				}
				if (R.IsReady() && Player.CountEnemiesInRange(3000) > 0 && Player.HealthPercentage() <20)
                    {
                        R.Cast(Player);
                    }
            }
        }

        public override void OnBeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
        }

        public override void OnAfterAttack(AttackableUnit unit, AttackableUnit target)
        {
        }


        public override void ComboMenu(Menu config)
        {
            config.AddBool("ComboQ", "Use Q", true);
            config.AddBool("ComboW", "Use W", true);
            config.AddBool("ComboE", "Use E", true);
            config.AddBool("ComboR", "Use R", true);
            config.AddSlider("ComboCountR", "Targets in range to Ult", 2, 0, 5);
            config.AddSlider("ComboHealthR", "Health to Ult", 20, 1, 100);
        }



        public override void MiscMenu(Menu config)
        {

        }
    }
}