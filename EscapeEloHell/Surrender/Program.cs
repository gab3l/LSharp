using System;
using LeagueSharp.Common;

namespace Surrender
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            LeagueSharp.Game.OnGameNotifyEvent -= Game.Game_OnGameNotifyEvent;
            LeagueSharp.Game.OnUpdate -= Game.Game_OnUpdate;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            RegisterEvents();
            LeagueSharp.Game.PrintChat("Surrender loaded.");
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnStart += Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate += Game.Game_OnUpdate;
            LeagueSharp.Game.OnGameNotifyEvent += Game.Game_OnGameNotifyEvent;

            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}