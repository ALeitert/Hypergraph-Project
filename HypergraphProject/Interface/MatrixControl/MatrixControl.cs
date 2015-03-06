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

        private BitMatrix matrix = new BitMatrix(0, 0);

        private Point mousCoord = new Point(-1, -1);


        public MatrixControl()
        {
            InitializeComponent();

            // Double buffered, to avoid flickering.
            this.DoubleBuffered = true;

            Colors = new MatrixColors();

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
        /// </summary>
        public bool this[int x, int y]
        {
            get
            {
                return matrix[x, y];
            }
            set
            {
                bool oldVal = matrix[x, y];

                if (oldVal != value)
                {
                    matrix[x, y] = value;
                    OnFieldChanged(x, y, oldVal);
                }
            }
        }

        /// <summary>
        /// Returns the set of colors for this matrix.
        /// </summary>
        public MatrixColors Colors { get; protected set; }

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

            EditStatus matrixEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.


            if (mousCoord.X > 0 && mousCoord.Y > 0)
            {
                Point fieldPt = GuiToField(mousCoord);

                EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                Brush rowBrush = new SolidBrush(Colors[matrixEditStatus, rowEditStatus, true]);
                Brush colBrush = new SolidBrush(Colors[matrixEditStatus, colEditStatus, true]);

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


                // Because row and column cross, there has to be a rule which color the field gets if both edit modes are different.
                if (colEditStatus > rowEditStatus)
                {
                    g.FillRectangle(rowBrush, rowRec);
                    g.FillRectangle(colBrush, colRec);
                }
                else
                {
                    g.FillRectangle(colBrush, colRec);
                    g.FillRectangle(rowBrush, rowRec);
                }

            }


            // ----------

            // Preprocessing for strings
            Font font = new Font("Consolas", 10.0f);
            StringFormat strForm = new StringFormat();
            strForm.Alignment = StringAlignment.Center;
            strForm.LineAlignment = StringAlignment.Center;

            for (int x = 0; x < Dimension.Width; x++)
            {
                for (int y = 0; y < Dimension.Height; y++)
                {
                    bool isOne = this[x, y];
                    bool toggle = false&& isOne; // ToDo: determine toggle.

                    EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                    EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                    EditStatus fieldES = (EditStatus)Math.Max((int)rowEditStatus, (int)colEditStatus);

                    Color bgColor = Colors[isOne, toggle, fieldES, ColorFunction.Background];
                    Color borderColor = Colors[isOne, toggle, fieldES, ColorFunction.Border];
                    Color textColor = Colors[isOne, toggle, fieldES, ColorFunction.Text];

                    Rectangle fieldRec =
                        new Rectangle(
                            fieldSize.Height * x + 1,
                            fieldSize.Height * y + 1,
                            fieldSize.Width - 1,
                            fieldSize.Height - 1
                        );

                    if (bgColor != Color.Transparent)
                    {
                        g.FillRectangle(new SolidBrush(bgColor), fieldRec);
                    }

                    if (borderColor != Color.Transparent)
                    {
                        g.DrawRectangle(new Pen(borderColor, 1F), fieldRec);
                    }

                    if (textColor != Color.Transparent)
                    {
                        g.DrawString(
                            isOne ? "1" : "0",
                            font,
                            new SolidBrush(textColor),
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

                } // for x
            } // for y


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
            BitMatrix newMatrix = new BitMatrix(Dimension.Width, Dimension.Height);

            int maxX = Math.Min(oldDim.Width, Dimension.Width);
            int maxY = Math.Min(oldDim.Height, Dimension.Height);

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    newMatrix[x, y] = matrix[x, y];
                }
            }

            matrix = newMatrix;


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
