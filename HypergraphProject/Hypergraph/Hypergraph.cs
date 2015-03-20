using HypergraphProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class Hypergraph
    {
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

    }
}
