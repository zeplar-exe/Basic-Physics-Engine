# Basic Physics Engine

A lightweight resume project used to simulate physics environments

Table of Contents
- [`PhysicsSimulator`](Physics-Simulator)
- [`PhysicsObject`](Physics-Object)
- [`PhysicsBounds`](Physics-Bounds)
  - [`Rectangle`](Rectangle)

# Physics Simulator
Class used to create instances of physics simulations. Each simulation has a unique timescale and physics settings. Through the usage of threads, each instance can be analyzed side-by-side. Time is measured in milliseconds and handled by the Milliseconds class see ([see milliseconds](Milliseconds))

Usage (C#):
```cs
PhysicsSimulator simulator = new PhysicsSimulator();
simulator.SetPhysicsStep(new Milliseconds(10));
simulator.ApplyGravitySettings(new Gravity());
simulator.ApplyTimeSettings(new Time());
```

# Physics Object
Object that can be manipulated by physics.

Cannot be subclassed.

# Physics Bounds
## Rectangle

# Other Classes

## Milliseconds
