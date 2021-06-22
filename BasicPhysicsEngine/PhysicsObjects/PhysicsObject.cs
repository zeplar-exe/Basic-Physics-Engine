using System;
using SFML.Graphics;

namespace BasicPhysicsEngine.PhysicsObjects
{
    public sealed class PhysicsObject
    {
        public readonly ObjectBounds Bounds;
        internal Vector2 velocity;
        
        public readonly PhysicsObjectConfiguration ObjectConfiguration;
        
        internal ObjectType ObjectType = ObjectType.Default;
        
        internal Milliseconds FallingTime;

        public PhysicsObject(BoundsType boundsType)
        {
            switch (boundsType)
            {
                case BoundsType.Rectangle:
                    Bounds = new Rectangle();
                    break;
                default:
                    goto case BoundsType.Rectangle;
            }
            
            ObjectConfiguration = new PhysicsObjectConfiguration
            {
                Name = "", Color = Color.White, Mass = 1
            };
        }
        
        public PhysicsObject(ObjectBounds boundsType)
        {
            Bounds = boundsType;

            ObjectConfiguration = new PhysicsObjectConfiguration
            {
                Name = "", Color = Color.White, Mass = 1
            };
        }
        
        public void SetPosition(Vector2 position)
        {
            Bounds.Center = position;
            
            PositionChanged?.Invoke(position, new PhysicsEventArgs());
            BoundsChanged?.Invoke(Bounds, new PhysicsEventArgs());
        }

        public void SetConfiguration(PhysicsObjectConfiguration configuration)
        {
            ObjectConfiguration.Name = configuration.Name;
            ObjectConfiguration.Mass = configuration.Mass;
            ObjectConfiguration.Color = configuration.Color;
        }
        
        public void Resize(Vector2 size)
        {
            Bounds.Resize(size);
            
            BoundsChanged?.Invoke(Bounds, new PhysicsEventArgs());
        }

        public void SetObjectType(ObjectType type)
        {
            ObjectType = type;
        }

        public void AddToSimulator(PhysicsSimulator simulator)
        {
            simulator.AddObject(this);
        }
        

        public class PhysicsEventArgs
        {
            public long UnixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
        public delegate void ListenerEventHandler(object message, PhysicsEventArgs args);
        internal event ListenerEventHandler PositionChanged;
        internal event ListenerEventHandler BoundsChanged;
        internal event ListenerEventHandler OnCollision;

        internal void InvokeCollision(PhysicsObject other)
        {
            OnCollision?.Invoke(other, new PhysicsEventArgs());
        }

        public void ListenTo(ListenerType listenerType, Action<object, PhysicsEventArgs> callback)
        {
            switch (listenerType)
            {
                case ListenerType.Position:
                    PositionChanged += new ListenerEventHandler(callback);
                    break;
                case ListenerType.Bounds:
                    BoundsChanged += new ListenerEventHandler(callback);
                    break;
                case ListenerType.Collision:
                    OnCollision += new ListenerEventHandler(callback);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum ListenerType
    {
        Position,
        Bounds,
        Collision,
    }
}