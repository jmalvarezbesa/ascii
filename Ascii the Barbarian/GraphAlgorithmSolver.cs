﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{

    class GraphAlgorithmSolver
    {
        private Graph graph;
        public GraphAlgorithmSolver(Graph graph)
        {
            this.graph = graph;
        }

        public List<Tuple<int, int>> AAlgorithmSolver(GameObject ascii, GameObject zombie)
        {
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            GraphNode <Cell> start_node = graph.GetNode(ascii.position[0], ascii.position[1]);
            start_node.GValue = 0;
            // the rest of the Nodes start with int36.MaxValue
            List<GraphNode<Cell>> TO_VISIT = new List<GraphNode<Cell>>();
            List<GraphNode<Cell>> VISITED = new List<GraphNode<Cell>>();

            GraphNode<Cell> end_node = graph.GetNode(zombie.position[0], zombie.position[1]);

            // calculate the h(n) of each node
            foreach (GraphNode<Cell> graph_node in graph.Nodes)
            {
                int actual_node_x = graph_node.Data.x;
                int actual_node_y = graph_node.Data.y;

                int end_node_x = end_node.Data.x;
                int end_node_y = end_node.Data.y;
                double euclidean_distance = Math.Sqrt(Math.Pow((actual_node_x - end_node_x), 2) + Math.Pow((actual_node_y - end_node_y), 2));
                graph_node.HValue = euclidean_distance;
            }
            List<GraphNode<Cell>> start_neighbors = graph.GetNeighbors(start_node);
            foreach (GraphNode<Cell> start_neighbor in start_neighbors)
            {
                TO_VISIT.Add(start_neighbor);
            }
            while (TO_VISIT.Count != 0)
            {
                double min_F = Int32.MaxValue; // default value
                int min_node_index = Int32.MaxValue; // default value
                GraphNode<Cell> s;

                for (int index = 0; index < TO_VISIT.Count; index++)
                {
                    if (TO_VISIT[index].F() < min_F)
                    {
                        s = TO_VISIT[index];
                        min_node_index = index;
                    }
                }
                s = TO_VISIT[min_node_index];
                TO_VISIT.RemoveAt(min_node_index);

                if (s.Data.x == end_node.Data.x && s.Data.y == end_node.Data.y)
                {
                    foreach (GraphNode<Cell> visited_node in VISITED)
                    {
                        path.Add(new Tuple<int, int>(visited_node.Data.x, visited_node.Data.y));
                    }
                    return path;
                }
                List<GraphNode<Cell>> s_neighbors = graph.GetNeighbors(start_node);

                foreach (GraphNode<Cell> s_prim in s_neighbors)
                {
                    foreach (GraphNode<Cell> visited_node in VISITED)
                    {

                        if (s_prim.Data.x == visited_node.Data.x && s_prim.Data.y == visited_node.Data.y)
                        {
                            if (s_prim.GValue > s.GValue + 1)
                            {
                                s_prim.GValue = s.GValue + 1;
                                s_prim.CameFrom = s;
                                TO_VISIT.Add(s_prim);
                            }

                        }
                        VISITED.Add(s);
                    }
                }
            }
            return path;
        }

    }
}
