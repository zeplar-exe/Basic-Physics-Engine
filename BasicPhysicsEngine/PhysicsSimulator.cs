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
                Thread.Sleep(ScaledStep);
                
                time.ElapsedTime += ScaledStep;
                
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
            var window = new RenderWindow(new VideoMode(800, 500), "Physics Simulation", Styles.Default);
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

            window.MouseWheelScrolled += delegate(object sender, MouseWheelScrollEventArgs args)
            {
                if (args.Delta <= -1)
                    zoomLevel = Math.Min(2f, zoomLevel + .1f);
                else if (args.Delta >= 1)
                    zoomLevel = Math.Max(.5f, zoomLevel - .1f);
                
                view.Size = window.DefaultView.Size;
                view.Zoom(zoomLevel);
                window.SetView(view);
            };
            
            window.Resized += delegate(object? sender, SizeEventArgs args)
            {
                view.Size = new Vector2f(args.Width, args.Height);;
                view.Center = new Vector2f(args.Width / 2f, args.Height / 2f);
                
                window.SetView(view);
            };

            #endregion

            while (window.IsOpen && running)
            {
                var windowCenter = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

                window.Clear();
                window.DispatchEvents();

                foreach (PhysicsObject physicsObject in objects)
                {
                    var shape = physicsObject.Bounds.ToShape();
                    shape.Position = windowCenter - physicsObject.Bounds.Center;
                    shape.FillColor = physicsObject.ObjectConfiguration.Color;

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
            Vector2 gravityAcceleration = new Vector2(0, gravity.GetOffsetAfterT(time.ElapsedTime) /
                                        physicsObject.ObjectConfiguration.Mass);

            physicsObject.velocity += gravityAcceleration * ScaledStep.ToSeconds();
            physicsObject.Bounds.Center += physicsObject.velocity * ScaledStep.ToSeconds();

            foreach (PhysicsObject otherObject in objects)
            {
                if (otherObject == physicsObject)
                    continue;

                CollisionArea area = physicsObject.Bounds.IsOverlapping(otherObject);

                if (area.HasFlag(CollisionArea.None))
                    return;
                    
                if (area.HasFlag(CollisionArea.Left) || area.HasFlag(CollisionArea.Right))
                {
                    physicsObject.velocity.X = -physicsObject.velocity.X * 0.75f;
                }
                
                if (area.HasFlag(CollisionArea.Bottom) || area.HasFlag(CollisionArea.Top))
                {
                    physicsObject.velocity.Y = -physicsObject.velocity.Y * 0.75f;
                }
            }
        }

        private void HandleKinematicCollision(PhysicsObject objectA, PhysicsObject objectB)
        {
            objectA.InvokeCollision(objectB);

            if (!objectB.ObjectType.HasFlag(ObjectType.Kinematic))
                return;
        }

        private Milliseconds ScaledStep => Milliseconds.FromSeconds(updateStep.ToSeconds() * time.Scale);
    }
}