﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class RatControllerComponent :  IControllerComponent
    {
        private int v;
        private int n;
        private int radium;
        private int index;
        static private Random rnd = new Random(1);

        public RatControllerComponent()
        {
            this.v = 1;
            this.n = 1;
            this.radium = 4;
            this.index = 0;
        }

        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            GameObject ascii = gameObject;
            foreach (GameObject go in gameObjects)
            {
                if (go.tag == "User")
                {
                    ascii = go;
                    break;
                }
            }
            if (index % 3 == 0)
            {
                WandersMovement(gameObject);
                FleesMovement(gameObject, ascii);
            }
            index++;
        }

        public void WandersMovement(GameObject gameObject)
        {
                int caseSwitch = rnd.Next(0, 4);
                switch (caseSwitch)
                {
                    case 0:
                        gameObject.velocity[0] = v;
                        break;
                    case 1:
                        gameObject.velocity[0] = -v;
                        break;
                    case 2:
                        gameObject.velocity[1] = v;
                        break;
                    case 3:
                        gameObject.velocity[1] = -v;
                        break;
                    default:
                        break;
                }
        }

        public void FleesMovement(GameObject gameObject, GameObject ascii)
        {
            int pfx = ascii.position[0] - gameObject.position[0];
            int pfy = ascii.position[1] - gameObject.position[1];
            if (Math.Sqrt(Math.Pow(pfx,2) + Math.Pow(pfy,2)) < radium)
            {
                int dfx = (-n) * (pfx - gameObject.position[0]);
                int dfy = (-n) * (pfy - gameObject.position[1]);
                if (pfx > 0){ dfx = -dfx; }
                if (pfy > 0){ dfy = -dfy; }
                gameObject.velocity[0] = dfx;
                gameObject.velocity[1] = dfy;
            }            
        }
    }
}
