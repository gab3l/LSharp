using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrender
{
    public class UserInterface
    {
        internal static Menu Menu { get; set; }

        internal static bool IsEnabled { get { return Menu.Item("enabled").GetValue<bool>(); } }

        internal static void CreateMenu()
        {
            Menu = new Menu("Surrender", "surrender", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));
            Menu.AddToMainMenu();
        }

    }
}
