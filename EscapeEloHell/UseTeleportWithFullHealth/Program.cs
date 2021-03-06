﻿using System;
using LeagueSharp.Common;

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
            LeagueSharp.Game.PrintChat(string.Format("UseTeleportWithFullHealth loaded."));
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}