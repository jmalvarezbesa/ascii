using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GenericPhysicsComponent : IPhysicsComponent
    {
        public void Update(GameObject gameObject, List<GameObject> gameObjects)
        {
            gameObject.position[0] += gameObject.velocity[0];
            gameObject.position[1] += gameObject.velocity[1];
            gameObject.velocity = new int[] { 0, 0 };
        }
    }
}
