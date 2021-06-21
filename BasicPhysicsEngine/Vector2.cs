using SFML.System;

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

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, int b) => new(a.X * b, a.Y * b);
        public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
        
        public static implicit operator Vector2f(Vector2 v2) => new Vector2f(v2.X, v2.Y);
    }
}