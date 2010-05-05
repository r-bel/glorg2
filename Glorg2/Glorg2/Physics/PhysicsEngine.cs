using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Physics
{
    public abstract class PhysicsEngine
    {
        public abstract void AddObject(Glorg2.Scene.Node node);
        public abstract void RemoveObject(Glorg2.Scene.Node node);
    }
}
