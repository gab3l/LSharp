using System;
using System.Reflection;

namespace Surrender
{
    internal class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
            LeagueSharp.Game.OnNotify -= Game.OnNotify;
        }

        private static void Main(string[] args)
        {
            LeagueSharp.Game.PrintChat(
                string.Format("Surrender loaded (v{0}).", Assembly.GetExecutingAssembly().GetName().Version));

            UserInterface.CreateMenu();

            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            LeagueSharp.Game.OnNotify += Game.OnNotify;
            LeagueSharp.Game.OnEnd += RemoveEventHandler;
        }
    }
}