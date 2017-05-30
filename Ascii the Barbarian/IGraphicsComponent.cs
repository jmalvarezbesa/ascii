using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    interface IGraphicsComponent
    {
        void Update(GameObject gameObject, char graphic);
        char GetSymbol();
    }
}
