namespace BasicPhysicsEngine
{
    public class Gravity
    {
        private float strength = 9.81f;

        public void SetGravity(float gravity)
        {
            strength = gravity;
        }

        public float GetForceAfterT(float time)
        {
            return strength * time;
        }
    }
}