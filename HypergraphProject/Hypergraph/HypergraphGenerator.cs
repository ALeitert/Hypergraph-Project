using HypergraphProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    class HypergraphGenerator
    {

        /// <summary>
        /// Generates a matrix which represents a hypertree.
        /// </summary>
        /// <returns>
        /// The algorithm *does not* ensure that edges are unique, every vertex is in an edge, or that the graph is connected.
        /// </returns>
        public static BitMatrix GenerateHypertree(int vertices, int edges, int maxCard)
        {

            if (vertices < 1 || edges < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (maxCard > vertices || maxCard < 1)
            {
                maxCard = vertices;
            }

            BitMatrix matrix = new BitMatrix(vertices, edges);

            Random rng = new Random();

            // Generate tree
            List<int>[] tree = new List<int>[vertices];

            for (int i = 0; i < vertices; i++)
            {
                tree[i] = new List<int>();
            }

            for (int i = 1; i < vertices; i++)
            {
                int parentId = rng.Next(i);

                tree[i].Add(parentId);
                tree[parentId].Add(i);
            }

            // Sets the maximum degree for each vertex.
            int[] maxDegree = new int[vertices];
            for (int i = 0; i < vertices; i++)
            {
                maxDegree[i] = rng.Next(maxCard / 2 + maxCard % 2) + maxCard / 2;
            }

            int[] edgeBuffer = new int[edges];
            List<int>[] neighbours = new List<int>[edges];
            List<int>[] parents = new List<int>[edges];

            for (int i = 0; i < edges; i++)
            {
                neighbours[i] = new List<int>();
                parents[i] = new List<int>();
                edgeBuffer[i] = i;
            }

            // Determine first vertex in edge.
            int[] verBuffer = new int[vertices];
            int verBuSize = vertices;
            for (int i = 0; i < vertices; i++)
            {
                verBuffer[i] = i;
            }

            for (int i = 0; i < edges; i++)
            {
                int rndInd = rng.Next(verBuSize);
                int vId = verBuffer[rndInd];

                maxDegree[vId]--;
                matrix[vId, i] = true;

                if (maxDegree[vId] <= 0)
                {
                    verBuSize--;
                    verBuffer[rndInd] = verBuffer[verBuSize];
                }

                // Copy neighbours of first vertex into list.
                for (int neighIndex = 0; neighIndex < tree[vId].Count; neighIndex++)
                {
                    int nId = tree[vId][neighIndex];
                    if (maxDegree[nId] <= 0) continue;
                    neighbours[i].Add(nId);
                    parents[i].Add(vId);
                }
            }


            int noOfEdges = edges;

            while (noOfEdges > 0)
            {
                int edgeInd = rng.Next(noOfEdges);
                int edgeId = edgeBuffer[edgeInd];

                List<int> neighs = neighbours[edgeId];
                List<int> pars = parents[edgeId];

                bool foundNext = false;

                while (!foundNext && neighs.Count > 0)
                {
                    int neigInd = rng.Next(neighs.Count);

                    int verId = neighs[neigInd];
                    int parId = pars[neigInd];

                    // Remove form lists.
                    neighs[neigInd] = neighs[neighs.Count - 1];
                    pars[neigInd] = pars[pars.Count - 1];
                    neighs.RemoveAt(neighs.Count - 1);
                    pars.RemoveAt(pars.Count - 1);

                    if (maxDegree[verId] <= 0) continue;

                    // Found a valid vertex
                    foundNext = true;
                    maxDegree[verId]--;

                    matrix[verId, edgeId] = true;

                    // Copy neighbours into list.
                    for (int neighIndex = 0; neighIndex < tree[verId].Count; neighIndex++)
                    {
                        int nId = tree[verId][neighIndex];
                        if (maxDegree[nId] <= 0 || nId == parId) continue;
                        neighs.Add(nId);
                        pars.Add(verId);
                    }

                }

                if (!foundNext)
                {
                    // List of possible new vertices is empty.
                    // The edge cannot grow any more.
                    noOfEdges--;
                    neighbours[edgeInd] = neighbours[noOfEdges];
                    parents[edgeInd] = parents[noOfEdges];
                    edgeBuffer[edgeInd] = edgeBuffer[noOfEdges];
                }
            }

            return matrix;

        }

        /// <summary>
        /// Generates a matrix which represents a random hypergraph.
        /// </summary>
        /// <returns>
        /// The algorithm *does not* ensure that edges are unique, every vertex is in an edge, or that the graph is connected.
        /// </returns>
        public static BitMatrix GenerateHypergraph(HypergraphType type, int vertices, int edges, int maxCard)
        {
            BitMatrix matrix;

            switch (type)
            {
                case HypergraphType.Arbitrary:
                    break;

                case HypergraphType.Acyclic:
                    matrix = GenerateHypertree(edges, vertices, maxCard);
                    matrix.Transpose();
                    return matrix;

                case HypergraphType.Hypertree:
                    return GenerateHypertree(vertices, edges, maxCard);

                default:
                    throw new ArgumentException();
            }

            if (vertices < 1 || edges < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (maxCard > vertices || maxCard < 1)
            {
                maxCard = vertices;
            }

            matrix = new BitMatrix(vertices, edges);
            Random rng = new Random();

            int[] vIds = new int[vertices];
            for (int vId = 0; vId < vertices; vId++)
            {
                vIds[vId] = vId;
            }

            for (int eId = 0; eId < edges; eId++)
            {
                Shuffle(rng, vIds);
                int card = rng.Next(maxCard - 1) + 1;

                for (int i = 0; i < card; i++)
                {
                    matrix[vIds[i], eId] = true;
                }
            }

            return matrix;

        }

        private static void Shuffle(Random rng, int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int ind = rng.Next(array.Length - i) + i;

                int h = array[i];
                array[i] = array[ind];
                array[ind] = h;
            }
        }

    }
}
