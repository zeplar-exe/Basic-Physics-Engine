namespace BasicPhysicsEngine.PhysicsObjects
{
    public enum BoundsType { None, Rectangle }
    
    internal class ObjectBounds
    {
        private Vector2 center;

        public Vector2 Center
        {
            set
            {
                var change = value - center;
                
                center = value;
            
                Top += change;
                Right += change;
                Right += change;
                Left += change;
            }
            get => center;
        }

        public Vector2 Top;
        public Vector2 Bottom;
        public Vector2 Left;
        public Vector2 Right;

        public void Resize(Vector2 size)
        {
            Top = Center + new Vector2(0, size.Y/2);
            Bottom =Center + new Vector2(0, -size.Y/2);
            Left = Center + new Vector2(size.X/2, 0);
            Right = Center + new Vector2(-size.X/2, 0);
        }
    }

    internal class Rectangle : ObjectBounds
    {
        
    }
}