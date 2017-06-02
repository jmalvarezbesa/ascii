using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    class ZombieControllerComponent : IControllerComponent
    {
        private int v;
        private int index;
        private int index2;
        private GraphAlgorithmSolver solver;
        private Level lvl;
        private List<Tuple<int, int>> path; 

        public ZombieControllerComponent(Level level)
        {
            this.solver = new GraphAlgorithmSolver();
            this.lvl = level;
            this.v = 1;
            this.index = 0;
            this.index2 = 0;
            this.path = new List<Tuple<int, int>>();
        }
        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            GameObject ascii = gameObject;
            if (index % 2 == 0)
            {
                if (index2 % 5 == 0)
                {
                    foreach (GameObject go in gameObjects)
                    {
                        if (go.tag == "User")
                        {
                            ascii = go;
                            path = solver.AAlgorithmSolver(go, gameObject, lvl);
                            int difx = ascii.position[0] - path[path.Count - 1].Item1;
                            int dify = ascii.position[1] - path[path.Count - 1].Item2;
                            if (difx != 0) { gameObject.velocity[0] = -v * (difx / Math.Abs(difx)); }
                            if (dify != 0) { gameObject.velocity[1] = -v * (dify / Math.Abs(dify)); }
                            break;
                        }
                    }
                }
                else
                {
                    if ((path.Count - 1 - index2 % 5) > 0)
                    {
                        int difx = ascii.position[0] - path[path.Count - 1 - index2%5].Item1;
                        int dify = ascii.position[1] - path[path.Count - 1 - index2%5].Item2;
                        if (difx != 0) { gameObject.velocity[0] = -v * (difx / Math.Abs(difx)); }
                        if (dify != 0) { gameObject.velocity[1] = -v * (dify / Math.Abs(dify)); }
                    }
                }
                index2++;
            }
            index++;
        }
    }
}
