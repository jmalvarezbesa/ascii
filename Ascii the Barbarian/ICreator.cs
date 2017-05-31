using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    interface ICreator
    {
        GameObject CreateObject(int x, int y, MapSymbol symbol);
    }
}
