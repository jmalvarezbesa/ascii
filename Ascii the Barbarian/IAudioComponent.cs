using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    interface IAudioComponent
    {
        void Update(GameObject gameObject);
        void OnNotify(GameObject object_, string event_);
    }
}
