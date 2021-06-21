using System;

namespace BasicPhysicsEngine
{
    [Flags]
    public enum ObjectType
    {
        Default = Dynamic | Kinematic,
        Static = 1<<1,
        Dynamic = 1<<2,
        Kinematic = 1<<3
    }
}