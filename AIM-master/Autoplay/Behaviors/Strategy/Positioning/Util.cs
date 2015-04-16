using System;
using System.Collections.Generic;
using System.Linq;
using AIM.Autoplay.Util.Data;
using ClipperLib;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;
using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;

namespace AIM.Autoplay.Behaviors.Strategy.Positioning
{
    public class Util
    {
        /// <summary>
        /// Returns a list of points in the Ally Zone
        /// </summary>
        internal static Paths AllyZone()
        {
            var teamPolygons = new List<Geometry.Polygon>();
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>().Where(h => h.IsAlly && !h.IsDead && !h.IsMe && !(h.InFountain() || h.InShop())))
            {
                teamPolygons.Add(GetChampionRangeCircle(hero).ToPolygon());
            }
            var teamPaths = Geometry.ClipPolygons(teamPolygons);
            var newTeamPaths = teamPaths;
            foreach (var pathList in teamPaths)
            {
                Path wall = new Path();
                foreach (var path in pathList)
                {
                    if (Utility.IsWall(new Vector2(path.X, path.Y)))
                    {
                        wall.Add(path);
                    }
                }
                newTeamPaths.Remove(wall);
            }
            return newTeamPaths;
        }

        /// <summary>
        /// Returns a list of points in the Enemy Zone
        /// </summary>
        internal static Paths EnemyZone()
        {
            var teamPolygons = new List<Geometry.Polygon>();
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>().Where(h => !h.IsAlly && !h.IsDead && h.IsVisible))
            {
                teamPolygons.Add(GetChampionRangeCircle(hero).ToPolygon());
            }
            var teamPaths = Geometry.ClipPolygons(teamPolygons);
            var newTeamPaths = teamPaths;
            foreach (var pathList in teamPaths)
            {
                Path wall = new Path();
                foreach (var path in pathList)
                {
                    if (Utility.IsWall(new Vector2(path.X, path.Y)))
                    {
                        wall.Add(path);
                    }
                }
                newTeamPaths.Remove(wall);
            }
            return newTeamPaths;
        }
        
        /// <summary>
        /// Returns a circle with center at hero position and radius of the highest impact range a hero has.
        /// </summary>
        /// <param name="hero">The target hero.</param>
        internal static Geometry.Circle GetChampionRangeCircle(Obj_AI_Hero hero)
        {
            var heroSpells = new List<SpellData>
            {
                SpellData.GetSpellData(hero.GetSpell(SpellSlot.Q).Name),
                SpellData.GetSpellData(hero.GetSpell(SpellSlot.W).Name),
                SpellData.GetSpellData(hero.GetSpell(SpellSlot.E).Name),
            };
            var spellsOrderedByRange = heroSpells.OrderBy(s => s.CastRange);
            if (spellsOrderedByRange.FirstOrDefault() != null)
            {
                var highestSpellRange = spellsOrderedByRange.FirstOrDefault().CastRange;
                return new Geometry.Circle(hero.ServerPosition.To2D(), highestSpellRange> hero.AttackRange ? highestSpellRange : hero.AttackRange);
            }
            return new Geometry.Circle(hero.ServerPosition.To2D(), hero.AttackRange);
        }

        /// <summary>
        /// Returns a polygon that contains each position of a team champion
        /// </summary>
        /// <param name="allyTeam">returns the polygon for ally team if true, enemy if false</param>
        /// <returns></returns>
        internal static Geometry.Polygon GetTeamPolygon(bool allyTeam = true)
        {
            var poly = new Geometry.Polygon();
            foreach (var v2 in allyTeam ? new Util().GetAllyPosList() : new Util().GetEnemyPosList())
            {
                poly.Add(v2);
            }
            poly.ToClipperPath();
            return poly;
        }

        /// <summary>
        /// Returns a clipper path list of all ally champions
        /// </summary>
        public Paths GetAllyPaths()
        {
            var allyPaths = new Paths(GetAllyPosList().Count);
            for (int i = 0; i < GetAllyPosList().Count; i++)
            {
                allyPaths[i].Add(new IntPoint(GetAllyPosList().ToArray()[i].X + Randoms.Rand.Next(-150, 150), GetAllyPosList().ToArray()[i].Y + Randoms.Rand.Next(-150, 150)));
            }
            return allyPaths;
        }

        /// <summary>
        /// returns a clipper paths list of all enemy champion positions
        /// </summary>
        public Paths GetEnemyPaths()
        {
            var enemyPaths = new Paths(GetEnemyPosList().Count);
            for (int i = 0; i < GetEnemyPosList().Count; i++)
            {
                enemyPaths[i].Add(new IntPoint(GetEnemyPosList().ToArray()[i].X, GetEnemyPosList().ToArray()[i].Y));
            }
            return enemyPaths;
        }

        /// <summary>
        /// returns a list of all ally positions
        /// </summary>
        public List<Vector2> GetAllyPosList()
        {
            var allies = ObjectManager.Get<Obj_AI_Hero>().Where(h => h.IsAlly && !h.IsMe && !h.IsDead && !h.InFountain()).ToList();
            return allies.Select(ally => ally.ServerPosition.To2D()).ToList();
        }

        /// <summary>
        /// returns a list of all enemy positions
        /// </summary>
        public List<Vector2> GetEnemyPosList()
        {
            var enemies = ObjectManager.Get<Obj_AI_Hero>().Where(h => h.IsEnemy && !h.IsDead && h.IsVisible).ToList();
            return enemies.Select(enemy => enemy.ServerPosition.To2D()).ToList();
        }

        /// <summary>
        /// draws a line (c) kortatu
        /// </summary>
        /// <param name="start">line start pos</param>
        /// <param name="end">line end pos</param>
        /// <param name="width">line width</param>
        /// <param name="color">line color</param>
        public static void DrawLineInWorld(Vector3 start, Vector3 end, int width, Color color)
        {
            var from = Drawing.WorldToScreen(start);
            var to = Drawing.WorldToScreen(end);
            Drawing.DrawLine(from[0], from[1], to[0], to[1], width, color);
            //Drawing.DrawLine(from.X, from.Y, to.X, to.Y, width, color);
        }
    }
}
