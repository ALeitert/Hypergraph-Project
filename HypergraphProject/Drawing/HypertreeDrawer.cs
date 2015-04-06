using System;
using System.Collections.Generic;
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

        int[] colouring;
        List<int>[] edges;

        HypertreeDrawer(Hypergraph hTree)
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
            int maxCol = 0;

            for (int i = 0; i < colouring.Length; i++)
            {
                maxCol = Math.Max(colouring[i], maxCol);
            }

            hypertree.TransformToDual();


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

                    foreach(int nId in neighs)
                    {
                        if (nId == pId) continue;

                        verStack.Push(nId);
                        parStack.Push(vId);
                    }
                }
            }

            // Now, data contains the radial coordinates of each vertex,
            // edges contains the list of tree-edges of each hyperedge,
            // and colouring contains the colours of each hyperedge.
        }
    }
}
