using System;
using System.Diagnostics;
using System.Management;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
            CustomEvents.Game.OnGameEnd -= RemoveEventHandler;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnStart += Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }

    }
}