using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxedWinner
{
   public  class UserInterface
    {
       internal static Menu Menu { get; set; }

        internal static bool IsEnabled { get { return Menu.Item("enabled").GetValue<bool>(); } }

        internal static bool IsMuteAll { get { return Menu.Item("muteall").GetValue<bool>(); } }

        internal static bool IsStartMessage { get { return Menu.Item("startmessage").GetValue<bool>(); } }

        internal static bool IsEndMessage { get { return Menu.Item("endmessage").GetValue<bool>(); } }

        internal static void CreateMenu()
        {
            Menu = new Menu("Relaxed Winner", "relaxedwinner", true);

            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));

            submenu.AddItem(new MenuItem("startmessage", "StartMessage").SetValue(true));
            submenu.AddItem(new MenuItem("endmessage", "EndMessage").SetValue(true));
            submenu.AddItem(new MenuItem("muteall", "Mute while game").SetValue(true));
            Menu.AddToMainMenu();
        }

    }
}
