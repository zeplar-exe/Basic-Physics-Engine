# Basic Physics Engine

A lightweight physics engine built in C# to simulate multiple environments.

Table of Contents
- [`Physics Simulator`](#physics-simulator)
- [`Physics Object`](#physics-object)
- [`Object Bounds`](#object-bounds)
  - [`Rectangle`](#rectangle)

# Physics Simulator
Class used to create instances of physics simulations. Each simulation has a unique timescale and physics settings. 
Through the usage of threads, each can be analyzed side-by-side.

Usage (C#):
```c#
PhysicsSimulator simulator = new PhysicsSimulator();

simulator.SetPhysicsStep(new Milliseconds(10));
simulator.ApplyGravitySettings(Gravity.Default()); // 'Gravity.Default()' is an alternative to 'new Gravity()'
simulator.ApplyTimeSettings(Time.Default()); // 'Time.Default()' is an alternative to 'new Time()'

simulator.Start(); // Keep in mind that a simulation will automatically stop if there are no PhysicsObjects
```

A simulation can be successfully started when Gravity and Time settings have been applied.
It will automatically stop if all PhysicsObjects have been destroyed.

# Physics Object
Object that can be manipulated by physics.

PhysicsObjects can be constructed using a BoundsType or a ObjectBounds instance directly.
Unhandled BoundsTypes wil redirect to a Rectangle.

Usage (C#)
```c#
PhysicsObject physObject = new PhysicsObject(BoundsType.Rectangle);

physObject.SetObjectType(ObjectType.Dynamic | ObjectType.Kinematic);
// Dynamic objects are affected by physics.
// Kinematic objects can affect other non-static objects.
// Non-Kinematic objects are not affected by any forces and must be moved directly through their ObjectBounds.

physObject.Resize(new Vector2(5, 5));
physObject.AddToSimulation(simulator);

PhysicsObject ground = new PhysicsObject(Bounds.Rectangle);

physObject.SetObjectType(ObjectType.Static);
// Static objects cannot be moved and are not affected by physics.

physObject.Resize(new Vector3(30, 1));
physObject.AddToSimulation(simulator);

// Object type 'ObjectType.Dynamic | ObjectType.Kinematic' can also be written as 'ObjectType.Default'
```

The PhysicsObject class cannot be subclassed.

# Object Bounds
The ObjectBounds class defines a template for collision boundaries of all Physics Objects.
ObjectBounds are publicly exposed, allowing for custom shapes.

Pre-made shapes can be accessed using the BoundsType enum.
```c#
public enum BoundsType { Rectangle }
```

Creating your own BoundsType will require the following class attributes
```c#
public abstract Vector2 Center { get; set; }

public abstract Vector2 Top { get; set; }

public abstract Vector2 Bottom { get; set; }

public abstract Vector2 Left { get; set; }

public abstract Vector2 Right { get; set; }

public abstract void Resize(Vector2 size);

public abstract bool IsOverlapping(PhysicsObject other);

public abstract Shape ToShape();

public abstract override string ToString();
```

# Installation

Project is not yet ready for external use.