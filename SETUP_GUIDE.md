# TABSTIK Scene Setup Guide

## Automated Scene Setup

This project now includes an automated scene setup utility that creates everything you need to get started with TABSTIK.

### Quick Setup (Recommended)

1. **Create a new scene** in Unity or open an existing one
2. **Create an empty GameObject** in the scene hierarchy
3. **Add the `SceneSetupUtility` component** to this GameObject
4. **Configure the options** in the Inspector:
   - `Spawn Points Per Team`: Number of spawn locations for each team (default: 3)
   - `Spawn Point Spacing`: Distance between spawn points (default: 10)
   - `Create Prefabs`: Automatically create unit prefabs (recommended: true)
   - `Setup UI`: Create UI canvas and elements (recommended: true)
   - `Add Test Script`: Add testing controls (recommended: true)
5. **Click the "Setup Scene" button** in the Inspector
6. **Press Play** to test the scene!

### What Gets Created

The automated setup creates:

#### 1. Manager GameObjects
- **GameManager**: Handles game rounds, scoring, and battlefield management
- **UnitSpawner**: Spawns units and manages prefabs
- **TikTokEventListener**: Handles TikTok integration (configured for mock API testing)

#### 2. Multiple Spawn Points
- **Red Team Spawn Points**: Array of spawn locations for red team units
- **Blue Team Spawn Points**: Array of spawn locations for blue team units
- Spawn points are positioned in an arc formation on opposite sides of the battlefield
- Units will randomly spawn at one of the available spawn points

#### 3. Unit Prefabs
- **Soldier**: Basic melee unit (cube-based body)
- **Archer**: Ranged unit (capsule-based body with bow)
- **Tank**: Heavy unit (larger, bulkier design)
- **Wacky**: Chaotic unit (unusual proportions)
- **Projectile**: Arrow/projectile for ranged attacks

Each prefab includes:
- 3D model with simple geometry
- Rigidbody for physics
- Colliders
- Unit component with stats
- UnitAI component for autonomous behavior

#### 4. Team Materials
- **RedTeamMaterial**: Red colored material for red team units
- **BlueTeamMaterial**: Blue colored material for blue team units

#### 5. Environment
- **Ground**: Large plane for the battlefield
- **Directional Light**: Lighting for the scene

#### 6. Camera
- Positioned and angled for optimal battlefield view
- Configured for 3D gameplay

#### 7. UI System
- **Canvas**: UI overlay for scoreboard and notifications
- **EventSystem**: For UI interaction
- **UIManager**: Component for managing UI updates

#### 8. Test Controller
- **SimpleTest**: Component for testing unit spawning via keyboard
- Press numeric keys to spawn units
- Press C to simulate comments, G to simulate gifts

## Multiple Spawn Points Feature

### How It Works

When multiple spawn points are configured:
- Units will spawn at a **randomly selected spawn point**
- Each team can have a different number of spawn points
- Spawn points can be positioned anywhere in the scene
- A small random offset is applied within the spawn area around each point

### Configuration

The GameManager now supports two spawn modes:

1. **Single Spawn Area** (legacy):
   - Set `redSpawnArea` and `blueSpawnArea` to Transform references
   - Units spawn randomly within the area defined by `spawnAreaSize`

2. **Multiple Spawn Points** (new):
   - Set `redSpawnPoints[]` and `blueSpawnPoints[]` arrays in GameManager
   - Leave at least one point assigned for each team
   - Units will randomly select one of the points and spawn near it

### Customizing Spawn Points

You can manually adjust spawn points:
1. Find the "SpawnPoints" GameObject in the hierarchy
2. Adjust positions of individual spawn point GameObjects
3. Add or remove spawn points from the arrays in GameManager
4. Change the `spawnAreaSize` to control randomization around each point

## Testing

### Keyboard Controls (via SimpleTest)

- **1**: Spawn Red Soldier
- **2**: Spawn Blue Soldier
- **3**: Spawn Red Archer
- **4**: Spawn Blue Archer
- **C**: Simulate TikTok comment (spawns unit)
- **G**: Simulate TikTok gift (spawns special unit)

### Programmatic Testing

```csharp
// Spawn a unit
UnitSpawner.Instance.SpawnUnit(UnitType.Soldier, Team.Red);

// Simulate TikTok events
TikTokEventListener.Instance.SimulateComment("User123", "Hello!");
TikTokEventListener.Instance.SimulateGift("Viewer456", TikTokGiftType.Rose, 1);

// Spawn multiple units
UnitSpawner.Instance.SpawnMultipleUnits(UnitType.Archer, Team.Blue, 5);
```

## Manual Scene Setup

If you prefer to set up the scene manually, follow the detailed instructions in `SCENE_SETUP.md`.

## Troubleshooting

### Setup Button Doesn't Appear
- Make sure you added the SceneSetupUtility component in the Unity Editor
- The button only appears in Edit mode, not Play mode

### Prefabs Not Created
- Ensure you're running Unity Editor (not a build)
- Check that Assets/Prefabs and Assets/Materials folders were created
- Look for any error messages in the Console

### Units Not Spawning
- Verify GameManager has spawn points assigned
- Check that UnitSpawner has prefabs assigned
- Make sure there are no compilation errors in the Console

### Units Don't Move or Fight
- Verify each unit prefab has a UnitAI component
- Check that units are on different teams
- Ensure the GameManager round has started

## Next Steps

After setup:
1. **Customize unit prefabs**: Edit the prefabs in Assets/Prefabs to add better models, textures, or animations
2. **Adjust spawn points**: Move spawn points to different locations
3. **Configure team balance**: Adjust unit stats in the Unit components
4. **Add more spawn points**: Increase `spawnPointsPerTeam` and run setup again
5. **Integrate real TikTok API**: Follow instructions in `TIKTOK_INTEGRATION.md`

## Advanced Customization

### Creating Custom Prefabs

Instead of using the auto-generated prefabs:
1. Create your own 3D models or import from asset store
2. Add Unit component and configure stats
3. Add UnitAI component for autonomous behavior
4. Add Rigidbody and Collider components
5. Save as prefab in Assets/Prefabs
6. Assign to UnitSpawner in the Inspector

### Modifying Spawn Behavior

Edit the `GetSpawnPosition` method in GameManager.cs to:
- Change spawn formation patterns
- Add height variations
- Implement spawn zones
- Create dynamic spawn points

### UI Customization

The UIManager supports:
- Custom scoreboard layouts
- Notification systems
- Unit counters
- Connection status indicators

Add your own UI elements and reference them in UIManager.

## Files Created by Setup

- `Assets/Prefabs/Soldier.prefab`
- `Assets/Prefabs/Archer.prefab`
- `Assets/Prefabs/Tank.prefab`
- `Assets/Prefabs/Wacky.prefab`
- `Assets/Prefabs/Projectile.prefab`
- `Assets/Materials/RedTeamMaterial.mat`
- `Assets/Materials/BlueTeamMaterial.mat`

## Support

For issues or questions, please check:
- `README.md` - Main project documentation
- `SCENE_SETUP.md` - Detailed manual setup instructions
- `DOCUMENTATION.md` - API and architecture documentation
- `TIKTOK_INTEGRATION.md` - TikTok integration guide
