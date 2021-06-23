using SFML.Graphics;

namespace BasicPhysicsEngine
{
    public class ColorWrapper
    {
        public ColorWrapper(int r, int g, int b)
        {
            value = new Color((byte)r, (byte)g, (byte)b);
        }

        public static ColorWrapper White = new ColorWrapper(255, 255, 255);
        public static ColorWrapper Black = new ColorWrapper(0, 0, 0);
        public static ColorWrapper Red = new ColorWrapper(255, 0, 0);
        public static ColorWrapper Blue = new ColorWrapper(0, 0, 255);
        public static ColorWrapper Green = new ColorWrapper(0, 255, 0);

        private Color value;

        public static implicit operator Color(ColorWrapper wrapper) => wrapper.value;
    }
}