using System;
using System.Reflection;
using LeagueSharp.Common;
using System.Diagnostics;

namespace UseTeleportWithFullHealth
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();
            LeagueSharp.Game.PrintChat(string.Format("UseTeleportWithFullHealth loaded.", Assembly.GetExecutingAssembly().GetName().Version));
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}