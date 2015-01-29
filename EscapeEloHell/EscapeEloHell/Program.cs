using System;
using LeagueSharp.Common;

namespace RelaxedWinner
{
    public class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnGameStart -= Game.Game_OnGameStart;
            LeagueSharp.Game.OnGameUpdate -= Game.GameEnd;
        }

        private static void Main(string[] args)
        {
            Initialize();

            if (!UserInterface.IsEnabled)
            {
                LeagueSharp.Game.PrintChat("Relaxed Winner disabled!");
                return;
            }

            LeagueSharp.Game.OnGameStart += Game.Game_OnGameStart;
            LeagueSharp.Game.OnGameUpdate += Game.GameEnd;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }

        private static void Initialize()
        {
            UserInterface.CreateMenu();
            Files.CreateFolder();
            Files.GetData();
        }
    }
}