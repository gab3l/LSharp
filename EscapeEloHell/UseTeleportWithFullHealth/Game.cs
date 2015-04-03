using System;
using System.Linq;
using System.Threading;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using UseTeleportWithFullHealthDll;

namespace UseTeleportWithFullHealth
{
    public class Game
    {
        private static SpellSlot teleportingSpell;
        private const int Tolerance = 2;
        internal static void OnUpdate(EventArgs args)
        {
            if (!UserInterface.IsEnabled || ObjectManager.Player.GetSpellSlot("SummonerTeleport") == SpellSlot.Unknown ||
                !UserInterface.IsCastOnReadyPressed)
            {
                return;
            }

            var hero = ObjectManager.Player;
            teleportingSpell = ObjectManager.Player.GetSpellSlot("SummonerTeleport");
          

            if (Teleport.IsStart((hero.Health / hero.MaxHealth) * 100, (hero.Mana / hero.MaxMana)*100) && teleportingSpell.IsReady())
            {
                var results =
                              ObjectManager.Get<Obj_AI_Base>()
                                  .Where(
                                      x =>
                                          Math.Abs(x.Position.X - LeagueSharp.Game.CursorPos.X) < 2000 &&
                                          Math.Abs(x.Position.Y - LeagueSharp.Game.CursorPos.Y) < 2000 &&
                                          Math.Abs(x.Position.Z - LeagueSharp.Game.CursorPos.Z) < 2000);
                foreach (var item in results)
                {
                    if (!ObjectManager.Player.Spellbook.IsCastingSpell)
                    {
                        ObjectManager.Player.Spellbook.CastSpell(teleportingSpell, item);
                    } 
                }

                var minions =
                           ObjectManager.Get<Obj_AI_Minion>()
                               .Where(
                                   x =>
                                       Math.Abs(x.Position.X - LeagueSharp.Game.CursorPos.X) < 20 &&
                                       Math.Abs(x.Position.Y - LeagueSharp.Game.CursorPos.Y) < 20 &&
                                       Math.Abs(x.Position.Z - LeagueSharp.Game.CursorPos.Z) < 20);
                foreach (var item in minions)
                {
                    if (!ObjectManager.Player.Spellbook.IsCastingSpell)
                    {
                        ObjectManager.Player.Spellbook.CastSpell(teleportingSpell, item);
                    }
                }

            }
        }
    }
}