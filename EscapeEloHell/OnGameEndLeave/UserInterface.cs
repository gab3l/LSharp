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

        internal static void CreateMenu()
        {
            Menu = new Menu("On Game End Leave", "ongameendleave", true);
            Menu.AddItem(new MenuItem("enabled", "Enable").SetValue(true));

            Menu.AddToMainMenu();
        }
    }
}