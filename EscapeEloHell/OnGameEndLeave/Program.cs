﻿using System;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            CustomEvents.Game.OnGameEnd -= Game.Game_OnGameEnd;
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
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
            CustomEvents.Game.OnGameEnd += Game.Game_OnGameEnd;
        }
    }
}