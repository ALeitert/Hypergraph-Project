using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class BitMatrix
    {

        BitArray bits;

        // Factors to compute the index for the bit array.
        int xFactor = 1;
        int yFactor = 1;

        private BitMatrix() { }

        public BitMatrix(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException();
            }

            Width = width;
            Height = height;

            yFactor = width;

            bits = new BitArray(width * height);

        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        /// <summary>
        /// Returns or sets the value of a field.
        /// </summary>
        public bool this[int x, int y]
        {
            get
            {
                if (x < 0 || Width <= x || y < 0 || Height <= y)
                {
                    throw new ArgumentOutOfRangeException();
                }

                int index = GetCoordinateIndex(x, y, Width);
                return bits[index];
            }
            set
            {
                if (x < 0 || Width <= x || y < 0 || Height <= y)
                {
                    throw new ArgumentOutOfRangeException();
                }

                int index = GetCoordinateIndex(x, y, Width);
                bits[index] = value;
            }
        }

        /// <summary>
        /// Calculates the index in an one dimensional array of the given coordinate.
        /// </summary>
        private int GetCoordinateIndex(int x, int y, int width)
        {
            return yFactor * y + xFactor * x;
        }

        public BitMatrix Clone()
        {
            return new BitMatrix()
                {
                    Width = this.Width,
                    Height = this.Height,
                    bits = (BitArray)this.bits.Clone(),
                    xFactor = this.xFactor,
                    yFactor = this.yFactor
                };
        }

        public void Transpose()
        {
            int h = yFactor;
            yFactor = xFactor;
            xFactor = h;

            h = Width;
            Width = Height;
            Height = h;
        }

    }
}
