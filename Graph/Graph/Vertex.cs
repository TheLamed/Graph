using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Vertex<VertexT, EdgeT> : IEnumerable<Edge<EdgeT, VertexT>>
    {
        public VertexT Data { get; set; }
        private Graph<VertexT, EdgeT> graph;

        public Vertex(VertexT data = default)
        {
            Data = data;
        }
        internal Vertex(Graph<VertexT, EdgeT> graph, VertexT data = default)
        {
            setGraph(graph);
            Data = data;
        }

        internal void setGraph(Graph<VertexT, EdgeT> graph)
        {
            this.graph = graph;
        }

        public IEnumerable<Edge<EdgeT, VertexT>> Edges
        {
            get => graph.Edges.FindAll(e => e.First == this || e.Second == this);
        }
        public IEnumerable<Edge<EdgeT, VertexT>> OutputEdges
        {
            get => graph.Edges.FindAll(e => e.isDirected ? e.First == this : e.First == this || e.Second == this);
        }
        public IEnumerable<Edge<EdgeT, VertexT>> InputEdges
        {
            get => graph.Edges.FindAll(e => e.isDirected ? e.Second == this : e.First == this || e.Second == this);
        }

        public IEnumerator<Edge<EdgeT, VertexT>> GetEnumerator()
        {
            return graph.Edges.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override string ToString() => $"({Data})";
    }
}
