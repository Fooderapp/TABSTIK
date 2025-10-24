# TABSTIK Project Documentation

## Architecture Overview

The TABSTIK project is organized into modular systems that work together to create an autonomous 3D battle simulator with TikTok integration.

## System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     TikTok Live Stream                       │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│              TikTok Integration Layer                        │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ TikTokAPI    │  │ Event        │  │ Mock API     │      │
│  │ Interface    │  │ Listener     │  │ (Testing)    │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│                    Manager Layer                             │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ Game         │  │ Unit         │  │ UI           │      │
│  │ Manager      │  │ Spawner      │  │ Manager      │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│                    Game Objects Layer                        │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ Units        │  │ AI           │  │ Physics      │      │
│  │ (Prefabs)    │  │ Controllers  │  │ System       │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└─────────────────────────────────────────────────────────────┘
```

## Core Modules

### 1. Unit System (`Assets/Scripts/Units/`)

**Purpose**: Manages unit behavior, stats, and lifecycle.

**Components**:
- `Unit.cs` - Base unit class with health, combat, and movement
- `UnitStats.cs` - ScriptableObject for configurable unit stats
- `ArcherUnit.cs` - Ranged combat specialist
- `SpecialUnits.cs` - Tank and Wacky unit implementations

**Key Features**:
- Health management with damage and death
- Attack system with cooldowns
- Team-based identification (Red vs Blue)
- Physics-based movement
- Visual team color application

**Unit Types**:
1. **Soldier**: Balanced melee fighter
2. **Archer**: Ranged projectile attacks
3. **Tank**: High health, slow, heavy damage
4. **Wacky**: Unpredictable chaos unit

### 2. AI System (`Assets/Scripts/AI/`)

**Purpose**: Autonomous unit behavior and combat decision-making.

**Components**:
- `UnitAI.cs` - Combat AI with targeting and behavior

**Key Features**:
- Automatic enemy detection within range
- Target prioritization (closest enemy)
- Movement toward targets
- Attack execution when in range
- Configurable update intervals for performance
- Visual debugging with Gizmos

**AI Behavior Loop**:
1. Find nearest enemy (periodic update)
2. If enemy in attack range → Stop and attack
3. If enemy beyond attack range → Move toward enemy
4. If no enemy → Stop moving

### 3. Manager System (`Assets/Scripts/Managers/`)

**Purpose**: High-level game flow and system coordination.

**Components**:
- `GameManager.cs` - Round management, scoring, and game flow
- `UnitSpawner.cs` - Unit instantiation and prefab management

**GameManager Responsibilities**:
- Round state management
- Score tracking for both teams
- Unit registration and lifecycle
- Round end detection
- Battlefield reset
- Spawn position calculation

**UnitSpawner Responsibilities**:
- Unit prefab management
- Dynamic unit creation
- Team color application
- Prefab auto-generation for testing

### 4. TikTok Integration (`Assets/Scripts/TikTokIntegration/`)

**Purpose**: Connect TikTok live events to game actions.

**Components**:
- `TikTokAPI.cs` - API interface and mock implementation
- `TikTokEventListener.cs` - Event handling and unit spawning

**Event Flow**:
```
TikTok Event → API → Event Listener → Spawn Queue → Unit Spawner → Unit
```

**Supported Events**:
- Comments → Spawn basic soldiers
- Gifts → Spawn special units based on gift type
- Customizable gift-to-unit mapping

**Mock API Features**:
- Testing without live connection
- Simulated comments and gifts
- Debug methods for manual triggering

### 5. UI System (`Assets/Scripts/UI/`)

**Purpose**: Visual feedback and user interaction.

**Components**:
- `UIManager.cs` - Main UI controller
- `DebugSpawnUI.cs` - Debug controls for testing

**UI Elements**:
- Real-time scoreboard
- Unit counters for each team
- Event notifications
- Connection status
- Debug spawn buttons

### 6. Testing System (`Assets/Scripts/Testing/`)

**Purpose**: Validation and testing tools.

**Components**:
- `SimpleTest.cs` - Keyboard-driven test interface

**Test Features**:
- Manager verification
- Unit spawning tests
- TikTok event simulation
- Keyboard shortcuts for quick testing

## Data Flow

### Unit Spawning Flow
```
TikTok Event
    ↓
TikTokEventListener.HandleTikTokEvent()
    ↓
Add to spawnQueue
    ↓
Update() processes queue
    ↓
UnitSpawner.SpawnUnit()
    ↓
Instantiate prefab
    ↓
GameManager.RegisterUnit()
    ↓
Unit enters battle
```

### Combat Flow
```
UnitAI.Update()
    ↓
FindNewTarget() (periodic)
    ↓
Target found?
    ↓ Yes
ExecuteCombatBehavior()
    ↓
In range? → Attack
Not in range? → Move
    ↓
Unit.Attack() or Unit.MoveTo()
```

### Round End Flow
```
Unit.Die()
    ↓
GameManager.OnUnitDied()
    ↓
Remove from active units
    ↓
CheckRoundEnd()
    ↓
