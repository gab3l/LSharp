using System;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnNotify -= Game.OnNotify;
            CustomEvents.Game.OnGameEnd -= RemoveEventHandler;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            RegisterEvents();
            LeagueSharp.Game.PrintChat(string.Format("OnGameEndLeave loaded."));
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnNotify += Game.OnNotify;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}