namespace BasicPhysicsEngine.PhysicsObjects
{
    public sealed class PhysicsObject
    {
        internal ObjectBounds Bounds;
        internal ObjectType ObjectType = ObjectType.Default;
        
        internal Milliseconds FallingTime;

        public PhysicsObject(BoundsType boundsType = BoundsType.None)
        {
            switch (boundsType)
            {
                case BoundsType.None:
                    Bounds = new ObjectBounds();
                    break;
                case BoundsType.Rectangle:
                    Bounds = new Rectangle();
                    break;
                default:
                    goto case BoundsType.None;
            }
        }
        
        public void SetPosition(Vector2 position) => Bounds.Center = position;

        public void Resize(Vector2 size) => Bounds.Resize(size);

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
    }
}