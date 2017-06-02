using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class ZombieControllerComponent : IControllerComponent
    {
        private int v;
        private int index;

        public ZombieControllerComponent()
        {
            this.v = 1;
            this.index = 0;
        }
        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            
        }
    }
}
