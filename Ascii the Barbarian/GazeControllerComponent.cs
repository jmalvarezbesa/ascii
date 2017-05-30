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
        private int counter;
        
        public GazeControllerComponent()
        {
            this.counter = 0;
            this.isMoveDown = true;
        }

        public void Update(GameObject gameObject, movement command)
        {
            if (counter % 3 == 0)
            {
                if (gameObject.position[1] == 10) isMoveDown = false;
                if (gameObject.position[1] == 0) isMoveDown = true;
                if (isMoveDown) gameObject.velocity[1] = 1;
                else
                {
                    gameObject.velocity[1] = -1;
                }
            }
            counter++;
        }
    }
}
