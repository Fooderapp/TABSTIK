# Multiple Spawn Points System

## Overview

The TABSTIK project now supports multiple spawn points per team, allowing for more dynamic and realistic unit deployment across the battlefield.

## How It Works

### Traditional Single Spawn Area (Legacy)
```
Before (Single Spawn Area):

Red Team Area                    Blue Team Area
┌──────────┐                    ┌──────────┐
│          │                    │          │
│    R     │                    │    B     │
│          │                    │          │
└──────────┘                    └──────────┘
  Random                          Random
  position                        position
  within area                     within area
```

All red units spawn randomly within a single rectangular area on the left.
All blue units spawn randomly within a single rectangular area on the right.

### New Multiple Spawn Points System
```
Multiple Spawn Points (NEW):

Red Spawn Points                Blue Spawn Points
     R₁                               B₁
       ↓ random                         ↓ random
       
     R₂                               B₂
       ↓ random                         ↓ random
       
     R₃                               B₃
       ↓ random                         ↓ random
```

Each team can have multiple discrete spawn points.
When a unit spawns, the system:
1. Randomly selects one of the available spawn points
2. Applies a small random offset around that point

## Benefits

### 1. More Strategic Gameplay
- Units don't all spawn from the same location
- Creates more realistic battle formations
- Allows for flanking and multi-directional attacks

### 2. Flexible Configuration
- Each team can have different numbers of spawn points
- Spawn points can be positioned anywhere in the scene
- Easy to adjust formations without code changes

### 3. Visual Variety
- More interesting to watch as units appear from different locations
- Better for streaming and viewer engagement
- Reduces spawn camping in the same spot

## Configuration Examples

### Example 1: Symmetric 3-Point Setup (Default)
```
Top View of Battlefield:

         Blue Spawn Points
         B₁      B₂      B₃
          |      |       |
          ↓      ↓       ↓
    
    ╔════════════════════════╗
    ║                        ║
    ║      Battlefield       ║
    ║                        ║
    ╚════════════════════════╝
    
          ↑      ↑       ↑
          |      |       |
         R₁      R₂      R₃
    Red Spawn Points
```

Positions:
- R₁: (-25, 0, -10)  B₁: (25, 0, -10)
- R₂: (-25, 0,   0)  B₂: (25, 0,   0)
- R₃: (-25, 0,  10)  B₃: (25, 0,  10)

### Example 2: Asymmetric Setup
```
Red: 5 spawn points (more spread out)
Blue: 3 spawn points (concentrated)

         Blue
         B₁  B₂  B₃
          |  |   |
          ↓  ↓   ↓
    
    ╔════════════════════════╗
    ║                        ║
    ║      Battlefield       ║
    ║                        ║
    ╚════════════════════════╝
    
     ↑   ↑   ↑   ↑   ↑
     |   |   |   |   |
    R₁  R₂  R₃  R₄  R₅
    Red
```

This creates asymmetric gameplay where red has more entry points.

### Example 3: Circular/Arc Formation
```
Top View:

         B₂
        / | \
       /  |  \
      B₁  |  B₃
          ↓
    
    ╔════════════════════════╗
    ║                        ║
    ║      Battlefield       ║
    ║                        ║
    ╚════════════════════════╝
    
          ↑
      R₁  |  R₃
       \  |  /
        \ | /
         R₂
    Red
```

Creates an arc formation for more tactical positioning.

### Example 4: Front-Line Formation
```
All spawn points in a line at the edge:

Blue: B₁ B₂ B₃ B₄ B₅ B₆
       ↓  ↓  ↓  ↓  ↓  ↓
    
    ╔════════════════════════╗
    ║                        ║
    ║      Battlefield       ║
    ║                        ║
    ╚════════════════════════╝
    
       ↑  ↑  ↑  ↑  ↑  ↑
Red:  R₁ R₂ R₃ R₄ R₅ R₆
```

Creates a battle line formation.

## Code Implementation

### GameManager Update

```csharp
// New fields added to GameManager
public Transform[] redSpawnPoints;  // Array of red team spawn points
public Transform[] blueSpawnPoints; // Array of blue team spawn points

// Updated GetSpawnPosition method
public Vector3 GetSpawnPosition(Units.Team team)
{
    // Check if multiple spawn points are configured
    Transform[] spawnPoints = team == Units.Team.Red ? redSpawnPoints : blueSpawnPoints;
    
    if (spawnPoints != null && spawnPoints.Length > 0)
    {
        // Randomly select one spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawnPoint = spawnPoints[randomIndex];
        
        // Apply small random offset
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0f,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        
        return selectedSpawnPoint.position + randomOffset;
    }
    
    // Fallback to single spawn area (backward compatible)
    // ... existing code ...
}
```

