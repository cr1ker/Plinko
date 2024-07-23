using CORE.AUDIO;
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
            UpdateBallMachineButton();
            
            _ballSpawnMachineUpgradeButton.Button.onClick
                .AddListener(() =>
                {
                    var isUpgraded = _upgrades.TryUpgrade(Upgrades.SPAWN_BALL_MACHINES_UPGRADE);
                    
                    if (isUpgraded)
                    {
                        AudioService.Singleton.PlayAudioOnce(AudioTypes.UpgradeSound);
                    }
                    UpgradeData data = _upgrades.GetSpawnBallMachinesUpgradeData();

                    string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
                    _ballSpawnMachineUpgradeButton.UpdateButton(targetText);
                });

            UpdateSpeedSpawnUpgrade();
            
            _speedSpawnUpgradeButton.Button.onClick
                .AddListener(() =>
                {
                    var isUpgraded = _upgrades.TryUpgrade(Upgrades.SPAWN_BALL_SPEED_UPGRADE);
                    
                    if (isUpgraded)
                    {
                        AudioService.Singleton.PlayAudioOnce(AudioTypes.UpgradeSound);
                    }

                    
                    UpgradeData data = _upgrades.GetSpawnBallSpeedUpgradeData();

                    string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
                    _speedSpawnUpgradeButton.UpdateButton(targetText);
                });
        }

        #endregion

        private void UpdateBallMachineButton()
        {
            UpgradeData data = _upgrades.GetSpawnBallMachinesUpgradeData();

            string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
            _ballSpawnMachineUpgradeButton.UpdateButton(targetText);
        }

        private void UpdateSpeedSpawnUpgrade()
        {
            UpgradeData data = _upgrades.GetSpawnBallSpeedUpgradeData();

            string targetText = data.IsMaxLevel ? "MAX LEVEL" : data.UpgradePrice.ToString();
                    
            _speedSpawnUpgradeButton.UpdateButton(targetText);
        }

        #region CALLBACKS

        public void OnCompleteLevel()
        {
            UpdateBallMachineButton();
            UpdateSpeedSpawnUpgrade();
        }

        #endregion
    }
}