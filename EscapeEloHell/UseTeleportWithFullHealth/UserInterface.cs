using LeagueSharp.Common;

namespace UseTeleportWithFullHealth
{
    public class UserInterface
    {
        internal static Menu Menu { get; set; }

        internal static bool IsEnabled
        {
            get { return Menu.Item("enabled").GetValue<bool>(); }
        }

        internal static bool IsCastOnReadyPressed
        {
            get { return Menu.Item("castifready").GetValue<KeyBind>().Active; }
        }

        internal static void CreateMenu()
        {
            Menu = new Menu("Smart Teleport", "smartteleport", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));
            //submenu.AddItem(new KeyBind("teleportkey", "Teleport when full key").SetValue(false));

            submenu.AddItem(new MenuItem("castifready", "Cast if ready").SetValue(new KeyBind(115, KeyBindType.Press)));
            Menu.AddToMainMenu();
        }
    }
}