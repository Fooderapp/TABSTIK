# Example Scene Configuration

This document provides a quick reference for setting up a battle scene in Unity.

## Scene Hierarchy Example

```
BattleScene
├── GameManager (Empty GameObject)
│   └── GameManager (Component)
│       ├── Red Spawn Area: RedSpawnArea
│       ├── Blue Spawn Area: BlueSpawnArea
│       └── Settings: Auto Start Round ✓
│
├── UnitSpawner (Empty GameObject)
│   └── UnitSpawner (Component)
│       ├── Soldier Prefab: (Assign or auto-generate)
│       ├── Archer Prefab: (Assign or auto-generate)
│       ├── Tank Prefab: (Assign or auto-generate)
│       ├── Wacky Prefab: (Assign or auto-generate)
│       ├── Red Team Material: RedMaterial
│       └── Blue Team Material: BlueMaterial
│
├── TikTokEventListener (Empty GameObject)
│   └── TikTokEventListener (Component)
│       ├── Use Mock API: ✓
│       ├── Auto Connect: ✓
│       ├── Alternate Teams: ✓
│       └── Enable Comment/Gift Spawning: ✓
│
├── RedSpawnArea (Empty GameObject)
│   └── Position: (-20, 0, 0)
│
├── BlueSpawnArea (Empty GameObject)
│   └── Position: (20, 0, 0)
│
├── Environment
│   ├── Ground (Plane)
│   │   ├── Position: (0, 0, 0)
│   │   └── Scale: (10, 1, 10)
│   │
│   └── Directional Light
│       ├── Rotation: (50, -30, 0)
│       └── Intensity: 1
│
├── Main Camera
│   ├── Position: (0, 15, -25)
│   ├── Rotation: (30, 0, 0)
│   └── Projection: Perspective
│
└── Canvas
    ├── UI Manager (Component)
    │   ├── Red Score Text: RedScoreText
    │   ├── Blue Score Text: BlueScoreText
    │   ├── Red Unit Count: RedUnitCountText
    │   ├── Blue Unit Count: BlueUnitCountText
    │   ├── Notification Panel: NotificationPanel
    │   └── Notification Text: NotificationText
    │
    ├── Canvas Scaler
    │   ├── UI Scale Mode: Scale With Screen Size
    │   └── Reference Resolution: 1920 x 1080
    │
    ├── Scoreboard
    │   ├── RedScoreText (TextMeshPro)
    │   │   ├── Position: (-800, 450)
    │   │   ├── Font Size: 48
    │   │   ├── Color: Red
    │   │   └── Text: "Red: 0"
    │   │
    │   └── BlueScoreText (TextMeshPro)
    │       ├── Position: (800, 450)
    │       ├── Font Size: 48
    │       ├── Color: Blue
    │       └── Text: "Blue: 0"
    │
    ├── Unit Counters
    │   ├── RedUnitCountText (TextMeshPro)
    │   │   ├── Position: (-800, 380)
    │   │   ├── Font Size: 32
    │   │   └── Text: "Units: 0"
    │   │
    │   └── BlueUnitCountText (TextMeshPro)
    │       ├── Position: (800, 380)
    │       ├── Font Size: 32
    │       └── Text: "Units: 0"
    │
    └── NotificationPanel (Panel)
        ├── Position: (0, 0)
        ├── Size: (800, 100)
        ├── Active: False (disabled by default)
        └── NotificationText (TextMeshPro)
            ├── Alignment: Center
            ├── Font Size: 36
            └── Color: White
```

## Component Settings Quick Reference

### GameManager
```
Auto Start Round: True
Round Start Delay: 3
Spawn Area Size: (10, 0, 10)
Red Spawn Area: Link to RedSpawnArea GameObject
Blue Spawn Area: Link to BlueSpawnArea GameObject
```

### UnitSpawner
```
Soldier Prefab: Optional (auto-generates if null)
Archer Prefab: Optional (auto-generates if null)
Tank Prefab: Optional (auto-generates if null)
Wacky Prefab: Optional (auto-generates if null)
Red Team Material: RedMaterial (Create in Assets/Materials)
Blue Team Material: BlueMaterial (Create in Assets/Materials)
```

### TikTokEventListener
```
TikTok Room ID: "test_room" (or actual room ID)
Use Mock API: True (for testing)
Auto Connect: True
Alternate Teams: True (spawns alternate between red/blue)
Default Team: Red (used if Alternate Teams = False)
Enable Gift Spawning: True
Enable Comment Spawning: True
```

