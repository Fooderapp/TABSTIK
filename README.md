# TABSTIK - TikTok Battle Simulator

A fully autonomous 3D battle simulation game inspired by Totally Accurate Battle Simulator (TABS), where units are spawned dynamically based on TikTok live interactions.

## 🎮 Overview

TABSTIK is a physics-based battle simulator that integrates with TikTok live streams. Viewers can spawn units on the battlefield through:
- **Comments**: Each comment spawns a basic soldier
- **Gifts**: Different gift types spawn special units (archers, tanks, wacky units)

The game features AI-controlled units that automatically fight in team-based battles (Red vs Blue), with a scoring system that tracks wins across multiple rounds.

## ✨ Features

### Core Gameplay
- **3D Physics-Based Combat**: Ragdoll physics and realistic unit interactions
- **Autonomous AI**: Units automatically find targets, move, and attack
- **Dynamic Unit Spawning**: Real-time spawning based on TikTok interactions
- **Team-Based Battles**: Red Team vs Blue Team
- **Round System**: Automatic round end detection and battlefield reset
- **Scoring**: Persistent score tracking across rounds

### Unit Types
1. **Soldier** (Basic Unit)
   - Balanced stats
   - Melee combat
   - Spawned by comments or Rose gift

2. **Archer** (Ranged Unit)
   - Long-range projectile attacks
   - Lower health
   - Spawned by TikTok Logo gift

3. **Tank** (Heavy Unit)
   - High health and armor
   - Slower movement
   - High damage output
   - Spawned by Drama gift

4. **Wacky Unit** (Chaos Unit)
   - Unpredictable behavior
   - Random jumps and spins
   - Spawned by Lion gift

### TikTok Integration
- Real-time event listening for comments and gifts
- Mock API for testing without live connection
- Configurable gift-to-unit mapping
- Team assignment options (alternating or fixed)

### UI System
- Live scoreboard (Red vs Blue)
- Unit counter for each team
- Event notifications
- Debug spawn controls for testing
- Connection status indicator

## 📁 Project Structure

```
TABSTIK/
├── Assets/
│   ├── Scripts/
│   │   ├── Units/
│   │   │   ├── Unit.cs                 # Base unit class
│   │   │   ├── UnitStats.cs            # ScriptableObject for unit stats
│   │   │   ├── ArcherUnit.cs           # Archer with ranged attacks
│   │   │   └── SpecialUnits.cs         # Tank and Wacky units
│   │   ├── AI/
│   │   │   └── UnitAI.cs               # Combat AI and targeting
│   │   ├── Managers/
│   │   │   ├── GameManager.cs          # Round and score management
│   │   │   └── UnitSpawner.cs          # Unit spawning system
│   │   ├── TikTokIntegration/
│   │   │   ├── TikTokAPI.cs            # API interface and mock
│   │   │   └── TikTokEventListener.cs  # Event handling and spawning
│   │   └── UI/
│   │       ├── UIManager.cs            # Main UI controller
│   │       └── DebugSpawnUI.cs         # Debug controls
│   ├── Prefabs/                        # Unit prefabs
│   ├── Materials/                      # Team materials
│   ├── Scenes/                         # Game scenes
│   └── Resources/                      # Runtime resources
└── ProjectSettings/                    # Unity project settings
```

## 🚀 Getting Started

### Prerequisites
- Unity 2020.3 LTS or newer
- TextMeshPro package (usually included)
- Basic understanding of Unity Editor

### Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Fooderapp/TABSTIK.git
   cd TABSTIK
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the TABSTIK folder
   - Open the project

3. **Create the Scene**
   - Create a new scene or use the default scene
   - Add the following GameObjects:
     - **GameManager**: Empty GameObject with `GameManager` script
     - **UnitSpawner**: Empty GameObject with `UnitSpawner` script
     - **TikTokEventListener**: Empty GameObject with `TikTokEventListener` script
     - **UIManager**: Empty GameObject with `UIManager` script
     - **Main Camera**: Position at (0, 10, -20) looking at battlefield
     - **Directional Light**: For scene lighting
     - **Plane**: Ground plane scaled to (10, 1, 10)

4. **Configure Spawn Areas**
   - Create two empty GameObjects as spawn areas
   - Assign to GameManager's `redSpawnArea` and `blueSpawnArea`
   - Position them on opposite sides of the battlefield

5. **Create Unit Prefabs**
   - The UnitSpawner can auto-generate basic cube units
   - Or create custom 3D models for better visuals

6. **Setup UI**
   - Create Canvas with UI elements
   - Assign UI references in UIManager
   - Add TextMeshPro components for scores and counters

### Testing Without TikTok

The project includes a mock TikTok API for testing:

