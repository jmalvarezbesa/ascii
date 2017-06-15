using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class ActionNode : Node
    {
        public string Name;

        public ActionNode(string Name)
        {
            this.Name = Name;
        }
        public override Node Decide(GameObject gameObject, GameObject ascii)
        {
            return this;

        }
    }
}
