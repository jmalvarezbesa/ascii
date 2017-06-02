using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GazeControllerComponent : IControllerComponent
    {
        private bool isMoveDown;
        private int v;
        private int firstPosition;
        private bool init;
        
        public GazeControllerComponent(int v)
        {
            this.isMoveDown = true;
            this.v = v;
            this.init = true;
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            if (init)
            {
                firstPosition = gameObject.position[1];
                init = false;
            }
            if (gameObject.position[1] == firstPosition + 10) isMoveDown = false;
            if (gameObject.position[1] == firstPosition) isMoveDown = true;
            if (isMoveDown)
            {
                gameObject.velocity[1] = v;
            }
            else
            {
                gameObject.velocity[1] = -v;
            }
        }
    }
}
