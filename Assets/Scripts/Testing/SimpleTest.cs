using UnityEngine;

namespace TABSTIK.Testing
{
    /// <summary>
    /// Simple test script to validate TABSTIK functionality
    /// Attach to any GameObject in the scene to run tests
    /// </summary>
    public class SimpleTest : MonoBehaviour
    {
        [Header("Test Controls")]
        public bool runTestOnStart = false;
        public KeyCode spawnRedSoldierKey = KeyCode.Alpha1;
        public KeyCode spawnBlueSoldierKey = KeyCode.Alpha2;
        public KeyCode spawnRedArcherKey = KeyCode.Alpha3;
        public KeyCode spawnBlueArcherKey = KeyCode.Alpha4;
        public KeyCode simulateCommentKey = KeyCode.C;
        public KeyCode simulateGiftKey = KeyCode.G;

        private void Start()
        {
            if (runTestOnStart)
            {
                RunBasicTests();
            }

            PrintInstructions();
        }

        private void Update()
        {
            HandleKeyboardInput();
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(spawnRedSoldierKey))
            {
                SpawnUnit(Units.UnitType.Soldier, Units.Team.Red);
            }
            else if (Input.GetKeyDown(spawnBlueSoldierKey))
            {
                SpawnUnit(Units.UnitType.Soldier, Units.Team.Blue);
            }
            else if (Input.GetKeyDown(spawnRedArcherKey))
            {
                SpawnUnit(Units.UnitType.Archer, Units.Team.Red);
            }
            else if (Input.GetKeyDown(spawnBlueArcherKey))
            {
                SpawnUnit(Units.UnitType.Archer, Units.Team.Blue);
            }
            else if (Input.GetKeyDown(simulateCommentKey))
            {
                SimulateComment();
            }
            else if (Input.GetKeyDown(simulateGiftKey))
            {
                SimulateGift();
            }
        }

        private void RunBasicTests()
        {
            Debug.Log("=== TABSTIK Basic Tests ===");

            // Test 1: Check managers exist
            Debug.Log("Test 1: Checking for managers...");
            bool managersExist = CheckManagers();
            Debug.Log($"Test 1: {(managersExist ? "PASSED" : "FAILED")} - Managers {(managersExist ? "found" : "missing")}");

            // Test 2: Spawn units
            Debug.Log("Test 2: Spawning test units...");
            bool spawnSuccess = TestUnitSpawning();
            Debug.Log($"Test 2: {(spawnSuccess ? "PASSED" : "FAILED")} - Unit spawning");

            // Test 3: Test TikTok integration
            Debug.Log("Test 3: Testing TikTok event simulation...");
            bool tiktokSuccess = TestTikTokEvents();
            Debug.Log($"Test 3: {(tiktokSuccess ? "PASSED" : "FAILED")} - TikTok events");

            Debug.Log("=== Tests Complete ===");
        }

        private bool CheckManagers()
        {
            bool gameManagerExists = Managers.GameManager.Instance != null;
            bool unitSpawnerExists = Managers.UnitSpawner.Instance != null;
            bool tiktokListenerExists = TikTokIntegration.TikTokEventListener.Instance != null;

            Debug.Log($"GameManager: {(gameManagerExists ? "✓" : "✗")}");
            Debug.Log($"UnitSpawner: {(unitSpawnerExists ? "✓" : "✗")}");
            Debug.Log($"TikTokEventListener: {(tiktokListenerExists ? "✓" : "✗")}");

            return gameManagerExists && unitSpawnerExists && tiktokListenerExists;
        }

        private bool TestUnitSpawning()
        {
            if (Managers.UnitSpawner.Instance == null)
            {
                Debug.LogWarning("UnitSpawner not found in scene!");
                return false;
            }

            try
            {
                // Spawn a soldier for each team
                Units.Unit redUnit = Managers.UnitSpawner.Instance.SpawnUnit(Units.UnitType.Soldier, Units.Team.Red);
                Units.Unit blueUnit = Managers.UnitSpawner.Instance.SpawnUnit(Units.UnitType.Soldier, Units.Team.Blue);

                bool success = redUnit != null && blueUnit != null;
                if (success)
                {
                    Debug.Log($"Successfully spawned units: Red={redUnit.name}, Blue={blueUnit.name}");
                }
                return success;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Exception during unit spawning: {e.Message}");
                return false;
            }
        }

        private bool TestTikTokEvents()
        {
            if (TikTokIntegration.TikTokEventListener.Instance == null)
            {
                Debug.LogWarning("TikTokEventListener not found in scene!");
                return false;
            }

            try
            {
                // Simulate events
                TikTokIntegration.TikTokEventListener.Instance.SimulateComment("TestUser", "Test comment");
                TikTokIntegration.TikTokEventListener.Instance.SimulateGift("TestUser", TikTokIntegration.TikTokGiftType.Rose, 1);

                Debug.Log("TikTok events simulated successfully");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Exception during TikTok event simulation: {e.Message}");
                return false;
            }
        }

        private void SpawnUnit(Units.UnitType unitType, Units.Team team)
        {
            if (Managers.UnitSpawner.Instance != null)
            {
                Managers.UnitSpawner.Instance.SpawnUnit(unitType, team);
                Debug.Log($"Spawned {unitType} for {team} team");
            }
            else
            {
                Debug.LogWarning("UnitSpawner not found!");
            }
        }

        private void SimulateComment()
        {
            if (TikTokIntegration.TikTokEventListener.Instance != null)
            {
                string[] usernames = { "TestUser1", "TestUser2", "TestUser3", "Viewer123" };
                string username = usernames[Random.Range(0, usernames.Length)];
                TikTokIntegration.TikTokEventListener.Instance.SimulateComment(username, "Test comment from " + username);
                Debug.Log($"Simulated comment from {username}");
            }
            else
            {
                Debug.LogWarning("TikTokEventListener not found!");
            }
        }

        private void SimulateGift()
        {
            if (TikTokIntegration.TikTokEventListener.Instance != null)
            {
                string[] usernames = { "Gifter1", "Gifter2", "GenerousViewer" };
                TikTokIntegration.TikTokGiftType[] gifts = {
                    TikTokIntegration.TikTokGiftType.Rose,
                    TikTokIntegration.TikTokGiftType.TikTokLogo,
                    TikTokIntegration.TikTokGiftType.Drama
                };

                string username = usernames[Random.Range(0, usernames.Length)];
                TikTokIntegration.TikTokGiftType gift = gifts[Random.Range(0, gifts.Length)];

                TikTokIntegration.TikTokEventListener.Instance.SimulateGift(username, gift, 1);
                Debug.Log($"Simulated {gift} gift from {username}");
            }
            else
            {
                Debug.LogWarning("TikTokEventListener not found!");
            }
        }

        private void PrintInstructions()
        {
            Debug.Log("=== TABSTIK Test Controls ===");
            Debug.Log($"Press {spawnRedSoldierKey} to spawn Red Soldier");
            Debug.Log($"Press {spawnBlueSoldierKey} to spawn Blue Soldier");
            Debug.Log($"Press {spawnRedArcherKey} to spawn Red Archer");
            Debug.Log($"Press {spawnBlueArcherKey} to spawn Blue Archer");
            Debug.Log($"Press {simulateCommentKey} to simulate TikTok comment");
            Debug.Log($"Press {simulateGiftKey} to simulate TikTok gift");
            Debug.Log("===========================");
        }
    }
}
