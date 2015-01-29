using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace RelaxedWinner
{
    public class Game
    {
        private static readonly Random Random = new Random(20000);
       
        public static void Game_OnGameStart(EventArgs args)
        {
            UserInterface.CreateMenu();
            Files.CreateFolder();
            Files.GetData();

            LeagueSharp.Game.PrintChat("Relaxed Winner loaded.");
            if (!UserInterface.IsEnabled)
            {
                LeagueSharp.Game.PrintChat("Relaxed Winner disabled!");
                return;
            }

            if (RelaxedWinnerDll.RelaxedWinner.RepeatMaximum(
                    20, RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart) && UserInterface.IsStartMessage)
            {
                ChatTalk(
                    6000, 15000,
                    RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                        .Message);
                ChatTalk(
                    20000, 35000,
                    @"/all " +
                    RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                        .Message);
               
            }

            ToogleMuteAll(UserInterface.IsMuteAll);

            Files.Save();
        }

        private static void ToogleMuteAll(bool active)
        {
            if (active)
            {
                ChatTalk(36000, 36000, @"/mute all");
            }
        }

        private static void ChatTalk(int minDelaxInMs, int maxDelayInMs, string chatMessage)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(Random.Next(minDelaxInMs, maxDelayInMs));
                    LeagueSharp.Game.Say(chatMessage);
                });
        }

        public static void Game_Update(EventArgs args)
        {
            if (!UserInterface.IsEnabled)
	        {
	        	 return;
	        }

            var nexus = ObjectManager.Get<Obj_HQ>().FirstOrDefault(n => n.Health < 1);
            if (nexus == null)
            {
                return;
            }

            ToogleMuteAll(UserInterface.IsMuteAll);

            LeagueSharp.Game.Say(
                @"/all " +
                RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd).Message);
            ChatTalk(
                2000, 3000,
                RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd).Message);
            LeagueSharp.Game.OnGameUpdate -= Game_Update;
        }
    }
}