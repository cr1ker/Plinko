using LOGIC.Money;
using VContainer;
using VContainer.Unity;

namespace LOGIC.UPGRADES
{
    public class Upgrades : IInitializable, IStartable
    {
        #region CONSTANTS

        public const string SPAWN_BALL_MACHINES_UPGRADE = nameof(SPAWN_BALL_MACHINES_UPGRADE);
        public const string SPAWN_BALL_SPEED_UPGRADE = nameof(SPAWN_BALL_SPEED_UPGRADE);
        private const int MAX_SPAWN_BALL_MACHINE_UPGRADE_LEVEL = 4;
        private const int MAX_SPAWN_BALL_SPEED_UPGRADE_LEVEL = 8;

        #endregion

        private readonly LevelMoney _levelMoney;
        private readonly BallSpawner _ballSpawner;

        public Upgrades(LevelMoney levelMoney, BallSpawner ballSpawner)
        {
            _levelMoney = levelMoney;
            _ballSpawner = ballSpawner;
        }
        
        #region VCONTAINER

        public void Initialize()
        {
            if (!SaveManager.HasData(SPAWN_BALL_SPEED_UPGRADE) && !SaveManager.HasData(SPAWN_BALL_MACHINES_UPGRADE))
            {
                SaveManager.SaveData(SPAWN_BALL_SPEED_UPGRADE, 1);
                SaveManager.SaveData(SPAWN_BALL_MACHINES_UPGRADE, 1);
            }
        }

        public void Start()
        {
            UpgradeData data = GetSpawnBallSpeedUpgradeData();
            _ballSpawner.SetSpawnSpeed(data.UpgradeLevel);

            data = GetSpawnBallMachinesUpgradeData();

            for (int i = 0; i < data.UpgradeLevel; i++)
            {
                _ballSpawner.SpawnBallMachine();
            }
        }

        #endregion

        public UpgradeData GetSpawnBallMachinesUpgradeData()
        {
            SaveManager.GetData(SPAWN_BALL_MACHINES_UPGRADE, out int upgradeLevel);
            
            UpgradesPrices.BallMachineUpgradesPrices.TryGetValue(upgradeLevel, out int price);
            
            return new UpgradeData(price, upgradeLevel, upgradeLevel >= MAX_SPAWN_BALL_MACHINE_UPGRADE_LEVEL);
        }

        public UpgradeData GetSpawnBallSpeedUpgradeData()
        {
            SaveManager.GetData(SPAWN_BALL_SPEED_UPGRADE, out int upgradeLevel);
            
            UpgradesPrices.SpawnSpeedUpgradesPrices.TryGetValue(upgradeLevel, out int price);
            
            return new UpgradeData(price, upgradeLevel, upgradeLevel >= MAX_SPAWN_BALL_SPEED_UPGRADE_LEVEL);
        }

        public bool TryUpgrade(string targetUpgrade)
        {
            int upgradeLevel;
            if (SPAWN_BALL_SPEED_UPGRADE == targetUpgrade)
            { 
                SaveManager.GetData(SPAWN_BALL_SPEED_UPGRADE, out upgradeLevel);

                var isMaxLevel = upgradeLevel >= MAX_SPAWN_BALL_SPEED_UPGRADE_LEVEL;
                UpgradesPrices.SpawnSpeedUpgradesPrices.TryGetValue(upgradeLevel, out int price);
                var isEnoughMoney = _levelMoney.IsEnoughMoney(price);
                
                if (!isMaxLevel && isEnoughMoney)
                {
                    _levelMoney.TrySubtractMoney(price);
                    _ballSpawner.SetSpawnSpeed(upgradeLevel);
                    SaveManager.SaveData(SPAWN_BALL_SPEED_UPGRADE, upgradeLevel + 1);
                }

                return true;
            }
            
            if(SPAWN_BALL_MACHINES_UPGRADE == targetUpgrade)
            {
                SaveManager.GetData(SPAWN_BALL_MACHINES_UPGRADE, out upgradeLevel);

                var isMaxLevel = upgradeLevel >= MAX_SPAWN_BALL_MACHINE_UPGRADE_LEVEL;
                UpgradesPrices.BallMachineUpgradesPrices.TryGetValue(upgradeLevel, out int price);
                var isEnoughMoney = _levelMoney.IsEnoughMoney(price);
                
                if (!isMaxLevel && isEnoughMoney)
                {
                    _levelMoney.TrySubtractMoney(price);
                    _ballSpawner.SpawnBallMachine();
                    SaveManager.SaveData(SPAWN_BALL_MACHINES_UPGRADE, upgradeLevel + 1);
                }

                return true;
            }

            return false;
        }
    }
}

public readonly struct UpgradeData
{
    public readonly int UpgradePrice;
    public readonly int UpgradeLevel;
    public readonly bool IsMaxLevel;

    public UpgradeData(int upgradePrice, int upgradeLevel, bool isMaxLevel)
    {
        UpgradePrice = upgradePrice;
        UpgradeLevel = upgradeLevel;
        IsMaxLevel = isMaxLevel;
    }
}