### Key Features of Implementation

1. **Backward Compatible**: If spawn points array is empty or null, falls back to single spawn area mode
2. **Random Selection**: Uses `Random.Range()` to select spawn point index
3. **Random Offset**: Adds small randomization around the selected point
4. **Flexible**: Supports different array sizes per team
5. **Simple**: No complex logic, easy to understand and maintain

## Scene Setup

### Automated Setup
The `SceneSetupUtility` automatically creates spawn points:

```csharp
// In SceneSetupUtility
private void CreateSpawnPoints()
{
    for (int i = 0; i < spawnPointsPerTeam; i++)
    {
        GameObject spawnPoint = new GameObject($"RedSpawnPoint_{i + 1}");
        // Position based on spacing
        float z = i * spawnPointSpacing - (spawnPointsPerTeam - 1) * spawnPointSpacing / 2f;
        spawnPoint.transform.position = new Vector3(-25f, 0f, z);
        
        // Add visual gizmo
        CreateSpawnPointGizmo(spawnPoint, Color.red);
    }
}
```

### Manual Setup
1. Create empty GameObjects for spawn points
2. Position them in the scene
3. Assign to GameManager's spawn point arrays
4. Optional: Add visual gizmos for debugging

## Spawn Area Size

The `spawnAreaSize` setting controls the randomization around each spawn point:

```
Small spawnAreaSize (5, 0, 5):
    Spawn Point
        |
        ↓
      [·····]
      [··●··]  ← Units spawn tightly around point
      [·····]

Large spawnAreaSize (15, 0, 15):
    Spawn Point
        |
        ↓
  [···············]
  [···············]
  [········●······]  ← Units spawn spread out
  [···············]
  [···············]
```

Default: `(10, 0, 10)` - good balance between spread and clustering

## Testing

### Visual Testing
1. Press Play
2. Spawn multiple units of one team
3. Watch them appear at different spawn points
4. Look for the spawn point gizmos (colored cylinders)

### Debug Logging
The system logs spawn positions:
```
Spawned Soldier for Red team at (-24.3, 0, -9.7)
Spawned Soldier for Red team at (-25.8, 0, 1.2)
Spawned Soldier for Red team at (-23.9, 0, 8.5)
```

You can verify units are spawning at different points by checking coordinates.

### Manual Test Script
Use `SpawnPointManualTest` component:
- Press R: Spawn 10 red units
- Press B: Spawn 10 blue units
- Press T: Spawn 10 units per team

Watch them distribute across spawn points.

## Performance Considerations

### Array Access
- Array lookups are O(1) - very fast
- Random.Range() is optimized
- No performance concerns for typical unit counts (< 1000 units)

### Memory
- Spawn point arrays are small (typically 3-10 elements)
- Negligible memory overhead
- Transform references are lightweight

### Recommended Limits
- Spawn Points per Team: 1-20 (optimal: 3-7)
- Total Units in Scene: < 100 for 60 FPS
- Spawn Rate: < 10 units/second for smooth gameplay

## Future Enhancements

Potential additions to the system:

1. **Weighted Spawn Points**: Some points spawn units more frequently
2. **Conditional Spawning**: Spawn at specific points based on unit type
3. **Dynamic Spawn Points**: Points that move or activate/deactivate
4. **Spawn Animations**: Visual effects when units appear
5. **Spawn Blocking**: Don't spawn if point is occupied

## Troubleshooting

### Units All Spawn in Same Place
- Check that spawn points array is assigned in GameManager
- Verify array is not empty
- Ensure spawn points have different positions

### Units Spawn in Wrong Location
- Check spawn point positions in Scene view
- Verify correct array assigned (red vs blue)
- Look for typos in spawn point names

### Spawn Points Not Visible
- Spawn gizmos only created by automated setup
- Add your own visual markers if needed
- Use Scene view to see empty GameObjects

### Random Selection Not Working
- Unity's Random.Range is deterministic but pseudo-random
- May see patterns with very few spawn points (< 3)
- Use more spawn points for better distribution

### Null Reference Exceptions at Runtime
- Check that spawn point GameObjects haven't been destroyed
- Verify Transform references are valid in GameManager
- Use `if (spawnPoint != null)` checks before accessing
- Re-assign spawn points after scene reloads if needed
- Spawn points must persist in scene (don't destroy them)

## Conclusion

The multiple spawn points system provides:
- ✅ More dynamic gameplay
- ✅ Easy configuration
- ✅ Backward compatible
- ✅ Simple to understand
- ✅ Performant
- ✅ Flexible for different game modes

Enjoy creating diverse battle scenarios with multiple spawn points!
