using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    abstract class Node
    {
        public abstract Node Decide(GameObject gameObject, GameObject ascii);
    }
}
