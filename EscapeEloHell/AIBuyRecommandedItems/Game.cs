using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace AIBuyRecommandedItems
{
    public class Game
    {
        private static readonly Random Random = new Random(20000);

        public static void Game_OnStart(EventArgs args)
        {
        }

        //public static void ChatTalk(int minDelaxInMs, int maxDelayInMs, string chatMessage)
        //{
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            Thread.Sleep(Random.Next(minDelaxInMs, maxDelayInMs));
        //            LeagueSharp.Game.Say(chatMessage);
        //        });
        //}

        private const double MaximumDistanceToShop = 200;
        private static Obj_Shop shop;

        private static bool IsHeroNearShop()
        {
            var hero = ObjectManager.Player;
            var shops = ObjectManager.Get<Obj_Shop>();
            foreach (var item in shops)
            {
                if (MaximumDistanceToShop < Vector3.Distance(item.Position, hero.Position))
                {
                    shop = item;
                    return true;
                }

                return false;
            }
        }

        internal static void OnUpdate(EventArgs args)
        {
            var hero = ObjectManager.Player;
            var shops = ObjectManager.Get<Obj_Shop>();
            if (IsHeroNearShop())
            {
                if (hero.InShop())
                {
                   // get req. items from website --> buy them step by step
                }
                else
                {
                    VirtualMouse.RightClick(shop.Position);
                }
            }
            else
            {
                // close shop
            }
        }
    }
}

