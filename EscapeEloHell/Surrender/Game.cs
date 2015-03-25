﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;

namespace Surrender
{
    public class Game
    {
        private static DateTime time;
        public static void Game_OnGameStart(EventArgs args) {}

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
                    LeagueSharp.Game.PrintChat("DeclineSurrender");

                    DeclineSurrender();
                }

                return;
            }


            if (LeagueSharp.Game.ClockTime > 1470 && DateTime.Now > time.AddMinutes(3) /* todo testen! */||
                SurrenderVoteRunning(args))
            {
                if (!UserInterface.IsSmartSurrender)
                {
                    // tested it's working
                    AgreeSurrender();
                    return;
                }

                if (IsTeamLossing())
                {
                    // tested it's working
                    AgreeSurrender();
                }
                else if (SurrenderVoteRunning(args))
                {
                    DeclineSurrender();
                }
            }
        }

        private static bool IsTeamLossing()
        {
            var enemyStats =
                ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.IsEnemy).Sum(enemy => enemy.ChampionsKilled);
            var allyStats = ObjectManager.Get<Obj_AI_Hero>()
                .Where(hero => hero.IsAlly)
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

        internal static void Game_OnGameUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled)
            {
                return;
            }

            if (LeagueSharp.Game.ClockTime > 1470 && DateTime.Now > time.AddMinutes(3))
            {
                if (!UserInterface.IsSmartSurrender)
                {
                    AgreeSurrender();
                    return;
                }

                if (IsTeamLossing())
                {
                    AgreeSurrender();
                }
            }
        }
    }
}