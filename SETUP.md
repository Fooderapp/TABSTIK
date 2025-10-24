# TABSTIK Setup Guide

## Quick Start Guide for Unity

### Step 1: Open Project in Unity

1. Install Unity Hub if not already installed
2. Install Unity 2020.3 LTS or newer
3. Open Unity Hub → Projects → Add → Select the TABSTIK folder
4. Open the project (first time may take a few minutes to import)

### Step 2: Create the Main Scene

1. **Create a new scene** or open SampleScene
2. Save it as "BattleScene" in `Assets/Scenes/`

### Step 3: Setup Core GameObjects

#### A. Game Manager
1. Create Empty GameObject: `GameObject → Create Empty`
2. Rename to "GameManager"
3. Add Component → Search for "Game Manager"
4. Configure settings:
   - Auto Start Round: ✓
   - Round Start Delay: 3

#### B. Spawn Areas
1. Create Empty GameObject, rename to "RedSpawnArea"
   - Position: (-20, 0, 0)
2. Create Empty GameObject, rename to "BlueSpawnArea"
   - Position: (20, 0, 0)
3. In GameManager, drag these objects to the spawn area slots

#### C. Unit Spawner
1. Create Empty GameObject: "UnitSpawner"
2. Add Component → "Unit Spawner"
3. Create Materials (optional):
   - Right-click in Assets → Create → Material → "RedTeamMaterial"
   - Set color to red
   - Repeat for "BlueTeamMaterial" (blue color)
4. Assign materials to Unit Spawner

#### D. TikTok Event Listener
1. Create Empty GameObject: "TikTokEventListener"
2. Add Component → "TikTok Event Listener"
3. Settings:
   - Use Mock API: ✓ (for testing)
   - Auto Connect: ✓
   - Alternate Teams: ✓

#### E. Setup Environment
1. **Ground Plane**:
   - `GameObject → 3D Object → Plane`
   - Scale: (10, 1, 10)
   - Position: (0, 0, 0)

2. **Camera**:
   - Select Main Camera
   - Position: (0, 15, -25)
   - Rotation: (30, 0, 0)
   - This gives a good overhead view

3. **Lighting**:
   - Ensure Directional Light exists
   - Adjust intensity as needed

### Step 4: Setup UI

#### A. Create Canvas
1. `GameObject → UI → Canvas`
2. Canvas Scaler:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080

#### B. Add UI Manager
1. Select Canvas
2. Add Component → "UI Manager"

#### C. Create Scoreboard (Simple Version)
1. Create UI Text (TextMeshPro):
   - Right-click Canvas → UI → Text - TextMeshPro
   - If prompted, import TMP Essentials
   - Name it "RedScoreText"
   - Position: (-800, 450) (top left)
   - Text: "Red: 0"
   - Color: Red
   - Font Size: 48

2. Duplicate for Blue:
   - Name: "BlueScoreText"
   - Position: (800, 450) (top right)
   - Text: "Blue: 0"
   - Color: Blue

3. Create Unit Counters:
   - Duplicate score texts
   - Position below scores
   - Name: "RedUnitCountText" and "BlueUnitCountText"
   - Text: "Units: 0"

4. Create Notification Panel:
   - Right-click Canvas → UI → Panel
   - Name: "NotificationPanel"
   - Position: (0, 0)
   - Size: (800, 100)
   - Add Text child named "NotificationText"

5. Connect UI Elements:
   - Select Canvas (UI Manager component)
   - Drag all text elements to appropriate slots

### Step 5: Create Basic Unit Prefabs

#### Option A: Auto-Generate (Quick Test)
The UnitSpawner can create basic cube units automatically. Just press Play!

#### Option B: Manual Prefabs (Better Quality)
1. Create a unit:
   - `GameObject → 3D Object → Cube`
   - Name: "Soldier"
   - Scale: (1, 2, 1) for humanoid proportions

2. Add Components:
   - Add Component → "Unit"
   - Add Component → "Unit AI"
   - Add Component → "Rigidbody"
     - Mass: 1
     - Drag: 2
     - Constraints: Freeze Rotation X & Z

3. Save as Prefab:
   - Drag from Hierarchy to Assets/Prefabs folder
   - Delete from scene

4. Assign to Spawner:
   - Select UnitSpawner
   - Drag Soldier prefab to "Soldier Prefab" slot

