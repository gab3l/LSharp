using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Surrender;

namespace Surrender
{
    public class Game
    {
        public static void Game_OnGameStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("Surrender loaded.");
        }

        private static DateTime time;

        private static void AgreeSurrender()
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

        internal static void Game_OnGameNotifyEvent(GameNotifyEventArgs args)
        {
            if (!UserInterface.IsEnabled)
            {
                return;
            }

            if (UserInterface.IsAlwaysDeclineSurrender)
            {
                if (SurrenderVoteRunning(args))
                {
                    DeclineSurrender(); 
                }

                return;
            }



            if (LeagueSharp.Game.ClockTime > 1470 && DateTime.Now > time.AddMinutes(3) || SurrenderVoteRunning(args))
            {
                if (!UserInterface.IsSmartSurrender)
                {
                    AgreeSurrender();
                    return;
                }

                if (TeamIsLossing(args))
                {
                    AgreeSurrender();
                }
                else if (SurrenderVoteRunning(args))
                {
                    DeclineSurrender(); 
                }
            }
        }

        private static bool TeamIsLossing(GameNotifyEventArgs args)
        {
            var enemyStats = 0;
            var allyStats = 0;
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.IsEnemy))
            {
                enemyStats += enemy.ChampionsKilled;
            }
            
            foreach (var ally in ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.IsAlly))
            {
                enemyStats += ally.ChampionsKilled;
            }

            return enemyStats - UserInterface.KillDifference > allyStats;
       }

        private static void DeclineSurrender()
        {
            ChatWithDelay(2000, 10000, new Random(2).Next() == 1 ? @"/noff" : @"/nosurrender");
        }

        private static bool SurrenderVoteRunning(GameNotifyEventArgs args)
        {
            return args.EventId == GameEventId.OnSurrenderVote || args.EventId == GameEventId.OnSurrenderVoteStart; 
        }
    }
}