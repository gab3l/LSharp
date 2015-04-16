using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace AIM.Autoplay.Behaviors.Strategy.Positioning
{

    /// <summary>
    /// Stuff used to determine the teamfight behavior ^^
    /// </summary>
    internal class Teamfight
    {
        private static Util Utils = new Util();


        /// <summary>
        /// Returns a random position in the team zone or the position of the ally champion farthest from base
        /// </summary>
        internal static Vector2 GetPos()
        {
			//Game.PrintChat("TeamFightGetPos");
            if (Game.MapId == GameMapId.HowlingAbyss)
            {
                    var allyZonePathList = Util.AllyZone().OrderBy(p => Randoms.Rand.Next()).FirstOrDefault();
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
                    var pointClosestToEnemyHQ =
                        allyZoneVectorList.OrderBy(p => p.Distance(HQ.EnemyHQ.Position)).FirstOrDefault();
					var zz = new Constants();
					int minNum = 250;	
					int maxNum = 600;
					var closestEnemy = Utils.GetEnemyPosList().OrderByDescending(b => b.Distance(HQ.AllyHQ.Position)).FirstOrDefault();
					if (Heroes.Me.Team == GameObjectTeam.Order)
					{
						//Game.PrintChat("Team Order");
						pointClosestToEnemyHQ = Utils.GetAllyPosList().OrderByDescending(b => b.Distance(HQ.AllyHQ.Position)).FirstOrDefault();
						var randy = Randoms.Rand.Next(minNum, maxNum);
						pointClosestToEnemyHQ.X = pointClosestToEnemyHQ.X - Randoms.Rand.Next(minNum, maxNum);	
						pointClosestToEnemyHQ.Y = pointClosestToEnemyHQ.Y - Randoms.Rand.Next(minNum, maxNum);
                    }
					if (Heroes.Me.Team == GameObjectTeam.Chaos)
					{
					//	Game.PrintChat("Team Chaos");
						pointClosestToEnemyHQ = Utils.GetAllyPosList().OrderByDescending(q => q.Distance(HQ.AllyHQ.Position)).FirstOrDefault();
						pointClosestToEnemyHQ.X = pointClosestToEnemyHQ.X + Randoms.Rand.Next(minNum, maxNum);	
						pointClosestToEnemyHQ.Y = pointClosestToEnemyHQ.Y + Randoms.Rand.Next(minNum, maxNum);	
						
					}
					return pointClosestToEnemyHQ;
              
            }
			
            
            //for SR :s
            var minion =
                ObjectManager.Get<Obj_AI_Minion>().OrderBy(m => m.Distance(HQ.EnemyHQ)).FirstOrDefault().Position.To2D();
            var turret = ObjectManager.Get<Obj_AI_Turret>().OrderByDescending(m => m.Distance(HQ.AllyHQ)).FirstOrDefault().Position.To2D();
            return (minion != null && minion.IsValid()) ? minion : turret;
        }
    }
}
