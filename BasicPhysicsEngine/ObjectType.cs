using System;

namespace BasicPhysicsEngine
{
    [Flags]
    public enum ObjectType
    {
        Default = 1<<2 | 1<<3,
        Static = 1<<1,
        Dynamic = 1<<2,
        Kinematic = 1<<3
    }
}