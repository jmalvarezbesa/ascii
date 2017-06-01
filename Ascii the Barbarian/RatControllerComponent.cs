using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class RatControllerComponent :  IControllerComponent
    {
        private int v;

        public RatControllerComponent()
        {
            this.v = 1;
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            WandersMovement(gameObject);
        }

        public void WandersMovement(GameObject gameObject)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            switch (rnd.Next(0, 3)% 4)
            {
                case 0:
                    gameObject.velocity[0] = v;
                    break;
                case 1:
                    gameObject.velocity[0] = -v;
                    break;
                case 2:
                    gameObject.velocity[1] = v;
                    break;
                case 3:
                    gameObject.velocity[1] = -v;
                    break;
            }
        }

        public void FleesMovement(GameObject gameObject)
        {

        }

    }
}
