using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class NullAudioComponent : IAudioComponent
    {
        public void OnNotify(GameObject object_, string event_)
        {
        }

        public void Update(GameObject gameObject)
        {
        }
    }
}
