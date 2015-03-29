using System;
using LeagueSharp.Common;

namespace RelaxedWinner
{
    public class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate -= Game.GameEnd;
        }

        private static void Main(string[] args)
        {
            Initialize();

            LeagueSharp.Game.PrintChat("Relaxed Winner loaded.");
            LeagueSharp.Game.PrintChat("Change messages from RelaxedWinner in " + Files.Folder + @"\" + Files.FileName);

            if (!UserInterface.IsEnabled)
            {
                LeagueSharp.Game.PrintChat("Relaxed Winner disabled!");
                return;
            }

            LeagueSharp.Game.OnStart += Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate += Game.GameEnd;
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