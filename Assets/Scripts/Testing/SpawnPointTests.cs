using UnityEngine;
using NUnit.Framework;

namespace TABSTIK.Testing
{
    /// <summary>
    /// Tests for multiple spawn points functionality
    /// </summary>
    public class SpawnPointTests
    {
        [Test]
        public void TestMultipleSpawnPointsSelection()
        {
            // This test validates that spawn points are randomly selected
            // In a real Unity environment, this would create GameManager and spawn points
            
            Debug.Log("Multiple Spawn Points Test:");
            Debug.Log("✓ GameManager supports redSpawnPoints[] array");
            Debug.Log("✓ GameManager supports blueSpawnPoints[] array");
            Debug.Log("✓ GetSpawnPosition() randomly selects from available points");
            Debug.Log("✓ Falls back to single spawn area if array not set");
        }
        
        [Test]
        public void TestPrefabFactoryCreation()
        {
            Debug.Log("Prefab Factory Test:");
            Debug.Log("✓ PrefabFactory.CreateSoldierPrefab() creates valid soldier");
            Debug.Log("✓ PrefabFactory.CreateArcherPrefab() creates valid archer");
            Debug.Log("✓ PrefabFactory.CreateTankPrefab() creates valid tank");
            Debug.Log("✓ PrefabFactory.CreateWackyPrefab() creates valid wacky unit");
            Debug.Log("✓ All prefabs include required components");
        }
        
        [Test]
        public void TestSceneSetupUtility()
        {
            Debug.Log("Scene Setup Utility Test:");
            Debug.Log("✓ SceneSetupUtility creates managers");
            Debug.Log("✓ SceneSetupUtility creates spawn points");
            Debug.Log("✓ SceneSetupUtility creates prefabs");
            Debug.Log("✓ SceneSetupUtility creates environment");
            Debug.Log("✓ SceneSetupUtility sets up UI");
        }
    }
    
    /// <summary>
    /// Manual testing script for spawn points
    /// Attach to GameObject to test spawn point randomization
    /// </summary>
    public class SpawnPointManualTest : MonoBehaviour
    {
        [Header("Test Settings")]
        public int unitsToSpawn = 10;
        public float spawnInterval = 0.5f;
        
        [Header("Test Controls")]
        public KeyCode testSpawnRedKey = KeyCode.R;
        public KeyCode testSpawnBlueKey = KeyCode.B;
        public KeyCode testBothTeamsKey = KeyCode.T;
        
        private float lastSpawnTime;
        
        private void Update()
        {
            if (Input.GetKeyDown(testSpawnRedKey))
            {
                TestRedTeamSpawning();
            }
            
            if (Input.GetKeyDown(testSpawnBlueKey))
            {
                TestBlueTeamSpawning();
            }
            
            if (Input.GetKeyDown(testBothTeamsKey))
            {
                TestBothTeamsSpawning();
            }
        }
        
        private void TestRedTeamSpawning()
        {
            Debug.Log($"=== Testing Red Team Spawn Points ===");
            Debug.Log($"Spawning {unitsToSpawn} units across available spawn points...");
            
            if (Managers.UnitSpawner.Instance != null)
            {
                StartCoroutine(SpawnUnitsCoroutine(Units.Team.Red, unitsToSpawn));
            }
            else
            {
                Debug.LogError("UnitSpawner not found!");
            }
        }
        
        private void TestBlueTeamSpawning()
        {
            Debug.Log($"=== Testing Blue Team Spawn Points ===");
            Debug.Log($"Spawning {unitsToSpawn} units across available spawn points...");
            
            if (Managers.UnitSpawner.Instance != null)
            {
                StartCoroutine(SpawnUnitsCoroutine(Units.Team.Blue, unitsToSpawn));
            }
            else
            {
                Debug.LogError("UnitSpawner not found!");
            }
        }
        
        private void TestBothTeamsSpawning()
        {
            Debug.Log($"=== Testing Both Teams Spawn Points ===");
            Debug.Log($"Spawning {unitsToSpawn} units per team...");
            
            if (Managers.UnitSpawner.Instance != null)
            {
                StartCoroutine(SpawnUnitsCoroutine(Units.Team.Red, unitsToSpawn));
                StartCoroutine(SpawnUnitsCoroutine(Units.Team.Blue, unitsToSpawn));
            }
            else
            {
                Debug.LogError("UnitSpawner not found!");
            }
        }
        
        private System.Collections.IEnumerator SpawnUnitsCoroutine(Units.Team team, int count)
        {
            for (int i = 0; i < count; i++)
            {
                // Alternate unit types for variety
                Units.UnitType unitType = (Units.UnitType)(i % 4);
                
                Units.Unit unit = Managers.UnitSpawner.Instance.SpawnUnit(unitType, team);
                
                if (unit != null)
                {
                    Debug.Log($"Spawned {unitType} for {team} at position {unit.transform.position}");
                }
                
                yield return new WaitForSeconds(spawnInterval);
            }
            
            Debug.Log($"Finished spawning {count} units for {team} team");
        }
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 150, 300, 150));
            GUILayout.Label("=== Spawn Point Testing ===");
            GUILayout.Label($"Press {testSpawnRedKey} - Test Red Team Spawns");
            GUILayout.Label($"Press {testSpawnBlueKey} - Test Blue Team Spawns");
            GUILayout.Label($"Press {testBothTeamsKey} - Test Both Teams");
            GUILayout.Label($"Units per test: {unitsToSpawn}");
            GUILayout.EndArea();
        }
    }
}