### Unit (Component on unit prefabs)
```
Unit Type: Set appropriately (Soldier/Archer/Tank/Wacky)
Team: Set at runtime by spawner
Max Health: 100 (Soldier/Archer), 300 (Tank), 80 (Wacky)
Attack Damage: 10 (Soldier), 8 (Archer), 25 (Tank), 15 (Wacky)
Attack Range: 2 (Soldier), 10 (Archer), 2 (Tank), 3 (Wacky)
Attack Cooldown: 1
Move Speed: 3 (Soldier), 3.5 (Archer), 1.5 (Tank), 4 (Wacky)
```

### UnitAI (Component on unit prefabs)
```
Detection Range: 15
Update Interval: 0.5 (lower = more responsive, higher CPU)
```

### UIManager
```
Red Score Text: Link to RedScoreText TextMeshPro
Blue Score Text: Link to BlueScoreText TextMeshPro
Red Unit Count Text: Link to RedUnitCountText TextMeshPro
Blue Unit Count Text: Link to BlueUnitCountText TextMeshPro
Notification Panel: Link to NotificationPanel GameObject
Notification Text: Link to NotificationText TextMeshPro
Notification Duration: 3 seconds
Connection Status Indicator: Optional
Connected Color: Green
Disconnected Color: Red
```

## Material Setup

### Red Team Material
```
Shader: Standard
Albedo Color: RGB(255, 0, 0) - Pure Red
Metallic: 0
Smoothness: 0.5
```

### Blue Team Material
```
Shader: Standard
Albedo Color: RGB(0, 0, 255) - Pure Blue
Metallic: 0
Smoothness: 0.5
```

## Camera Settings

### For Small Battles (< 20 units)
```
Position: (0, 10, -15)
Rotation: (30, 0, 0)
Field of View: 60
```

### For Large Battles (> 20 units)
```
Position: (0, 20, -30)
Rotation: (35, 0, 0)
Field of View: 70
```

### Top-Down View
```
Position: (0, 40, 0)
Rotation: (90, 0, 0)
Field of View: 60
```

## Testing Checklist

- [ ] All manager GameObjects present in scene
- [ ] Spawn areas positioned correctly
- [ ] UI elements connected to UIManager
- [ ] Materials created and assigned
- [ ] Ground plane has collider
- [ ] Camera positioned for good view
- [ ] Lighting adequate
- [ ] All scripts compile without errors
- [ ] Console shows no errors on Play
- [ ] Units spawn when test methods called
- [ ] Units move toward enemies
- [ ] Units attack enemies
- [ ] UI updates with scores and counts
- [ ] Round ends when one team eliminated
- [ ] Battlefield resets after round

## Quick Test Commands

In Unity Console or test script:
```csharp
// Spawn units manually
TABSTIK.Managers.UnitSpawner.Instance.SpawnUnit(TABSTIK.Units.UnitType.Soldier, TABSTIK.Units.Team.Red);
TABSTIK.Managers.UnitSpawner.Instance.SpawnUnit(TABSTIK.Units.UnitType.Soldier, TABSTIK.Units.Team.Blue);

// Simulate TikTok events
TABSTIK.TikTokIntegration.TikTokEventListener.Instance.SimulateComment("Player1", "Spawn!");
TABSTIK.TikTokIntegration.TikTokEventListener.Instance.SimulateGift("Player2", TABSTIK.TikTokIntegration.TikTokGiftType.Rose, 1);

// Spawn multiple units for testing
for (int i = 0; i < 5; i++)
{
    TABSTIK.Managers.UnitSpawner.Instance.SpawnUnit(TABSTIK.Units.UnitType.Soldier, TABSTIK.Units.Team.Red);
    TABSTIK.Managers.UnitSpawner.Instance.SpawnUnit(TABSTIK.Units.UnitType.Soldier, TABSTIK.Units.Team.Blue);
}
```

## Performance Targets

### Minimum Specs
- 60 FPS with 20 units
- < 100ms input latency
- < 1GB RAM usage

### Recommended Specs
- 60 FPS with 50 units
- < 50ms input latency
- < 2GB RAM usage

## Optimization Tips

1. **Reduce AI update frequency** for large unit counts
2. **Use object pooling** for frequently spawned units
3. **Limit particle effects** on lower-end systems
4. **Use LOD (Level of Detail)** for unit models
5. **Batch UI updates** instead of per-frame
6. **Cull off-screen units** from AI updates

## Troubleshooting

### Units spawn but don't move
- Check Rigidbody isn't Kinematic
- Verify UnitAI component is attached
- Ensure Detection Range > 0

### Units move but don't attack
- Verify units are on different teams
- Check Attack Range setting
- Ensure Attack Damage > 0

### UI doesn't update
- Verify all text components assigned in UIManager
- Check Canvas render mode
- Ensure EventSystem exists

### Round doesn't end
- Check GameManager is registered with units
- Verify OnUnitDied is being called
- Ensure CheckRoundEnd logic is correct

---

This example configuration provides a solid foundation for the TABSTIK battle simulator.
