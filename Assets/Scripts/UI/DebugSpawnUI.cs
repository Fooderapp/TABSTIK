using UnityEngine;
using UnityEngine.UI;

namespace TABSTIK.UI
{
    /// <summary>
    /// Debug UI for testing without TikTok connection
    /// </summary>
    public class DebugSpawnUI : MonoBehaviour
    {
        [Header("Spawn Buttons")]
        public Button spawnRedSoldierButton;
        public Button spawnBlueSoldierButton;
        public Button spawnRedArcherButton;
        public Button spawnBlueArcherButton;
        public Button spawnRedTankButton;
        public Button spawnBlueTankButton;

        [Header("TikTok Simulation")]
        public Button simulateCommentButton;
        public Button simulateGiftButton;
        public InputField usernameInput;

        private void Start()
        {
            SetupButtons();
        }

        private void SetupButtons()
        {
            if (spawnRedSoldierButton != null)
                spawnRedSoldierButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Soldier, Units.Team.Red));

            if (spawnBlueSoldierButton != null)
                spawnBlueSoldierButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Soldier, Units.Team.Blue));

            if (spawnRedArcherButton != null)
                spawnRedArcherButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Archer, Units.Team.Red));

            if (spawnBlueArcherButton != null)
                spawnBlueArcherButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Archer, Units.Team.Blue));

            if (spawnRedTankButton != null)
                spawnRedTankButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Tank, Units.Team.Red));

            if (spawnBlueTankButton != null)
                spawnBlueTankButton.onClick.AddListener(() => SpawnUnit(Units.UnitType.Tank, Units.Team.Blue));

            if (simulateCommentButton != null)
                simulateCommentButton.onClick.AddListener(SimulateComment);

            if (simulateGiftButton != null)
                simulateGiftButton.onClick.AddListener(SimulateGift);
        }

        private void SpawnUnit(Units.UnitType unitType, Units.Team team)
        {
            Managers.UnitSpawner.Instance?.SpawnUnit(unitType, team);
        }

        private void SimulateComment()
        {
            string username = usernameInput != null ? usernameInput.text : "TestUser";
            TikTokIntegration.TikTokEventListener.Instance?.SimulateComment(username, "Test comment");
        }

        private void SimulateGift()
        {
            string username = usernameInput != null ? usernameInput.text : "TestUser";
            TikTokIntegration.TikTokEventListener.Instance?.SimulateGift(
                username, 
                TikTokIntegration.TikTokGiftType.Rose, 
                1
            );
        }
    }
}
