using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIM.Autoplay.Util;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Orbwalking = AIM.Autoplay.Util.Orbwalking;

namespace AIM.Autoplay.Behaviors.Strategy
{
	internal class Actions
	{
		/// <summary>
		/// This Behavior Action will make the bot go all in without any consideration just to push the lane.
		/// </summary>
        internal BehaviorAction PushLane = new BehaviorAction(
            () =>
												{
													try
													{
														Console.WriteLine("pushlane");
														var objConstants = new Constants();
														var isInDanger = ObjectManager.Player.UnderTurret(true) && Modes.Base.InDangerUnderEnemyTurret();
														if (Heroes.Me.UnderTurret(true))
														{
															var turret = Turrets.EnemyTurrets.OrderBy(t => t.Distance(Heroes.Me)).FirstOrDefault();
															Modes.Base.OrbW.ForceTarget(turret);
														}

														if (isInDanger)
														{
															var orbwalkingPos = new Vector2
															{
																X = ObjectManager.Player.Position.X + (objConstants.DefensiveAdditioner),
                            Y = ObjectManager.Player.Position.Y + (objConstants.DefensiveAdditioner)
															};
															ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, orbwalkingPos.To3D());
															Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.None;
															Modes.Base.OrbW.SetAttack(false);
															Modes.Base.OrbW.SetMovement(false);
															return BehaviorState.Success;
														}

														if (Modes.Base.LeadingMinion != null)
														{
															var orbwalkingPos = new Vector2
															{
																X =
                                Modes.Base.LeadingMinion.Position.X + (objConstants.DefensiveAdditioner / 8f) +
                                Randoms.Rand.Next(-100, 100),
                            Y =
                                Modes.Base.LeadingMinion.Position.Y + (objConstants.DefensiveAdditioner / 8f) +
                                Randoms.Rand.Next(-100, 100)
															};
															Utility.DelayAction.Add(new Random(Environment.TickCount).Next(500, 1500), () => Modes.Base.OrbW.SetOrbwalkingPoint(orbwalkingPos.To3D()));
															Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.LaneClear;
															Modes.Base.OrbW.SetAttack(true);
															Modes.Base.OrbW.SetMovement(true);
															Orbwalking.SetMovementDelay(Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);
															return BehaviorState.Success;
														}

														return BehaviorState.Failure;
													}
													catch (NullReferenceException e)
													{
														Console.WriteLine(e);
													}

													return BehaviorState.Failure;
												});

