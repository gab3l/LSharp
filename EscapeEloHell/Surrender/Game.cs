using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;

namespace Surrender
{
    public class Game
    {
        private static DateTime time;
        public static void Game_OnStart(EventArgs args) {}

        public static void AgreeSurrender()
        {
            ChatWithDelay(2000, 10000, new Random(2).Next() == 1 ? @"/ff" : @"/surrender");
            time = DateTime.Now;
        }

        private static void ChatWithDelay(int minWaitInMs, int maxWaitInMs, string text)
        {
            Task.Factory.StartNew(
                () =>
                {
                    var sleep = new Random().Next(minWaitInMs, maxWaitInMs);
                    Thread.Sleep(sleep);
                    LeagueSharp.Game.Say(text);
                });
        }

        private static bool IsTeamLossing()
        {
            var enemyStats =
                ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsEnemy).Sum(enemy => enemy.ChampionsKilled);
            var allyStats = ObjectManager.Get<Obj_AI_Hero>()
                .Where(x => x.IsAlly)
                .Sum(ally => ally.ChampionsKilled);

            return enemyStats - UserInterface.KillDifference > allyStats;
        }

        private static void DeclineSurrender()
        {
            ChatWithDelay(2000, 3000, new Random(2).Next() == 1 ? @"/noff" : @"/nosurrender");
        }

        private static bool SurrenderVoteRunning(GameNotifyEventArgs args)
        {
            if (args.EventId == GameEventId.OnSurrenderVote)
            {
                LeagueSharp.Game.PrintChat("args.EventId == GameEventId.OnSurrenderVote");
            }
            else if (args.EventId == GameEventId.OnSurrenderVoteStart)
            {
                LeagueSharp.Game.PrintChat("args.EventId == GameEventId.OnSurrenderVoteStart");
            }
            return args.EventId == GameEventId.OnSurrenderVote || args.EventId == GameEventId.OnSurrenderVoteStart;
        }

        internal static void GameOnUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled)
            {
                return;
            }

            // WTF ist das bitte
            if (LeagueSharp.Game.ClockTime > 1470 && DateTime.Now > time.AddMinutes(3))
            {
                if (UserInterface.IsAlwaysDeclineSurrender)
                {
                    DeclineSurrender();
                    time = DateTime.Now;
                    return;
                }

                if (UserInterface.IsSmartSurrender)
                {
                    if (IsTeamLossing())
                    {
                        AgreeSurrender();
                        time = DateTime.Now;
                        return;
                    }
                }
            }

            if (LeagueSharp.Game.ClockTime > 1470 * 3 /* 60 min */&& DateTime.Now > time.AddMinutes(3) &&
                UserInterface.IsSurrenderAfterOneHour)
            {
                time = DateTime.Now;
                AgreeSurrender();
            }
        }
    }
}