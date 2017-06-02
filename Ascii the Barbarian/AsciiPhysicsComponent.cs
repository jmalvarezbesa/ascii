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

        public void Update(GameObject ascii, List<GameObject> gameObjects)
        {
            ascii.existCollition = false;
            foreach (GameObject gameObject in gameObjects)
            {
                if ((gameObject.GetPosition()[0] == (ascii.position[0] + ascii.velocity[0]) && (gameObject.GetPosition()[1] == ascii.position[1] + ascii.velocity[1])) && gameObject.tag != "User") 
                {
                    ascii.existCollition = true;
                    ascii.velocity = new int[] { 0, 0 };

                    foreach (IObserver observer in this.observers)
                    {
                        observer.OnNotify(gameObject, gameObject.tag);
                    }

                    break;
                }
            }
            ascii.position[0] += ascii.velocity[0];
            ascii.position[1] += ascii.velocity[1];
            ascii.velocity = new int[] { 0, 0 };
        }
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
    }
}