5. Repeat for other unit types:
   - Archer (add "Archer Unit" component instead)
   - Tank (add "Tank Unit" component, scale 1.5x)
   - Wacky (add "Wacky Unit" component)

### Step 6: Test the Game

1. Press **Play** in Unity Editor
2. Open Console window: `Window → General → Console`
3. Watch for spawn messages

#### Testing Unit Spawning

**Method 1: Code Console**
Open the Console and try:
```csharp
// This would be in a test script
TikTokEventListener.Instance.SimulateComment("Player1", "Spawn unit!");
TikTokEventListener.Instance.SimulateGift("Player2", TikTokGiftType.Rose);
```

**Method 2: Debug UI (Recommended)**
1. Create a button for testing:
   - Canvas → Right-click → UI → Button
   - Name: "SpawnTestButton"
   - Position in corner of screen
   - Text: "Spawn Unit"

2. Add Debug Script:
   - Create C# script in Assets/Scripts/: "TestSpawner.cs"
   ```csharp
   using UnityEngine;
   
   public class TestSpawner : MonoBehaviour
   {
       public void SpawnRedSoldier()
       {
           TABSTIK.Managers.UnitSpawner.Instance?.SpawnUnit(
               TABSTIK.Units.UnitType.Soldier, 
               TABSTIK.Units.Team.Red
           );
       }
   }
   ```

3. Attach to Button:
   - Select button
   - In OnClick() event, drag TestSpawner GameObject
   - Select TestSpawner → SpawnRedSoldier

### Step 7: Fine-Tuning

#### Adjust Spawn Positions
- Move RedSpawnArea and BlueSpawnArea further apart if units overlap
- Increase spawn area size in GameManager

#### Adjust AI Behavior
- Select a spawned unit in Hierarchy
- View Unit AI component
- Adjust Detection Range (larger = more aggressive)
- Adjust Update Interval (smaller = more responsive, higher CPU)

#### Adjust Physics
- `Edit → Project Settings → Physics`
- Increase Fixed Timestep for more stable physics
- Adjust gravity if needed

### Step 8: Build for Streaming

1. `File → Build Settings`
2. Add current scene to "Scenes in Build"
3. Select platform (PC, Mac & Linux Standalone)
4. Player Settings:
   - Company Name: Your name
   - Product Name: TABSTIK
   - Default Screen Width: 1920
   - Default Screen Height: 1080
   - Fullscreen Mode: Windowed
5. Build and Run

## Common Issues

### Issue: Units fall through ground
**Fix**: Ensure ground plane has a Collider component

### Issue: Units don't move
**Fix**: Check that Rigidbody is not kinematic and has reasonable constraints

### Issue: No units spawn
**Fix**: 
- Check UnitSpawner has Instance (should auto-create)
- Verify prefabs are assigned or auto-generation is enabled
- Check console for errors

### Issue: Units don't fight
**Fix**:
- Ensure units are on different teams
- Check UnitAI detection range
- Verify units have attack damage > 0

### Issue: UI not updating
**Fix**:
- Verify UI Manager has all text components assigned
- Check Canvas is in correct render mode
- Ensure EventSystem exists in scene

## Next Steps

1. **Add Better Graphics**:
   - Import 3D models from Asset Store
   - Add particle effects for attacks
   - Create custom materials

2. **Improve Animations**:
   - Add Animator components
   - Create animation controllers
   - Animate walk, attack, death

3. **Sound Effects**:
   - Add Audio Source components
   - Import sound effects
   - Play on attack/death events

4. **Real TikTok Integration**:
   - Research TikTok Live API
   - Implement ITikTokAPI interface
   - Test with live stream

5. **Additional Features**:
   - Victory screen
   - Unit abilities/special moves
   - Power-ups
   - Multiple battlefields
   - Replay system

## Resources

- Unity Learn: https://learn.unity.com/
- Unity Asset Store: https://assetstore.unity.com/
- TextMeshPro Documentation: https://docs.unity3d.com/Manual/com.unity.textmeshpro.html

## Getting Help

- Check Unity Console for error messages
- Review script references in Inspector
- Ensure all components are properly connected
- Try rebuilding prefabs from scratch if issues persist

---

Happy battling! 🎮⚔️
