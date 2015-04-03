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
            var enemyStats = ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsEnemy).Sum(enemy => enemy.ChampionsKilled);
            var allyStats = ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsAlly).Sum(ally => ally.ChampionsKilled);

            return enemyStats - UserInterface.KillDifference > allyStats;
        }

        private static void DeclineSurrender()
        {
            ChatWithDelay(2000, 3000, new Random(2).Next() == 1 ? @"/noff" : @"/nosurrender");
        }

        internal static void OnUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled)
            {
                return;
            }

            if (UserInterface.IsSurrenderAfterOneHour && LeagueSharp.Game.ClockTime > 1470 * 3 /* 60 min */&&
                DateTime.Now > time.AddMinutes(3))
            {
                time = DateTime.Now;
                AgreeSurrender();
            }
        }

        internal static void OnNotify(GameNotifyEventArgs args)
        {
            if (!VoteStarted(args))
            {
                return;
            }
            Console.WriteLine("Id={0} Network={1}", args.EventId, args.NetworkId);
            // Vote Running...
            if (UserInterface.IsSurrenderAfterOneHour && LeagueSharp.Game.ClockTime > 1470 * 3)
            {
                AgreeSurrender();
            }
            else if (UserInterface.IsAlwaysDeclineSurrender)
            {
                DeclineSurrender();
            }
            else if (UserInterface.IsSmartSurrender)
            {
                if (IsTeamLossing())
                {
                    AgreeSurrender();
                }
            }
        }

        private static bool VoteStarted(GameNotifyEventArgs args)
        {
            //if (args.EventId.ToString() == "bla" || args.EventId == dec)
            //{
            //return true;
            //}
            return false;
        }
    }
}