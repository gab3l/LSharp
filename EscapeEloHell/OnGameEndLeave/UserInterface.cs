using LeagueSharp.Common;

namespace OnGameEndLeave
{
    public class UserInterface
    {
        internal static Menu Menu { get; set; }

        internal static bool IsEnabled
        {
            get { return Menu.Item("enabled").GetValue<bool>(); }
        }

        internal static int WaitTimeInSeconds
        {
            get { return Menu.Item("waitTimeInSecondsBeforeKill").GetValue<int>(); }
        }

        internal static void CreateMenu()
        {
            Menu = new Menu("On Game End Leave", "ongameendleave", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));
            var submenu = Menu.AddSubMenu(new Menu("Configuration", "configuration"));

            submenu.AddItem(
                new MenuItem("waitTimeInSecondsBeforeKill", "Wait seconds before killing client").SetValue(
                    new Slider(15, 0, 120)));
            Menu.AddToMainMenu();
        }
    }
}