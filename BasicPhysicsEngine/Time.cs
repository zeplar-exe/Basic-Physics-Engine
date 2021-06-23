namespace BasicPhysicsEngine
{
    public class Time
    {
        internal float Scale;
        internal Milliseconds ElapsedTime = 0;

        public Time(float timeScale)
        {
            SetTimescale(timeScale);
        }
        
        public void SetTimescale(float timeScale)
        {
            Scale = timeScale;
        }
        
        public static Time Default()
        {
            return new(1);
        }
    }
}