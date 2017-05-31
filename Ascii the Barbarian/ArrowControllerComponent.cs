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
        private int counter;

        public ArrowControllerComponent(int seed)
        {
            this.counter = 0;
            this.rnd = new Random(seed);
        }

        public void Update(GameObject gameObject, movement command)
        {
            gameObject.velocity[0] = rnd.Next(0, Console.WindowWidth);
                gameObject.velocity[1] = rnd.Next(0, Console.WindowHeight);
                gameObject.position[0] = 20;
                gameObject.position[1] = 20;
        }
    }
}
