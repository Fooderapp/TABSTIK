using UnityEngine;
using System.Collections.Generic;

namespace TABSTIK.Managers
{
    public class UnitSpawner : MonoBehaviour
    {
        public static UnitSpawner Instance { get; private set; }

        [Header("Unit Prefabs")]
        public GameObject soldierPrefab;
        public GameObject archerPrefab;
        public GameObject tankPrefab;
        public GameObject wackyPrefab;

        [Header("Materials")]
        public Material redTeamMaterial;
        public Material blueTeamMaterial;

        private Dictionary<Units.UnitType, GameObject> unitPrefabs;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            InitializePrefabs();
        }

        private void InitializePrefabs()
        {
            unitPrefabs = new Dictionary<Units.UnitType, GameObject>
            {
                { Units.UnitType.Soldier, soldierPrefab },
                { Units.UnitType.Archer, archerPrefab },
                { Units.UnitType.Tank, tankPrefab },
                { Units.UnitType.Wacky, wackyPrefab }
            };
        }

        public Units.Unit SpawnUnit(Units.UnitType unitType, Units.Team team)
        {
            GameObject prefab = GetPrefabForType(unitType);
            if (prefab == null)
            {
                Debug.LogWarning($"No prefab found for unit type: {unitType}");
                return null;
            }

            Vector3 spawnPosition = GameManager.Instance.GetSpawnPosition(team);
            GameObject unitObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

            Units.Unit unit = unitObject.GetComponent<Units.Unit>();
            if (unit != null)
            {
                unit.team = team;
                unit.unitType = unitType;
                unit.redTeamMaterial = redTeamMaterial;
                unit.blueTeamMaterial = blueTeamMaterial;

                // Register with game manager
                GameManager.Instance.RegisterUnit(unit);

                Debug.Log($"Spawned {unitType} for {team} team at {spawnPosition}");
            }

            return unit;
        }

        public void SpawnMultipleUnits(Units.UnitType unitType, Units.Team team, int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnUnit(unitType, team);
            }
        }

        private GameObject GetPrefabForType(Units.UnitType unitType)
        {
            if (unitPrefabs.TryGetValue(unitType, out GameObject prefab))
            {
                return prefab;
            }

            // Fallback to soldier if prefab not found
            return soldierPrefab;
        }

        public GameObject CreateDefaultUnitPrefab(Units.UnitType unitType)
        {
            // Create a simple cube-based unit if no prefab exists
            GameObject unitObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            unitObj.name = $"{unitType}_Unit";

            // Add physics
            Rigidbody rb = unitObj.GetComponent<Rigidbody>();
            if (rb == null) rb = unitObj.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.drag = 2f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            // Add appropriate unit component
            switch (unitType)
            {
                case Units.UnitType.Soldier:
                    unitObj.AddComponent<Units.Unit>();
                    break;
                case Units.UnitType.Archer:
                    unitObj.AddComponent<Units.ArcherUnit>();
                    break;
                case Units.UnitType.Tank:
                    unitObj.AddComponent<Units.TankUnit>();
                    unitObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    break;
                case Units.UnitType.Wacky:
                    unitObj.AddComponent<Units.WackyUnit>();
                    break;
            }

            // Add AI component
            unitObj.AddComponent<AI.UnitAI>();

            return unitObj;
        }
    }
}
