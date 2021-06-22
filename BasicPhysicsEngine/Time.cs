namespace BasicPhysicsEngine
{
    public class Time
    {
        internal float Scale = 1;
        internal Milliseconds ElapsedTime = 0;

        public void SetTimescale(float timeScale)
        {
            Scale = timeScale;
        }
        
        public static Time Default()
        {
            return new();
        }
    }
}