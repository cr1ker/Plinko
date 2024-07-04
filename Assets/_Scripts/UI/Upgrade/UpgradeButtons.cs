using LOGIC.UPGRADES;
using UnityEngine;
using VContainer;

namespace UI
{
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField] private UpgradeButton _ballSpawnMachineUpgradeButton;
        [SerializeField] private UpgradeButton _speedSpawnUpgradeButton;

        [Inject] private Upgrades _upgrades;

        public UpgradeButton BallSpawnMachineUpgradeButton => _ballSpawnMachineUpgradeButton;
        public UpgradeButton SpeedSpawnUpgradeButton => _speedSpawnUpgradeButton;

        #region MONO

        private void Awake()
        {
            UpgradeData data = _upgrades.GetSpawnBallMachinesUpgradeData();

            string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
            _ballSpawnMachineUpgradeButton.UpdateButton(targetText);
            
            _ballSpawnMachineUpgradeButton.Button.onClick
                .AddListener(() =>
                {
                    _upgrades.TryUpgrade(Upgrades.SPAWN_BALL_MACHINES_UPGRADE);
                    UpgradeData data = _upgrades.GetSpawnBallMachinesUpgradeData();

                    string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
                    _ballSpawnMachineUpgradeButton.UpdateButton(targetText);
                });
            
            
            data = _upgrades.GetSpawnBallSpeedUpgradeData();

            targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
            _speedSpawnUpgradeButton.UpdateButton(targetText);
            _speedSpawnUpgradeButton.Button.onClick
                .AddListener(() =>
                {
                    _upgrades.TryUpgrade(Upgrades.SPAWN_BALL_SPEED_UPGRADE);
                    
                    UpgradeData data = _upgrades.GetSpawnBallSpeedUpgradeData();

                    string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
                    _speedSpawnUpgradeButton.UpdateButton(targetText);
                });
        }

        #endregion
    }
}