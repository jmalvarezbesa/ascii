﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class NullControllerComponent : IControllerComponent
    {
        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
        }
    }
}
