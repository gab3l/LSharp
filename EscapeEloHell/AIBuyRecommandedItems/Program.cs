using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;

namespace AIBuyRecommandedItems
{
    public class Program
    {
        private static void RemoveEventHandler(EventArgs args)
        {
            LeagueSharp.Game.OnUpdate -= Game.OnUpdate;
        }

        private static void Main(string[] args)
        {
            UserInterface.CreateMenu();

            LeagueSharp.Game.PrintChat("AIBuyRecommandedItems loaded.");

            LeagueSharp.Game.OnUpdate += Game.OnUpdate;
            CustomEvents.Game.OnGameEnd += RemoveEventHandler;
        }
    }
}
