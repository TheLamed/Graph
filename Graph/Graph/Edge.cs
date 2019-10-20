using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Edge<EdgeT, VertexT> : IEnumerable<Vertex<VertexT, EdgeT>>
    {
        public EdgeT Data { get; set; }

        public bool isDirected { get; set; }

        public Vertex<VertexT, EdgeT> First { get; private set; }
        public Vertex<VertexT, EdgeT> Second { get; private set; }

        public Edge(EdgeT data = default, bool isDirected = false, Vertex<VertexT, EdgeT> first = null, Vertex<VertexT, EdgeT> second = null)
        {
            Data = data;
            this.isDirected = isDirected;
            First = first;
            Second = second;
        }

        public bool Connected(Vertex<VertexT, EdgeT> v)
        {
            if (ReferenceEquals(First, v) || ReferenceEquals(Second, v))
                return true;
            return false;
        }

        internal void Clear()
        {
            First = null;
            Second = null;
        }

        public IEnumerator<Vertex<VertexT, EdgeT>> GetEnumerator()
        {
            yield return First;
            yield return Second;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"{First}-[{Data}]-{(isDirected ? ">" : "")}{Second}";
    }
}
