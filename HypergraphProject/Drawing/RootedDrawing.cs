using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    /// <summary>
    /// Represents a drawing of a tree/forest.
    /// </summary>
    class RootedDrawing
    {
        private enum BorderSide
        {
            Left,
            Right,
        }

        private class TreeBorders
        {

            List<double> leftBorders;
            List<double> rightBorders;

            internal TreeBorders(int capacity)
            {
                leftBorders = new List<double>(capacity);
                rightBorders = new List<double>(capacity);
            }

            internal double this[int height, BorderSide side]
            {
                get
                {
                    height = TransformIndex(height);
                    switch (side)
                    {
                        case BorderSide.Left:
                            return leftBorders[height];

                        case BorderSide.Right:
                            return rightBorders[height];

                        default:
                            throw new ArgumentException();
                    }
                }
                set
                {
                    height = TransformIndex(height);
                    switch (side)
                    {
                        case BorderSide.Left:
                            leftBorders[height] = value;
                            break;

                        case BorderSide.Right:
                            rightBorders[height] = value;
                            break;

                        default:
                            throw new ArgumentException();
                    }
                }
            }

            internal void Add(double left, double right)
            {
                leftBorders.Add(left);
                rightBorders.Add(right);
            }

            private int TransformIndex(int index)
            {
                return leftBorders.Count - index - 1;
            }
        }

        DynamicForest forest;
        int rootId;
        bool isDummyRoot;

        public RootedDrawing(DynamicForest forest)
        {

            if (forest == null)
            {
                throw new ArgumentNullException();
            }

            if (forest.Size == 0)
            {
                throw new ArgumentException();
            }

            this.forest = forest.Clone();

            PrepareTree();

        }

        private void PrepareTree()
        {
            int centerCount = 0;
            List<int[]> allCenters = new List<int[]>(forest.NumberOfTrees);

            int[] roots = forest.GetRoots();
            foreach (int rId in roots)
            {
                int[] centers = forest.GetCenter(rId);
                centerCount += centers.Length;
                allCenters.Add(centers);
            }

            foreach (int[] centers in allCenters)
            {
                forest.SetToRoot(centers[0]);
            }

            if (centerCount > 1)
            {
                // Forest is not connected or has two centers.
                // Add dummy root.

                isDummyRoot = true;
                rootId = forest.AddVertex();

                foreach (int[] centers in allCenters)
                {
                    foreach (int c in centers)
                    {
                        forest.SetParent(c, rootId);
                    }
                }
            }
            else
            {
                isDummyRoot = false;
                rootId = allCenters[0][0];
            }
        }

        public DrawingData Draw()
        {
            // Preprocessing
            int size = forest.Size;
            DrawingData data = new DrawingData(size, rootId, isDummyRoot);

            Stack<int> vStack = new Stack<int>(size);
            vStack.Push(rootId);
            data.Depth[rootId] = 0;

            while (vStack.Count > 0)
            {
                int vId = vStack.Peek();
                int pId = forest.GetParent(vId);

                int[] neighs = forest[vId];

                int maxHeight = -1;
                bool maxFound = true;

                foreach (int nId in neighs)
                {
                    if (nId == pId) continue;

                    data.Depth[nId] = data.Depth[vId] + 1;

                    int nH = data.Height[nId];

                    if (nH == -1)
                    {
                        vStack.Push(nId);
                        maxFound = false;
                    }
                    else
                    {
                        maxHeight = Math.Max(nH, maxHeight);
                    }
                }

                if (maxFound)
                {
                    data.Height[vId] = maxHeight + 1;
                    vStack.Pop();
                }
            } // while -- calculates height and depth for each vertex.

            TreeBorders borders = DrawSubtree(rootId, data, data.Height[rootId] + 1);

            vStack.Push(rootId);

            while (vStack.Count > 0)
            {
                int vId = vStack.Pop();
                int pId = forest.GetParent(vId);
                double xShift = data.XShift[vId];

                int[] neighs = forest[vId];

                foreach (int nId in neighs)
                {
                    if (nId == pId) continue;

                    vStack.Push(nId);
                    data.XShift[nId] += xShift;

                    data.MinX = Math.Min(data.MinX, data.XShift[nId]);
                    data.MaxX = Math.Max(data.MaxX, data.XShift[nId]);
                }
            }

            return data;
        }

        private TreeBorders DrawSubtree(int vId, DrawingData data, int borderCap)
        {
            TreeBorders borders;

            if (data.Height[vId] == 0)
            {
                borders = new TreeBorders(borderCap);
                borders.Add(0.0, 0.0);
                return borders;
            }

            int maxHeigtIndex = -1;
            int maxHeigtId = -1;
            int maxHeigt = -1;

            int pId = forest.GetParent(vId);
            int[] neighs = forest[vId];

            for (int i = 0; i < neighs.Length; i++)
            {
                int nId = neighs[i];
                if (nId == pId) continue;

                if (data.Height[nId] > maxHeigt)
                {
                    maxHeigtIndex = i;
                    maxHeigtId = nId;
                    maxHeigt = data.Height[nId];
                }
            }

            // Draw largest subtree with full capacity for borders.
            borders = DrawSubtree(maxHeigtId, data, borderCap);

            // Draw trees left of largest subtree.
            for (int i = maxHeigtIndex - 1; i >= 0; i--)
            {
                int leftId = neighs[i];
                int leftHeight = data.Height[leftId];
                int hDif = maxHeigt - leftHeight;

                if (leftId == pId) continue;

                TreeBorders leftBorders = DrawSubtree(leftId, data, leftHeight + 1);

                // The distance the left tree is moved.
                // Because it will move to the left, shiftDist will become negative.
                double shiftDist = 0.0;

                double totalLeftShift = 0.0;
                double totalMaxShift = 0.0;

                for (int h = 0; h <= leftHeight; h++)
                {
                    totalLeftShift += leftBorders[h, BorderSide.Right];
                    totalMaxShift += borders[h, BorderSide.Left];

                    double dist = totalMaxShift - totalLeftShift;
                    shiftDist = Math.Min(shiftDist, dist);
                }

                // Have 1 unit space between both trees.
                shiftDist -= 1.0;

                // Move tree.
                data.XShift[leftId] = shiftDist;
                borders[0, BorderSide.Left] = shiftDist;
                totalLeftShift = shiftDist;

                for (int h = 1; h <= leftHeight; h++)
                {
                    double localShift = leftBorders[h, BorderSide.Left];
                    totalLeftShift += localShift;
                    borders[h, BorderSide.Left] = localShift;
                }

                if (leftHeight < maxHeigt)
                {
                    totalMaxShift += borders[leftHeight + 1, BorderSide.Left];
                    borders[leftHeight + 1, BorderSide.Left] = totalMaxShift - totalLeftShift;
                }

            }


            // Draw trees right of largest subtree.
            for (int i = maxHeigtIndex + 1; i < neighs.Length; i++)
            {
                int rightId = neighs[i];
                int rightHeight = data.Height[rightId];
                int hDif = maxHeigt - rightHeight;

                if (rightId == pId) continue;

                TreeBorders rightBorders = DrawSubtree(rightId, data, rightHeight + 1);

                // The distance the right tree is moved.
                // Because it will move to the right, shiftDist will become positive.
                double shiftDist = 0.0;

                double totalRightShift = 0.0;
                double totalMaxShift = 0.0;

                for (int h = 0; h <= rightHeight; h++)
                {
                    totalRightShift += rightBorders[h, BorderSide.Left];
                    totalMaxShift += borders[h, BorderSide.Right];

                    double dist = totalMaxShift - totalRightShift;
                    shiftDist = Math.Max(shiftDist, dist);
                }

                // Have 1 unit space between both trees.
                shiftDist += 1.0;

                // Move tree.
                data.XShift[rightId] = shiftDist;
                borders[0, BorderSide.Right] = shiftDist;
                totalRightShift = shiftDist;

                for (int h = 1; h <= rightHeight; h++)
                {
                    double localShift = rightBorders[h, BorderSide.Right];
                    totalRightShift += localShift;
                    borders[h, BorderSide.Right] = localShift;
                }

                if (rightHeight < maxHeigt)
                {
                    totalMaxShift += borders[rightHeight + 1, BorderSide.Right];
                    borders[rightHeight + 1, BorderSide.Right] = totalMaxShift - totalRightShift;
                }

            }

            // Move trees such that center is 0.
            double xShift = -(borders[0, BorderSide.Right] + borders[0, BorderSide.Left]) / 2.0;

            borders[0, BorderSide.Right] += xShift;
            borders[0, BorderSide.Left] += xShift;

            for (int i = 0; i < neighs.Length; i++)
            {
                int nId = neighs[i];
                if (nId == pId) continue;

                data.XShift[nId] += xShift;
            }

            borders.Add(0.0, 0.0);
            return borders;
        }

    }
}
