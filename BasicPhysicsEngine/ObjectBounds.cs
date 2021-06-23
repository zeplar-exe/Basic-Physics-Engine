using System;
using SFML.Graphics;
using SFML.System;

namespace BasicPhysicsEngine
{
    public enum BoundsType { Rectangle }
    
    public abstract class ObjectBounds
    {
        protected Vector2 min => center - new Vector2(Left.X / 2, Bottom.Y / 2);
        protected Vector2 max => center + new Vector2(Right.X / 2, Top.Y / 2);

        protected Vector2 center;
        protected Vector2 size;

        public abstract Vector2 Center { get; set; }

        protected Vector2 top;
        public abstract Vector2 Top { get; set; }

        
        protected Vector2 bottom;
        public abstract Vector2 Bottom { get; set; }

        
        protected Vector2 left;
        public abstract Vector2 Left { get; set; }

        
        protected Vector2 right;
        public abstract Vector2 Right { get; set; }

        public abstract void Resize(Vector2 newSize);

        public abstract CollisionArea IsOverlapping(PhysicsObject other);

        public abstract Shape ToShape();
        
        public abstract override string ToString();
    }

    internal class Rectangle : ObjectBounds
    {
        public override Vector2 Center
        {
            set
            {
                var change = value - center;

                center = value;
            
                Top += change;
                Bottom += change;
                Right += change;
                Left += change;
            }
            get => center;
        }
        
        public override Vector2 Top
        {
            get => top;
            set => top = value;
        }

        public override Vector2 Bottom
        {
            get => bottom;
            set => bottom = value;
        }

        public override Vector2 Left
        {
            get => left;
            set => left = value;
        }
        
        public override Vector2 Right
        {
            get => right;
            set => right = value;
        }

        public override void Resize(Vector2 newSize)
        {
            size = newSize;
            
            Top = Center + new Vector2(0, newSize.Y/2);
            Bottom = Center + new Vector2(0, -(newSize.Y/2));
            Left = Center + new Vector2(-(newSize.X/2), 0);
            Right = Center + new Vector2(newSize.X/2, 0);
        }

        public override CollisionArea IsOverlapping(PhysicsObject other)
        {
            CollisionArea collisionArea = CollisionArea.None;

            if (!(Left.X < other.Bounds.Right.X && Right.X > other.Bounds.Left.X &&
                  Top.X > other.Bounds.Bottom.Y && Bottom.Y < other.Bounds.Top.Y))
                return collisionArea;

            if (Left.X <= other.Bounds.Right.X && Right.X >= other.Bounds.Right.X)
                collisionArea |= CollisionArea.Right;

            if (Right.X >= other.Bounds.Left.X && Left.X <= other.Bounds.Left.X)
                collisionArea |= CollisionArea.Left;

            if (Bottom.Y <= other.Bounds.Top.Y && Top.Y >= other.Bounds.Top.Y)
                collisionArea |= CollisionArea.Top;
            
            if (Top.Y >= other.Bounds.Bottom.Y && Bottom.Y <= other.Bounds.Bottom.Y)
                collisionArea |= CollisionArea.Bottom;

            return collisionArea;
        }

        public override Shape ToShape()
        {
            return new RectangleShape
            {
                Size = size,
                Origin = new Vector2f(size.X / 2, size.Y / 2),
                FillColor = Color.White
            };
        }

        public override string ToString()
        {
            return $"X: ({Left}, {Right}), Y: ({Top}, {Bottom})";
        }
    }
}