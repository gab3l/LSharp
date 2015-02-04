using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;

namespace Surrender
{
    class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnGameStart -= Game.Game_OnGameStart;
            LeagueSharp.Game.OnGameUpdate -= Game.OnGameUpdate;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnGameStart += Game.Game_OnGameStart;
            LeagueSharp.Game.OnGameUpdate += Game.OnGameUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }

    }
}
