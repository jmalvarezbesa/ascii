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
        
        public GazeControllerComponent()
        {
            int v = 1;
            this.isMoveDown = true;
            this.v = v;
        }

        public void Update(GameObject gameObject, movement command)
        {
            gameObject.positionBuffer[0] = 0;
            if (gameObject.position[1] == 10) isMoveDown = false;
            if (gameObject.position[1] == 0) isMoveDown = true;
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
