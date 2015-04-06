using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    class HypertreeDrawer
    {

        private Hypergraph hypertree;

        DynamicForest joinForest;
        RootedDrawing drawer;
        DrawingData data;

        int maxCol = 0;
        int[] colouring;
        int[] edgeByColour;

        List<int>[] edges;

        internal HypertreeDrawer(Hypergraph hTree)
        {

            if (hTree == null)
            {
                throw new ArgumentNullException();
            }

            hypertree = hTree.Clone();
            hypertree.TransformToDual();

            if (!hypertree.IsAcyclic)
            {
                throw new ArgumentException();
            }

            hypertree.TransformToDual();

        }

        private void Preprocess()
        {

            hypertree.TransformToDual();

            // Generate and "draw" underlying tree.
            joinForest = hypertree.GetJoinTree();
            drawer = new RootedDrawing(joinForest);
            data = drawer.DrawRadial();

            // Computes colouring and max colour.
            colouring = hypertree.GetVertexColouring();
            hypertree.TransformToDual();

            maxCol = 0;
            for (int i = 0; i < colouring.Length; i++)
            {
                maxCol = Math.Max(colouring[i], maxCol);
            }

            // Counting sort to sort edges by their colour.
            edgeByColour = new int[colouring.Length];
            int[] colCounter = new int[maxCol + 1];

            for (int i = 0; i < colouring.Length; i++)
            {
                colCounter[colouring[i]]++;
            }

            for (int i = 1; i < colCounter.Length; i++)
            {
                colCounter[i] += colCounter[i - 1];
            }

            for (int i = colouring.Length - 1; i >= 0; i--)
            {
                int col = colouring[i];

                colCounter[col]--;
                int ind = colCounter[col];

                edgeByColour[ind] = i;
            }
            // End of counting sort.

            Array.Reverse(edgeByColour);

            edges = new List<int>[hypertree.NoOfEdges];
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = new List<int>(hypertree.GetCardinality(i) * 2 - 2);
            }

            // Use a DFS to determine the tree-edges of each hyperedge.
            // The search starts at the root.
            // Each time the search reaces a vertex v, it checks for all edges e containing v, if e is already activated.
            // If e is already activated, the tree-edge from v to its parent is part of the edge.

            bool[] activeEdges = new bool[hypertree.NoOfEdges];
            Stack<int> verStack = new Stack<int>();
            Stack<int> parStack = new Stack<int>();

            int[] rootIds = joinForest.GetRoots();
            foreach (int rId in rootIds)
            {
                verStack.Push(rId);
                parStack.Push(-1);

                while (verStack.Count > 0)
                {
                    int vId = verStack.Pop();
                    int pId = parStack.Pop();

                    int[] edgeIds = hypertree.GetEdges(vId);

                    foreach (int eId in edgeIds)
                    {
                        if (activeEdges[eId])
                        {
                            edges[eId].Add(pId);
                            edges[eId].Add(vId);
                        }

                        activeEdges[eId] = true;
                    }

                    int[] neighs = joinForest[vId];

                    foreach (int nId in neighs)
                    {
                        if (nId == pId) continue;

                        verStack.Push(nId);
                        parStack.Push(vId);
                    }
                }
            }

            // Now, data contains the radial coordinates of each vertex,
            // edges contains the list of tree-edges of each hyperedge,
            // colouring contains the colours of each hyperedge,
            // and edgeByColur has the edges ordered by their colour.
        }

        /// <summary>
        /// Draws the hypertree as bitmap.
        /// </summary>
        /// <param name="size">
        /// The default distance between two vertices.
        /// </param>
        public Bitmap DrawAsBitmap(float size)
        {
            Preprocess();

            float radius = (float)data.Height[data.RootId];

            float w = 2F * (radius + 0.5F) * size;
            float h = 2F * (radius + 0.5F) * size;

            Bitmap bmp = new Bitmap((int)w, (int)h);

            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            g.ScaleTransform(size, size);
            g.TranslateTransform(radius + 0.5F, radius + 0.5F);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            PointF[] points = new PointF[hypertree.NoOfVertices];

            for (int vId = 0; vId < points.Length; vId++)
            {
                double x = Math.Cos(data.XShift[vId] * 2 * Math.PI / 360) * (double)data.Depth[vId];
                double y = Math.Sin(data.XShift[vId] * 2 * Math.PI / 360) * (double)data.Depth[vId];

                points[vId] = new PointF((float)x, (float)y);
            }

            float colScale = 1 / ((float)maxCol * 3F);

            for (int i = 0; i < edgeByColour.Length; i++)
            {
                int eId = edgeByColour[i];
                int eCol = colouring[eId];

                List<int> treeEdges = edges[eId];
                int[] vertices = hypertree.GetVertices(eId);

                float borderWidth = 0.15F;

                float borderRad = eCol * colScale;
                float fillRad = (eCol - borderWidth) * colScale;

                Pen borderPen = new Pen(Color.Black, borderRad);
                Pen fillPen = new Pen(Color.White, borderRad - 2 * borderWidth * colScale);

                foreach (int vId in vertices)
                {
                    g.FillEllipse(Brushes.Black, points[vId].X - borderRad, points[vId].Y - borderRad, 2F * borderRad, 2F * borderRad);
                }

                for (int j = 0; j < treeEdges.Count; j += 2)
                {
                    g.DrawLine(borderPen, points[treeEdges[j]], points[treeEdges[j + 1]]);
                }

                foreach (int vId in vertices)
                {
                    g.FillEllipse(Brushes.White, points[vId].X - fillRad, points[vId].Y - fillRad, 2F * fillRad, 2F * fillRad);
                }

                for (int j = 0; j < treeEdges.Count; j += 2)
                {
                    g.DrawLine(fillPen, points[treeEdges[j]], points[treeEdges[j + 1]]);
                }
            }

            float verRadius = 0.5F * colScale;

            for (int vId = 0; vId < points.Length; vId++)
            {
                g.FillEllipse(Brushes.Blue, points[vId].X - verRadius, points[vId].Y - verRadius, 2F * verRadius, 2F * verRadius);
            }


            g.Dispose();

            return bmp;

        }
    }
}
