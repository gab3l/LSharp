using LeagueSharp.Common;

namespace AIBuyRecommandedItems
{
    public class UserInterface
    {
        internal static Menu Menu { get; set; }

        internal static bool IsEnabled
        {
            get { return Menu.Item("enabled").GetValue<bool>(); }
        }

        internal static void CreateMenu()
        {
            Menu = new Menu("Relaxed Winner", "relaxedwinner", true);

            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            // SubMenu Configuration
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));
         
            //submenu.AddItem(new MenuItem("muteall", "Mute Teams while ingame").SetValue(true));
            Menu.AddToMainMenu();
        }
    }
}