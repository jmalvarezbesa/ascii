using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class AsciiPhysicsComponent : IPhysicsComponent
    {
        public List<IObserver> observers = new List<IObserver>();
        private double deltaTime;

        public AsciiPhysicsComponent(double deltaTime)
        {
            this.deltaTime = (double)(deltaTime / 1000.0);
        }

        public void Update(GameObject ascii, List<GameObject> gameObjects)
        {
            ascii.existCollition = false;
            foreach (GameObject gameObject in gameObjects)
            {
                if ((gameObject.GetPosition()[0] == (ascii.position[0] + ascii.velocity[0]) && (gameObject.GetPosition()[1] == ascii.position[1] + ascii.velocity[1])) && gameObject.tag != "User" && gameObject.tag != "Rat") 
                {
                    ascii.existCollition = true;
                    ascii.velocity = new int[] { 0, 0 };
                    ascii.positionBuffer = new double[] { 0.0, 0.0 };

                    foreach (IObserver observer in this.observers)
                    {
                        observer.OnNotify(gameObject, gameObject.tag);
                    }

                    break;
                }
            }
            //Pf = Pi + v * deltaTime
            //positionBuffer exist to compensate the movement
            double vx = ascii.velocity[0] * deltaTime + ascii.positionBuffer[0];
            double vy = (double)ascii.velocity[1] * deltaTime + ascii.positionBuffer[1];

            //x
            if (ascii.velocity[0] > 0)
            {
                if (ascii.positionBuffer[0] < 0) { ascii.positionBuffer[0] = 0; }
                if (vx < 1)
                {
                    ascii.positionBuffer[0] += (double)Math.Round(vx, 4);
                }
                else
                {
                    ascii.position[0] += (int)Convert.ToInt32(Math.Truncate(vx));
                    ascii.positionBuffer[0] = (double)Math.Round(vx - Math.Truncate(vx), 4);
                }
            }
            else if (ascii.velocity[0] < 0)
            {
                if (ascii.positionBuffer[0] > 0) { ascii.positionBuffer[0] = 0; }
                if (vx > -1)
                {
                    ascii.positionBuffer[0] += (double)Math.Round(vx, 4); ;
                }
                else
                {
                    ascii.position[0] += (int)Convert.ToInt32(Math.Truncate(vx));
                    ascii.positionBuffer[0] = (double)Math.Round(vx - Math.Truncate(vx), 4);
                }
            }
            //y
            if (ascii.velocity[1] > 0)
            {
                if (ascii.positionBuffer[1] < 0) { ascii.positionBuffer[1] = 0; }
                if (vy < 1)
                {
                    ascii.positionBuffer[1] += (double)Math.Round(vy, 4);
                }
                else
                {
                    ascii.position[1] += (int)Convert.ToInt32(Math.Truncate(vy));
                    ascii.positionBuffer[1] = (double)Math.Round(vy - Math.Truncate(vy), 4);
                }
            }
            else if (ascii.velocity[1] < 0)
            {
                if (ascii.positionBuffer[1] > 0) { ascii.positionBuffer[1] = 0; }
                if (vy > -1)
                {
                    ascii.positionBuffer[1] += (double)Math.Round(vy, 4); ;
                }
                else
                {
                    ascii.position[1] += (int)Convert.ToInt32(Math.Truncate(vy));
                    ascii.positionBuffer[1] = (double)Math.Round(vy - Math.Truncate(vy), 4);
                }
            }
            ascii.velocity = new int[] { 0, 0 };
        }
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
    }
}
