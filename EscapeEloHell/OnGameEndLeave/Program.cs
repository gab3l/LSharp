using System;
using System.Diagnostics;
using System.Management;
using LeagueSharp.Common;

namespace OnGameEndLeave
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnStart -= Game.Game_OnStart;
            CustomEvents.Game.OnGameEnd -= Game.Game_OnGameEnd;
            CustomEvents.Game.OnGameEnd -= RemoveEventHandler;
        }

        private static void Main(string[] args)
        {
            //var myId = Process.GetCurrentProcess().Id;

            //// Happy SQL-Injection
            //var query = string.Format(
            //    "SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
            //var search = new ManagementObjectSearcher("root\\CIMV2", query);
            //var results = search.Get().GetEnumerator();
            //results.MoveNext();
            //var queryObj = results.Current;
            //var parentId = (uint)queryObj["ParentProcessId"];

            //LeagueSharp.Game.PrintChat(myId.ToString());
            //LeagueSharp.Game.PrintChat(parentId.ToString());

            UserInterface.CreateMenu();
            RegisterEvents();
        }

        private static void RegisterEvents()
        {
            LeagueSharp.Game.OnStart += Game.Game_OnStart;
            LeagueSharp.Game.OnUpdate += Game.Game_OnGameEnd;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }

    }
}