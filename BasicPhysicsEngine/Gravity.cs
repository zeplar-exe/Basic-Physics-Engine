namespace BasicPhysicsEngine
{
    public class Gravity
    {
        private float strength = -9.81f;

        public void SetGravity(float gravity)
        {
            strength = gravity;
        }

        public float GetOffsetAfterT(Milliseconds milliseconds)
        {
            return strength * milliseconds.ToSeconds();
        }

        public static Gravity Default()
        {
            return new();
        }
    }
}