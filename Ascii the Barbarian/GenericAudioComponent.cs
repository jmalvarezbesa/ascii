using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ascii_the_Barbarian
{
    class GenericAudioComponent : IAudioComponent, IObserver 
    {
        public void Update(GameObject gameObject)
        {
            if (gameObject.existCollition)
            {
                Console.Beep();
                
            }
        }
        public static void MakeSound()
        {
            Console.Beep(800, 125);
        }

        public void OnNotify(GameObject object_, string event_)
        {
            if (event_ == "Wall")
            {
                Thread oThread = new Thread(new ThreadStart(MakeSound));
                oThread.Start();
            }
        }
    }
}
