namespace BasicPhysicsEngine
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, b.X + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, b.X - b.Y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, b.X * b.Y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, b.X / b.Y);
    }
}