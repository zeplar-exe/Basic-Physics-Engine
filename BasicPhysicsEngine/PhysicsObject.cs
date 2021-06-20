using System;

namespace BasicPhysicsEngine
{
    public class PhysicsObject
    {
        internal ObjectBounds bounds = new();

        internal ObjectType ObjectType = ObjectType.Default;

        internal bool Grounded;
        internal Milliseconds FallingTime;

        public void SetPosition(Vector2 position)
        {
            bounds.Center = position;
        }

        public void Resize(Vector2 size)
        {
            bounds.Top = bounds.Center + new Vector2(0, size.Y/2);
            bounds.Bottom = bounds.Center + new Vector2(0, -size.Y/2);
            bounds.Left = bounds.Center + new Vector2(size.X/2, 0);
            bounds.Right = bounds.Center + new Vector2(-size.X/2, 0);
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
            if (Grounded)
                return;

            bounds.Center -= new Vector2(0, gravity.GetPositionAfterT(FallingTime));
        }
    }

    public class ObjectBounds
    {
        private Vector2 center;

        public Vector2 Center
        {
            set
            {
                var change = value - center;
                
                center = value;
            
                Top -= change;
                Right -= change;
                Right -= change;
                Left -= change;
            }
            get => center;
        }

        public Vector2 Top;
        public Vector2 Bottom;
        public Vector2 Left;
        public Vector2 Right;
    }
}