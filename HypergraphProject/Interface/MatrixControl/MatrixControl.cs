using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private Point mouseCoord = new Point(-1, -1);

        // Variables for editing.
        private Size oldDimension;
        private BitMatrix oldMatrix = null;

        public MatrixControl()
        {
            InitializeComponent();

            // Double buffered, to avoid flickering.
            this.DoubleBuffered = true;

            Colors = new MatrixColors();

        }

        /// <summary>
        /// Determines if the matrix can be edited.
        /// </summary>
        public bool IsEditing { get; protected set; }

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

        /// <summary>
        /// Resizes the based on its status and matrix.
        /// </summary>
        private void UpdateSize()
        {

            // 1 extra space for a 1 pixel frame.
            int editWidth = IsEditing ? fieldSize.Width + 1 : 0;
            int editHeight = IsEditing ? fieldSize.Height + 1 : 0;

            // 2 extra space for a 1 pixel frame.
            this.Size =
                new Size(
                    fieldSize.Width * Dimension.Width + editWidth + 2,
                    fieldSize.Height * Dimension.Height + editHeight + 2
                );
        }

        /// <summary>
        /// Determines if the given coordinate is in the matrix.
        /// </summary>
        private bool IsInMatrix(Point guiPoint)
        {
            // 1 space for border.
            int matrixX = 1;
            int matrixY = 1;

            int matrixWidth = fieldSize.Width * Dimension.Width;
            int matrixHeight = fieldSize.Height * Dimension.Height;

            return
                guiPoint.X >= matrixX &&
                guiPoint.X < matrixX + matrixWidth &&
                guiPoint.Y >= matrixY &&
                guiPoint.Y < matrixY + matrixHeight;
        }

        /// <summary>
        /// Determines if the given coordinate is in the bottom border.
        /// </summary>
        private bool IsInBottomBorder(Point guiPoint)
        {
            int matrixWidth = fieldSize.Width * Dimension.Width;
            int matrixHeight = fieldSize.Height * Dimension.Height;

            return // 1 and 2 for one pixel frames.
                guiPoint.X >= 1 &&
                guiPoint.X < matrixWidth + 1 &&
                guiPoint.Y >= matrixHeight + 2 &&
                guiPoint.Y < matrixHeight + fieldSize.Height + 2;
        }

        /// <summary>
        /// Determines if the given coordinate is in the right border.
        /// </summary>
        private bool IsInRightBorder(Point guiPoint)
        {
            int matrixWidth = fieldSize.Width * Dimension.Width;
            int matrixHeight = fieldSize.Height * Dimension.Height;

            return // 1 and 2 for one pixel frames.
                guiPoint.X >= matrixWidth + 2 &&
                guiPoint.X < matrixWidth + fieldSize.Width + 2 &&
                guiPoint.Y >= 1 &&
                guiPoint.Y < matrixHeight + 1;
        }

        /// <summary>
        /// Draws a field of the matrix.
        /// </summary>
        private void DrawField(Graphics g, string fieldChar, Font font, StringFormat strForm, Point fieldPt, Color bgColor, Color borderColor, Color textColor)
        {

            if (bgColor != Color.Transparent)
            {
                g.FillRectangle(
                    new SolidBrush(bgColor),
                    fieldPt.X,
                    fieldPt.Y,
                    fieldSize.Width,
                    fieldSize.Height
                );
            }

            if (borderColor != Color.Transparent)
            {
                g.DrawRectangle(
                    new Pen(borderColor, 1F),
                    fieldPt.X,
                    fieldPt.Y,
                    fieldSize.Width - 1,
                    fieldSize.Height - 1
                );
            }

            if (textColor != Color.Transparent)
            {
                g.DrawString(
                    fieldChar,
                    font,
                    new SolidBrush(textColor),
                    new RectangleF(
                        fieldPt.X,
                        fieldPt.Y + 1,
                        fieldSize.Width,
                        fieldSize.Height
                    ),
                    strForm
                );
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouseCoord = e.Location;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseCoord = new Point(-1, -1);
            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!IsEditing) return;

            if (IsInMatrix(mouseCoord))
            {
                Point fieldPt = GuiToField(mouseCoord);
                this[fieldPt.X, fieldPt.Y] = !this[fieldPt.X, fieldPt.Y];
            }
        }

        /// <summary>
        /// Draws the matrix;
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            int startTime = Environment.TickCount;
            base.OnPaint(e);

            // ----------
            // Preprocessing

            Graphics g = e.Graphics;

            int matrixWidth = fieldSize.Width * Dimension.Width;
            int matrixHeight = fieldSize.Height * Dimension.Height;

            Font font = new Font("Consolas", 10.0f);
            StringFormat strForm = new StringFormat();
            strForm.Alignment = StringAlignment.Center;
            strForm.LineAlignment = StringAlignment.Center;

            bool mouseInMatrix = IsInMatrix(mouseCoord);

            bool mouseInBottomBorder = IsInBottomBorder(mouseCoord);
            bool mouseInRightBorder = IsInRightBorder(mouseCoord);
            bool mouseInBorder = mouseInBottomBorder || mouseInRightBorder;

            // ToDo: Mouse in corner.

            Point fieldPt = GuiToField(mouseCoord);
            // ToDo: border point

            // ----------
            // Background

            g.Clear(this.BackColor);

            bool drawRow = mouseInMatrix || mouseInRightBorder;
            bool drawCol = mouseInMatrix || mouseInBottomBorder;

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

            if (drawRow && drawCol)
            {
                EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                Brush rowBrush = new SolidBrush(Colors[IsEditing, rowEditStatus, true]);
                Brush colBrush = new SolidBrush(Colors[IsEditing, colEditStatus, true]);

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

            if (drawRow && !drawCol)
            {
                EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                Brush rowBrush = new SolidBrush(Colors[IsEditing, rowEditStatus, true]);

                g.FillRectangle(rowBrush, rowRec);
            }

            if (!drawRow && drawCol)
            {
                EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                Brush colBrush = new SolidBrush(Colors[IsEditing, colEditStatus, true]);

                g.FillRectangle(colBrush, colRec);
            }

            // ----------
            // Frame

            if (IsEditing)
            {
                g.DrawRectangle(new Pen(Color.Gray) { DashStyle = DashStyle.Dash }, 0, 0, matrixWidth + 1, matrixHeight + 1);
            }

            g.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);


            // ----------
            // Fields

            for (int x = 0; x < Dimension.Width; x++)
            {
                for (int y = 0; y < Dimension.Height; y++)
                {
                    bool isOne = this[x, y];
                    bool toggle =
                        IsEditing &&
                        x < oldDimension.Width &&
                        y < oldDimension.Height &&
                        oldMatrix[x, y] != isOne;

                    EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.
                    EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                    EditStatus fieldES = (EditStatus)Math.Max((int)rowEditStatus, (int)colEditStatus);

                    Color bgColor = Colors[isOne, toggle, fieldES, ColorFunction.Background];
                    Color borderColor = Colors[isOne, toggle, fieldES, ColorFunction.Border];
                    Color textColor = Colors[isOne, toggle, fieldES, ColorFunction.Text];

                    DrawField(
                        g,
                        isOne ? "1" : "0",
                        font,
                        strForm,
                        new Point(
                            fieldSize.Width * x + 1,
                            fieldSize.Height * y + 1
                        ),
                        bgColor,
                        borderColor,
                        textColor
                    );

                } // for x
            } // for y


            // ----------
            // Border for edit mode

            if (IsEditing)
            {
                for (int x = 0; x < Dimension.Width; x++)
                {
                    EditStatus colEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                    // ToDo: Dynamic charachter and color

                    Color bgColor = Color.Transparent;
                    Color borderColor = Color.Transparent;
                    Color textColor = Color.Gray;
                    string fieldChar = "-";

                    DrawField(
                        g,
                        fieldChar,
                        font,
                        strForm,
                        new Point(
                            fieldSize.Width * x + 1,
                            matrixHeight + 2
                        ),
                        bgColor,
                        borderColor,
                        textColor
                    );

                }

                for (int y = 0; y < Dimension.Height; y++)
                {
                    EditStatus rowEditStatus = EditStatus.Fixed; // ToDo: Build proper matrix edit status.

                    // ToDo: Dynamic charachter and color

                    Color bgColor = Color.Transparent;
                    Color borderColor = Color.Transparent;
                    Color textColor = Color.Gray;
                    string fieldChar = "-";

                    DrawField(
                        g,
                        fieldChar,
                        font,
                        strForm,
                        new Point(
                            matrixWidth + 2,
                            fieldSize.Height * y + 1
                        ),
                        bgColor,
                        borderColor,
                        textColor
                    );

                }

            }

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

            UpdateSize();

        }

        /// <summary>
        /// Switches the control into edit mode and allows to change values of the matrix.
        /// </summary>
        public void StartEditing()
        {
            oldMatrix = matrix.Clone();
            oldDimension = Dimension;
            IsEditing = true;
            UpdateSize();
        }

        /// <summary>
        /// Switches back to normal mode and restores the original matrix.
        /// </summary>
        public void CancelEditing()
        {
            matrix = oldMatrix;
            oldMatrix = null;

            // ToDo: Handle change of dimensions.

            IsEditing = false;
            UpdateSize();
            Refresh();
        }

        /// <summary>
        /// Switches back to normal mode and saves the changes.
        /// </summary>
        public void ExecuteEditing()
        {
            oldMatrix = null;

            // ToDo: Remove/Add rows and columns. 

            IsEditing = false;
            UpdateSize();
            Refresh();
        }

    }
}
