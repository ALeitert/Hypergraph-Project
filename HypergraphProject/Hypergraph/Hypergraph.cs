using HypergraphProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class Hypergraph
    {
        /// <summary>
        /// Manages the edge sets for MaxCardinalitySearch.
        /// </summary>
        /// <remarks>
        /// Invarian: The field in front of a set j (i.e. edges[start[j] - 1]) is the last element of set j-1 or empty (i.e. -1).
        /// </remarks>
        private class EdgeSets
        {
            int[] edges;
            int[] edgePosition;

            int[] start;
            int[] length;

            internal EdgeSets(int noOfEdg, int noOfVer)
            {
                edges = new int[noOfEdg];
                edgePosition = new int[noOfEdg];

                start = new int[noOfVer + 1];
                length = new int[noOfVer + 1];

                for (int eId = 0; eId < noOfEdg; eId++)
                {
                    edges[eId] = eId;
                    edgePosition[eId] = eId;
                }

                start[0] = 0;
                length[0] = noOfEdg;
                for (int sInd = 1; sInd < length.Length; sInd++)
                {
                    start[sInd] = noOfEdg;
                    length[sInd] = 0;
                }
            }

            internal int Size(int j)
            {
                return length[j];
            }

            /// <summary>
            /// Removes an element from the set with the given index.
            /// </summary>
            /// <returns>
            /// The removed element (id of an edge).
            /// </returns>
            internal int Remove(int j)
            {

                if (length[j] <= 0)
                {
                    throw new InvalidOperationException();
                }

                int eInd = start[j] + length[j] - 1;
                int eId = edges[eInd];

                length[j]--;
                edges[eInd] = -1;
                edgePosition[eId] = -1;

                return eId;
            }

            /// <summary>
            /// Removes the given edge from the set with the given index.
            /// </summary>
            internal void Remove(int eId, int j)
            {
                int eInd = edgePosition[eId];
                int endInd = start[j] + length[j] - 1;

                if (eInd < start[j] || eInd > endInd)
                {
                    throw new InvalidOperationException();
                }

                int endId = edges[endInd];

                // Swap with last element.
                edges[eInd] = endId;
                edges[endInd] = eId;
                edgePosition[eId] = endInd;
                edgePosition[endId] = eInd;
                eInd = endInd;

                // Remove.
                edges[eInd] = -1;
                edgePosition[eId] = -1;
                length[j]--;
            }

            /// <summary>
            /// Adds the given edge to the set with the given index.
            /// </summary>
            internal void Add(int eId, int j)
            {
                int eInd = start[j] - 1;

                if (edges[eInd] >= 0)
                {
                    throw new InvalidOperationException();
                }

                edges[eInd] = eId;
                edgePosition[eId] = eInd;

                start[j]--;
                length[j]++;
            }
        }

        /// <summary>
        /// Stores all the information gathered and required during the acyclicity test of the hypergraph.
        /// </summary>
        private class AcyclicityInfo
        {
            public int[] gamma;
            public int[] R;
            public int[] betaV;
            public bool? IsAcyclic;

            private AcyclicityInfo() { }

            public AcyclicityInfo(int noOfVer, int noOfEdg)
            {
                gamma = new int[noOfEdg];
                R = new int[noOfEdg];
                betaV = new int[noOfVer];

                for (int i = 0; i < R.Length; i++)
                {
                    R[i] = -1;
                }
            }

            public AcyclicityInfo Clone()
            {
                AcyclicityInfo clone = new AcyclicityInfo();

                clone.gamma = (int[])this.gamma.Clone();
                clone.R = (int[])this.R.Clone();
                clone.betaV = (int[])this.betaV.Clone();
                clone.IsAcyclic = this.IsAcyclic;

                return clone;
            }
        }

        private AcyclicityInfo ai = null;
        private AcyclicityInfo aiDual = null;

        int[][] vertexList;
        int[][] edgeList;

        private Hypergraph() { }

        /// <summary>
        /// Initilises an hypergraph from a given matrix.
        /// </summary>
        public Hypergraph(bool[,] matrix)
        {

            int vertices = matrix.GetLength(0);
            int edges = matrix.GetLength(1);

            vertexList = new int[vertices][];
            edgeList = new int[edges][];

            int[] edgeBuffer = new int[edges];
            int bufferSize = 0;

            // Counts the number of vertices in each edge.
            int[] vertexCounter = new int[edges];

            // Read edges of vertices
            for (int vId = 0; vId < vertices; vId++)
            {
                bufferSize = 0;

                for (int eId = 0; eId < edges; eId++)
                {
                    if (matrix[vId, eId])
                    {
                        edgeBuffer[bufferSize] = eId;
                        bufferSize++;
                        vertexCounter[eId]++;
                    }
                }

                vertexList[vId] = new int[bufferSize];
                Array.Copy(edgeBuffer, vertexList[vId], bufferSize);

            }

            // Creates arrays for edges.
            for (int eId = 0; eId < edges; eId++)
            {
                edgeList[eId] = new int[vertexCounter[eId]];
                vertexCounter[eId] = 0;
            }

            // Fills arrays of edges.
            for (int vId = 0; vId < vertices; vId++)
            {
                for (int eIndex = 0; eIndex < vertexList[vId].Length; eIndex++)
                {
                    int eId = vertexList[vId][eIndex];

                    int vIndex = vertexCounter[eId];
                    edgeList[eId][vIndex] = vId;
                    vertexCounter[eId]++;
                }
            }
        }

        /// <summary>
        /// Initilises an hypergraph from a given matrix.
        /// </summary>
        public Hypergraph(BitMatrix matrix)
        {

            int vertices = matrix.Width;
            int edges = matrix.Height;

            vertexList = new int[vertices][];
            edgeList = new int[edges][];

            int[] edgeBuffer = new int[edges];
            int bufferSize = 0;

            // Counts the number of vertices in each edge.
            int[] vertexCounter = new int[edges];

            // Read edges of vertices
            for (int vId = 0; vId < vertices; vId++)
            {
                bufferSize = 0;

                for (int eId = 0; eId < edges; eId++)
                {
                    if (matrix[vId, eId])
                    {
                        edgeBuffer[bufferSize] = eId;
                        bufferSize++;
                        vertexCounter[eId]++;
                    }
                }

                vertexList[vId] = new int[bufferSize];
                Array.Copy(edgeBuffer, vertexList[vId], bufferSize);

            }

            // Creates arrays for edges.
            for (int eId = 0; eId < edges; eId++)
            {
                edgeList[eId] = new int[vertexCounter[eId]];
                vertexCounter[eId] = 0;
            }

            // Fills arrays of edges.
            for (int vId = 0; vId < vertices; vId++)
            {
                for (int eIndex = 0; eIndex < vertexList[vId].Length; eIndex++)
                {
                    int eId = vertexList[vId][eIndex];

                    int vIndex = vertexCounter[eId];
                    edgeList[eId][vIndex] = vId;
                    vertexCounter[eId]++;
                }
            }
        }

        /// <summary>
        /// Determines if the hypergraph is alpha-acyclic.
        /// </summary>
        public bool IsAcyclic
        {
            get
            {
                if (ai == null || !ai.IsAcyclic.HasValue)
                {
                    AcyclicityTest();
                }

                return ai.IsAcyclic.Value;
            }
        }

        public Hypergraph Clone()
        {
            Hypergraph clone  = new Hypergraph();

            if (this.ai != null)
            {
                clone.ai = this.ai.Clone();
            }

            if (this.aiDual != null)
            {
                clone.aiDual = this.aiDual.Clone();
            }

            clone.vertexList = new int[this.vertexList.Length][];
            for (int i = 0; i < this.vertexList.Length; i++)
            {
                clone.vertexList[i] = (int[])this.vertexList[i].Clone();
            }

            clone.edgeList = new int[this.edgeList.Length][];
            for (int i = 0; i < this.edgeList.Length; i++)
            {
                clone.edgeList[i] = (int[])this.edgeList[i].Clone();
            }

            return clone;

        }

        /// <summary>
        /// Thransforms this hypergraph to its dual.
        /// </summary>
        public void TransformToDual()
        {
            AcyclicityInfo aiTmp = ai;
            ai = aiDual;
            aiDual = aiTmp;

            int[][] tmp = vertexList;
            vertexList = edgeList;
            edgeList = tmp;
        }

        /// <summary>
        /// Return the perfect elimination ordering of the underlying chordal graph if the hypergraph is acyclic.
        /// </summary>
        public int[] GetEliminationOrdering()
        {
            if (!IsAcyclic)
            {
                return null;
            }

            int noOfVer = vertexList.Length;
            int noOfEdg = edgeList.Length;

            int[] peo = new int[noOfVer];

            // Counting sort

            // The maximum k is the number of edges.
            int[] counter = new int[noOfEdg];

            for (int vId = 0; vId < noOfVer; vId++)
            {
                counter[ai.betaV[vId]]++;
            }

            for (int i = 1; i < counter.Length; i++)
            {
                counter[i] = counter[i - 1];
            }

            for (int vId = noOfVer - 1; vId >= 0; vId--)
            {
                int cInd = ai.betaV[vId];
                int pInd = counter[cInd] - 1;

                peo[pInd] = vId;
                counter[cInd]--;
            }

            return peo;
        }

        /// <summary>
        /// Creates the join tree (or forest) of this hypergraph.
        /// If the graph is not acylic, it returns null.
        /// </summary>
        public DynamicForest GetJoinTree()
        {
            if (!IsAcyclic)
            {
                return null;
            }

            DynamicForest forest = new DynamicForest(edgeList.Length);

            for (int eId = 0; eId < edgeList.Length; eId++)
            {
                forest.AddVertex();
            }

            for (int eId = 0; eId < edgeList.Length; eId++)
            {
                int gamma = ai.gamma[eId];

                if (gamma < 0) continue;

                int parId = ai.R[gamma];
                forest.SetParent(eId, parId);
            }

            return forest;
        }

        private void MaxCardinalitySearch()
        {
            int noOfVer = vertexList.Length;
            int noOfEdg = edgeList.Length;

            ai = new AcyclicityInfo(noOfVer, noOfEdg);

            int i = noOfVer + 1;
            int j = 0;

            // Paper has a 1 based index.
            // Therefore, initial value is set to -1 instead of 0.
            int k = -1;

            int[] alpha = new int[noOfVer];
            int[] betaV = ai.betaV;

            for (int vId = 0; vId < noOfVer; vId++)
            {
                alpha[vId] = -1;
            }

            EdgeSets sets = new EdgeSets(noOfEdg, noOfVer);

            int[] R = ai.R;
            int[] size = new int[noOfEdg];
            int[] betaE = new int[noOfEdg];
            int[] gamma = ai.gamma;

            for (int S = 0; S < noOfEdg; S++)
            {
                gamma[S] = -1;
            }

            while (j >= 0)
            {
                int S = sets.Remove(j);

                k++;
                betaE[S] = k;
                R[k] = S;
                size[S] = -1;

                foreach (int vId in edgeList[S])
                {
                    if (alpha[vId] >= 0) continue;

                    i--;
                    alpha[vId] = i;
                    betaV[vId] = k;

                    foreach (int eId in vertexList[vId])
                    {
                        if (size[eId] < 0) continue;

                        gamma[eId] = k;

                        sets.Remove(eId, size[eId]);
                        size[eId]++;

                        if (size[eId] < edgeList[eId].Length)
                        {
                            sets.Add(eId, size[eId]);
                        }
                        else if (size[eId] == edgeList[eId].Length)
                        {
                            size[eId] = -1;
                        }
                    } // foreach eId
                } // foreach vId

                // In paper: j++
                j = edgeList[S].Length;

                while (j >= 0 && sets.Size(j) == 0)
                {
                    j--;
                }
            }
        }

        private void AcyclicityTest()
        {

            if (ai == null)
            {
                MaxCardinalitySearch();
            }

            int noOfVer = vertexList.Length;
            int noOfEdg = edgeList.Length;

            // Stores the edges based on their gamm value, i.e. gammaEdges[i] are all edges S with gamma[S] == i.
            List<int>[] gammaEdges = new List<int>[noOfEdg];
            for (int i = 0; i < noOfEdg; i++)
            {
                gammaEdges[i] = new List<int>();
            }

            for (int eId = 0; eId < noOfEdg; eId++)
            {
                if (ai.gamma[eId] < 0) continue;
                gammaEdges[ai.gamma[eId]].Add(eId);
            }

            int[] index = new int[noOfVer];
            for (int i = 0; i < noOfVer; i++)
            {
                index[i] = -1;
            }

            for (int i = 0; i < noOfEdg; i++)
            {
                int eId = ai.R[i];

                if (eId < 0) continue;

                foreach (int vId in edgeList[eId])
                {
                    index[vId] = i;
                }

                foreach (int S in gammaEdges[i])
                {
                    foreach (int v in edgeList[S])
                    {
                        if (ai.betaV[v] < i && index[v] < i)
                        {
                            ai.IsAcyclic = false;
                            return;
                        }
                    }
                }
            }

            ai.IsAcyclic = true;
        }

        /// <summary>
        /// Computes an optimal colouring for the underlying chordal graph if the hypergraph is acyclic.
        /// The smalles colour is 1.
        /// </summary>
        public int[] GetVertexColouring()
        {
            if (!IsAcyclic)
            {
                return null;
            }

            int noOfVer = vertexList.Length;
            int noOfEdg = edgeList.Length;

            int[] colouring = new int[noOfVer];

            for (int i = 0; i < noOfEdg; i++)
            {
                int eId = ai.R[i];

                if (eId == -1) continue; // Not all edges are in the ordering.

                int eCard = edgeList[eId].Length;

                bool[] usedColours = new bool[eCard];

                for (int vInd = 0; vInd < eCard; vInd++)
                {
                    int vId = edgeList[eId][vInd];
                    int vCol = colouring[vId];

                    if (vCol >= eCard || vCol == 0) continue;

                    usedColours[vCol - 1] = true;
                }

                int colInd = 0;

                for (int vInd = 0; vInd < eCard; vInd++)
                {
                    int vId = edgeList[eId][vInd];
                    int vCol = colouring[vId];

                    if (vCol > 0) continue;

                    while (usedColours[colInd])
                    {
                        colInd++;
                    }

                    colouring[vId] = colInd + 1;
                    usedColours[colInd] = true;
                }
            }

            return colouring;
        }

    }
}
