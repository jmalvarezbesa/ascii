using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class BinaryDecisionNode : DecisionNode
    {
        Node YesNode;
        Node NoNode;
        private Evaluator eval;

        public BinaryDecisionNode(Evaluator eval, Node YesNode, Node NoNode)
        {
            this.eval = eval;
            this.YesNode = YesNode;
            this.NoNode = NoNode;
        }
        public override Node GetBranch(GameObject gameObject, GameObject ascii)
        {
           if (Evaluate(gameObject, ascii)){
                return YesNode;
            }
            else
            {
                return NoNode;
            }
        }
        private bool Evaluate(GameObject gameObject, GameObject ascii)
        {
            return eval(gameObject, ascii);
        }
    }
}
