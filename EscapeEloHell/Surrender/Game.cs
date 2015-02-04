using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Surrender;

namespace Surrender
{
    public class Game
    {
        public static void Game_OnGameStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("Surrender loaded.");
        }
        private static DateTime time;

        internal static void OnGameUpdate(EventArgs args)
        {

            if (LeagueSharp.Game.ClockTime > 1470 && DateTime.Now > time.AddMinutes(3))
            {
                LeagueSharp.Game.Say(new Random(2).Next() == 1 ? @"/ff" : @"/surrender");
                time = DateTime.Now;
            }
        }
    }
}