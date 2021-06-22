using SFML.Graphics;

namespace BasicPhysicsEngine.PhysicsObjects
{
    public class ColorWrapper
    {
        public ColorWrapper(int r, int g, int b)
        {
            value = new Color((byte)r, (byte)g, (byte)b);
        }

        private Color value;

        public static implicit operator Color(ColorWrapper wrapper) => wrapper.value;
    }
}