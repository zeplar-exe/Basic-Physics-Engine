using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BasicPhysicsEngine.PhysicsObjects;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace BasicPhysicsEngine
{
    public class PhysicsSimulator
    {
        private bool running;
        private Milliseconds updateStep;

        private Gravity gravity;
        private Time time;
        
        private readonly List<PhysicsObject> objects = new();

        internal void AddObject(PhysicsObject physicsObject)
        {
            objects.Add(physicsObject);
        }

        public void SetPhysicsStep(Milliseconds milliseconds)
        {
            updateStep = milliseconds;
        }

        public void ApplyGravitySettings(Gravity settings)
        {
            gravity = settings;
        }

        public void ApplyTimeSettings(Time settings)
        {
            time = settings;
        }

        public void Start()
        {
            if (gravity == null)
                throw new Exception("Simulation gravity settings cannot be null. Did you forget to call 'ApplyGravitySettings'?");
            if (time == null)
                throw new Exception("Simulation time settings cannot be null. Did you forget to call 'ApplyTimeSettings'?");

            running = true;
            
            new Thread(UpdateLoop).Start();
        }
        
        private void UpdateLoop()
        {
            while (running)
            {
                Thread.Sleep(updateStep);
                if (Update() == 1) 
                    break;
            }
        }
        
        public void StartWithVisuals()
        {
            throw new NotImplementedException();
            
            new Thread(WindowThread).Start();
            
            Start();
        }

        private void WindowThread()
        {
            var window = new RenderWindow(new VideoMode(500, 300), "Physics Simulation", Styles.Default);

            window.Closed += (sender, args) => window.Close();
            
            while (window.IsOpen && running)
            {
                window.Clear();
                window.DispatchEvents();

                foreach (PhysicsObject physicsObject in objects)
                {
                    Shape shape = physicsObject.Bounds.ToShape();
                    shape.Position = physicsObject.Bounds.Center;
                    window.Draw(shape);
                }
                
                window.Display();
            }
        }
        
        public void Stop()
        {
            running = false;
        }

        public void Step()
        {
            Update();
        }
        
        private int Update()
        {
            if (objects.Count == 0)
                return 1;
            
            foreach (var physicsObject in objects.Where(physicsObject => physicsObject.ObjectType.HasFlag(ObjectType.Dynamic)))
                HandleDynamicObject(physicsObject);

            return 0;
        }

        private void HandleDynamicObject(PhysicsObject physicsObject)
        {
            bool grounded = false;
            
            // TODO: Possibly use k-d trees to optimize
            foreach (PhysicsObject otherObject in objects)
            {
                if (physicsObject == otherObject)
                    continue;
                
                if (physicsObject.Bounds.IsOverlapping(otherObject))
                {
                    grounded = true;
                    break;
                }
            }
            
            if (!grounded)
                physicsObject.FallingTime += updateStep;
            else
                physicsObject.FallingTime = 0;

            physicsObject.ApplyGravity(gravity);
        }

        private void HandleKinematicCollision(PhysicsObject objectA, PhysicsObject objectB)
        {
            if (!objectB.ObjectType.HasFlag(ObjectType.Kinematic))
                return;

            
        }
    }
}