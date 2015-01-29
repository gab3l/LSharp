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
            if (RelaxedWinnerDll.RelaxedWinner.RepeatMaximum(
                    20, RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart) && UserInterface.IsStartMessage)
            {
                ChatTalk(
                    15000, 25000,
                    RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                        .Message);
                ChatTalk(
                    29000, 45000,
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
                ChatTalk(46000, 46000, @"/mute all");
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

        public static void GameEnd(EventArgs args)
        {
            if (UserInterface.Menu == null || !UserInterface.IsEnabled)
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
            LeagueSharp.Game.OnGameUpdate -= GameEnd;
        }
    }
}