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
        public static Menu Menu{ get; set; }

        public static bool IsEnabled { get {  return UserInterface.Menu.Item("enabled").GetValue<bool>();}  }

        public static bool IsMuteAll { get { return UserInterface.Menu.Item("muteAll").GetValue<bool>(); } }

        public static bool IsStartMessage { get { return UserInterface.Menu.Item("startmessage").GetValue<bool>(); } }

        public static bool IsEndMessage { get { return UserInterface.Menu.Item("endmessage").GetValue<bool>(); } }

        internal static void CreateMenu()
        {
            Menu = new Menu("Relaxed Winner", "relaxedwinner", true);

            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            Menu.AddSubMenu(new Menu("Configuration", "configuration", true));
            Menu.SubMenu("configuration").AddItem(new MenuItem("StartMessage", "startmessage").SetValue(true));
            Menu.SubMenu("configuration").AddItem(new MenuItem("EndMessage", "endmessage").SetValue(true));
            Menu.AddToMainMenu();
        }

    }
}
