using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GenericPhysicsComponent : IPhysicsComponent
    {
        private double deltaTime;

        public GenericPhysicsComponent(int deltaTime)
        {
            this.deltaTime = (double)(deltaTime / 1000.0);
        }

        public void Update(GameObject gameObject, List<GameObject> gameObjects)
        {
            //Pf = Pi + v * deltaTime
            //positionBuffer exist to compensate the movement
            double vx = (double)gameObject.velocity[0] * deltaTime + gameObject.positionBuffer[0];
            double vy = (double)gameObject.velocity[1] * deltaTime + gameObject.positionBuffer[1];

            //x
            if (gameObject.velocity[0] > 0)
            {
                if (vx < 1)
                {
                    gameObject.positionBuffer[0] += vx;
                }
                else
                {
                    gameObject.position[0] += Convert.ToInt32(Math.Truncate(vx));
                    gameObject.positionBuffer[0] = Math.Round(vx - Math.Truncate(vx), 4);
                }
            }
            else
            {
                if (vx < -1)
                {
                    gameObject.positionBuffer[0] += vx;
                }
                else
                {
                    gameObject.position[0] += Convert.ToInt32(Math.Truncate(vx));
                    gameObject.positionBuffer[0] = Math.Round(vx - Math.Truncate(vx), 4);
                }
            }
            //y
            if (gameObject.velocity[1] > 0)            {
                if (vy < 1)
                {
                    gameObject.positionBuffer[1] += vy;
                }
                else
                {
                    gameObject.position[1] += Convert.ToInt32(Math.Truncate(vy));
                    gameObject.positionBuffer[1] = Math.Round(vy - Math.Truncate(vy), 4);
                }
            }
            else
            {
                if (vy <-1)                {
                    gameObject.positionBuffer[1] += vy;
                }
                else
                {
                    gameObject.position[1] += Convert.ToInt32(Math.Truncate(vy));
                    gameObject.positionBuffer[1] = Math.Round(vy - Math.Truncate(vy), 4);
                }
            }
        }
    }
}
