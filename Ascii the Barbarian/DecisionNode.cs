using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    abstract class  DecisionNode : Node
    {
        public abstract Node GetBranch(GameObject gameObject, GameObject ascii);
        public override Node Decide(GameObject gameObject, GameObject ascii)
        {
            return GetBranch(gameObject, ascii).Decide(gameObject, ascii);
        }
    }
}
