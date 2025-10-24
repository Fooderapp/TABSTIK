# TABSTIK - Project Summary

## What is TABSTIK?

TABSTIK (TikTok Battle Simulator) is a Unity3D game that combines:
- **TABS-style physics combat** - Units fight autonomously with ragdoll physics
- **TikTok live integration** - Viewers spawn units via comments and gifts
- **Team battles** - Red vs Blue competitive matches
- **Real-time gameplay** - Perfect for live streaming

## Quick Start

### For Developers
1. Clone: `git clone https://github.com/Fooderapp/TABSTIK.git`
2. Open in Unity 2020.3 LTS or newer
3. Follow setup instructions in `SETUP.md`
4. Start testing with mock TikTok API

### For Streamers
1. Setup Unity project (see `SETUP.md`)
2. Configure TikTok integration (see `TIKTOK_INTEGRATION.md`)
3. Build and run during your live stream
4. Viewers spawn units via comments and gifts!

## Key Features

### ✅ Fully Implemented
- 4 unit types (Soldier, Archer, Tank, Wacky)
- Autonomous AI combat
- Physics-based battles
- Team scoring system
- Round management
- TikTok mock API
- Real-time UI
- Debug testing tools

### 🚧 Ready to Extend
- Custom unit types
- Advanced abilities
- Better 3D models
- Animations
- Sound effects
- Real TikTok integration

## Project Structure

```
TABSTIK/
├── Assets/Scripts/          # All C# code (1,300+ lines)
│   ├── Units/              # Unit classes
│   ├── AI/                 # Combat AI
│   ├── Managers/           # Game management
│   ├── TikTokIntegration/  # TikTok API
│   ├── UI/                 # User interface
│   └── Testing/            # Test utilities
├── Documentation/
│   ├── README.md           # Main documentation
│   ├── SETUP.md            # Unity setup guide
│   ├── TIKTOK_INTEGRATION.md # API integration
│   ├── SCENE_SETUP.md      # Scene configuration
│   ├── DOCUMENTATION.md    # Technical details
│   └── CONTRIBUTING.md     # How to contribute
└── ProjectSettings/        # Unity configuration
```

## How It Works

1. **TikTok Event** → Comment or gift sent
2. **Event Listener** → Receives and processes event
3. **Unit Spawner** → Creates appropriate unit
4. **AI Controller** → Unit finds and fights enemies
5. **Game Manager** → Tracks score, detects round end
6. **UI Manager** → Updates scoreboard and notifications

## Tech Stack

- **Engine**: Unity 2020.3 LTS+
- **Language**: C#
- **Physics**: Unity Physics System
- **UI**: Unity UI + TextMeshPro
- **Integration**: WebSocket (planned) or Mock API

## Documentation

| File | Purpose | Size |
|------|---------|------|
| README.md | Overview & features | 9.5 KB |
| SETUP.md | Unity setup guide | 7.7 KB |
| TIKTOK_INTEGRATION.md | API integration | 11.7 KB |
| SCENE_SETUP.md | Scene configuration | 8.4 KB |
| DOCUMENTATION.md | Architecture | 12.1 KB |
| CONTRIBUTING.md | Contribution guide | 7.3 KB |
| **Total** | **Complete documentation** | **57 KB** |

## Code Statistics

- **Scripts**: 12 C# files
- **Lines of Code**: 1,300+
- **Namespaces**: 6 organized modules
- **Unit Types**: 4 (extensible)
- **Test Coverage**: Manual testing framework

## Testing

### Quick Test (No TikTok)
1. Open scene in Unity
2. Press Play
3. Use keyboard shortcuts:
   - `1` - Spawn Red Soldier
   - `2` - Spawn Blue Soldier
   - `C` - Simulate comment
   - `G` - Simulate gift

### With Mock TikTok API
```csharp
TikTokEventListener.Instance.SimulateComment("User123", "Spawn!");
TikTokEventListener.Instance.SimulateGift("User456", TikTokGiftType.Rose);
```

## Next Steps

### Immediate
1. Open in Unity
2. Create scene using `SCENE_SETUP.md`
3. Test with keyboard controls
4. Watch units fight!

### Short-term
1. Add 3D models (replace cubes)
2. Add animations
3. Add sound effects
4. Improve visuals

### Long-term
1. Implement real TikTok API
2. Add more unit types
3. Create multiple maps
4. Add special abilities
5. Build for streaming platforms

## Community

### Contribute
- See `CONTRIBUTING.md` for guidelines
- Open issues for bugs
- Submit PRs for features
- Share your creations!

### We Need
- 3D modelers
- Animators
- Sound designers
- Unity developers
- TikTok API experts
- Testers and streamers

## License

MIT License - Free to use, modify, and distribute

## Credits

- Inspired by Totally Accurate Battle Simulator (TABS)
- Built for interactive live streaming
- Created for the community

## Contact

- GitHub: https://github.com/Fooderapp/TABSTIK
- Issues: Report bugs and request features
- Discussions: Ask questions and share ideas

---

**Status**: ✅ Complete and ready to use
**Version**: 1.0.0
**Last Updated**: 2025-10-24

## Getting Started in 5 Minutes

1. **Clone the repo**
   ```bash
   git clone https://github.com/Fooderapp/TABSTIK.git
   ```

2. **Open in Unity**
   - Unity Hub → Add → Select TABSTIK folder

3. **Create basic scene**
   - Follow Quick Start in SETUP.md
   - 5-10 minutes to basic scene

4. **Test it**
   - Press Play
   - Press `1` and `2` to spawn units
   - Watch them fight!

5. **Customize**
   - Adjust unit stats
   - Change team colors
   - Add your own units

That's it! You're ready to battle! 🎮⚔️

---

For detailed instructions, see the documentation files in this repository.
