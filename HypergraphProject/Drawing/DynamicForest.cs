using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class DynamicForest
    {
        List<List<int>> vertexList;
        List<int> parentIds;
        List<int> vertexData;

        HashSet<int> rootIds;

        public DynamicForest()
        {
            vertexList = new List<List<int>>();
            parentIds = new List<int>();
            vertexData = new List<int>();
            rootIds = new HashSet<int>();
        }

        public DynamicForest(int capacity)
        {
            vertexList = new List<List<int>>(capacity);
            parentIds = new List<int>(capacity);
            vertexData = new List<int>(capacity);
            rootIds = new HashSet<int>();
        }

        public int Size
        {
            get
            {
                return vertexList.Count;
            }
        }

        public int NumberOfTrees
        {
            get
            {
                return rootIds.Count;
            }
        }

        /// <summary>
        /// Returns the ids of the neighbours of this vertex.
        /// </summary>
        public int[] this[int vId]
        {
            get
            {
                return vertexList[vId].ToArray();
            }
        }

        /// <summary>
        /// Adds a vertex without any connection to the forest.
        /// </summary>
        /// <returns>
        /// The id of the new vertex.
        /// </returns>
        public int AddVertex()
        {
            int newId = Size;
            vertexList.Add(new List<int>());
            parentIds.Add(-1);
            vertexData.Add(0);

            rootIds.Add(newId);

            return newId;
        }

        /// <summary>
        /// Adds a new vertex to the forest and connects it with the given parent.
        /// Returns the id of the new vertex.
        /// </summary>
        /// <param name="parentId">
        /// Id of the parent vertex.
        /// </param>
        public int AddVertex(int parentId)
        {
            if (parentId < 0 || parentId >= Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            int newId = AddVertex();

            SetParent(newId, parentId);

            return newId;
        }

        /// <summary>
        /// Set the parent of a vertex.
        /// If the vertex has already a parent, it get disconnected from it.
        /// If vId is equal to pId, vId gets diconnected without getting a new parrent.
        /// </summary>
        public void SetParent(int vId, int pId)
        {
            if (vId < 0 || pId < 0 || vId >= Size || pId >= Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            int oldParId = parentIds[vId];

            if (oldParId == pId)
            {
                // Nothing to do.
                return;
            }

            if (oldParId != -1)
            {
                // Disconnect vertex from its parent.
                vertexList[oldParId].Remove(vId);
                vertexList[vId].Remove(oldParId);
            }

            if (vId == pId)
            {
                parentIds[vId] = -1;
                rootIds.Add(vId);
                return;
            }

            vertexList[pId].Add(vId);
            vertexList[vId].Add(pId);

            parentIds[vId] = pId;
            rootIds.Remove(vId);

        }

        /// <summary>
        /// Sets a vertex as root vertex in its tree.
        /// </summary>
        public void SetToRoot(int vId)
        {
            Queue<int> idQ = new Queue<int>(Size);

            idQ.Enqueue(vId);
            parentIds[vId] = -1;
            rootIds.Add(vId);

            while (idQ.Count > 0)
            {
                int id = idQ.Dequeue();
                List<int> neighs = vertexList[id];

                foreach (int nId in neighs)
                {
                    if (nId == parentIds[id]) continue;

                    int oldParId = parentIds[nId];
                    if (oldParId == -1)
                    {
                        rootIds.Remove(nId);
                    }

                    parentIds[nId] = id;
                    idQ.Enqueue(nId);
                }
            }
        }

        /// <summary>
        /// Returns the roots of all trees in this forrest.
        /// </summary>
        public int[] GetRoots()
        {
            int[] ids = new int[rootIds.Count];
            rootIds.CopyTo(ids);
            return ids;
        }

        /// <summary>
        /// Creates a copy of this forest.
        /// </summary>
        public DynamicForest Clone()
        {
            DynamicForest clone = new DynamicForest(Size);

            for (int i = 0; i < Size; i++)
            {
                clone.parentIds.Add(this.parentIds[i]);
                clone.vertexData.Add(this.vertexData[i]);
                clone.vertexList.Add(new List<int>(this.vertexList[i]));
            }

            foreach (int rId in this.rootIds)
            {
                clone.rootIds.Add(rId);
            }

            return clone;
        }

        public int GetParent(int vId)
        {
            return parentIds[vId];
        }

        private int CleanData(int vId)
        {
            return CleanData(vId, 0);
        }

        private int CleanData(int vId, int value)
        {
            if (vId < 0 || vId >= Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Find root
            while (parentIds[vId] != -1)
            {
                vId = parentIds[vId];
            }

            int counter = 0;

            Queue<int> vQ = new Queue<int>();
            vQ.Enqueue(vId);

            while (vQ.Count > 0)
            {
                int v = vQ.Dequeue();
                vertexData[v] = value;

                counter++;

                foreach (int nId in this[v])
                {
                    if (nId != parentIds[v])
                    {
                        vQ.Enqueue(nId);
                    }
                }
            }

            return counter;

        }

        /// <summary>
        /// Finds the center of the tree containing the given vertex.
        /// </summary>
        public int[] GetCenter(int vId)
        {
            if (vId < 0 || vId >= Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this[vId].Length == 0)
            {
                return new int[] { vId };
            }

            int treeSize = CleanData(vId, -1);
            int indexCounter = 0;

            // Search for leaves
            Queue<int> vQ = new Queue<int>(treeSize);
            List<int> leaves = new List<int>();

            vQ.Enqueue(vId);

            while (vQ.Count > 0)
            {
                int v = vQ.Dequeue();

                vertexData[v] = indexCounter;
                indexCounter++;

                int[] neighs = this[v];

                if (neighs.Length == 1)
                {
                    leaves.Add(v);
                }

                foreach (int nId in neighs)
                {
                    if (vertexData[nId] < 0)
                    {
                        vQ.Enqueue(nId);
                    }
                }
            }

            foreach (int v in leaves)
            {
                vQ.Enqueue(v);
            }

            int[] maxLayer = { leaves[0], leaves[1] };
            int[] weight = new int[treeSize];
            int[] layer = new int[treeSize];

            while (vQ.Count > 0)
            {
                int v = vQ.Dequeue();

                int[] neighs = this[v];
                foreach (int nId in neighs)
                {
                    weight[nId]++;
                    if (weight[nId] == this[nId].Length - 1)
                    {
                        vQ.Enqueue(nId);
                        layer[nId] = Math.Max(layer[nId], layer[v] + 1);
                    }
                }

                if (layer[v] > layer[maxLayer[0]])
                {
                    maxLayer[1] = maxLayer[0];
                    maxLayer[0] = v;
                }
                else if (layer[v] > layer[maxLayer[1]])
                {
                    maxLayer[1] = v;
                }

            }

            if (layer[maxLayer[0]] == layer[maxLayer[1]])
            {
                return maxLayer;
            }
            else // layer[maxLayer[0]] > layer[maxLayer[1]]
            {
                return new int[] { maxLayer[0] };
            }

        }

    }
}
