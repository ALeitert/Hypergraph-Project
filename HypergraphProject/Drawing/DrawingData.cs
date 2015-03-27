using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class DrawingData
    {
        public int RootId { get; set; }
        public bool IsDummyRoot { get; set; }

        public double MinX { get; set; }
        public double MaxX { get; set; }

        public int[] Height { get; set; }
        public int[] Depth { get; set; }
        public double[] XShift { get; set; }

        public DrawingData(int size, int rootId, bool isDummyRoot)
        {
            RootId = rootId;
            IsDummyRoot = isDummyRoot;

            Height = new int[size];
            Depth = new int[size];
            XShift = new double[size];

            for (int i = 0; i < size; i++)
            {
                Height[i] = -1;
                Depth[i] = -1;
                XShift[i] = 0.0;
            }
        }
    }
}
