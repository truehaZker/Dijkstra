using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deikstra_09_963
{
    class Graph
    {
        private double[,] gr;

        public double[,] Matrix
        {
            get => (double[,])gr; //ВЕРНУТЬ КОПИЮ!!!
        }

        private List<Vertex> vrts;

        public Graph(double[,] g)
        {
            gr = g;
            vrts = new List<Vertex>();
            Vertex.ResetCounter();
            for (int i = 0; i < gr.GetLength(0); i++)
            {
                vrts.Add(new Vertex());
            }
        }

        public int VertexCount {
            get
            {
                return vrts.Count;
            }
        }

        private void ResetVertices()
        {
            foreach (var v in vrts)
            {
                v.Reset();
            }
        }

        public List<Vertex> GetShortestPath(int b, int e)
        {
            ResetVertices();
            vrts[b - 1].SetStart();

            var currVrt = GetNextVertex();
            while (currVrt != null)
            {
                foreach (var v in vrts)
                    if (Math.Abs(gr[currVrt.Num - 1, v.Num - 1]) > Double.Epsilon)
                        v.SetPrevious(currVrt, gr[currVrt.Num - 1, v.Num - 1]);
                currVrt.SetVisited();
                currVrt = GetNextVertex();
            }

            var result = new List<Vertex>();
            result.Add(vrts[e-1]);
            while (result[result.Count - 1].Previous != null)
            {
                result.Add(result[result.Count - 1].Previous);
            }

            result.Reverse();
            return result;
        }

        public static double GetShortestPathLength(List<Vertex> path)
        {
            return path[path.Count - 1].Label;
        }

        private Vertex GetNextVertex()
        {
            double minLabel = Double.PositiveInfinity;
            Vertex resV = null;
            foreach (var v in vrts)
            {
                if (!v.IsVisited && v.Label < minLabel)
                {
                    minLabel = v.Label;
                    resV = v;
                }
            }

            return resV;
        }
    }
}
