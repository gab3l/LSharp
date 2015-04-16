using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIM.Autoplay.Behaviors.Strategy;
using AIM.Autoplay.Util.Objects;
using AIM.Autoplay.Modes;
using AIM.Autoplay.Util.Data;
using BehaviorSharp;
using BehaviorSharp.Components.Composites;
using BehaviorSharp.Components.Conditionals;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Behaviors
{
	internal class MainBehavior
	{
		internal static Behavior Root = new Behavior(new IndexSelector(
            () =>
    								{
    									var heroes = new Heroes();
    									var minions = new Minions();
    									if (Heroes.Me.IsDead)
    									{
        						return 0;
        					}

        					if (!ObjectManager.Get<Obj_AI_Minion>().Any() && !ObjectManager.Get<Obj_AI_Hero>().Any())
        					{
        						Console.WriteLine("5");
        						return 5;
        					}

        					if (ObjectManager.Get<Obj_AI_Hero>().Any(h => h.IsAlly && !h.IsMe && !h.InFountain()))
        					{
													//	Console.WriteLine("1");
                    return 1;

                }

                if (heroes.AllyHeroes.All(h => h.InFountain()) || Heroes.Me.Level >= 16 || !heroes.EnemyHeroes.Any(h => h.IsVisible) 
					|| (float)(Heroes.Me.ChampionsKilled + Heroes.Me.Assists) / ((Heroes.Me.Deaths == 0) ? 1 : Heroes.Me.Deaths) > 2.5f || !minions.EnemyMinions.Any(m => m.IsVisible))
                {
					Console.WriteLine("2");
                    return 2;
                }
                if (Heroes.Me.HealthPercentage() < Modes.Base.Menu.Item("LowHealth").GetValue<Slider>().Value && Relics.ClosestRelic() != null)
                {
					Console.WriteLine("3");
                    return 3;
                }
				Console.WriteLine("4");
                return 4;
            }, new Sequence(), new Sequences().TeamFight, new Sequences().LanePush, new Sequences().CollectHealthPack, new Sequences().StayWithinExpRange, new Sequences().WalkToLane));
    }
}
