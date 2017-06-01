using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class GenericGraphicsComponent : IGraphicsComponent
    {
        public char symbol;

        public GenericGraphicsComponent(char symbol)
        {
            this.symbol = symbol;
        }
        public char GetSymbol()
        {
            return this.symbol;
        }
        public void Update(GameObject gameObject, DoubleBuffer.DoubleGraphicsBuffer doubleBuffer)
        {
            doubleBuffer.Draw(gameObject.GetSymbol(), gameObject.position[0], gameObject.position[1]);
        }
    }
}
