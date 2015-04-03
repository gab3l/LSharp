using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using UseTeleportWithFullHealthDll;

namespace UseTeleportWithFullHealth
{
    public class Game
    {
        private static Spell teleportingSpell;
        private const int Tolerance = 2;
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
                LeagueSharp.Game.PrintChat("Go teleport now...");
                teleportingSpell.Cast(new Vector3(LeagueSharp.Game.CursorPos.X, LeagueSharp.Game.CursorPos.Y, LeagueSharp.Game.CursorPos.Y));
                //var results =
                //    ObjectManager.Get<Obj_AI_Base>()
                //        .Where(
                //            x =>
                //                Math.Abs(x.Position.X - LeagueSharp.Game.CursorPos.X) < Tolerance &&
                //                Math.Abs(x.Position.Y - LeagueSharp.Game.CursorPos.Y) < Tolerance);

                //foreach (var item in results.Where(x => teleportingSpell.CanCast(x)))
                //{
                //    teleportingSpell.Cast(item);
                //}
            }
        }
    }
}