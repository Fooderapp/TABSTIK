using UnityEngine;
using System.Collections.Generic;

namespace TABSTIK.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        public bool autoStartRound = true;
        public float roundStartDelay = 3f;

        [Header("Battlefield Settings")]
        public Transform redSpawnArea;
        public Transform blueSpawnArea;
        public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);

        private int redScore = 0;
        private int blueScore = 0;
        private bool roundInProgress = false;
        private List<Units.Unit> activeUnits = new List<Units.Unit>();

        public int RedScore => redScore;
        public int BlueScore => blueScore;
        public bool RoundInProgress => roundInProgress;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (autoStartRound)
            {
                Invoke(nameof(StartNewRound), roundStartDelay);
            }
        }

        public void StartNewRound()
        {
            roundInProgress = true;
            activeUnits.Clear();
            Debug.Log("Round started!");
        }

        public void RegisterUnit(Units.Unit unit)
        {
            if (!activeUnits.Contains(unit))
            {
                activeUnits.Add(unit);
            }
        }

        public void OnUnitDied(Units.Unit unit)
        {
            if (activeUnits.Contains(unit))
            {
                activeUnits.Remove(unit);
            }

            CheckRoundEnd();
        }

        private void CheckRoundEnd()
        {
            if (!roundInProgress) return;

            int redUnits = 0;
            int blueUnits = 0;

            foreach (Units.Unit unit in activeUnits)
            {
                if (unit != null && unit.IsAlive)
                {
                    if (unit.team == Units.Team.Red)
                        redUnits++;
                    else
                        blueUnits++;
                }
            }

            // Check if one team is eliminated
            if (redUnits == 0 && blueUnits > 0)
            {
                EndRound(Units.Team.Blue);
            }
            else if (blueUnits == 0 && redUnits > 0)
            {
                EndRound(Units.Team.Red);
            }
        }

        private void EndRound(Units.Team winner)
        {
            roundInProgress = false;

            if (winner == Units.Team.Red)
            {
                redScore++;
                Debug.Log($"Red Team wins! Score: Red {redScore} - Blue {blueScore}");
            }
            else
            {
                blueScore++;
                Debug.Log($"Blue Team wins! Score: Red {redScore} - Blue {blueScore}");
            }

            // Update UI
            UI.UIManager uiManager = FindObjectOfType<UI.UIManager>();
            if (uiManager != null)
            {
                uiManager.UpdateScoreboard(redScore, blueScore);
            }

            // Reset battlefield and start new round
            Invoke(nameof(ResetBattlefield), 3f);
        }

        private void ResetBattlefield()
        {
            // Destroy all remaining units
            foreach (Units.Unit unit in activeUnits)
            {
                if (unit != null)
                {
                    Destroy(unit.gameObject);
                }
            }
            activeUnits.Clear();

            // Start new round
            if (autoStartRound)
            {
                Invoke(nameof(StartNewRound), roundStartDelay);
            }
        }

        public Vector3 GetSpawnPosition(Units.Team team)
        {
            Transform spawnArea = team == Units.Team.Red ? redSpawnArea : blueSpawnArea;
            
            if (spawnArea == null)
            {
                // Fallback positions
                return team == Units.Team.Red ? new Vector3(-20f, 0f, 0f) : new Vector3(20f, 0f, 0f);
            }

            // Random position within spawn area
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0f,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            return spawnArea.position + randomOffset;
        }

        public void AddScore(Units.Team team, int points)
        {
            if (team == Units.Team.Red)
                redScore += points;
            else
                blueScore += points;

            UI.UIManager uiManager = FindObjectOfType<UI.UIManager>();
            if (uiManager != null)
            {
                uiManager.UpdateScoreboard(redScore, blueScore);
            }
        }
    }
}
