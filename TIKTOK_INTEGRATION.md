# TikTok Integration Guide

This guide explains how to integrate TABSTIK with a live TikTok stream.

## Overview

The TABSTIK project includes a mock TikTok API for testing. To connect to a real TikTok live stream, you need to implement the `ITikTokAPI` interface with an actual TikTok API client.

## Architecture

```
TikTok Live Stream
        ↓
   TikTok API
        ↓
ITikTokAPI Implementation
        ↓
TikTokEventListener
        ↓
   UnitSpawner
        ↓
  Battle Units
```

## Implementation Options

### Option 1: TikTok-Live-Connector (Recommended)

TikTok-Live-Connector is an unofficial Node.js library for connecting to TikTok live streams.

**Repository**: https://github.com/zerodytrash/TikTok-Live-Connector

#### Setup Steps:

1. **Create a Node.js Bridge Server**:
   ```javascript
   // server.js
   const { WebcastPushConnection } = require('tiktok-live-connector');
   const WebSocket = require('ws');

   const wss = new WebSocket.Server({ port: 8080 });
   let tiktokConnection = null;

   wss.on('connection', (ws) => {
       console.log('Unity client connected');

       ws.on('message', (message) => {
           const data = JSON.parse(message);
           
           if (data.action === 'connect') {
               connectToTikTok(data.roomId, ws);
           } else if (data.action === 'disconnect') {
               disconnectFromTikTok();
           }
       });
   });

   function connectToTikTok(roomId, ws) {
       tiktokConnection = new WebcastPushConnection(roomId);

       tiktokConnection.connect().then(state => {
           console.log(`Connected to ${state.roomId}`);
           ws.send(JSON.stringify({ event: 'connected', roomId: state.roomId }));
       }).catch(err => {
           console.error('Failed to connect', err);
           ws.send(JSON.stringify({ event: 'error', message: err.message }));
       });

       // Listen for comments
       tiktokConnection.on('chat', data => {
           ws.send(JSON.stringify({
               event: 'comment',
               username: data.uniqueId,
               message: data.comment
           }));
       });

       // Listen for gifts
       tiktokConnection.on('gift', data => {
           ws.send(JSON.stringify({
               event: 'gift',
               username: data.uniqueId,
               giftType: mapGiftType(data.giftId),
               giftCount: data.repeatCount
           }));
       });
   }

   function mapGiftType(giftId) {
       // Map TikTok gift IDs to your game's gift types
       const mapping = {
           5655: 'Rose',
           5269: 'TikTokLogo',
           // Add more mappings
       };
       return mapping[giftId] || 'Rose';
   }

   function disconnectFromTikTok() {
       if (tiktokConnection) {
           tiktokConnection.disconnect();
           tiktokConnection = null;
       }
   }
   ```

2. **Install Dependencies**:
   ```bash
   npm install tiktok-live-connector ws
   ```

3. **Run the Server**:
   ```bash
   node server.js
   ```

4. **Implement Unity WebSocket Client**:

Create `Assets/Scripts/TikTokIntegration/WebSocketTikTokAPI.cs`:

```csharp
using UnityEngine;
using System;
using System.Collections;
using WebSocketSharp;
using Newtonsoft.Json.Linq;

namespace TABSTIK.TikTokIntegration
{
    public class WebSocketTikTokAPI : ITikTokAPI
    {
        private WebSocket ws;
        private bool isConnected = false;

        public bool IsConnected => isConnected;
        public event Action<TikTokEventData> OnEvent;

        public void Connect(string roomId)
        {
            ws = new WebSocket("ws://localhost:8080");

            ws.OnOpen += (sender, e) =>
            {
                Debug.Log("WebSocket connected");
                
                // Send connect command
                var connectMsg = new
                {
                    action = "connect",
                    roomId = roomId
                };
                ws.Send(JsonUtility.ToJson(connectMsg));
            };

            ws.OnMessage += (sender, e) =>
            {
                HandleMessage(e.Data);
            };

            ws.OnError += (sender, e) =>
            {
                Debug.LogError($"WebSocket error: {e.Message}");
                isConnected = false;
            };

            ws.OnClose += (sender, e) =>
            {
                Debug.Log("WebSocket closed");
                isConnected = false;
            };

            ws.Connect();
        }

        private void HandleMessage(string jsonData)
        {
            JObject data = JObject.Parse(jsonData);
            string eventType = data["event"].ToString();

            switch (eventType)
            {
                case "connected":
                    isConnected = true;
                    Debug.Log($"Connected to TikTok room: {data["roomId"]}");
                    break;

                case "comment":
                    OnEvent?.Invoke(new TikTokEventData
                    {
                        eventType = TikTokEventType.Comment,
                        username = data["username"].ToString(),
                        message = data["message"].ToString(),
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    });
                    break;

                case "gift":
                    TikTokGiftType giftType = ParseGiftType(data["giftType"].ToString());
                    OnEvent?.Invoke(new TikTokEventData
                    {
                        eventType = TikTokEventType.Gift,
                        username = data["username"].ToString(),
                        giftType = giftType,
                        giftCount = int.Parse(data["giftCount"].ToString()),
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    });
                    break;
            }
        }

        private TikTokGiftType ParseGiftType(string giftName)
        {
            if (Enum.TryParse(giftName, out TikTokGiftType result))
            {
                return result;
            }
            return TikTokGiftType.Rose; // Default
        }

        public void Disconnect()
        {
            if (ws != null)
            {
                var disconnectMsg = new { action = "disconnect" };
                ws.Send(JsonUtility.ToJson(disconnectMsg));
                ws.Close();
                ws = null;
            }
            isConnected = false;
        }
    }
}
```

