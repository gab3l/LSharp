using System;
using LeagueSharp;
using LeagueSharp.Common;
using UseTeleportWithFullHealthDll;

namespace UseTeleportWithFullHealth
{
    public class Game
    {
        private static Spell teleportingSpell;

        internal static void OnUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled || ObjectManager.Player.GetSpellSlot("SummonerTeleport") == SpellSlot.Unknown ||
                !UserInterface.IsCastOnReadyPressed)
            {
                return;
            }

            var hero = ObjectManager.Player;
            teleportingSpell = new Spell(ObjectManager.Player.GetSpellSlot("SummonerTeleport"));
            if (Teleport.IsStart(hero.HealthPercent, hero.ManaPercent) && teleportingSpell.IsReady())
            {
                // start 
                //LeagueSharp.Game.CursorPos.teleportingSpell.Cast();
                    /*new Vector2(LeagueSharp.Game.CursorPos.X, LeagueSharp.Game.CursorPos.Y) */
                //LeagueSharp.Game.PrintChat("teleporting");
                //break;
            }
        }
    }
}