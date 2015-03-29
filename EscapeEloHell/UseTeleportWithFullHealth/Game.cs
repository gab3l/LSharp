using System;
using System.Threading;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using UseTeleportWithFullHealthDll;

namespace UseTeleportWithFullHealth
{
    public class Game
    {
        private static Spell teleportingSpell;
        public static object Lock = new object();

        public static void OnStart(EventArgs args)
        {
            LeagueSharp.Game.PrintChat("Smart teleport loaded.");
        }

        internal static void OnUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled || ObjectManager.Player.GetSpellSlot("SummonerTeleport") == SpellSlot.Unknown ||
                !UserInterface.IsCastOnReadyPressed)
            {
                return;
            }



            var time = DateTime.Now.AddSeconds(30);
            var hero = ObjectManager.Player;
            teleportingSpell = new Spell(ObjectManager.Player.GetSpellSlot("SummonerTeleport"));
            //while (UserInterface.IsCastOnReadyPressed)
            //{
            // should teleport now?
            if (Teleport.IsStart(hero.HealthPercent, hero.ManaPercent) && teleportingSpell.IsReady())
            {
                // start 
                LeagueSharp.Game.CursorPos.
                teleportingSpell.Cast(); /*new Vector2(LeagueSharp.Game.CursorPos.X, LeagueSharp.Game.CursorPos.Y) */
                //LeagueSharp.Game.PrintChat("teleporting");
                //break;
            }
            //}

        }
    }
}