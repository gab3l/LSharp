
namespace FriendlyWinner
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using LeagueSharp;
    using LeagueSharp.Common;

    public class Game
    {
        private static readonly Random Random = new Random(20000);
        private static Menu menu;

        private static void CreateMenu()
        {
            menu = new Menu("Friendy Winner Sharp", "friendlywinner", true);
            menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            menu.AddToMainMenu();
        }

        public static void Game_OnGameStart(EventArgs args)
        {
            CreateMenu();
            Files.CreateFolder();
            Files.GetData();

            LeagueSharp.Game.PrintChat("Friendly Winner loaded.");
            LeagueSharp.Game.PrintChat("Change input :" + Files.Folder);

            if (menu.Item("enabled").GetValue<bool>() && FriendlyWinnerDll.FriendlyWinner.RepeatMaximum(20, FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart))
            {
                ChatTalk(6000, 15000, FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart).Message);
                ChatTalk(20000, 35000, @"/all " + FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameStart).Message);
                ChatTalk(36000, 36000, @"/mute all");
            }

            Files.Save();
        }

        private static void ChatTalk(int min, int max, string message)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(Random.Next(min, max));
                LeagueSharp.Game.Say(message);
            });
        }

        public static void Game_Update(EventArgs args)
        {
            var nexus = ObjectManager.Get<Obj_HQ>().FirstOrDefault(n => n.Health < 1);
            if (nexus == null || !menu.Item("enabled").GetValue<bool>()) return;

            LeagueSharp.Game.Say(@"/mute all");
            LeagueSharp.Game.Say(@"/all " + FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameEnd).Message);
            ChatTalk(2000, 3000, FriendlyWinnerDll.FriendlyWinner.GetMessage(FriendlyWinnerDll.FriendlyWinner.Motivation.GameEnd).Message);
            LeagueSharp.Game.OnGameUpdate -= Game_Update;
        }
    }
}