namespace BasicPhysicsEngine
{
    public class Gravity
    {
        private readonly float strength;

        public Gravity(float gravity)
        {
            strength = gravity;
        }

        public float GetOffsetAfterT(Milliseconds milliseconds)
        {
            return strength * milliseconds.ToSeconds();
        }

        public static Gravity Earth()
        {
            return new(-9.81f);
        }
        
        public static Gravity Moon()
        {
            return new(-1.62f);
        }

        public static Gravity Mars()
        {
            return new(-3.71f);
        }
    }
}