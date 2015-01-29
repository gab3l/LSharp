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

        internal static bool IsStartMessageTeam { get { return Menu.Item("startmessageteam").GetValue<bool>(); } }

        internal static bool IsEndMessageTeam { get { return Menu.Item("endmessageteam").GetValue<bool>(); } }

        internal static bool IsStartMessageAll { get { return Menu.Item("startmessageall").GetValue<bool>(); } }

        internal static bool IsEndMessageAll { get { return Menu.Item("endmessageall").GetValue<bool>(); } }

        internal static void CreateMenu()
        {
            Menu = new Menu("Relaxed Winner", "relaxedwinner", true);

            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));

            submenu.AddItem(new MenuItem("startmessageteam", "StartMessage Team").SetValue(true));
            submenu.AddItem(new MenuItem("startmessageall", "StartMessage All").SetValue(true));
            submenu.AddItem(new MenuItem("endmessageteam", "EndMessage Team").SetValue(true));
            submenu.AddItem(new MenuItem("endmessageall", "EndMessage All").SetValue(true));
            submenu.AddItem(new MenuItem("muteall", "Mute Teams while game").SetValue(true));
            Menu.AddToMainMenu();
        }

    }
}
