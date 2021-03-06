﻿using LeagueSharp.Common;

namespace Surrender
{
    public class UserInterface
    {
        internal static Menu Menu { get; set; }

        internal static bool IsEnabled
        {
            get { return Menu.Item("enabled").GetValue<bool>(); }
        }

        internal static bool IsAlwaysDeclineSurrender
        {
            get { return Menu.Item("declinesurrender").GetValue<bool>(); }
        }

        internal static bool IsSmartSurrender
        {
            get { return Menu.Item("smartsurrender").GetValue<bool>(); }
        }

        internal static bool IsSurrenderAfterOneHour
        {
            get { return Menu.Item("surrenderafteronehour").GetValue<bool>(); }
        }

        internal static int KillDifference
        {
            get { return Menu.Item("killsdifferencesmartsurrender").GetValue<int>(); }
        }

        internal static void CreateMenu()
        {
            Menu = new Menu("Surrender", "surrender", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));
            submenu.AddItem(new MenuItem("declinesurrender", "Always decline surrender").SetValue(false));
            submenu.AddItem(new MenuItem("smartsurrender", "Surrender only on loosing").SetValue(false));
            submenu.AddItem(
                new MenuItem("killsdifferencesmartsurrender", "Kills difference smart surrender").SetValue(
                    new Slider(50)));

            submenu.AddItem(new MenuItem("surrenderafteronehour", "Surrender after one hour").SetValue(true));


            Menu.AddToMainMenu();
        }
    }
}