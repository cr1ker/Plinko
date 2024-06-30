using System.Collections.Generic;

namespace LOGIC.UPGRADES
{
    public static class UpgradesPrices
    {
        public static Dictionary<int, int> BallMachineUpgradesPrices = new ()
        {
            {1, 150},
            {2, 1000},
            {3, 5000},
            {4, 10_000}
        };
        
        public static Dictionary<int, int> SpawnSpeedUpgradesPrices = new ()
        {
            {1, 50},
            {2, 150},
            {3, 350},
            {4, 1000},
            {5, 2500},
            {6, 5000},
            {7, 7500},
            {8, 10_000}
        };
    }
}