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
                if (nexus != null)
                {
                    LeagueSharp.Game.PrintChat(String.Format("Killing lol in {0} sec...", UserInterface.WaitTimeInSeconds));
                    Thread.Sleep(UserInterface.WaitTimeInSeconds * 1000);
                    var myId = Process.GetCurrentProcess().Id;
                    var process = Process.GetProcessById(myId);
                    process.Kill();
                } 
            }
        }

        internal static void OnNotify(GameNotifyEventArgs args)
        {
            //Console.WriteLine("Buffs: {0}", string.Join(" | ", target.Buffs.Where(b => b.Caster.NetworkId == zilean.NetworkId).Select(b => b.DisplayName)));
            if (args.EventId == GameEventId.OnSurrenderAgreed)
            {
            }
        }
    }
}