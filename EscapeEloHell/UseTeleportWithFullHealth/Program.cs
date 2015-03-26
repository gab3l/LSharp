using System;
using LeagueSharp.Common;

namespace UseTeleportWithFullHealth
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.OnStart;
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();

            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnStart += Game.OnStart;
            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}