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
        internal static bool IsAlwaysDeclineSurrender { get { return Menu.Item("alwaysdeclinesurrender").GetValue<bool>(); } }
        internal static bool IsSmartSurrender { get { return Menu.Item("alwaysdeclinesurrender").GetValue<bool>(); } }
        internal static int KillDifference { get { return Menu.Item("killsdifferencesmartsurrender").GetValue<int>(); } }
        
        internal static void CreateMenu()
        {
            Menu = new Menu("Surrender", "surrender", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));
            submenu.AddItem(new MenuItem("declinesurrender", "Always decline surrender").SetValue(false));
            submenu.AddItem(new MenuItem("smartsurrender", "Surrender only on loosing").SetValue(true));
            submenu.AddItem(new MenuItem("killsdifferencesmartsurrender", "Kills difference smart surrender").SetValue(new Slider(5, 0, 25)));
            
            Menu.AddToMainMenu();
        }

    }
}
