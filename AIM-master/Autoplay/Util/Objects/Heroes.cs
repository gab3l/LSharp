using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util.Objects
{
    public class Heroes
    {
        public static Obj_AI_Hero Me = ObjectHandler.Player;
        public List<Obj_AI_Hero> AllHeroes;
        public List<Obj_AI_Hero> AllyHeroes;
        public List<Obj_AI_Hero> EnemyHeroes;

        public Heroes()
        {
            CreateHeroesList();
        }

        public void CreateHeroesList()
        {
            AllHeroes = ObjectHandler.Get<Obj_AI_Hero>().ToList();
            AllyHeroes = AllHeroes.FindAll(hero => hero.IsAlly);
            EnemyHeroes = AllHeroes.FindAll(hero => hero.IsEnemy);
        }

        public void SortHeroesListByDistance()
        {
            AllHeroes = AllHeroes.OrderBy(hero => hero.Distance(Me, true)).ToList();
            AllyHeroes = AllyHeroes.OrderBy(hero => hero.Distance(Me, true)).ToList();
            EnemyHeroes = EnemyHeroes.OrderBy(hero => hero.Distance(Me, true)).ToList();
        }

        public void RemoveFromHeroList(Obj_AI_Hero hero)
        {
            AllHeroes.Remove(hero);
            AllyHeroes.Remove(hero);
            EnemyHeroes.Remove(hero);
        }

        public int EnemiesInRange(int range)
        {
            return EnemyHeroes.Count(h => h.Distance(Me) < range);
        }

        public int EnemiesInRange(Obj_AI_Base obj, int range)
        {
            return EnemyHeroes.Count(h => h.Distance(Me) < range);
        }

        public int AlliesInRange(int range)
        {
            return AllyHeroes.Count(h => h.Distance(Me) < range);
        }

        public int AlliesInRange(Obj_AI_Base obj, int range)
        {
            return AllyHeroes.Count(h => h.Distance(Me) < range);
        }
    }
}