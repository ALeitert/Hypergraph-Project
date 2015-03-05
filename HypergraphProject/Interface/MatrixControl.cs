using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace HypergraphProject.Interface
{
    public partial class MatrixControl : UserControl
    {

        private Size fieldSize = new Size(16, 16);
        private Size dimension;

        private BitArray fields = new BitArray(0);

        public MatrixControl()
        {
            InitializeComponent();

            // Double buffered, to avoid flickering.
            this.DoubleBuffered = true;

        }

        /// <summary>
        /// Returns or sets the size of the matrix.
        /// </summary>
        public Size Dimension
        {
            get
            {
                return dimension;
            }
            set
            {
                if (value == dimension)
                {
                    return;
                }

                if (value.Width < 0 || value.Height < 0)
                {
                    throw new ArgumentException();
                }

                Size oldDim = dimension;
                dimension = value;

                OnDimensionChanged(oldDim);

            }
        }

        /// <summary>
        /// Returns or sets the value of a field.
        /// If a coordinate is out of range, the property returns false.
        /// </summary>
        public bool this[int x, int y]
        {
            get
            {
                if (x < 0 || Dimension.Width <= x || y < 0 || Dimension.Height <= y)
                {
                    return false;
                }

                int index = GetCoordinateIndex(x, y, Dimension.Width);

                return fields[index];
            }
            set
            {
                if (x < 0 || Dimension.Width <= x || y < 0 || Dimension.Height <= y)
                {
                    throw new ArgumentOutOfRangeException();
                }

                int index = GetCoordinateIndex(x, y, Dimension.Width);

                bool oldVal = fields[index];

                if (oldVal != value)
                {
                    fields[index] = value;
                    OnFieldChanged(x, y, oldVal);
                }
            }
        }

        /// <summary>
        /// Calculates the index in an one dimensional array of the given coordinate.
        /// </summary>
        private int GetCoordinateIndex(int x, int y, int width)
        {
            return width * y + x;
        }


        private Point GuiToField(Point guiPt)
        {
            // - 1 to correct extra pixel taken by frame.
            return new Point(
                (guiPt.X - 1) / fieldSize.Width,
                (guiPt.Y - 1) / fieldSize.Height
            );
        }

        private Point mousCoord = new Point(-1, -1);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mousCoord = e.Location;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mousCoord = new Point(-1, -1);
            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (mousCoord.X > 0 && mousCoord.X < this.Width - 1 && mousCoord.Y > 0 && mousCoord.Y < this.Height - 1)
            {
                Point fieldPt = GuiToField(mousCoord);
                this[fieldPt.X, fieldPt.Y] = !this[fieldPt.X, fieldPt.Y];
            }
        }

        /// <summary>
        /// Draws the matrix;
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(this.BackColor);


            // ----------


            if (mousCoord.X > 0 && mousCoord.Y > 0)
            {
                Point fieldPt = GuiToField(mousCoord);

                Rectangle rowRec =
                    new Rectangle(
                        1,
                        fieldSize.Height * fieldPt.Y + 1,
                        this.Width - 2,
                        fieldSize.Height
                    );

                Rectangle colRec =
                    new Rectangle(
                        fieldSize.Height * fieldPt.X + 1,
                        1,
                        fieldSize.Width,
                        this.Height - 2
                    );


                g.FillRectangles(Brushes.LightYellow, new Rectangle[] { rowRec, colRec });
            }


            // ----------

            // Preprocessing for strings
            Font font = new Font("Consolas", 10.0f);
            Brush oneBrush = Brushes.Black;
            Brush zeroBrush = Brushes.Gray;

            StringFormat strForm = new StringFormat();
            strForm.Alignment = StringAlignment.Center;
            strForm.LineAlignment = StringAlignment.Center;

            for (int x = 0; x < Dimension.Width; x++)
            {
                for (int y = 0; y < Dimension.Height; y++)
                {
                    bool isOne = this[x, y];

                    if (isOne)
                    {

                        Rectangle fieldRec =
                            new Rectangle(
                                fieldSize.Height * x + 1,
                                fieldSize.Height * y + 1,
                                fieldSize.Width - 1,
                                fieldSize.Height - 1
                            );

                        g.FillRectangle(Brushes.Yellow, fieldRec);
                        g.DrawRectangle(Pens.Gold, fieldRec);

                    }

                    g.DrawString(
                        isOne ? "1" : "0",
                        font,
                        isOne ? oneBrush : zeroBrush,
                        new RectangleF(
                        // + 1 because of the frame.
                            fieldSize.Width * x + 1,
                            fieldSize.Height * y + 2,
                            fieldSize.Width,
                            fieldSize.Height
                        ),
                        strForm
                    );
                }
            }


            // ----------

            g.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);

        }

        /// <summary>
        /// Is called when the value of a field was changed.
        /// </summary>
        protected void OnFieldChanged(int x, int y, bool oldVal)
        {
            // Redraw matrix.
            Refresh();
        }

        /// <summary>
        /// Is called when the size of the matrix changes.
        /// </summary>
        protected void OnDimensionChanged(Size oldDim)
        {
            // Create new bit array
            BitArray newFields = new BitArray(Dimension.Width * Dimension.Height);

            for (int x = 0; x < oldDim.Width; x++)
            {
                for (int y = 0; y < oldDim.Height; y++)
                {
                    int oldIndex = GetCoordinateIndex(x, y, oldDim.Width);
                    int newIndex = GetCoordinateIndex(x, y, Dimension.Width);

                    newFields[newIndex] = fields[oldIndex];
                }
            }

            fields = newFields;


            //-----------

            // 2 extra space for a 1 pixel frame.
            this.Size =
                new Size(
                    fieldSize.Width * Dimension.Width + 2,
                    fieldSize.Height * Dimension.Height + 2
                );

        }

    }
}
