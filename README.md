# Basic Physics Engine

A lightweight resume project used to simulate physics environments

Table of Contents
- [`Physics Simulator`](#physics-simulator)
- [`Physics Object`](#physics-object)
- [`Physics Bounds`](#physics-bounds)
  - [`Rectangle`](#rectangle)

# Physics Simulator
Class used to create instances of physics simulations. Each simulation has a unique timescale and physics settings. 
Through the usage of threads, each instance can be analyzed side-by-side. 
Time is measured in milliseconds and handled by the [Milliseconds class](Milliseconds).

Usage (C#):
```c#
PhysicsSimulator simulator = new PhysicsSimulator();

simulator.SetPhysicsStep(new Milliseconds(10));
simulator.ApplyGravitySettings(Gravity.Default()); // 'Gravity.Default()' is an alternative to 'new Gravity()'
simulator.ApplyTimeSettings(Time.Default()); // 'Time.Default()' is an alternative to 'new Time()'

simulator.Start(); // Keep in mind that a simulation will automatically stop if there are no PhysicsObjects
```

A simulation can be successfully started when Gravity and Time settings have been applied.
It will automatically stop once all PhysicsObjects have been destroyed.

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

physObject.Resize(5, 5);
physObject.AddToSimulation(simulator);

PhysicsObject ground = new PhysicsObject(Bounds.Rectangle);

physObject.SetObjectType(ObjectType.Static);
// Static objects cannot be moved and are not affected by physics.

physObject.Resize(30, 1);
physObject.AddToSimulation(simulator);

// Object type 'ObjectType.Dynamic | ObjectType.Kinematic' can also be written as 'ObjectType.Default'
```

The PhysicsObject class cannot be subclassed.

# Object Bounds
The ObjectBounds class defines a template for collision boundaries of all Physics Objects.
ObjectBounds are publicly exposed, allowing for custom shapes.

## Rectangle
Rectangles are the default bounds shape. They can be resized with one parameter `Resize(Vector2)` and are the most optimization friendly.

# Other Classes

## Gravity
Data structure that holds information on gravity settings and configuration.
Uses earth gravity by default (9.81)

## Time
Data structure that holds values related to timescale among others.
Timescale is 1 by default.

## Milliseconds
A Millisecond converter/helper class. Can convert seconds to milliseconds and vica versa, as well as be used in place of integers.
Constructed using a factory method or integer, `Milliseconds.FromSeconds(1)` is equivalent to `new Milliseconds(1000)`.
FromSeconds also accepts floating point values.

## Vector2
Uses the cartesian point system. Allows for basic arithmetic. 
Constructed using two floats, `new Vector2()`