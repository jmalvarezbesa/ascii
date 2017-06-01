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
        private GraphAlgorithmSolver solver;
        private List<Tuple<int, int>> path; 

        public ZombieControllerComponent(Level level)
        {
            this.solver = new GraphAlgorithmSolver(new Graph(level));
            this.v = 1;
            this.index = 0;
            this.path = new List<Tuple<int, int>>();
        }
        public void Update(GameObject gameObject, movement command, List<GameObject> gameObjects)
        {
            if (index % 4 == 0)
            {
                path = solver.AAlgorithmSolver( , gameObject);
            }
            else
            {

            }
            index++;
        }
    }
}
