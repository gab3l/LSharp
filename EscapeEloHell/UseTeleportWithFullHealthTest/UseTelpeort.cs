using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseTeleportWithFullHealthDll;

namespace UseTeleportWithFullHealthTest
{
    [TestClass]
    public class UseTelpeort
    {
        // Every sec - not instant
        // Now restores 35% of your missing health and mana per second while on the fountain.
        // ==> direkter port
        // combat? 8 sec delay

        // ohne boots enchant
        // http://leagueoflegends.wikia.com/wiki/Fountain
        //The Spawns in Summoner's Rift, Crystal Scar and Twisted Treeline will rapidly regenerate a percentage of the champion's maximum health and mana every second (Summoner's Rift 2.1% per 0.25 second). If the champion is equipped with Homeguard item.png Homeguard and hasn't been in combat in the last 8 seconds, he/she will be given a large speed boost and will regenerate health and mana even faster.

        [TestMethod]
        public void InstantCallBackTime()
        {
            Assert.AreEqual(true, Teleport.IsStart(100, 100));
        }

        [TestMethod]
        public void InstantCallBackTimeCauseDelayHighEnough()
        {
            Assert.AreEqual(true, Teleport.IsStart(100 - 29.4, 100 - 20));
        }

        [TestMethod]
        public void WaitMore()
        {
            Assert.AreEqual(false, Teleport.IsStart(10, 1));
        }
    }
}