using System;

namespace BasicPhysicsEngine
{
    [Flags]
    public enum CollisionArea
    {
        None = 1<<1,
        Top = 1<<2,
        Left = 1<<3,
        Right = 1<<4,
        Bottom = 1<<5
    }
}