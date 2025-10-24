# Contributing to TABSTIK

Thank you for your interest in contributing to TABSTIK! This document provides guidelines for contributing to the project.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/TABSTIK.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Test your changes thoroughly
6. Commit with clear messages: `git commit -m "Add feature: description"`
7. Push to your fork: `git push origin feature/your-feature-name`
8. Open a Pull Request

## Development Guidelines

### Code Style

- Follow Unity C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and under 50 lines when possible
- Use proper namespacing: `TABSTIK.ModuleName`

### Example:
```csharp
namespace TABSTIK.Units
{
    /// <summary>
    /// Represents a combat unit in the game
    /// </summary>
    public class Unit : MonoBehaviour
    {
        /// <summary>
        /// Applies damage to this unit
        /// </summary>
        /// <param name="damage">Amount of damage to apply</param>
        public void TakeDamage(float damage)
        {
            // Implementation
        }
    }
}
```

### Project Structure

- `Assets/Scripts/Units/` - Unit classes and behaviors
- `Assets/Scripts/AI/` - AI controllers
- `Assets/Scripts/Managers/` - Game managers
- `Assets/Scripts/TikTokIntegration/` - TikTok API integration
- `Assets/Scripts/UI/` - UI components
- `Assets/Scripts/Testing/` - Test utilities
- `Assets/Prefabs/` - Unity prefabs
- `Assets/Materials/` - Materials and textures
- `Assets/Scenes/` - Scene files

### Testing

- Test your changes in Unity Editor
- Verify unit spawning works correctly
- Test AI behavior with multiple units
- Check UI updates properly
- Test with both mock and real TikTok events (if applicable)
- Ensure no console errors or warnings

### Commit Messages

Use clear, descriptive commit messages:

**Good:**
- `Add archer unit with ranged attack capability`
- `Fix unit AI target selection bug`
- `Improve performance by batching unit spawns`

**Bad:**
- `Update code`
- `Fix bug`
- `Changes`

## What to Contribute

### High Priority

- **Real TikTok API Integration**: Implement actual TikTok Live connection
- **Unit Animations**: Add Animator components and animation controllers
- **Sound Effects**: Audio for attacks, deaths, victories
- **Better 3D Models**: Replace cube placeholders with proper models
- **Performance Optimizations**: Object pooling, spatial partitioning

### Medium Priority

- **New Unit Types**: More diverse units (mage, ninja, giant, etc.)
- **Unit Abilities**: Special attacks, buffs, debuffs
- **Power-ups**: Pickups that enhance units
- **Multiple Maps**: Different battlefield environments
- **Camera Controls**: Better camera angles and controls

### Low Priority (Nice to Have)

- **Replay System**: Record and playback battles
- **Leaderboards**: Track top contributors
- **Tournament Mode**: Structured battle format
- **Mobile Support**: Touch controls and optimization
- **Web Build**: WebGL deployment

## Areas That Need Help

### 3D Artists
- Unit models and textures
- Battlefield environments
- Particle effects
- UI elements

### Animators
- Unit movement animations
- Attack animations
- Death animations
- Victory celebrations

### Sound Designers
- Combat sound effects
- Background music
- UI sounds
- Victory/defeat themes

### Programmers
- TikTok API integration
- Advanced AI behaviors
- Performance optimizations
- Network features

### Designers
- UI/UX improvements
- Game balance tuning
- Unit stats and abilities
- Map designs

## Pull Request Guidelines

### Before Submitting

- [ ] Code follows project style guidelines
- [ ] All scripts compile without errors
- [ ] Changes tested in Unity Editor
- [ ] No console errors or warnings
- [ ] Documentation updated (if needed)
- [ ] README updated (if adding features)

### PR Description Template

```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Performance improvement
- [ ] Documentation update
- [ ] Other (please describe)

## Testing
How you tested the changes

## Screenshots (if applicable)
Add screenshots for visual changes

## Checklist
- [ ] Code compiles without errors
- [ ] Tested in Unity Editor
- [ ] Documentation updated
- [ ] No breaking changes
```

## Code Review Process

1. Submit pull request
2. Automated checks run (if configured)
3. Maintainer reviews code
4. Address feedback if requested
5. Approval and merge

## Bug Reports

When reporting bugs, include:

1. **Description**: Clear description of the bug
2. **Steps to Reproduce**: Detailed steps to reproduce
3. **Expected Behavior**: What should happen
4. **Actual Behavior**: What actually happens
5. **Unity Version**: Your Unity version
6. **Platform**: Windows, Mac, Linux, etc.
7. **Console Errors**: Any error messages
8. **Screenshots**: Visual evidence if applicable

### Bug Report Template

```markdown
**Bug Description**
A clear description of the bug

**To Reproduce**
1. Open scene '...'
2. Click on '...'
3. See error

**Expected Behavior**
What you expected to happen

**Screenshots**
If applicable, add screenshots

**Environment**
- Unity Version: [e.g., 2020.3.25f1]
- Platform: [e.g., Windows 10]
- Build Target: [e.g., Standalone]

**Additional Context**
Any other relevant information
```

## Feature Requests

When requesting features, include:

1. **Description**: Clear feature description
2. **Use Case**: Why this feature is needed
3. **Proposed Solution**: How it could work
4. **Alternatives**: Other solutions considered
5. **Additional Context**: Any other information

## Questions and Support

- **Documentation**: Check DOCUMENTATION.md first
- **Setup Issues**: See SETUP.md
- **TikTok Integration**: See TIKTOK_INTEGRATION.md
- **GitHub Issues**: Open an issue for help
- **Discussions**: Use GitHub Discussions for general questions

## License

By contributing to TABSTIK, you agree that your contributions will be licensed under the MIT License.

## Code of Conduct

### Our Standards

- Be respectful and inclusive
- Welcome newcomers
- Provide constructive feedback
- Focus on what's best for the project
- Show empathy towards others

### Unacceptable Behavior

- Harassment or discrimination
- Trolling or insulting comments
- Personal or political attacks
- Publishing others' private information
- Any conduct harmful to the community

## Recognition

Contributors will be:
- Listed in project credits
- Mentioned in release notes
- Acknowledged in README (for major contributions)

## Getting Help

- **Discord**: (Add link if created)
- **GitHub Issues**: For bugs and features
- **GitHub Discussions**: For questions
- **Email**: (Add if desired)

## Additional Resources

- [Unity C# Style Guide](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)
- [Git Best Practices](https://git-scm.com/book/en/v2/Distributed-Git-Contributing-to-a-Project)
- [Unity Documentation](https://docs.unity3d.com/)

---

Thank you for contributing to TABSTIK! Every contribution, no matter how small, helps make this project better.
