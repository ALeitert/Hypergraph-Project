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
        /// Stores all the information gathered and required during the acyclicity test of the hypergraph.
        /// </summary>
        private class AcyclicityInfo
        {
            public int[] gamma;
            public int[] R;
            public int[] betaV;
            public bool? IsAcyclic;

            public AcyclicityInfo(int noOfVer, int noOfEdg)
            {
                gamma = new int[noOfEdg];
                R = new int[noOfEdg];
                betaV = new int[noOfVer];
            }
        }

        private AcyclicityInfo ai = null;
        private AcyclicityInfo aiDual = null;

        int[][] vertexList;
        int[][] edgeList;

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
            List<int>[] sets = new List<int>[noOfVer];
            for (int vId = 0; vId < noOfVer; vId++)
            {
                sets[vId] = new List<int>();
                alpha[vId] = -1;
            }

            int[] R = ai.R;
            int[] size = new int[noOfEdg];
            int[] betaE = new int[noOfEdg];
            int[] gamma = ai.gamma;

            for (int S = 0; S < noOfEdg; S++)
            {
                gamma[S] = -1;
                sets[0].Add(S);
            }

            while (j >= 0)
            {
                List<int> set = sets[j];
                int S = set[set.Count - 1];
                set.RemoveAt(set.Count - 1);

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

                        // ToDo: in O(1)
                        sets[size[eId]].Remove(eId);

                        size[eId]++;

                        if (size[eId] < edgeList[eId].Length)
                        {
                            sets[size[eId]].Add(eId);
                        }
                        else if (size[eId] == edgeList[eId].Length)
                        {
                            size[eId] = -1;
                        }
                    } // foreach eId
                } // foreach vId

                // In paper: j++
                j = edgeList[S].Length;

                while (j >= 0 && sets[j].Count == 0)
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

            for (int i = 0; i < noOfEdg; i++)
            {
                int eId = ai.R[i];
                foreach (int vId in edgeList[eId])
                {
                    index[vId] = i + 1;
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
    }
}
