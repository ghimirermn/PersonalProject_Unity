# Unity Cube Defense Game 

A 3D cube defense game where cubes spawn at the edges of an arena and move toward a statue.  
The player controls a character to defend the statue, collect power-ups, and interact with cubes.

---
https://github.com/user-attachments/assets/52b669fb-4a04-4c5e-a441-f6ce8e0babe6

## Features
- Player movement with walking, running, and smooth rotation.
- Collectible defender power-ups with jump and push-back effects.
- Cubes spawn at arena edges and move toward a target statue.
- Power-ups and their indicators rotate continuously for visual effect.
- Score tracking, game start, and game-over logic.

---
## Scripts

```
Scripts
├── GameManager.cs
├── PlayerController.cs
├── DefenderPowerUp.cs
├── RotateCamera.cs
├── Rotator.cs
├── CubeSpawner.cs
└── TestCubeScript.cs
```
## Scripts Overview

### **GameManager.cs**
**Purpose:** Central game controller (score, game state, statue hits).  
**Important Variables:**  
- `score`, `scoreText`  
- `controlsText`, `TitleText`, `gameOverText`  
- `restartButton`, `startButton`  
- `statueHitCount`, `maxStatueHits`  

### **PlayerController.cs**
**Purpose:** Handles player movement, rotation, and animation.  
**Important Variables:**  
- `walkSpeed`, `runSpeed`, `rotationSpeed`  
- `speedParam` (Animator)  
- `Rigidbody rb`, `Animator anim`  
- `GameManager gameManager`  

### **DefenderPowerUp.cs**
**Purpose:** Manages collection and activation of player power-ups.  
**Important Variables:**  
- `powerupIndicator`, `pushBackForce`, `cooldownTime`  
- `jumpForce`, `activateKey`  
- `powerReleaseSound`  
- `powerupPrefab`, `spawnInterval`  

### **RotateCamera.cs**
**Purpose:** Allows manual camera rotation.  
**Important Variables:**  
- `rotationSpeed`  

### **Rotator.cs**
**Purpose:** Rotates GameObjects (PowerUp and PowerUpIndicator) continuously.  
**Important Variables:**  
- `rotationSpeed`  

### **CubeSpawner.cs**
**Purpose:** Spawns cubes at arena edges toward the statue.  
**Important Variables:**  
- `cubePrefab`, `target`, `spawnInterval`  
- `minX`, `maxX`, `minZ`, `maxZ`, `yPosition`  

### **TestCubeScript.cs**
**Purpose:** Controls cube behavior, collisions, score updates, and reactions to power-ups.  
**Important Variables:**  
- `statuehitExplosion`, `fallOffExplosion`, `target`, `speed`  
- `cubeHitTowerSound`, `defenderHitSound`  
- `pushForce`, `GameManager gameManager`, `Rigidbody rb`, `AudioSource playerAudio`  


