using System;
using SFML.Graphics;

namespace BasicPhysicsEngine.PhysicsObjects
{
    public sealed class PhysicsObject
    {
        public readonly ObjectBounds Bounds;
        internal Vector2 Velocity;
        
        public readonly PhysicsObjectConfiguration ObjectConfiguration;
        
        internal ObjectType ObjectType = ObjectType.Default;

        public PhysicsObject(BoundsType boundsType)
        {
            switch (boundsType)
            {
                case BoundsType.Rectangle:
                    Bounds = new Rectangle();
                    break;
                default:
                    goto case BoundsType.Rectangle;
            }
            
            ObjectConfiguration = new PhysicsObjectConfiguration
            {
                Name = "", Color = new ColorWrapper(255, 255, 255), Mass = 1
            };
        }
        
        public PhysicsObject(ObjectBounds boundsType)
        {
            Bounds = boundsType;

            ObjectConfiguration = new PhysicsObjectConfiguration
            {
                Name = "", Color = new ColorWrapper(255, 255, 255), Mass = 1
            };
        }
        
        public void SetPosition(Vector2 position)
        {
            Bounds.Center = position;
        }

        public void SetConfiguration(PhysicsObjectConfiguration configuration)
        {
            ObjectConfiguration.Name = configuration.Name;
            ObjectConfiguration.Mass = configuration.Mass;
            ObjectConfiguration.Color = configuration.Color;
        }
        
        public void Resize(Vector2 size)
        {
            Bounds.Resize(size);
        }

        public void SetObjectType(ObjectType type)
        {
            ObjectType = type;
        }

        public void AddToSimulator(PhysicsSimulator simulator)
        {
            simulator.AddObject(this);
        }
    }
}