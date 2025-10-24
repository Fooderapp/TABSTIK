using UnityEngine;
using System;
using System.Collections.Generic;

namespace TABSTIK.TikTokIntegration
{
    /// <summary>
    /// TikTok event types
    /// </summary>
    public enum TikTokEventType
    {
        Comment,
        Gift,
        Like,
        Follow
    }

    /// <summary>
    /// TikTok gift types mapped to unit types
    /// </summary>
    public enum TikTokGiftType
    {
        Rose = 1,           // Basic soldier
        TikTokLogo = 10,    // Archer
        Drama = 50,         // Tank
        Lion = 100,         // Wacky unit
        Fireworks = 500     // Multiple units
    }

    /// <summary>
    /// Event data structure for TikTok events
    /// </summary>
    [Serializable]
    public class TikTokEventData
    {
        public TikTokEventType eventType;
        public string username;
        public string message;
        public TikTokGiftType giftType;
        public int giftCount;
        public long timestamp;
    }

    /// <summary>
    /// Interface for TikTok API integration
    /// Implement this with actual TikTok API or third-party service
    /// </summary>
    public interface ITikTokAPI
    {
        void Connect(string roomId);
        void Disconnect();
        bool IsConnected { get; }
        event Action<TikTokEventData> OnEvent;
    }

    /// <summary>
    /// Mock TikTok API for testing without live connection
    /// </summary>
    public class MockTikTokAPI : ITikTokAPI
    {
        public bool IsConnected { get; private set; }
        public event Action<TikTokEventData> OnEvent;

        public void Connect(string roomId)
        {
            IsConnected = true;
            Debug.Log($"Mock TikTok API connected to room: {roomId}");
        }

        public void Disconnect()
        {
            IsConnected = false;
            Debug.Log("Mock TikTok API disconnected");
        }

        // For testing: Simulate TikTok events
        public void SimulateComment(string username, string message)
        {
            OnEvent?.Invoke(new TikTokEventData
            {
                eventType = TikTokEventType.Comment,
                username = username,
                message = message,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
        }

        public void SimulateGift(string username, TikTokGiftType giftType, int count = 1)
        {
            OnEvent?.Invoke(new TikTokEventData
            {
                eventType = TikTokEventType.Gift,
                username = username,
                giftType = giftType,
                giftCount = count,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
        }
    }
}