5. **Update TikTokEventListener**:

In `TikTokEventListener.InitializeAPI()`:
```csharp
private void InitializeAPI()
{
    if (useMockAPI)
    {
        tiktokAPI = new MockTikTokAPI();
    }
    else
    {
        tiktokAPI = new WebSocketTikTokAPI();
    }

    tiktokAPI.OnEvent += HandleTikTokEvent;
}
```

### Option 2: TikTok Official API

TikTok provides official APIs, but they require:
- Business account
- API access approval
- OAuth authentication

**Documentation**: https://developers.tiktok.com/

This is more complex but more reliable for production use.

### Option 3: Third-Party Services

Several services offer TikTok live integration:
- **Streamlabs**: https://streamlabs.com/
- **StreamElements**: https://streamelements.com/
- **TikFinity**: https://tikfinity.zerody.one/

These typically provide webhooks or APIs that you can integrate with.

## Gift ID Mapping

Each TikTok gift has a unique ID. You need to map these to your game's unit types.

### Common TikTok Gift IDs:

| Gift Name | Gift ID | Suggested Unit |
|-----------|---------|----------------|
| Rose | 5655 | Soldier |
| TikTok | 5269 | Archer |
| Hearts | 5487 | Soldier |
| Finger Heart | 5509 | Soldier |
| Perfume | 5658 | Archer |
| Concert | 5760 | Tank |
| Corgi | 6426 | Wacky |
| Fireworks | 6064 | Multiple |

*Note: Gift IDs may change. Test with actual stream to confirm.*

## Testing Integration

### 1. Test with Mock API First
```csharp
TikTokEventListener.Instance.SimulateComment("TestUser", "Hello!");
TikTokEventListener.Instance.SimulateGift("TestUser", TikTokGiftType.Rose);
```

### 2. Test Bridge Server
```bash
# In separate terminal
node server.js

# In Unity, set:
useMockAPI = false
tiktokRoomId = "@yourusername"  // or unique_id
```

### 3. Test with Real Stream
- Start a TikTok live stream
- Connect TABSTIK
- Have friends send comments/gifts
- Monitor Unity console for events

## Debugging

### Enable Detailed Logging

In `TikTokEventListener.cs`, add:
```csharp
private void HandleTikTokEvent(TikTokEventData eventData)
{
    Debug.Log($"[TikTok Event] Type: {eventData.eventType}, User: {eventData.username}");
    
    // ... rest of code
}
```

### Common Issues

**WebSocket Won't Connect**
- Check firewall settings
- Ensure Node.js server is running
- Verify WebSocket port (8080)

**No Events Received**
- Confirm TikTok username is correct
- Check if stream is actually live
- Review Node.js server logs

**Wrong Units Spawning**
- Verify gift ID mapping
- Log received gift IDs
- Update mapping dictionary

## Security Considerations

1. **Rate Limiting**: Implement spawn limits to prevent spam
   ```csharp
   private float lastSpawnTime;
   private float spawnCooldown = 0.5f; // Min time between spawns

   if (Time.time - lastSpawnTime < spawnCooldown) return;
   ```

2. **User Validation**: Filter banned users
   ```csharp
   private HashSet<string> bannedUsers = new HashSet<string>();
   
   if (bannedUsers.Contains(eventData.username)) return;
   ```

3. **Gift Value Limits**: Cap expensive gifts
   ```csharp
   if (eventData.giftCount > 10)
   {
       eventData.giftCount = 10; // Cap at 10
   }
   ```

## Performance Optimization

### Batch Spawning
```csharp
private Queue<Action> spawnQueue = new Queue<Action>();
private int maxSpawnsPerFrame = 5;

void Update()
{
    int spawned = 0;
    while (spawnQueue.Count > 0 && spawned < maxSpawnsPerFrame)
    {
        spawnQueue.Dequeue()?.Invoke();
        spawned++;
    }
}
```

### Event Throttling
```csharp
private Dictionary<string, float> userLastEventTime = new Dictionary<string, float>();
private float userEventCooldown = 2f; // 2 seconds between events per user

bool CanUserTriggerEvent(string username)
{
    if (userLastEventTime.TryGetValue(username, out float lastTime))
    {
        if (Time.time - lastTime < userEventCooldown)
            return false;
    }
    userLastEventTime[username] = Time.time;
    return true;
}
```

## Advanced Features

### Custom Gift Reactions
```csharp
// Spawn special effects for expensive gifts
if (eventData.giftType == TikTokGiftType.Fireworks)
{
    SpawnFireworksEffect();
    SpawnMultipleRandomUnits(5);
}
```

### Viewer Integration
```csharp
// Thank viewers by name
public void HandleGift(TikTokEventData eventData)
{
    uiManager.ShowNotification($"Thank you {eventData.username} for {eventData.giftType}!");
    SpawnUnitForGift(eventData);
}
```

### Leaderboard
```csharp
private Dictionary<string, int> userGiftCounts = new Dictionary<string, int>();

public void TrackGift(string username)
{
    if (!userGiftCounts.ContainsKey(username))
        userGiftCounts[username] = 0;
    userGiftCounts[username]++;
}
```

## Resources

- TikTok-Live-Connector: https://github.com/zerodytrash/TikTok-Live-Connector
- TikTok Developer Portal: https://developers.tiktok.com/
- WebSocket-Sharp (Unity): https://github.com/sta/websocket-sharp
- TikFinity (TikTok Live Client): https://tikfinity.zerody.one/

## Support

For integration issues:
1. Check Node.js server logs
2. Review Unity console
3. Test with mock API first
4. Verify TikTok stream is live

---

**Note**: TikTok's API and gift IDs may change. Always test with current live streams and update mappings as needed.
