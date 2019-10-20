using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    public partial class Graph<VertexT, EdgeT> : IEnumerable<Vertex<VertexT, EdgeT>>
    {
        internal List<Vertex<VertexT, EdgeT>> Vertexes;
        internal List<Edge<EdgeT, VertexT>> Edges;

        public EdgeValue<EdgeT> EdgeValue;

        public Graph()
        {
            Vertexes = new List<Vertex<VertexT, EdgeT>>();
            Edges = new List<Edge<EdgeT, VertexT>>();
        }

        public IEnumerable<Vertex<VertexT, EdgeT>> GetVertexes()
        {
            return Vertexes;
        }
        public IEnumerable<Edge<EdgeT, VertexT>> GetEdges()
        {
            return Edges;
        }

        public Vertex<VertexT, EdgeT> this[int index]
        {
            get => Vertexes[index];
            set => Vertexes[index] = value;
        }

        public Vertex<VertexT, EdgeT> FindVertex(Predicate<Vertex<VertexT, EdgeT>> match)
        {
            return Vertexes.Find(match);
        }
        public IEnumerable<Vertex<VertexT, EdgeT>> FindAllVertexes(Predicate<Vertex<VertexT, EdgeT>> match)
        {
            return Vertexes.FindAll(match);
        }

        public Edge<EdgeT, VertexT> FindEdge(Predicate<Edge<EdgeT, VertexT>> match)
        {
            return Edges.Find(match);
        }
        public IEnumerable<Edge<EdgeT, VertexT>> FindAllVertexes(Predicate<Edge<EdgeT, VertexT>> match)
        {
            return Edges.FindAll(match);
        }

        public IEnumerator<Vertex<VertexT, EdgeT>> GetEnumerator()
        {
            return Vertexes.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Vertex<VertexT, EdgeT> AddVertex(VertexT data = default)
        {
            var v = new Vertex<VertexT, EdgeT>(this, data);
            Vertexes.Add(v);
            return v;
        }
        public Vertex<VertexT, EdgeT> AddVertex(Vertex<VertexT, EdgeT> vertex)
        {
            vertex.setGraph(this);
            Vertexes.Add(vertex);
            return vertex;
        }

        public Edge<EdgeT, VertexT> AddEdge(EdgeT data = default, bool isDirected = false, Vertex<VertexT, EdgeT> first = null, Vertex<VertexT, EdgeT> second = null)
        {
            var e = new Edge<EdgeT, VertexT>(data, isDirected, first, second);
            Edges.Add(e);
            return e;
        }

        public int RemoveAll(Predicate<Vertex<VertexT, EdgeT>> match)
        {
            Edges.RemoveAll(e =>
            {
                if(match(e.First) || match(e.Second))
                {
                    e.Clear();
                    return true;
                }
                return false;
            });
            return Vertexes.RemoveAll(match);
        }
        public bool RemoveVertex(Vertex<VertexT, EdgeT> vertex)
        {
            Edges.RemoveAll(e =>
            {
                if (e.First == vertex || e.Second == vertex)
                {
                    e.Clear();
                    return true;
                }
                return false;
            });
            return Vertexes.Remove(vertex);
        }
        public bool RemoveEdge(Edge<EdgeT, VertexT> edge)
        {
            return Edges.Remove(edge);
        }
    }
}
