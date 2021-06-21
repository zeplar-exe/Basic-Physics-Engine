namespace BasicPhysicsEngine
{
    public struct Milliseconds
    {
        private int value;

        public Milliseconds(int milliseconds)
        {
            value = milliseconds;
        }

        public float ToSeconds()
        {
            return value / 1000f;
        }

        public static Milliseconds FromSeconds(int seconds)
        {
            return new(seconds * 1000);
        }
        
        public static Milliseconds FromSeconds(float seconds)
        {
            return new((int)seconds * 1000);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static Milliseconds operator +(Milliseconds a, Milliseconds b) => new(a.value + b.value);
        public static Milliseconds operator -(Milliseconds a, Milliseconds b) => new(a.value - b.value);
        public static Milliseconds operator *(Milliseconds a, Milliseconds b) => new(a.value * b.value);
        public static Milliseconds operator /(Milliseconds a, Milliseconds b) => new(a.value / b.value);

        public static implicit operator int(Milliseconds ms) => ms.value;
        public static implicit operator Milliseconds(int ms) => new(ms);
    }
}