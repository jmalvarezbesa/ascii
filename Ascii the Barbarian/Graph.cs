using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    /// <summary>
    /// A simple 2D cell.
    /// </summary>
    public class Cell
    {
        public int x = -1;
        public int y = -1;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// A node in the graph.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphNode<T>
    {
        // Node data.
        private T data;
        public T Data { get { return data; } }

        // Neighbors to this node.
        private List<T> neighbors = new List<T>();
        public List<T> Neighbors
        {
            get { return neighbors; }
        }

        // We assume cost is always 1.
        private int cost = 1;
        public int Cost { get { return cost; } }

        // Initially all have a gValue of infinite.
        private int gValue = Int32.MaxValue;
        public int GValue { get { return gValue; } set { gValue = value;  } }

        // Heurisic value will be calculated using an heuristic, externally.
        private double hValue = 0;
        public double HValue { get { return hValue; } set { hValue = value; } }

        // Node reference, used to know how I got to this node. Needed when building the path.
        private GraphNode<Cell> cameFrom = null;
        public GraphNode<Cell> CameFrom { get { return cameFrom; } set { cameFrom = value; } }

        // Constructors.
        public GraphNode(T value) { data = value; }
        public GraphNode(T value, List<T> neighbors) { data = value;  this.neighbors = neighbors; }

        // Calculate the total cost so far.
        public double F()
        {
            return gValue + hValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Graph
    {
        // All nodes and their edges.
        private List<GraphNode<Cell>> nodes = new List<GraphNode<Cell>>();
        public List<GraphNode<Cell>> Nodes
        {
            get { return nodes; }
        }

        /// <summary>
        /// Creates a graph from a Level.
        /// </summary>
        /// <param name="level"></param>
        public Graph(Level level)
        {
            for (int y = 0; y < level.Height; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    MapSymbol symbol = level.GetMapSymbol(x, y);
                    if (symbol != MapSymbol.VerticalWall && symbol != MapSymbol.HorizontalWall)
                    {
                        List<Cell> neighbors = new List<Cell>();
                        List<Cell> toCheck = new List<Cell>();

                        // To build a 8-connected graph, these are the potential neighbors.
                        //for (int i=x-1; i<=x+1; i++)
                        //    for(int j=y-1; j<=y+1; j++)
                        //        toCheck.Add(new Cell(i, j));

                        // To build a 4-connected graph, these are the potential neighbors.
                        toCheck.Add(new Cell(x-1, y));
                        toCheck.Add(new Cell(x+1, y));
                        toCheck.Add(new Cell(x, y-1));
                        toCheck.Add(new Cell(x, y+1));

                        // Check all adjacent cells.
                        foreach (Cell cell in toCheck)
                        {
                            if(cell.x >=0 && cell.x < level.Width && cell.y >= 0 && cell.y < level.Height)
                            {
                                MapSymbol neighborSymbol = level.GetMapSymbol(cell.x, cell.y);
                                if (neighborSymbol != MapSymbol.VerticalWall &&
                                    neighborSymbol != MapSymbol.HorizontalWall)
                                {
                                    neighbors.Add(new Cell(cell.x, cell.y));
                                }
                            }
                        }

                        // Create node with its neighbors.
                        GraphNode<Cell> newNode = new GraphNode<Cell>(new Cell(x, y), neighbors);
                        nodes.Add(newNode);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a node given the x and y positions of its cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GraphNode<Cell> GetNode(int x, int y)
        {
            foreach(GraphNode<Cell> node in nodes)
            {
                if(node.Data.x == x && node.Data.y == y)
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a list of neighbors for the given node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<GraphNode<Cell>> GetNeighbors(GraphNode<Cell> node)
        {
            List<GraphNode<Cell>> neighbors = new List<GraphNode<Cell>>();
            foreach(Cell cell in node.Neighbors)
            {
                GraphNode<Cell> neighbor = this.GetNode(cell.x, cell.y);
                neighbors.Add(neighbor);
            }

            return neighbors;
        }

    }
}
