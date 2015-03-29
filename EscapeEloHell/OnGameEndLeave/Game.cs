using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;

namespace OnGameEndLeave
{
    public class Game
    {
        public static void Game_OnStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("On Game End Leave Loaded.");
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileName([In] IntPtr hModule,
            [Out] StringBuilder lpFilename,
            [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        internal static void Game_OnGameEnd(EventArgs args)
        {
            if (UserInterface.IsEnabled)
            {
                var nexus = ObjectManager.Get<Obj_HQ>().FirstOrDefault(n => n.Health < 1);
                if (nexus == null)
                {
                    return;
                }
                
                Task.Factory.StartNew(
                    () =>
                    {
                        Thread.Sleep(2000);
                        var myId = Process.GetCurrentProcess().Id;
                        var process = Process.GetProcessById((int)myId);
                        process.Kill();
                        
                        //// Happy SQL-Injection
                        //var query = string.Format(
                        //    "SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
                        //var search = new ManagementObjectSearcher("root\\CIMV2", query);
                        //var results = search.Get().GetEnumerator();
                        //results.MoveNext();
                        //var queryObj = results.Current;
                        //var parentId = (uint) queryObj["ParentProcessId"];
                        //var parent = Process.GetProcessById((int) parentId);
                        //parent.Kill();
                    });

                //StringBuilder a = new StringBuilder();
                //GetModuleFileName(GetModuleHandle(NULL), a, 4 /*"fileName.Capacity"*/);
            }
        }
    }
}