1. Ensure `TikTokEventListener` has `useMockAPI = true`
2. Use the `DebugSpawnUI` component to simulate events
3. Or call these methods from code:
   ```csharp
   TikTokEventListener.Instance.SimulateComment("TestUser", "Hello!");
   TikTokEventListener.Instance.SimulateGift("TestUser", TikTokGiftType.Rose);
   ```

## 🔧 Configuration

### Game Settings (GameManager)
- `autoStartRound`: Automatically start rounds
- `roundStartDelay`: Delay before round starts
- `spawnAreaSize`: Size of spawn zones

### AI Settings (UnitAI)
- `detectionRange`: How far units can detect enemies
- `updateInterval`: How often AI updates (performance)

### TikTok Settings (TikTokEventListener)
- `useMockAPI`: Use mock API for testing
- `alternateTeams`: Alternate spawns between teams
- `enableGiftSpawning`: Enable gift-based spawning
- `enableCommentSpawning`: Enable comment-based spawning

### Unit Stats (Unit.cs)
Each unit has configurable stats:
- `maxHealth`: Starting health
- `attackDamage`: Damage per attack
- `attackRange`: Attack distance
- `attackCooldown`: Time between attacks
- `moveSpeed`: Movement speed

## 🎯 TikTok Integration

### Live Integration (Coming Soon)

To integrate with a real TikTok live stream, you'll need:

1. **TikTok Live API Access** or use a third-party service like:
   - TikTok-Live-Connector
   - TikTok Live API wrappers

2. **Implement ITikTokAPI Interface**
   ```csharp
   public class RealTikTokAPI : ITikTokAPI
   {
       // Implement Connect, Disconnect, and event handling
   }
   ```

3. **Update TikTokEventListener**
   - Set `useMockAPI = false`
   - Initialize your real API implementation

### Gift Mapping

Current gift-to-unit mapping:
| Gift Type | Value | Unit Spawned |
|-----------|-------|--------------|
| Rose | 1 | Soldier |
| TikTok Logo | 10 | Archer |
| Drama | 50 | Tank |
| Lion | 100 | Wacky |
| Fireworks | 500 | 5 Random Units |

Customize in `TikTokEventListener.giftToUnitMap`

## 🎮 How to Play

1. **Start the Game**: Press Play in Unity Editor
2. **Watch the Battle**: Units spawn and fight automatically
3. **Spawn Units**: 
   - Via TikTok interactions (when connected)
   - Via Debug UI buttons
   - Via code/console commands
4. **Round Ends**: When one team is eliminated
5. **Score Updates**: Winning team gets +1 point
6. **Battlefield Resets**: New round begins automatically

## 🛠️ Development

### Adding New Unit Types

1. Create a new unit class inheriting from `Unit`:
   ```csharp
   public class MageUnit : Unit
   {
       // Custom behavior
   }
   ```

2. Add to `UnitType` enum in `Unit.cs`

3. Create prefab and add to `UnitSpawner`

4. Map to TikTok gift in `TikTokEventListener`

### Customizing AI Behavior

Modify `UnitAI.cs` to change:
- Target selection logic
- Movement patterns
- Attack behavior
- Formation tactics

### Extending TikTok Events

Add new event types in `TikTokAPI.cs`:
```csharp
public enum TikTokEventType
{
    Comment,
    Gift,
    Like,
    Follow,
    Share  // New event type
}
```

## 📊 Performance Considerations

- **Unit Limit**: Monitor performance with high unit counts
- **AI Update Frequency**: Adjust `updateInterval` for performance
- **Physics Quality**: Configure in Project Settings > Physics
- **Graphics**: Adjust quality settings for streaming

## 🎥 Streaming Setup

Recommended settings for streaming:
- **Resolution**: 1920x1080 (Full HD)
- **Build for Streaming**: Use Standalone builds
- **OBS Integration**: Capture game window
- **UI Overlay**: Enable scoreboard and notifications

## 🐛 Troubleshooting

### Units Not Spawning
- Check UnitSpawner has prefabs assigned
- Verify spawn areas are configured in GameManager
- Ensure TikTokEventListener is active

### AI Not Working
- Verify UnitAI component is attached to units
- Check detection range is sufficient
- Ensure units are on different teams

### TikTok Connection Issues
- Verify API credentials (when using real API)
- Check useMockAPI setting
- Review console for connection errors

## 🤝 Contributing

Contributions are welcome! Areas for improvement:
- Better 3D models and animations
- More unit types and abilities
- Enhanced AI behaviors
- Real TikTok API integration
- Multiplayer support
- Replay system

## 📝 License

This project is provided as-is for educational and entertainment purposes.

## 🙏 Acknowledgments

- Inspired by Totally Accurate Battle Simulator (TABS)
- Built for interactive live streaming
- Community-driven development

## 📧 Contact

For questions or support, please open an issue on GitHub.

---

**Note**: This is a prototype/framework. Full 3D models, animations, and live TikTok integration require additional development.