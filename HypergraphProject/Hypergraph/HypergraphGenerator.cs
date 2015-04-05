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
        public static BitMatrix GenerateHypertreeMatrix(int vertices, int edges, int maxCard)
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


            int[] verBuffer = new int[vertices];
            int[] parBuffer = new int[vertices];

            parBuffer[0] = -1;

            for (int i = 0; i < edges; i++)
            {
                int card = rng.Next(maxCard) + 1;
                int bufferSize = 1;
                int selectedIndex = 1;

                // Select fist vertex of edge
                int startId = rng.Next(vertices);
                verBuffer[0] = startId;

                // Copy neighbours of first vertex into buffer.
                for (int neighIndex = 0; neighIndex < tree[startId].Count; neighIndex++)
                {
                    verBuffer[bufferSize] = tree[startId][neighIndex];
                    parBuffer[bufferSize] = startId;
                    bufferSize++;
                }

                for (; selectedIndex < card; selectedIndex++)
                {
                    // Pick a random vertex from the available neighbours.
                    int rndIndex = rng.Next(bufferSize - selectedIndex) + selectedIndex;
                    int rndId = verBuffer[rndIndex];
                    int rndParId = parBuffer[rndIndex];

                    // Put in front.
                    int h = verBuffer[selectedIndex];
                    verBuffer[selectedIndex] = verBuffer[rndIndex];
                    verBuffer[rndIndex] = h;

                    h = parBuffer[selectedIndex];
                    parBuffer[selectedIndex] = parBuffer[rndIndex];
                    parBuffer[rndIndex] = h;

                    // Add neighbours to buffer
                    for (int neighIndex = 0; neighIndex < tree[rndId].Count; neighIndex++)
                    {
                        int neighId = tree[rndId][neighIndex];

                        if (neighId == rndParId) continue;

                        verBuffer[bufferSize] = neighId;
                        parBuffer[bufferSize] = rndId;
                        bufferSize++;
                    }

                }

                // Write edge into matrix.
                for (int j = 0; j < selectedIndex; j++)
                {
                    matrix[verBuffer[j], i] = true;
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
                    matrix = GenerateHypertreeMatrix(edges, vertices, maxCard);
                    matrix.Transpose();
                    return matrix;

                case HypergraphType.Hypertree:
                    return GenerateHypertreeMatrix(vertices, edges, maxCard);

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