One team eliminated?
    ↓ Yes
EndRound(winner)
    ↓
Update score
    ↓
ResetBattlefield()
    ↓
StartNewRound()
```

## Configuration

### Unit Configuration
```csharp
// In Unit prefab or UnitStats ScriptableObject
maxHealth = 100f;
attackDamage = 10f;
attackRange = 2f;
attackCooldown = 1f;
moveSpeed = 3f;
```

### AI Configuration
```csharp
// In UnitAI component
detectionRange = 15f;     // How far to detect enemies
updateInterval = 0.5f;    // How often to update (performance)
```

### Game Configuration
```csharp
// In GameManager
autoStartRound = true;
roundStartDelay = 3f;
spawnAreaSize = new Vector3(10f, 0f, 10f);
```

### TikTok Configuration
```csharp
// In TikTokEventListener
useMockAPI = true;              // Use mock for testing
alternateTeams = true;          // Alternate red/blue spawns
enableCommentSpawning = true;   // Comments spawn units
enableGiftSpawning = true;      // Gifts spawn units
```

## Extension Points

### Adding New Unit Types

1. Create new unit class:
```csharp
public class MageUnit : Unit
{
    // Custom behavior
}
```

2. Add to `UnitType` enum in `Unit.cs`
3. Create prefab
4. Add to UnitSpawner prefab dictionary
5. Map to TikTok gift in TikTokEventListener

### Adding New AI Behaviors

Modify `UnitAI.cs`:
```csharp
private void ExecuteCombatBehavior()
{
    // Add custom behavior logic
    // Example: Formation tactics, retreating, etc.
}
```

### Adding New TikTok Events

1. Add to `TikTokEventType` enum
2. Handle in `TikTokEventListener.HandleTikTokEvent()`
3. Implement in your `ITikTokAPI` implementation

### Custom UI Elements

Extend `UIManager.cs`:
```csharp
public void ShowCustomMessage(string message, Color color)
{
    // Custom UI logic
}
```

## Performance Considerations

### Optimization Strategies

1. **AI Update Intervals**: Increase `updateInterval` for large unit counts
2. **Object Pooling**: Reuse destroyed units instead of instantiating
3. **Spatial Partitioning**: Use for efficient enemy detection
4. **Batch Spawning**: Limit spawns per frame
5. **LOD System**: Reduce detail for distant units

### Performance Targets

| Unit Count | Target FPS | Expected RAM |
|------------|------------|--------------|
| 10 units   | 60 FPS     | < 500 MB     |
| 30 units   | 60 FPS     | < 1 GB       |
| 50 units   | 30-60 FPS  | < 2 GB       |
| 100 units  | 30 FPS     | < 3 GB       |

## Security Considerations

### Rate Limiting
Prevent spawn spam:
```csharp
private float spawnCooldown = 0.5f;
private float lastSpawnTime;
```

### User Filtering
Block banned users:
```csharp
private HashSet<string> bannedUsers;
if (bannedUsers.Contains(username)) return;
```

### Input Validation
Validate gift counts:
```csharp
giftCount = Mathf.Clamp(giftCount, 1, maxGiftsPerEvent);
```

## Debugging

### Console Logging
All systems log to Unity Console:
- Info: Normal operations
- Warnings: Non-critical issues
- Errors: Critical problems

### Visual Debugging
- Gizmos in Scene view show:
  - AI detection ranges
  - Attack ranges
  - Target connections

### Test Mode
Use `SimpleTest.cs` for:
- Manager verification
- Unit spawning tests
- Event simulation

## Common Issues and Solutions

### Issue: Units don't spawn
**Solution**: Check that UnitSpawner prefabs are assigned or auto-generation is enabled

### Issue: Units don't fight
**Solution**: Verify units are on different teams and have attack damage > 0

### Issue: Poor performance
**Solution**: Increase AI update intervals, reduce unit count, optimize prefabs

### Issue: UI not updating
**Solution**: Ensure all UI references are assigned in UIManager

## Future Enhancements

### Planned Features
- Advanced unit abilities
- Power-ups and items
- Multiple battlefield types
- Replay system
- Spectator camera modes
- Enhanced animations
- Sound effects and music
- Leaderboards
- Tournament mode

### Integration Opportunities
- Twitch integration
- YouTube Live integration
- Discord bot integration
- Web dashboard
- Mobile app for spawning

## Resources

### Unity Documentation
- Physics: https://docs.unity3d.com/Manual/PhysicsSection.html
- UI: https://docs.unity3d.com/Packages/com.unity.ugui@latest
- Scripting: https://docs.unity3d.com/Manual/ScriptingSection.html

### Third-Party Libraries
- TikTok-Live-Connector: https://github.com/zerodytrash/TikTok-Live-Connector
- WebSocket-Sharp: https://github.com/sta/websocket-sharp

### Community
- Unity Forums: https://forum.unity.com/
- Unity Discord: https://discord.gg/unity

---

**Last Updated**: 2025-10-24
**Version**: 1.0.0
**License**: See repository LICENSE file
