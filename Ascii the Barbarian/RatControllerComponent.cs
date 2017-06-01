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
        private int index;
        static private Random rnd = new Random(1);

        public RatControllerComponent()
        {
            this.v = 1;
            this.index = 0;
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            WandersMovement(gameObject);
            index++;
        }

        public void WandersMovement(GameObject gameObject)
        {
            if (index%10==0)
            {
                int caseSwitch = rnd.Next(0, 3);
                switch (caseSwitch)
                {
                    case 0:
                        gameObject.velocity[0] = v;
                        gameObject.velocity[1] = 0;
                        break;
                    case 1:
                        gameObject.velocity[0] = -v;
                        gameObject.velocity[1] = 0;
                        break;
                    case 2:
                        gameObject.velocity[0] = 0;
                        gameObject.velocity[1] = v;
                        break;
                    case 3:
                        gameObject.velocity[0] = 0;
                        gameObject.velocity[1] = -v;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                gameObject.velocity[0] = 0;
                gameObject.velocity[1] = 0;
            }
        }

        public void FleesMovement(GameObject gameObject)
        {

        }

    }
}
