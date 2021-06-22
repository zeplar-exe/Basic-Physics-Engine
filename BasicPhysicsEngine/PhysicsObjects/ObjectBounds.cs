using System;
using SFML.Graphics;
using SFML.System;

namespace BasicPhysicsEngine.PhysicsObjects
{
    public enum BoundsType { Rectangle }
    
    public abstract class ObjectBounds
    {
        protected Vector2 size;
        
        protected Vector2 center { get; set; }
        public abstract Vector2 Center { get; set; }

        protected Vector2 top;
        public abstract Vector2 Top { get; set; }

        
        protected Vector2 bottom;
        public abstract Vector2 Bottom { get; set; }

        
        protected Vector2 left;
        public abstract Vector2 Left { get; set; }

        
        protected Vector2 right;
        public abstract Vector2 Right { get; set; }

        public abstract void Resize(Vector2 size);

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

        public override void Resize(Vector2 size)
        {
            this.size = size;
            
            Top = Center + new Vector2(0, size.Y/2);
            Bottom = Center + new Vector2(0, -(size.Y/2));
            Left = Center + new Vector2(-(size.X/2), 0);
            Right = Center + new Vector2(size.X/2, 0);
        }

        public override CollisionArea IsOverlapping(PhysicsObject other)
        {
            CollisionArea collisionArea = CollisionArea.None;

            if (left.X <= other.Bounds.Right.X && Right.X >= other.Bounds.Right.X)
                collisionArea |= CollisionArea.Left;

            if (Right.X >= other.Bounds.Left.X && Left.X <= other.Bounds.Left.X)
                collisionArea |= CollisionArea.Right;

            if (Bottom.Y <= other.Bounds.Top.Y && Top.Y >= other.Bounds.Top.Y)
                return CollisionArea.Bottom;
            
            if (Top.Y >= other.Bounds.Bottom.Y && Bottom.Y <= other.Bounds.Bottom.Y)
                return CollisionArea.Top;

            return collisionArea;

            //return left.X < other.Bounds.Right.X && Right.X > other.Bounds.Left.X &&
            //Top.Y > other.Bounds.Bottom.Y && Bottom.Y < other.Bounds.Top.Y;
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