		/// <summary>
		/// This Behavior Action will make the bot stay in the safe exp zone
		/// </summary>
        internal BehaviorAction StayWithinExpRange = new BehaviorAction(
            () =>
												{
													Console.WriteLine("stay within range");
													var objConstants = new Constants();
													var isInDanger = ObjectManager.Player.UnderTurret(true) && Modes.Base.InDangerUnderEnemyTurret();
													if (Heroes.Me.UnderTurret(true))
													{
														var turret = Turrets.EnemyTurrets.OrderBy(t => t.Distance(Heroes.Me)).FirstOrDefault();
														Modes.Base.OrbW.ForceTarget(turret);
													}

													if (isInDanger)
													{
														var orbwalkingPos = new Vector2
														{
															X = ObjectManager.Player.ServerPosition.X + objConstants.DefensiveAdditioner,
                        Y = ObjectManager.Player.ServerPosition.Y + objConstants.DefensiveAdditioner
														};
														ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, orbwalkingPos.To3D());
														Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.None;
														Modes.Base.OrbW.SetAttack(true);
														Modes.Base.OrbW.SetMovement(false);
														return BehaviorState.Success;
													}

													if (Modes.Base.ClosestEnemyMinion != null)
													{
														var orbwalkingPos = new Vector2
														{
															X =
                            Modes.Base.ClosestEnemyMinion.Position.X + objConstants.DefensiveAdditioner +
                            Randoms.Rand.Next(-150, 150),
                        Y =
                            Modes.Base.ClosestEnemyMinion.Position.Y + objConstants.DefensiveAdditioner +
                            Randoms.Rand.Next(-150, 150)
														};
														Utility.DelayAction.Add(new Random(Environment.TickCount).Next(500, 1500), () => Modes.Base.OrbW.SetOrbwalkingPoint(orbwalkingPos.To3D()));
														Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.Mixed;
														Modes.Base.OrbW.SetAttack(true);
														Modes.Base.OrbW.SetMovement(true);
														Orbwalking.SetMovementDelay(Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);
														return BehaviorState.Success;
													}

													return BehaviorState.Success;
												});

		/// <summary>
		/// This BehaviorAction will make the bot go all in for a kill, l0l bronze bot
		/// </summary>
        internal BehaviorAction KillEnemy = new BehaviorAction(
            () =>
												{
													var spells = new List<SpellSlot> { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
													var heroes = new Heroes();
													var killableEnemy = heroes.EnemyHeroes.FirstOrDefault(h => h.Health < Heroes.Me.GetComboDamage(h, spells) + Heroes.Me.GetAutoAttackDamage(Heroes.Me));
													if (killableEnemy == null || killableEnemy.IsDead || !killableEnemy.IsValidTarget() ||
                    killableEnemy.IsInvulnerable || killableEnemy.UnderTurret(true) || Heroes.Me.IsDead)
													{
														return BehaviorState.Success;
													}

													Modes.Base.OrbW.ForceTarget(killableEnemy);
													Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.Combo;
													var orbwalkingPos = new Vector3
													{
														X =
                        killableEnemy.Position.X +
                        (Heroes.Me.AttackRange - 0.2f * Heroes.Me.AttackRange) *
                        Modes.Base.ObjConstants.DefensiveMultiplier,
                    Y =
                        killableEnemy.Position.Y +
                        (Heroes.Me.AttackRange - 0.2f * Heroes.Me.AttackRange) *
                        Modes.Base.ObjConstants.DefensiveMultiplier
													};
													Modes.Base.OrbW.SetOrbwalkingPoint(orbwalkingPos);
													Orbwalking.SetMovementDelay(Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);
													return BehaviorState.Success;
												});

		/// <summary>
		/// This behavior action makes the bot collect a health relic
		/// </summary>
        internal BehaviorAction CollectHealthRelic = new BehaviorAction(
            () =>
												{
													Console.WriteLine("healthrelic");

													if (Heroes.Me.Position != Relics.ClosestRelic().Position)
													{
														Heroes.Me.IssueOrder(GameObjectOrder.MoveTo, Relics.ClosestRelic().Position);
														Modes.Base.OrbW.SetAttack(false);
														Modes.Base.OrbW.SetMovement(false);
														return BehaviorState.Running;
													}

													Modes.Base.OrbW.SetAttack(true);
													Modes.Base.OrbW.SetMovement(true);
													Orbwalking.SetMovementDelay(Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);
													return BehaviorState.Success;
												});

		/// <summary>
		/// This Behavior action makes the bot walk to the farthest turret and orbwalk there spurdo
		/// </summary>
        internal BehaviorAction ProtectFarthestTurret = new BehaviorAction(
            () =>
												{
													Console.WriteLine("ProtectFarthestTurret");

													var farthestTurret = Turrets.AllyTurrets.OrderByDescending(t => t.Distance(HQ.AllyHQ)).FirstOrDefault();
													var objConstants = new Constants();
													var orbwalkingPos = new Vector2();
													if (farthestTurret != null)
													{
														orbwalkingPos.X = farthestTurret.Position.X;
														orbwalkingPos.Y = farthestTurret.Position.Y;
													}
													else
													{
														orbwalkingPos.X = HQ.AllyHQ.Position.X;
														orbwalkingPos.Y = HQ.AllyHQ.Position.Y;
													}

													if (Heroes.Me.Team == GameObjectTeam.Order)
													{
														orbwalkingPos.X = orbwalkingPos.X - Randoms.Rand.Next(-100, 200);
														orbwalkingPos.Y = orbwalkingPos.Y - Randoms.Rand.Next(-100, 200);
													}

													if (Heroes.Me.Team == GameObjectTeam.Chaos)
													{
														//	Console.WriteLine("Team Chaos");
														orbwalkingPos.X = orbwalkingPos.X + Randoms.Rand.Next(-100, 200);
														orbwalkingPos.Y = orbwalkingPos.Y + Randoms.Rand.Next(-100, 200);
													}

													Modes.Base.OrbW.SetOrbwalkingPoint(orbwalkingPos.To3D());
													int mvmtDelay = Randoms.Rand.Next(100, Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);
													Orbwalking.SetMovementDelay(mvmtDelay);
													return BehaviorState.Success;
												});

		/// <summary>
		/// This is the Teamfight Behavior, pretty self explainatory
		/// </summary>
        internal BehaviorAction Teamfight = new BehaviorAction(
            () =>
												{
													//			if (Environment.TickCount - Modes.Base.LastMove < 300) return BehaviorState.Success;

													string[] Ap =
													{

            "ahri", "akali", "anivia", "annie", "brand", "cassiopeia", "diana",
            "fiddlesticks", "fizz", "gragas", "heimerdinger", "karthus", "kassadin", "katarina", "kayle", "kennen",
            "leblanc", "lissandra", "lux", "malzahar", "mordekaiser", "morgana", "nidalee", "orianna", "ryze",
            "swain", "syndra", "teemo", "twistedfate", "veigar", "viktor", "vladimir", "xerath", "ziggs", "zyra",
            "velkoz", "shaco"
													};

													string[] Sup =
													{

            "blitzcrank", "janna", "karma", "leona", "lulu", "nami", "sona",
            "soraka", "thresh", "zilean"
													};

													string[] Tank =
													{

            "amumu", "chogath", "drmundo", "galio", "hecarim", "malphite",
            "maokai", "nasus", "rammus", "sejuani", "shen", "singed", "skarner", "volibear", "warwick", "yorick", "zac",
            "nunu", "taric", "alistar", "garen", "nautilus", "braum"
													};

													string[] Ad =
													{

            "ashe", "caitlyn", "corki", "draven", "ezreal", "graves", "kogmaw",
            "missfortune", "quinn", "sivir", "talon", "tristana", "twitch", "urgot", "varus", "vayne", "zed", "jinx",
            "yasuo", "lucian"
													};

													string[] Bruiser =
													{

            "darius", "elise", "evelynn", "fiora", "gangplank", "gnar", "jayce",
            "pantheon", "irelia", "jarvaniv", "jax", "khazix", "leesin", "nocturne", "olaf", "poppy", "renekton",
            "rengar", "riven", "shyvana", "trundle", "tryndamere", "udyr", "vi", "monkeyking", "xinzhao", "aatrox",
            "rumble", "masteryi", "sion"
													};

													var orbwalkingPos = Positioning.Teamfight.GetPos();
													Positioning.Util Utils = new Positioning.Util();
													//Console.WriteLine(Relics.ClosestRelic().Position.X);

													var allyZonePathList = Positioning.Util.AllyZone().OrderBy(p => Randoms.Rand.Next()).FirstOrDefault();
													var allyZoneVectorList = new List<Vector2>();

													//create vectors from points and remove walls
                    foreach (var point in allyZonePathList)
													{
														var v2 = new Vector2(point.X, point.Y);
														if (!v2.IsWall())
														{
															allyZoneVectorList.Add(v2);
														}
													}

													var closestPointEnemy = HQ.EnemyHQ.Position;
													if (Heroes.Me.Team == GameObjectTeam.Chaos)
													{
														closestPointEnemy.X = 1000;
														closestPointEnemy.Y = 1000;
													}
													else
													{
														closestPointEnemy.X = 10000;
														closestPointEnemy.Y = 10000;
													}

													var pointClosestToEnemyHQ =
													allyZoneVectorList.OrderBy(p => p.Distance(closestPointEnemy)).FirstOrDefault();
													var zz = new Constants();
													//Console.WriteLine(Modes.Base.Menu.Item("MovementDelay").GetValue<Slider>().Value);

													var minNum = Modes.Base.Menu.Item("MinDist").GetValue<Slider>().Value;
													var maxNum = Modes.Base.Menu.Item("MaxDist").GetValue<Slider>().Value;
													//Console.WriteLine(minNum);
													//Console.WriteLine(maxNum);
													var numEnemyPlayers = ObjectManager.Player.CountEnemiesInRange(3000);
													var numAllyPlayers = ObjectManager.Player.CountAlliesInRange(3000);
													if (numEnemyPlayers<3)
													{
														numEnemyPlayers++;
													}

													var difference = numAllyPlayers - numEnemyPlayers;
													var kda = (Heroes.Me.ChampionsKilled + (Heroes.Me.Assists/2)) / ((Heroes.Me.Deaths == 0) ? 1 : Heroes.Me.Deaths);
													//	Console.WriteLine(kda);
													if (difference<0)
													{
														minNum = 300 + kda * -40;;
													}
													else
													{
														minNum = difference * -50 + 290 + kda * -40;
													}

													if (Bruiser.Contains(ObjectManager.Player.BaseSkinName.ToLowerInvariant()) || Tank.Contains(ObjectManager.Player.BaseSkinName.ToLowerInvariant()))
													{
														maxNum = 379;
														if (difference<0)
														{
															minNum = 150 + kda * -40;;
														}
														else
														{
															minNum = difference * -50 + 150 + kda * -40;
														}
													}
													else
													{
														maxNum = 600;
													}

													if (Heroes.Me.Team == GameObjectTeam.Order)
													{
														//	Console.WriteLine("order teamfight");
														//1520, 1643
														//3805, 3782
														//5033x, 4913 y,
														//7848, 7747y,
														//8978, 8988	
														if (ObjectManager.Player.Position.X < 5033 && ObjectManager.Player.Position.Y < 4913) //inside own base area, push somewhat hard
														{
															//	Console.WriteLine("order inside own base area");

															minNum =-400;
															maxNum = 100;
														}
														else if(ObjectManager.Player.Position.X > 7417 && ObjectManager.Player.Position.Y > 7417
															&& ObjectManager.Player.Position.X <= 9100 && ObjectManager.Player.Position.Y	 <= 9100) // in their base area, push hard
														{
															//		Console.WriteLine("order inside their base area");
															minNum =-200;
															maxNum = 100;
														}
														else if(ObjectManager.Player.Position.X > 9100 && ObjectManager.Player.Position.Y > 9100) // in their base area, push hard
														{
															//Console.WriteLine("order inside their base area danger zone");
															minNum = 0;
															maxNum = 300;
														}
														else
														{
															//Console.WriteLine("inside mid");
														}
														if (!Modes.Base.Menu.Item("autobounds").GetValue<bool>())
														{
																Console.WriteLine("manually set bounds");
															 minNum = Modes.Base.Menu.Item("MinDist").GetValue<Slider>().Value;
															maxNum = Modes.Base.Menu.Item("MaxDist").GetValue<Slider>().Value;
														
														}
													}

													var closestPointAlly = HQ.AllyHQ.Position;
													if (Heroes.Me.Team == GameObjectTeam.Chaos)
													{
														closestPointAlly.X = 10000;
														closestPointAlly.Y = 10000;
													}
													else
													{
														closestPointAlly.X = 1000;
														closestPointAlly.Y = 1000;
													}

													var closestEnemy = Utils.GetEnemyPosList().OrderByDescending(b => b.Distance(closestPointAlly)).FirstOrDefault();
													pointClosestToEnemyHQ = Utils.GetAllyPosList().OrderByDescending(a => a.Distance(closestPointAlly)).FirstOrDefault();
													if (pointClosestToEnemyHQ.X == 0 &&closestEnemy.X == 0)
													{
														Console.WriteLine("going to middle");
														pointClosestToEnemyHQ.X = 5000;
														pointClosestToEnemyHQ.Y = 5000;
													}
													else if (pointClosestToEnemyHQ.X == 0)
													{
														Console.WriteLine("going to enemy");
														pointClosestToEnemyHQ = closestEnemy;
													}

													//1520, 1643 turret locations
													//3805, 3782
													//5033x, 4913 y,
													//7848, 7747y,
													//8978, 8988	
													if (Heroes.Me.Team == GameObjectTeam.Order)
													{
														var randx = Randoms.Rand.Next(minNum, maxNum);
														var randy = Randoms.Rand.Next(minNum, maxNum);
														pointClosestToEnemyHQ.X = pointClosestToEnemyHQ.X - randx;
														pointClosestToEnemyHQ.Y = pointClosestToEnemyHQ.Y - randy;
													}

													//1520, 1643
													//3805, 3782
													//5033x, 4913 y,
													//7848, 7747y,
													//8978, 8988
														if (Heroes.Me.Team == GameObjectTeam.Chaos)
													{
														//	Console.WriteLine("chaos teamfight");
														if (ObjectManager.Player.Position.X >= 1500 && ObjectManager.Player.Position.X < 3245
														&& ObjectManager.Player.Position.Y >= 1500 && ObjectManager.Player.Position.Y < 3245)//in opponent base
														{
															//	Console.WriteLine("chaos inside their base area b/t 1st and 2nd turret, play medium aggressive");
															minNum =-300;
															maxNum = 100;
														}
														else if(ObjectManager.Player.Position.X > 7557 && ObjectManager.Player.Position.Y > 7557	) //in our base
														{
															//	Console.WriteLine("chaos inside our base area, play super aggressive");
															minNum =-400;
															maxNum = 100;
														}
														else if(ObjectManager.Player.Position.X < 1500) //close to their nexus
														{
															//		Console.WriteLine("chaos inside their base area danger zone,play passive");
															minNum =-0;
															maxNum = 300;
														}
														else
														{
															//Console.WriteLine("inside mid");
														}

														if (!Modes.Base.Menu.Item("autobounds").GetValue<bool>())
														{
																Console.WriteLine("manually set bounds");
															 minNum = Modes.Base.Menu.Item("MinDist").GetValue<Slider>().Value;
															maxNum = Modes.Base.Menu.Item("MaxDist").GetValue<Slider>().Value;
														
														}

															
													

														var randx = Randoms.Rand.Next(minNum, maxNum);
														var randy = Randoms.Rand.Next(minNum, maxNum);
														pointClosestToEnemyHQ.X = pointClosestToEnemyHQ.X + randx;
														pointClosestToEnemyHQ.Y = pointClosestToEnemyHQ.Y + randy;
													}

													orbwalkingPos = pointClosestToEnemyHQ;
													//Console.WriteLine(pointClosestToEnemyHQ.X);
													//Console.WriteLine(pointClosestToEnemyHQ.Y);

													Modes.Base.OrbW.SetOrbwalkingPoint(orbwalkingPos.To3D());
													Modes.Base.OrbW.SetAttack(true);
													Modes.Base.OrbW.ActiveMode = Orbwalking.OrbwalkingMode.LaneClear;
													double delay = Math.Pow((double)maxNum*maxNum+minNum*minNum, .6);
													int mvmtDelay = Randoms.Rand.Next(250, (int)delay);
													Modes.Base.mvmntDelay=mvmtDelay;
													//		Console.WriteLine(mvmtDelay);
														Orbwalking.SetMovementDelay(mvmtDelay);
														return BehaviorState.Success;
													
												});
	}
}