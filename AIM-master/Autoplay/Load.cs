using System;
using System.Collections.Generic;
using System.Linq;
using AIM.Autoplay.Modes;
using AIM.Autoplay.Util.Data;
using LeagueSharp;
using LeagueSharp.Common;
using AutoLevel = AIM.Autoplay.Util.Data.AutoLevel;

namespace AIM.Autoplay
{
    internal class Load
    {
        public static readonly bool ShouldUsePoroSnaxThisGame = Randoms.RandomDecision();
        public static int LoadedTime;

        public Load()
        {
            Game.OnWndProc += OnWndProc;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
            Game.OnUpdate += OnGameUpdate;
        }

        public static void OnWndProc(EventArgs args)
        {
            //Draw AIMLoading.jpg
        }

        public void OnGameLoad(EventArgs args)
        {
            try
            {
                Utils.ClearConsole();

                LoadedTime = Environment.TickCount;
                Utility.DelayAction.Add(
                    Randoms.Rand.Next(1000, 30000), () =>
                    {
                        new Carry();
                        Console.WriteLine("Carry Init Success!");
                    });
                Utility.DelayAction.Add(
                    Randoms.Rand.Next(1000, 10000), () =>
                    {
                        new LeagueSharp.Common.AutoLevel(AutoLevel.GetSequence().Select(num => num - 1).ToArray());
                        LeagueSharp.Common.AutoLevel.Enable();
                        Console.WriteLine("AutoLevel Init Success!");
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Game.PrintChat("AIM {0} Successfuly Loaded, Enjoy!", Program.Version);
            Game.PrintChat("Thanks eLem3ntz and weirdbrian4 for updating!");
            Game.PrintChat("Currently still working on a version that improves performance by A LOT.");
        }

        public static void OnGameUpdate(EventArgs args)
        {
            if (Utility.Map.GetMap().Type == Utility.Map.MapType.HowlingAbyss)
            {
                UsePorosnax();
            }
        }

        public static bool UsePorosnax()
        {
            var trinket = ObjectHandler.Player.Spellbook.GetSpell(SpellSlot.Trinket);
            return trinket != null && trinket.IsReady() && ShouldUsePoroSnaxThisGame &&
                   ObjectHandler.Player.Spellbook.CastSpell(SpellSlot.Trinket);
        }
    }
}