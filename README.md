# RunTheBridge

A 3D game built with Unity where player navigate across bridges while avoiding obstacles and hazards.

## Features

- 3D movement with jumping mechanics
- Health system with damage from obstacles
- Background audio system
- Realistic water effects using Crest Ocean System
- Unity's new Input System for responsive controls


## Technical Details

- **Unity Version**: 6.0.1.13f1
- **Input System**: Unity's new Input System
- **Water Effects**: Crest Ocean System

## Installation

1. Open Unity Hub
2. Add project folder
3. Open `Assets/Scenes/home.unity`
4. Press Play

## Build

1. File > Build Settings
2. Add scenes in order: home, main, options, gameover
3. Select platform and build

## Project Structure

```
Assets/
├── scripts/           # Game logic
├── Scenes/           # Game scenes
├── animation/        # Character animations
├── audio/           # Background music
├── bridge/          # Bridge assets
├── Crest/           # Water simulation
└── sprite/          # UI assets
```

## Scripts

- `PlayerMovement.cs` - Player movement, health, collision
- `Menu.cs` - Scene navigation
- `audio.cs` - Background music
- `InputConflictResolver.cs` - Input management

---

*Built with Unity 6.0.1.13f1*

