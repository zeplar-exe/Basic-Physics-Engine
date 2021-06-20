using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicPhysicsEngine
{
    public class PhysicsInstance
    {
        private int updateStep;
        
        private List<PhysicsObject> objects = new List<PhysicsObject>();

        private bool running;

        public void SetPhysicsStep(int milliseconds)
        {
            updateStep = milliseconds;
        }
        
        public void Start()
        {
            running = true;

            var updateThread = new Thread(UpdateLoop);
            updateThread.Start();
        }

        private void UpdateLoop()
        {
            while (running)
            {
                Thread.Sleep(updateStep);
                Update();
            }
        }

        public void Stop()
        {
            running = false;
        }

        public void Step()
        {
            Update();
        }
        
        private void Update()
        {
            foreach (PhysicsObject physicsObject in objects)
                ;
        }
    }
}