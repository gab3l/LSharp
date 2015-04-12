using System;
using LeagueSharp.Common;
using RelaxedWinnerDll;

namespace RelaxedWinner
{
    public class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            LeagueSharp.Game.OnNotify -= Game.OnNotify;
        }

        private static void Main(string[] args)
        {
            Initialize();

            LeagueSharp.Game.PrintChat("Relaxed Winner loaded.");

            LeagueSharp.Game.OnStart += Game.Game_OnStart;
            LeagueSharp.Game.OnNotify += Game.OnNotify;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }

        private static void Initialize()
        {
            UserInterface.CreateMenu();
            //Files.CreateFolder();
            Files.GetData();
        }
    }
}