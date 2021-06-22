using BasicPhysicsEngine.PhysicsObjects;

namespace BasicPhysicsEngine
{
    public partial class PhysicsSimulator
    {
        internal void AddObject(PhysicsObject physicsObject)
        {
            objects.Add(physicsObject);
        }

        public void SetPhysicsStep(Milliseconds milliseconds)
        {
            updateStep = milliseconds;
        }

        public void ApplyGravitySettings(Gravity settings)
        {
            gravity = settings;
        }

        public void ApplyTimeSettings(Time settings)
        {
            time = settings;
        }
    }
}