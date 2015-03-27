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

        HashSet<int> rootIds;

        public DynamicForest()
        {
            vertexList = new List<List<int>>();
            parentIds = new List<int>();
            rootIds = new HashSet<int>();
        }

        public DynamicForest(int capacity)
        {
            vertexList = new List<List<int>>(capacity);
            parentIds = new List<int>(capacity);
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
                vertexList[vId].Remove(pId);
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

    }
}
