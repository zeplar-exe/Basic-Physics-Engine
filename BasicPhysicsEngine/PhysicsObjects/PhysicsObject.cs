using System;

namespace BasicPhysicsEngine.PhysicsObjects
{
    public sealed class PhysicsObject
    {
        public readonly ObjectBounds Bounds;
        
        public string Name;
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
        }
        
        public PhysicsObject(ObjectBounds boundsType)
        {
            Bounds = boundsType;
        }

        public void SetName(string name) => Name = name;

        public void SetPosition(Vector2 position)
        {
            Bounds.Center = position;
            
            PositionChanged?.Invoke(position, new PhysicsEventArgs());
            BoundsChanged?.Invoke(Bounds, new PhysicsEventArgs());
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

        internal void ApplyGravity(Gravity gravity)
        {
            Bounds.Center -= new Vector2(0, gravity.GetPositionAfterT(FallingTime));
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