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
    public partial class PhysicsSimulator
    {
        private bool running;
        private Milliseconds updateStep;

        private Gravity gravity;
        private Time time;

        private readonly List<PhysicsObject> objects = new();

        public void Start()
        {
            if (gravity == null)
                throw new Exception(
                    "Simulation gravity settings cannot be null. Did you forget to call 'ApplyGravitySettings'?");
            if (time == null)
                throw new Exception(
                    "Simulation time settings cannot be null. Did you forget to call 'ApplyTimeSettings'?");

            running = true;

            new Thread(UpdateLoop).Start();
        }

        private void UpdateLoop()
        {
            while (running)
            {
                Thread.Sleep(updateStep);
                Update();
            }
        }

        public void StartWithVisuals()
        {
            new Thread(WindowThread).Start();

            Start();
        }

        private void WindowThread()
        {
            var window = new RenderWindow(new VideoMode(800, 500), "Physics Simulation", Styles.Close);
            // TODO: Figure out how to allow for window resizing and keep proportions

            bool movingDisplay = false;
            float zoomLevel = 1;
            Vector2f oldPos = new Vector2f();
            View view = window.DefaultView;

            #region Event Handlers
            
            window.Closed += delegate
            {
                window.Close();
                Stop();
            };

            window.MouseButtonPressed += delegate(object _, MouseButtonEventArgs args)
            {
                if (args.Button == Mouse.Button.Left)
                {
                    movingDisplay = true;
                    oldPos = window.MapPixelToCoords(new Vector2i(args.X, args.Y));
                }
            };

            window.MouseButtonReleased += delegate(object _, MouseButtonEventArgs args)
            {
                if (args.Button == Mouse.Button.Left)
                    movingDisplay = false;
            };

            window.MouseMoved += delegate(object _, MouseMoveEventArgs args)
            {
                if (!movingDisplay)
                    return;

                Vector2f newPosition = window.MapPixelToCoords(new Vector2i(args.X, args.Y));
                Vector2f delta = newPosition - oldPos;

                view.Center -= delta;
                window.SetView(view);
            };

            window.MouseWheelScrolled += delegate
            {
                view.Size = window.DefaultView.Size;
                view.Zoom(zoomLevel);
                window.SetView(view);
            };

            window.Resized += (_, _) => window.SetView(view);

            #endregion
            
            Vector2f windowCenter;

            while (window.IsOpen && running)
            {
                windowCenter = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

                window.Clear();
                window.DispatchEvents();

                foreach (PhysicsObject physicsObject in objects)
                {
                    var shape = physicsObject.Bounds.ToShape();
                    shape.Position = windowCenter - physicsObject.Bounds.Center;

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

        private void Update()
        {
            if (objects.Count == 0)
                Stop();

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