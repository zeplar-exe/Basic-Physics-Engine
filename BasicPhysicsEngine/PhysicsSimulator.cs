using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BasicPhysicsEngine.PhysicsObjects;

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

            var updateThread = new Thread(UpdateLoop);
            updateThread.Start();
        }

        private void UpdateLoop()
        {
            while (running)
            {
                Thread.Sleep(updateStep);
                Update();
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
        
        private void Update()
        {
            foreach (var physicsObject in objects.Where(physicsObject => physicsObject.ObjectType.HasFlag(ObjectType.Dynamic)))
                HandleDynamicObject(physicsObject);
        }

        private void HandleDynamicObject(PhysicsObject physicsObject)
        {
            bool grounded = false;
            
            // TODO: Possibly use k-d trees to optimize
            foreach (PhysicsObject otherObject in objects)
            {
                if (physicsObject == otherObject)
                    continue;
                
                // TODO: Implement overlap checking
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