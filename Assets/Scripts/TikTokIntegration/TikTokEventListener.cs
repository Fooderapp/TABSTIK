using UnityEngine;
using System;
using System.Collections.Generic;

namespace TABSTIK.TikTokIntegration
{
    /// <summary>
    /// Manages TikTok event listening and unit spawning
    /// </summary>
    public class TikTokEventListener : MonoBehaviour
    {
        public static TikTokEventListener Instance { get; private set; }

        [Header("TikTok Settings")]
        public string tiktokRoomId = "test_room";
        public bool useMockAPI = true;
        public bool autoConnect = true;

        [Header("Team Assignment")]
        public bool alternateTeams = true; // Alternate spawns between teams
        public Units.Team defaultTeam = Units.Team.Red;

        [Header("Gift to Unit Mapping")]
        public bool enableGiftSpawning = true;
        public bool enableCommentSpawning = true;

        private ITikTokAPI tiktokAPI;
        private Units.Team nextSpawnTeam;
        private Queue<Action> spawnQueue = new Queue<Action>();

        private Dictionary<TikTokGiftType, Units.UnitType> giftToUnitMap = new Dictionary<TikTokGiftType, Units.UnitType>
        {
            { TikTokGiftType.Rose, Units.UnitType.Soldier },
            { TikTokGiftType.TikTokLogo, Units.UnitType.Archer },
            { TikTokGiftType.Drama, Units.UnitType.Tank },
            { TikTokGiftType.Lion, Units.UnitType.Wacky }
        };

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            nextSpawnTeam = defaultTeam;
        }

        private void Start()
        {
            InitializeAPI();

            if (autoConnect)
            {
                Connect();
            }
        }

        private void Update()
        {
            // Process spawn queue on main thread
            while (spawnQueue.Count > 0)
            {
                Action spawnAction = spawnQueue.Dequeue();
                spawnAction?.Invoke();
            }
        }

        private void InitializeAPI()
        {
            if (useMockAPI)
            {
                tiktokAPI = new MockTikTokAPI();
            }
            else
            {
                // TODO: Initialize actual TikTok API
                // tiktokAPI = new RealTikTokAPI();
                Debug.LogWarning("Real TikTok API not implemented, falling back to mock");
                tiktokAPI = new MockTikTokAPI();
            }

            tiktokAPI.OnEvent += HandleTikTokEvent;
        }

        public void Connect()
        {
            if (tiktokAPI != null)
            {
                tiktokAPI.Connect(tiktokRoomId);
                Debug.Log($"Connected to TikTok room: {tiktokRoomId}");
            }
        }

        public void Disconnect()
        {
            if (tiktokAPI != null)
            {
                tiktokAPI.Disconnect();
                Debug.Log("Disconnected from TikTok");
            }
        }

        private void HandleTikTokEvent(TikTokEventData eventData)
        {
            switch (eventData.eventType)
            {
                case TikTokEventType.Comment:
                    if (enableCommentSpawning)
                        HandleComment(eventData);
                    break;

                case TikTokEventType.Gift:
                    if (enableGiftSpawning)
                        HandleGift(eventData);
                    break;
            }
        }

        private void HandleComment(TikTokEventData eventData)
        {
            Debug.Log($"Comment from {eventData.username}: {eventData.message}");

            // Spawn a basic soldier for each comment
            spawnQueue.Enqueue(() =>
            {
                Units.Team team = GetNextTeam();
                Managers.UnitSpawner.Instance?.SpawnUnit(Units.UnitType.Soldier, team);
                
                UI.UIManager uiManager = FindObjectOfType<UI.UIManager>();
                uiManager?.ShowNotification($"{eventData.username} spawned a soldier!");
            });
        }

        private void HandleGift(TikTokEventData eventData)
        {
            Debug.Log($"Gift from {eventData.username}: {eventData.giftType} x{eventData.giftCount}");

            // Map gift to unit type
            Units.UnitType unitType = Units.UnitType.Soldier;
            if (giftToUnitMap.TryGetValue(eventData.giftType, out Units.UnitType mappedType))
            {
                unitType = mappedType;
            }

            // Handle special case: Fireworks spawns multiple units
            int spawnCount = eventData.giftCount;
            if (eventData.giftType == TikTokGiftType.Fireworks)
            {
                spawnCount = 5; // Spawn 5 random units
            }

            // Queue spawning
            for (int i = 0; i < spawnCount; i++)
            {
                Units.UnitType finalUnitType = unitType;
                if (eventData.giftType == TikTokGiftType.Fireworks)
                {
                    // Random unit for fireworks
                    finalUnitType = (Units.UnitType)UnityEngine.Random.Range(0, 4);
                }

                spawnQueue.Enqueue(() =>
                {
                    Units.Team team = GetNextTeam();
                    Managers.UnitSpawner.Instance?.SpawnUnit(finalUnitType, team);
                });
            }

            // Show notification
            spawnQueue.Enqueue(() =>
            {
                UI.UIManager uiManager = FindObjectOfType<UI.UIManager>();
                uiManager?.ShowNotification($"{eventData.username} sent {eventData.giftType}!");
            });
        }

        private Units.Team GetNextTeam()
        {
            if (!alternateTeams)
            {
                return defaultTeam;
            }

            Units.Team team = nextSpawnTeam;
            nextSpawnTeam = (nextSpawnTeam == Units.Team.Red) ? Units.Team.Blue : Units.Team.Red;
            return team;
        }

        // Public methods for manual testing
        public void SimulateComment(string username, string message)
        {
            if (tiktokAPI is MockTikTokAPI mockAPI)
            {
                mockAPI.SimulateComment(username, message);
            }
        }

        public void SimulateGift(string username, TikTokGiftType giftType, int count = 1)
        {
            if (tiktokAPI is MockTikTokAPI mockAPI)
            {
                mockAPI.SimulateGift(username, giftType, count);
            }
        }

        private void OnDestroy()
        {
            if (tiktokAPI != null)
            {
                tiktokAPI.OnEvent -= HandleTikTokEvent;
                Disconnect();
            }
        }
    }
}
