using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public partial class Graph<VertexT, EdgeT>
    {
        //public static List<Edge<int, VertexT>> ShortWay<VertexT>( this Graph<VertexT, int> graph, 
        //    Vertex<VertexT, int> start,
        //    Vertex<VertexT, int> finish,
        //    Algorithms algorithm = Algorithms.Dijkstra)
        //{
        //    switch (algorithm)
        //    {
        //        case Algorithms.Dijkstra:
        //            return graph.ShortWayDijkstra(start, finish);
        //        case Algorithms.Floid:
        //            return null;
        //        default:
        //            throw new Exception("ERROR: Unknown algorithm");
        //    }
        //}
        private static int Min(int a, int b) => a > b ? b : a;
        private class DijkstraObject
        {
            public Vertex<VertexT, EdgeT> Vertex;
            public int? Mark;
            public bool Constant;
            public Vertex<VertexT, EdgeT> PrevVertex;

            public DijkstraObject(Vertex<VertexT, EdgeT> item1, int? item2, bool item3, Vertex<VertexT, EdgeT> item4)
            {
                Vertex = item1;
                Mark = item2;
                Constant = item3;
                PrevVertex = item4;
            }
            public override string ToString() 
                => $"{Vertex}, {Mark}, {Constant}, {PrevVertex}";
        }
        public List<Vertex<VertexT, EdgeT>> ShortWayDijkstra(Vertex<VertexT, EdgeT> start, Vertex<VertexT, EdgeT> finish)
        {
            if (EdgeValue == null) throw new EdgeValueException();

            var list = new List<DijkstraObject>(Vertexes.Count);
            foreach (var item in this)
                list.Add(new DijkstraObject(item, null, false, null));

            var x = list.Find(v => ReferenceEquals(v.Vertex, start));
            x.Mark = 0;
            x.Constant = true;

            while (!ReferenceEquals(x.Vertex, finish))
            {
                foreach (var item in x.Vertex.OutputEdges)
                {
                    var tmp = list.FindAll(v => item.Connected(v.Vertex) && !ReferenceEquals(v.Vertex, item));
                    
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        if (tmp[i].Constant)
                            continue;
                        if(tmp[i].Mark == null)
                            tmp[i].Mark = x.Mark + EdgeValue(item.Data);
                        else
                            tmp[i].Mark = Min(tmp[i].Mark ?? 0, x.Mark ?? 0 + EdgeValue(item.Data));
                    }
                    
                }
                
                int first_index = -1;
                for (int i = 0; i < list.Count; i++)
                    if (!list[i].Constant && list[i].Mark != null)
                    {
                        first_index = i;
                        break;
                    }
                if (first_index <= -1)
                    break;
                var tmp_mark = list[first_index];
                for (int i = first_index; i < list.Count; i++)
                    if (!list[i].Constant && tmp_mark.Mark > list[i].Mark)
                        tmp_mark = list[i];
                tmp_mark.Constant = true;
                tmp_mark.PrevVertex = x.Vertex;
                x = tmp_mark;
            }

            var vertex_list = new List<Vertex<VertexT, EdgeT>>();

            if (!ReferenceEquals(x.Vertex, finish))
                return vertex_list;

            while (x.PrevVertex != null)
            {
                vertex_list.Add(x.Vertex);
                x = list.Find(v => ReferenceEquals(v.Vertex, x.PrevVertex));
            }
            vertex_list.Add(x.Vertex);

            vertex_list.Reverse();
            return vertex_list;
        }
    }
}
