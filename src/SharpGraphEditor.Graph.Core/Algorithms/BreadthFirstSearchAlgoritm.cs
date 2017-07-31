﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpGraphEditor.Graph.Core.Elements;

namespace SharpGraphEditor.Graph.Core.Algorithms
{
    public class BreadthFirstSearchAlgoritm : IAlgorithm
    {
        public string Name => "Breadth-first search";
        public string Description => "Breadth-first search";

        public AlgorithmResult Run(IGraph graph, IAlgorithmHost host)
        {
            if (graph.Vertices.Count() == 0)
            {
                host.Output.WriteLine("Graph is Empty.");
                return new AlgorithmResult(false, false);
            }

            host.ShowComment("Please, select vertex.");
            var selectedVertex = host.GetSelectedVertex();

            graph.ChangeColor(selectedVertex, VertexColor.Gray);
            var bfs = new Helpers.BreadthFirstSearch(graph)
            {
                ProcessEdge = (v1, v2) =>
                {
                    graph.ChangeColor(v2, VertexColor.Gray);
                },
                ProcessVertexLate = (v) =>
                {
                    graph.ChangeColor(v, VertexColor.Black);
                }
            };
            
            bfs.Run(selectedVertex);
            return new AlgorithmResult();
        }
    }
}
