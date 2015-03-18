using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using System.Diagnostics;

namespace OnGameEndLeave
{
    public class Game
    {
        public static void Game_OnGameStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("On Game End Leave Loaded.");
        }

        internal static void Game_OnGameEnd(EventArgs args)
        {
            if (UserInterface.IsEnabled)
            {
                Task.Factory.StartNew(new Action(() =>
                     {
                         Thread.Sleep(2000);
                         var myId = Process.GetCurrentProcess().Id;

                         // Happy SQL-Injection
                         var query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
                         var search = new System.Management.ManagementObjectSearcher("root\\CIMV2", query);
                         var results = search.Get().GetEnumerator();
                         results.MoveNext();
                         var queryObj = results.Current;
                         var parentId = (uint)queryObj["ParentProcessId"];
                         var parent = Process.GetProcessById((int)parentId);
                         parent.Kill();
                     }));
            }
        }
    }
}