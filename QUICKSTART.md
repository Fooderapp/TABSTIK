# Quick Start Example

This is a quick walkthrough showing how to use the new automated setup feature.

## Option 1: Automated Setup (Easiest)

### Step-by-Step Video Script

1. **Open Unity Project**
   - Open the TABSTIK project in Unity
   - Create a new scene (File > New Scene)

2. **Add Setup Utility**
   - In Hierarchy, right-click and create an empty GameObject
   - Name it "SceneSetup"
   - In Inspector, click "Add Component"
   - Search for "Scene Setup Utility" and add it

3. **Configure Options** (Optional)
   - Spawn Points Per Team: 3 (default)
   - Spawn Point Spacing: 10 (default)
   - Create Prefabs: ✓ (checked)
   - Setup UI: ✓ (checked)
   - Add Test Script: ✓ (checked)

4. **Run Setup**
   - Click the big **"Setup Scene"** button in the Inspector
   - Wait a few seconds while it creates everything
   - You'll see progress in the Console

5. **Press Play!**
   - Your scene is now ready to test
   - Check the Console for keyboard controls
   - Press 1, 2, 3, 4 to spawn units
   - Press C to simulate comments
   - Press G to simulate gifts

## What Gets Created

After setup, your scene hierarchy will look like:

```
Scene
├── GameManager
├── UnitSpawner
├── TikTokEventListener
├── SpawnPoints
│   ├── RedTeamSpawnPoints
│   │   ├── RedSpawnPoint_1 (at -25, 0, -10)
│   │   ├── RedSpawnPoint_2 (at -25, 0, 0)
│   │   └── RedSpawnPoint_3 (at -25, 0, 10)
│   └── BlueTeamSpawnPoints
│       ├── BlueSpawnPoint_1 (at 25, 0, -10)
│       ├── BlueSpawnPoint_2 (at 25, 0, 0)
│       └── BlueSpawnPoint_3 (at 25, 0, 10)
├── Ground
├── Directional Light
├── Main Camera
├── Canvas (with UIManager)
├── EventSystem
└── TestController
```

And in your Assets folder:

```
Assets
├── Prefabs
│   ├── Soldier.prefab
│   ├── Archer.prefab
│   ├── Tank.prefab
│   ├── Wacky.prefab
│   └── Projectile.prefab
└── Materials
    ├── RedTeamMaterial.mat
    └── BlueTeamMaterial.mat
```

## Testing the Multiple Spawn Points

1. **Press Play**
2. **Spawn some units**:
   - Press `1` multiple times to spawn Red Soldiers
   - Watch them spawn at different spawn points randomly!
   - Press `2` to spawn Blue Soldiers
   - Notice they also spawn at random locations

3. **Test with more units**:
   - Press `C` repeatedly to simulate TikTok comments
   - Units will spawn alternating between teams
   - Each will appear at a random spawn point

4. **Visual Confirmation**:
   - Look at the spawn point gizmos (transparent colored cylinders)
   - You should see units appearing near different spawn points
   - Each team has 3 spawn points by default

## Customizing Spawn Points

### Change Number of Spawn Points

Before running setup:
- Change "Spawn Points Per Team" to 5 (or any number)
- Click "Setup Scene"
- Now each team has 5 spawn points!

### Adjust Spawn Point Positions

After setup:
1. Find "SpawnPoints" in Hierarchy
2. Expand "RedTeamSpawnPoints" or "BlueTeamSpawnPoints"
3. Select individual spawn points
4. Move them in the Scene view
5. Press Play - units now spawn at the new positions!

### Add More Spawn Points Manually

1. Create empty GameObject under RedTeamSpawnPoints or BlueTeamSpawnPoints
2. Position it where you want
3. Select GameManager in Hierarchy
4. In Inspector, expand "redSpawnPoints" or "blueSpawnPoints" array
5. Increase array size by 1
6. Drag your new spawn point into the new slot
7. Done! Units will now also spawn there

## Advanced: Multiple Spawn Configurations

### Circular Formation

Position spawn points in a circle:
```
Spawn Point 1: (-25, 0, -10)
Spawn Point 2: (-20, 0, -15)
Spawn Point 3: (-15, 0, -10)
Spawn Point 4: (-20, 0, -5)
```

### Line Formation

Position spawn points in a straight line:
```
Spawn Point 1: (-25, 0, -15)
Spawn Point 2: (-25, 0, -5)
Spawn Point 3: (-25, 0, 5)
Spawn Point 4: (-25, 0, 15)
```

### Scattered Formation

Random positions across the spawn area:
```
Spawn Point 1: (-25, 0, -8)
Spawn Point 2: (-22, 0, 3)
Spawn Point 3: (-28, 0, 12)
Spawn Point 4: (-20, 0, -2)
```

## Option 2: Manual Setup

If you prefer manual control, see `SCENE_SETUP.md` for detailed step-by-step instructions.

## Troubleshooting

### "Setup Scene" button doesn't appear
- Make sure you're in Edit mode (not Play mode)
- Verify the SceneSetupUtility component is attached
- Check for compilation errors in Console

### Prefabs not created
- This only works in Unity Editor (not in builds)
- Check that Assets/Prefabs folder was created
- Look for any error messages in Console

### Units spawn but don't move
- Verify each prefab has UnitAI component
- Check that GameManager round has started
- Ensure units are on different teams

### Spawn points not working
- Verify GameManager has spawn points assigned in Inspector
- Check that spawn points exist in the scene
- Make sure spawn points array is not empty

## Next Steps

After the quick start:
1. Customize unit prefabs with better 3D models
2. Adjust unit stats (health, damage, speed)
3. Add more spawn points or change positions
4. Integrate with real TikTok API (see TIKTOK_INTEGRATION.md)
5. Build and deploy for streaming!

## Support

- Full documentation: `SETUP_GUIDE.md`
- Scene setup details: `SCENE_SETUP.md`
- Main project info: `README.md`
- TikTok integration: `TIKTOK_INTEGRATION.md`
