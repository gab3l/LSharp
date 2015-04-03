using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;

namespace OnGameEndLeave
{
    public class Game
    {
        internal static void OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(), "OnHQKill") || args.EventId == GameEventId.OnHQKill)
            {
                LeagueSharp.Game.PrintChat("Thread killing it in 20sec...");

                Task.Factory.StartNew(
                    () =>
                    {
                        Thread.Sleep(12000);
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    });
            }
        }
    }
}