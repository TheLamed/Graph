using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public partial class Graph<VertexT, EdgeT>
    {
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
            if (start == null || finish == null) return new List<Vertex<VertexT, EdgeT>>();

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

        private static int? FloidMin(int? a, int? b)
        {
            if (a == null && b == null) return null;
            if (a == null) return b;
            if (b == null) return a;
            return a > b ? b : a;
        }
        public List<Vertex<VertexT, EdgeT>> ShortWayFloid(Vertex<VertexT, EdgeT> start, Vertex<VertexT, EdgeT> finish)
        {
            if (EdgeValue == null) throw new EdgeValueException();
            if (start == null || finish == null) return new List<Vertex<VertexT, EdgeT>>();

            var list = new List<int?[,]>();
            list.Add(new int?[Vertexes.Count, Vertexes.Count]);
            foreach (var item in Vertexes)
                foreach (var edge in item.OutputEdges)
                {
                    if(edge.First == item)
                        list[0][Vertexes.IndexOf(edge.First), Vertexes.IndexOf(edge.Second)]
                        = EdgeValue(edge.Data);
                    else
                        list[0][Vertexes.IndexOf(edge.Second), Vertexes.IndexOf(edge.First)]
                        = EdgeValue(edge.Data);
                }

            for (int k = 1; k <= Vertexes.Count; k++)
            {
                var matrix = new int?[Vertexes.Count, Vertexes.Count];
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        matrix[i, j] = i == j ?
                            0 : FloidMin(list[k - 1][i, j], list[k - 1][i, k - 1] + list[k - 1][k - 1, j]);
                list.Add(matrix);
            }

            var O = new List<int?[,]>();
            O.Add(new int?[Vertexes.Count, Vertexes.Count]);
            for (int i = 0; i < O[0].GetLength(0); i++)
                for (int j = 0; j < O[0].GetLength(1); j++)
                    if (i == j) O[0][i, j] = 0;
                    else O[0][i, j] = i + 1;

            for (int k = 1; k <= Vertexes.Count; k++)
            {
                var matrix = new int?[Vertexes.Count, Vertexes.Count];
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        if (list[k][i, j] ==  list[k - 1][i, j]) matrix[i, j] = O[k - 1][i, j];
                        else matrix[i, j] = O[k - 1][k - 1, j];
                O.Add(matrix);
            }
            for (int i = 0; i < O[O.Count - 1].GetLength(0); i++)
                for (int j = 0; j < O[O.Count - 1].GetLength(1); j++)
                    O[O.Count - 1][i, j]--;

            int _start = Vertexes.IndexOf(start);
            int _finish = Vertexes.IndexOf(finish);
            var output = new List<Vertex<VertexT, EdgeT>>();
            output.Add(Vertexes[_finish]);
            while (_start != _finish)
            {
                _finish = O[list.Count - 1][_start, _finish] 
                    ?? throw new Exception("HAH GEIIIII");
                output.Add(Vertexes[_finish]);
            }

            output.Reverse();
            return output;
        }

    }
}
