using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    public class Game
    {
        internal static void OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(),"OnHQKill") || args.EventId == GameEventId.OnHQKill)
            {
                LeagueSharp.Game.PrintChat("Thread killing it in 20sec - should be dead already");

                Task.Factory.StartNew(
                    () =>
                    {
                        Console.WriteLine("Thread killing it in 20sec - should be dead already");
                        Thread.Sleep(20000);
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    });
            }
        }
    }
}