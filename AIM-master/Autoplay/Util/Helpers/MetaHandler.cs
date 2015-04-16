/* Autoplay Plugin of h3h3's AIO Support
*
* All credits go to him. I only wrote this and
* Autoplay.cs.
* The core is always updated to latest version.
* which you can find here:
* https://github.com/h3h3/LeagueSharp/tree/master/Support
*/

using System;
using System.IO;
using System.Linq;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;

namespace AIM.Autoplay.Util.Helpers
{
    internal class MetaHandler
    {
        public static string[] Supports =
        {
            "Alistar", "Annie", "Blitzcrank", "Braum", "Fiddlesticks", "Janna", "Karma",
            "Kayle", "Leona", "Lulu", "Morgana", "Nunu", "Nami", "Soraka", "Sona", "Taric", "Thresh", "Zilean", "Zyra"
        };

        public static string[] AP =
        {
            "Ahri",  "Alistar", "Anivia", "Annie", "Azir", 
            "Brand", "Braum", "Cassiopeia", "Chogath", "Diana", "Elise", "Evelynn", "ezreal", "FiddleSticks", "Fizz",
            "Galio", "Gragas", "Heimerdinger", "Janna", "Karma", "Karthus", "Kassadin",
            "Kayle",  "KogMaw", "LeBlanc", "Lissandra", "Lulu", "Lux", "Malphite", "Malzahar",
            "Maokai", "Morderkaiser", "Morgana", "Nami", "Nautilus", "Nidalee", "Nunu", "Orianna", "RekSai", "Rumble",
            "Ryze", "Shaco", "Singed", "Sona", "Soraka", "Swain", "Syndra", "Teemo", "Thresh", "TwistedFate", "veigar",
            "VelKoz", "Viktor", "Vladimir", "Xerath", "Ziggs", "Zilean", "Zyra"
        };
		public static string[] AD =
        {
            "ashe", "caitlyn", "corki", "draven", "ezreal", "graves", "kogmaw",
            "missfortune", "quinn", "sivir", "talon", "tristana", "twitch", "urgot", "varus", "vayne", "zed", "jinx",
            "yasuo", "lucian", "shaco", "masteryi"	
        };
		private static readonly string[] ADT =
        {
            "darius",  "evelynn", "fiora", "gangplank", "gnar", "jayce", "yorick", "warwick",
            "pantheon", "irelia", "jarvaniv", "jax", "khazix", "nocturne", "olaf", 
            "rengar", "riven", "shyvana", "trundle", "tryndamere", "udyr", "vi", "monkeyking", "xinzhao", "aatrox",
             
        };
		 private static readonly string[] APT =
        {
              "galio",  "malphite", "poppy", "elise",
            "maokai", "rammus", "sejuani", "shen", "singed", "zac",
            "nunu",  "alistar", "nautilus", "rumble", 
        };
		private static readonly string[] FT =
        {
	    "drmundo", "skarner", "braum", "nasus", "renekton", "chogath", "taric", "leesin", "garen", "volibear", "hecarim",
	    "Blitzcrank", "amumu",
		
		};
		
	
		private static readonly string[] EN =
        {
	    "Akali", "Kennen", "Katarina",
		
		};
		
        private static readonly ItemId[] SRShopList =
        {
            ItemId.Zhonyas_Hourglass, ItemId.Rabadons_Deathcap,
            ItemId.Morellonomicon, ItemId.Athenes_Unholy_Grail, ItemId.Rylais_Crystal_Scepter, ItemId.Mikaels_Crucible,
            ItemId.Frost_Queens_Claim, ItemId.Liandrys_Torment, ItemId.Lich_Bane, ItemId.Locket_of_the_Iron_Solari,
            ItemId.Rod_of_Ages, ItemId.Void_Staff, ItemId.Hextech_Gunblade, ItemId.Sorcerers_Shoes
        };

        private static readonly ItemId[] TTShopList =
        {
            ItemId.Wooglets_Witchcap, ItemId.Rod_of_Ages,
            ItemId.Rylais_Crystal_Scepter, ItemId.Lich_Bane, ItemId.Liandrys_Torment, ItemId.Morellonomicon,
            ItemId.Locket_of_the_Iron_Solari, ItemId.Void_Staff, ItemId.Sorcerers_Shoes
        };

        private static readonly ItemId[] ARAMShopListAP =
        {
            ItemId.Zhonyas_Hourglass, ItemId.Rabadons_Deathcap,
            ItemId.Rod_of_Ages, ItemId.Rylais_Crystal_Scepter,ItemId.Athenes_Unholy_Grail,
            ItemId.Will_of_the_Ancients, ItemId.Liandrys_Torment, ItemId.Void_Staff, ItemId.Abyssal_Scepter, ItemId.Sorcerers_Shoes
        };

