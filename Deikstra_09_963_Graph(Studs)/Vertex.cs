using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deikstra_09_963
{
    class Vertex
    {

        private double minPathLength;
        private Vertex prev;
        private int num;
        private static int count = 0;
        private bool visited;

        public void SetVisited()
        {
            visited = true;
        }

        public bool IsVisited
        {
            get => visited;
        }

        public double Label
        {
            get => minPathLength;
        }

        public int Num
        {
            get { return num; }
            private set
            {
                num = value;
            }
        }

        public Vertex()
        {
            count++;
            Num = count;
            Reset();
        }

        public static void ResetCounter()
        {
            count = 0;
        }

        public void Reset()
        {
            minPathLength = double.PositiveInfinity;
            prev = null;
            visited = false;
        }

        public void SetStart()
        {
            minPathLength = 0;
        }

        public Vertex Previous
        {
            get => prev;
        }

        public void SetPrevious(Vertex prev, double edge)
        {
            if (visited) return;
            var length = prev.minPathLength + edge;
            if (minPathLength > length)
            {
                minPathLength = length;
                this.prev = prev;
            }
        }

    }
}
