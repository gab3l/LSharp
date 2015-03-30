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
        [DllImport("kernel32.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileName([In] IntPtr hModule,
            [Out] StringBuilder lpFilename,
            [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        internal static void OnUpdate(EventArgs args)
        {
            if (UserInterface.IsEnabled)
            {
                var nexus = ObjectManager.Get<Obj_HQ>().FirstOrDefault(n => n.Health < 1);
                if (nexus == null)
                {
                    return;
                }

                var process = Process.GetProcessById(Process.GetCurrentProcess().Id);
                Thread.Sleep(20000);
                process.Kill();
                Task.Factory.StartNew(
                    () =>
                    {
                        Console.WriteLine("Thread killing it in 10sec - should be dead already");
                        Thread.Sleep(20000);
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    });
            }
        }

        internal static void OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(),"OnHQKill") || args.EventId == GameEventId.OnHQKill)
            {
                var process = Process.GetProcessById(Process.GetCurrentProcess().Id);
                Thread.Sleep(20000);
                process.Kill();
                Task.Factory.StartNew(
                    () =>
                    {
                        Console.WriteLine("Thread killing it in 10sec - should be dead already");
                        Thread.Sleep(20000);
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    });
            }
        }
    }
}