	 private static readonly ItemId[] ARAMShopListEN =
        {
            ItemId.Zhonyas_Hourglass, ItemId.Rabadons_Deathcap,
            ItemId.Rylais_Crystal_Scepter,
            ItemId.Will_of_the_Ancients, ItemId.Liandrys_Torment, ItemId.Void_Staff, ItemId.Abyssal_Scepter, ItemId.Sorcerers_Shoes
        };
		
		private static readonly ItemId[] ARAMShopListFT =
        {
            ItemId.Sunfire_Cape, ItemId.Randuins_Omen,
            ItemId.Spirit_Visage, ItemId.Banshees_Veil, ItemId.Frozen_Heart,
            ItemId.Locket_of_the_Iron_Solari, ItemId.Orb_of_Winter, ItemId.Thornmail, ItemId.Mercurys_Treads
        };

        private static readonly ItemId[] ARAMShopListAD =
        {
            ItemId.Blade_of_the_Ruined_King, ItemId.Last_Whisper,
            ItemId.Phantom_Dancer, ItemId.Infinity_Edge, ItemId.Statikk_Shiv, ItemId.Trinity_Force, ItemId.Youmuus_Ghostblade, ItemId.Berserkers_Greaves
        };
		private static readonly ItemId[] ARAMShopListADT =
        {
            ItemId.Maw_of_Malmortius, ItemId.Banshees_Veil, ItemId.Sunfire_Cape,
            ItemId.Locket_of_the_Iron_Solari, ItemId.Blade_of_the_Ruined_King, ItemId.Randuins_Omen, ItemId.Spirit_Visage, ItemId.Mercurys_Treads
        };
		private static readonly ItemId[] ARAMShopListAPT =
        {
            ItemId.Abyssal_Scepter, ItemId.Banshees_Veil, ItemId.Sunfire_Cape,
            ItemId.Locket_of_the_Iron_Solari, ItemId.Liandrys_Torment, ItemId.Randuins_Omen, ItemId.Spirit_Visage, ItemId.Mercurys_Treads
        };
        private static readonly ItemId[] CrystalScar =
        {
            ItemId.Rod_of_Ages_Crystal_Scar, ItemId.Wooglets_Witchcap,
            ItemId.Void_Staff, ItemId.Athenes_Unholy_Grail, ItemId.Abyssal_Scepter, ItemId.Liandrys_Torment,
            ItemId.Morellonomicon, ItemId.Rylais_Crystal_Scepter, ItemId.Sorcerers_Shoes
        };

        private static readonly ItemId[] Other = { };
        private static int LastShopAttempt;

        public static bool ShouldSupportTopLane
        {
            get { return false; }
        }

