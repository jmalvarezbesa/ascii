using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class ArrowControllerComponent : IControllerComponent
    {
        private Random rnd;
        private int width;
        private int height;

        public ArrowControllerComponent(int seed, int width, int height)
        {
            this.rnd = new Random(seed);
            this.width = width;
            this.height = height;
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            gameObject.velocity[0] = rnd.Next(0, width) - gameObject.position[0];
            gameObject.velocity[1] = rnd.Next(0, height)- gameObject.position[1];
        }
    }
}
