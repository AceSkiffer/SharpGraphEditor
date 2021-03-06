﻿using System.Collections.Generic;

using SharpGraphEditor.Graph.Core.Elements;

namespace SharpGraphEditor.Graph.Core
{
    public interface IGraph
    {
        IEnumerable<IVertex> Vertices { get; }

        IEnumerable<IEdge> Edges { get; }

        bool IsDirected { get; set; }

        T Add<T>(T element, bool addToUndoRedo) where T : IGraphElement;

        IVertex AddVertex(int index);

        IVertex AddVertex(double x, double y);

        IVertex AddVertex(double x, double y, int index);

        IVertex AddVertex(double x, double y, int index, string name, string title);

        IVertex AddVertex(double x, double y, int index, string name, string title, VertexColor color);

        IEdge AddEdge(IVertex source, IVertex target, bool isDirected = false, bool makeNotDirectedIfreversedExisted = true);

        void Remove(IGraphElement element);

        void ChangeColor(IVertex vertex, VertexColor newColor);

        void Clear();

        Dictionary<IVertex, IEnumerable<IVertex>> ToAdjList();

        IVertex FindVertexByIndex(int index);

        IVertex FindVertexByName(string name);
    }
}
