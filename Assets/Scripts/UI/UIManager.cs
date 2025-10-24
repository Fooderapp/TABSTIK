using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace TABSTIK.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("Scoreboard")]
        public TextMeshProUGUI redScoreText;
        public TextMeshProUGUI blueScoreText;
        public TextMeshProUGUI roundStatusText;

        [Header("Unit Counter")]
        public TextMeshProUGUI redUnitCountText;
        public TextMeshProUGUI blueUnitCountText;

        [Header("Notifications")]
        public GameObject notificationPanel;
        public TextMeshProUGUI notificationText;
        public float notificationDuration = 3f;

        [Header("TikTok Connection")]
        public Image connectionStatusIndicator;
        public Color connectedColor = Color.green;
        public Color disconnectedColor = Color.red;

        private Coroutine notificationCoroutine;

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
            UpdateScoreboard(0, 0);
            if (notificationPanel != null)
            {
                notificationPanel.SetActive(false);
            }
        }

        private void Update()
        {
            UpdateUnitCounts();
            UpdateConnectionStatus();
        }

        public void UpdateScoreboard(int redScore, int blueScore)
        {
            if (redScoreText != null)
            {
                redScoreText.text = $"Red: {redScore}";
            }

            if (blueScoreText != null)
            {
                blueScoreText.text = $"Blue: {blueScore}";
            }
        }

        private void UpdateUnitCounts()
        {
            int redCount = 0;
            int blueCount = 0;

            Units.Unit[] allUnits = FindObjectsOfType<Units.Unit>();
            foreach (Units.Unit unit in allUnits)
            {
                if (unit.IsAlive)
                {
                    if (unit.team == Units.Team.Red)
                        redCount++;
                    else
                        blueCount++;
                }
            }

            if (redUnitCountText != null)
            {
                redUnitCountText.text = $"Units: {redCount}";
            }

            if (blueUnitCountText != null)
            {
                blueUnitCountText.text = $"Units: {blueCount}";
            }
        }

        public void ShowNotification(string message)
        {
            if (notificationPanel == null || notificationText == null) return;

            if (notificationCoroutine != null)
            {
                StopCoroutine(notificationCoroutine);
            }

            notificationCoroutine = StartCoroutine(ShowNotificationCoroutine(message));
        }

        private IEnumerator ShowNotificationCoroutine(string message)
        {
            notificationText.text = message;
            notificationPanel.SetActive(true);

            yield return new WaitForSeconds(notificationDuration);

            notificationPanel.SetActive(false);
            notificationCoroutine = null;
        }

        private void UpdateConnectionStatus()
        {
            if (connectionStatusIndicator == null) return;

            TikTokIntegration.TikTokEventListener listener = TikTokIntegration.TikTokEventListener.Instance;
            if (listener != null)
            {
                // Connection status would be checked here
                connectionStatusIndicator.color = connectedColor;
            }
            else
            {
                connectionStatusIndicator.color = disconnectedColor;
            }
        }

        public void UpdateRoundStatus(string status)
        {
            if (roundStatusText != null)
            {
                roundStatusText.text = status;
            }
        }
    }
}