        public static void DoChecks()
        {
            var map = Utility.Map.GetMap();


            if (Heroes.Me.InFountain())
            {
                if (Heroes.Me.InFountain() && (Heroes.Me.Gold == 475 || Heroes.Me.Gold == 515))
                    //validates on SR untill 1:55 game time
                {
                    var startingItem = Randoms.Rand.Next(-6, 7);
                    if (startingItem <= 0)
                    {
                        Heroes.Me.BuyItem(ItemId.Spellthiefs_Edge);
                    }
                    if (startingItem > 0)
                    {
                        Heroes.Me.BuyItem(ItemId.Ancient_Coin);
                    }
                    Heroes.Me.BuyItem(ItemId.Warding_Totem_Trinket);
                }
                if (File.Exists(FileHandler.TheFile) && (FileHandler.CustomShopList != null))
                {
                    foreach (var item in FileHandler.CustomShopList)
                    {
                        if (!HasItem(item))
                        {
                            BuyItem(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in GetDefaultItemArray())
                    {
                        if (!HasItem(item))
                        {
                            BuyItem(item);
                        }
                    }
                }
            }
        }

        public static bool HasItem(ItemId item)
        {
            return Items.HasItem((int) item, Heroes.Me);
        }

        public static void BuyItem(ItemId item)
        {
            if (Environment.TickCount - LastShopAttempt > Randoms.Rand.Next(500, 1000))
            {
                Heroes.Me.BuyItem(item);
                LastShopAttempt = Environment.TickCount;
            }
        }

        public static ItemId[] GetDefaultItemArray()
        {
            var map = Utility.Map.GetMap();
            if (map.Type == Utility.Map.MapType.SummonersRift)
            {
                return SRShopList.OrderBy(item => Randoms.Rand.Next()).ToArray();
            }
            if (map.Type == Utility.Map.MapType.TwistedTreeline)
            {
                return TTShopList.OrderBy(item => Randoms.Rand.Next()).ToArray();
            }
            if (map.Type == Utility.Map.MapType.HowlingAbyss)
            {
				if (APT.Any(apchamp => Heroes.Me.BaseSkinName.ToLower() == apchamp.ToLower()))
                {
					Console.WriteLine("APT");
                    return ARAMShopListAPT;
                }
				if (ADT.Any(apchamp => Heroes.Me.BaseSkinName.ToLower() == apchamp.ToLower()))
                {
					Console.WriteLine("ADT");
                    return ARAMShopListADT;
                }
                if (AP.Any(apchamp => Heroes.Me.BaseSkinName.ToLower() == apchamp.ToLower()))
                {
					Console.WriteLine("APD");
                    return ARAMShopListAP;
                }
				if (FT.Any(FTchamp => Heroes.Me.BaseSkinName.ToLower() == FTchamp.ToLower()))
                {
					Console.WriteLine("FT");
                    return ARAMShopListFT;
                }
				if (EN.Any(ENchamp => Heroes.Me.BaseSkinName.ToLower() == ENchamp.ToLower()))
                {
					Console.WriteLine("EN");
                    return ARAMShopListEN;
                }


				 if (AD.Any(adchamp => Heroes.Me.BaseSkinName.ToLower() == adchamp.ToLower()))
                {
					Console.WriteLine("ADC");
                    return ARAMShopListAD;
                }
                return ARAMShopListADT;
            }
            if (map.Type == Utility.Map.MapType.CrystalScar)
            {
                return CrystalScar.OrderBy(item => Randoms.Rand.Next()).ToArray();
            }
            return Other;
        }

        public static bool HasSixItems()
        {
            return Heroes.Me.InventoryItems.ToList().Count >= 6;
        }

        public static bool HasSmite(Obj_AI_Hero hero)
        {
            return hero.GetSpellSlot("SummonerSmite") != SpellSlot.Unknown;
        }

        public static bool IsInBase(Obj_AI_Hero hero)
        {
            var map = Utility.Map.GetMap();
            if (map != null && map.Type == Utility.Map.MapType.SummonersRift)
            {
                const int baseRange = 16000000; //4000^2
                return hero.IsVisible &&
                       ObjectManager.Get<Obj_SpawnPoint>()
                           .Any(sp => sp.Team == hero.Team && hero.Distance(sp.Position, true) < baseRange);
            }
            return false;
        }

        public static bool IsSupport(Obj_AI_Hero hero)
        {
            return Supports.Any(support => hero.BaseSkinName.ToLower() == support.ToLower());
        }

        public static Obj_AI_Turret ClosestEnemyTurret(Vector3 point)
        {
            var turrets = ObjectManager.Get<Obj_AI_Turret>().FindAll(t => !t.IsAlly);
            return turrets.OrderBy(t => t.Distance(point)).FirstOrDefault();
        }

        public static Obj_AI_Minion LeadMinion()
        {
            return
                ObjectManager.Get<Obj_AI_Minion>()
                    .FindAll(m => m.IsAlly)
                    .OrderBy(m => ClosestEnemyTurret(m.Position))
                    .FirstOrDefault();
        }

        public static Obj_AI_Minion LeadMinion(Vector3 lane)
        {
            return
                ObjectManager.Get<Obj_AI_Minion>()
                    .FindAll(m => m.IsAlly)
                    .OrderBy(m => ClosestEnemyTurret(lane))
                    .FirstOrDefault();
        }

        public static int CountNearbyAllyMinions(Obj_AI_Base x, int distance)
        {
            return
                ObjectManager.Get<Obj_AI_Minion>()
                    .Count(minion => minion.IsAlly && !minion.IsDead && minion.Distance(x) < distance);
        }

        public static int CountNearbyAllies(Obj_AI_Base x, int distance)
        {
            return
                ObjectManager.Get<Obj_AI_Hero>()
                    .Count(
                        hero =>
                            hero.IsAlly && !hero.IsDead && !HasSmite(hero) && !hero.IsMe && hero.Distance(x) < distance);
        }

        public static int CountNearbyAllies(Vector3 x, int distance)
        {
            return
                ObjectManager.Get<Obj_AI_Hero>()
                    .Count(
                        hero =>
                            hero.IsAlly && !hero.IsDead && !HasSmite(hero) && !hero.IsMe && hero.Distance(x) < distance);
        }
    }
}