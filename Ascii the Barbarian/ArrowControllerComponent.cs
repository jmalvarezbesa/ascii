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

        public ArrowControllerComponent(int seed)
        {
            this.rnd = new Random(seed);
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            gameObject.velocity[0] = rnd.Next(0, Console.WindowWidth) - gameObject.position[0];
            gameObject.velocity[1] = rnd.Next(0, Console.WindowHeight)- gameObject.position[1];
        }
    }
}
