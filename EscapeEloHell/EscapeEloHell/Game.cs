using System;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;

namespace RelaxedWinner
{
    public class Game
    {
        private static readonly Random Random = new Random(20000);

        public static void Game_OnStart(EventArgs args)
        {
            if (RelaxedWinnerDll.RelaxedWinner.RepeatMaximum(20, RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart))
            {
                if (UserInterface.IsStartMessageTeam)
                {
                    ChatTalk(
                        15000, 35000,
                        RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                            .Message);
                }

                if (UserInterface.IsStartMessageAll)
                {
                    ChatTalk(
                        38000, 60000,
                        @"/all " +
                        RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameStart)
                            .Message);
                }
            }

            ToogleMuteAll(UserInterface.IsMuteAll, false);
        }

        private static void ToogleMuteAll(bool active, bool endGame)
        {
            if (!active)
            {
                return;
            }

            if (endGame)
            {
                LeagueSharp.Game.Say(@"/mute all");
            }
            else
            {
                ChatTalk(61000, 61000, @"/mute all");
            }
        }

        public static void ChatTalk(int minDelaxInMs, int maxDelayInMs, string chatMessage)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(Random.Next(minDelaxInMs, maxDelayInMs));
                    LeagueSharp.Game.Say(chatMessage);
                });
        }

        internal static void OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(), "OnHQKill") || args.EventId == GameEventId.OnHQKill)
            {
                if (UserInterface.Menu == null || !UserInterface.IsEnabled)
                {
                    return;
                }

                ToogleMuteAll(UserInterface.IsMuteAll, true);

                if (UserInterface.IsEndMessageAll)
                {
                    ChatTalk(
                         1000, 2250,
                        @"/all " +
                        RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd)
                            .Message);
                }

                if (UserInterface.IsEndMessageTeam)
                {
                    ChatTalk(
                        3000, 4000,
                        RelaxedWinnerDll.RelaxedWinner.GetMessage(RelaxedWinnerDll.RelaxedWinner.MessageData.GameEnd)
                            .Message);
                }
            }
        }
    }
}