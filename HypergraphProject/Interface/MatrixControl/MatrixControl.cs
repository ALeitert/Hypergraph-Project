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

        private enum Area
        {
            Matrix,
            Buttom,
            Right,
            Corner,
            Frame,
            Unknown,
        }

        private Size fieldSize = new Size(16, 16);
        private Size dimension;

        private BitMatrix matrix = new BitMatrix(0, 0);

        private Point mouseCoord = new Point(-1, -1);

        // Variables for editing.
        private Size oldDimension;
        private BitMatrix oldMatrix = null;

        // List to add new rows and columns easier and more performant.
        private List<EditStatus> rowEditStatus = null;
        private List<EditStatus> colEditStatus = null;

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

        public Rectangle Matrix
        {
            get
            {
                return new Rectangle(
                    1,
                    1,
                    fieldSize.Width * Dimension.Width,
                    fieldSize.Height * Dimension.Height
                );
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
            int dX = 0;
            int dY = 0;

            // Correct extra pixel taken by frame.
            switch (GetArea(guiPt))
            {
                case Area.Matrix:
                    dX = 1;
                    dY = 1;
                    break;

                case Area.Buttom:
                    dX = 1;
                    dY = 2;
                    break;

                case Area.Right:
                    dX = 2;
                    dY = 1;
                    break;

                case Area.Corner:
                    dX = 2;
                    dY = 2;
                    break;

                case Area.Unknown:
                    return new Point(-1, -1);
            }

            return new Point(
                (guiPt.X - dX) / fieldSize.Width,
                (guiPt.Y - dY) / fieldSize.Height
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
        /// Determines in which type of area the given coordinate is.
        /// </summary>
        private Area GetArea(Point guiPoint)
        {

            int[] xMapRange = { 0, 1, Matrix.Width, 1, fieldSize.Width, 1 };
            int[] yMapRange = { 0, 1, Matrix.Height, 1, fieldSize.Height, 1 };

            Area[] xMap = { Area.Unknown, Area.Frame, Area.Matrix, Area.Frame, Area.Right, Area.Frame };
            Area[] yMap = { Area.Unknown, Area.Frame, Area.Matrix, Area.Frame, Area.Buttom, Area.Frame };

            Area xArea = Area.Unknown;
            Area yArea = Area.Unknown;

            int width = 0;
            for (int x = 0; x < xMapRange.Length; x++)
            {
                width += xMapRange[x];

                if (guiPoint.X < width)
                {
                    xArea = xMap[x];
                    break;
                }
            }

            int height = 0;
            for (int y = 0; y < yMapRange.Length; y++)
            {
                height += yMapRange[y];

                if (guiPoint.Y < height)
                {
                    yArea = yMap[y];
                    break;
                }
            }

            if (xArea == Area.Right && yArea == Area.Buttom)
            {
                return Area.Corner;
            }

            if (xArea > yArea)
            {
                return xArea;
            }
            else
            {
                return yArea;
            }
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

            Point fieldPt = GuiToField(mouseCoord);

            EditStatus[] oldEditChange = 
            {
                EditStatus.Remove, 
                EditStatus.Remove, // Should not happen.
                EditStatus.Fixed 
            };

            EditStatus[] newEditChange = 
            {
                EditStatus.Remove, // Should not happen. 
                EditStatus.Remove,
                EditStatus.Add 
            };

            switch (GetArea(mouseCoord))
            {
                case Area.Matrix:
                    this[fieldPt.X, fieldPt.Y] = !this[fieldPt.X, fieldPt.Y];
                    break;

                case Area.Buttom:

                    if (fieldPt.X < oldDimension.Width)
                    {
                        // Alredy existing column.
                        colEditStatus[fieldPt.X] = oldEditChange[(int)colEditStatus[fieldPt.X]];
                    }
                    else
                    {
                        // New column.
                        colEditStatus[fieldPt.X] = newEditChange[(int)colEditStatus[fieldPt.X]];
                    }

                    break;

                case Area.Right:

                    if (fieldPt.Y < oldDimension.Height)
                    {
                        // Alredy existing column.
                        rowEditStatus[fieldPt.Y] = oldEditChange[(int)rowEditStatus[fieldPt.Y]];
                    }
                    else
                    {
                        // New column.
                        rowEditStatus[fieldPt.Y] = newEditChange[(int)rowEditStatus[fieldPt.Y]];
                    }

                    break;

                case Area.Corner:
                    break;
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

            Font font = new Font("Consolas", 10.0f);
            StringFormat strForm = new StringFormat();
            strForm.Alignment = StringAlignment.Center;
            strForm.LineAlignment = StringAlignment.Center;

            Point fieldPt = GuiToField(mouseCoord);

            // ----------
            // Background

            g.Clear(this.BackColor);

            List<int> fixedRCs = new List<int>();
            List<int> addRCs = new List<int>();
            List<int> removeRCs = new List<int>();

            for (int r = 0; r < Dimension.Height; r++)
            {
                EditStatus rES =
                    (rowEditStatus != null && rowEditStatus.Count > r) ?
                    rowEditStatus[r] : EditStatus.Fixed;

                int rowEntry = -(r + 1);

                switch (rES)
                {
                    case EditStatus.Fixed:
                        fixedRCs.Add(rowEntry);
                        break;

                    case EditStatus.Add:
                        addRCs.Add(rowEntry);
                        break;

                    case EditStatus.Remove:
                        removeRCs.Add(rowEntry);
                        break;
                }
            }

            for (int c = 0; c < Dimension.Width; c++)
            {
                EditStatus cES =
                    (colEditStatus != null && colEditStatus.Count > c) ?
                    colEditStatus[c] : EditStatus.Fixed;

                int colEntry = (c + 1);

                switch (cES)
                {
                    case EditStatus.Fixed:
                        fixedRCs.Add(colEntry);
                        break;

                    case EditStatus.Add:
                        addRCs.Add(colEntry);
                        break;

                    case EditStatus.Remove:
                        removeRCs.Add(colEntry);
                        break;
                }
            }

            // Allows to iterate over one list.
            List<int> allRCs = new List<int>(fixedRCs.Count + addRCs.Count + removeRCs.Count);
            allRCs.AddRange(fixedRCs);
            allRCs.AddRange(addRCs);
            allRCs.AddRange(removeRCs);

            for (int i = 0; i < allRCs.Count; i++)
            {
                int entry = allRCs[i];

                EditStatus entryES;

                if (i < fixedRCs.Count)
                {
                    entryES = EditStatus.Fixed;
                }
                else if (i < fixedRCs.Count + addRCs.Count)
                {
                    entryES = EditStatus.Add;
                }
                else
                {
                    entryES = EditStatus.Remove;
                }

                bool isRow = entry < 0;
                int entryIndex = (isRow ? -1 : 1) * entry - 1;
                bool isMouse = entryIndex == (isRow ? fieldPt.Y : fieldPt.X);
                
                Color rsColor = Colors[IsEditing, entryES, isMouse];

                if (rsColor != Color.Transparent)
                {
                    Rectangle rec =
                        new Rectangle(
                            isRow ? 1 : fieldSize.Width * entryIndex + 1,
                            isRow ? fieldSize.Height * entryIndex + 1 : 1,
                            isRow ? this.Width - 2 : fieldSize.Width,
                            isRow ? fieldSize.Height : this.Height - 2
                        );

                    g.FillRectangle(new SolidBrush(rsColor), rec);
                }
            }


            Rectangle colRec =
                new Rectangle(
                    fieldSize.Height * fieldPt.X + 1,
                    1,
                    fieldSize.Width,
                    this.Height - 2
                );


            // ----------
            // Frame

            if (IsEditing)
            {
                g.DrawRectangle(new Pen(Color.Gray) { DashStyle = DashStyle.Dash }, 0, 0, Matrix.Width + 1, Matrix.Height + 1);
            }

            g.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);


            // ----------
            // Fields

            for (int x = 0; x < Dimension.Width; x++)
            {

                EditStatus xES =
                    (colEditStatus != null && colEditStatus.Count > x) ?
                    colEditStatus[x] : EditStatus.Fixed;

                for (int y = 0; y < Dimension.Height; y++)
                {
                    bool isOne = this[x, y];
                    bool toggle =
                        IsEditing &&
                        x < oldDimension.Width &&
                        y < oldDimension.Height &&
                        oldMatrix[x, y] != isOne;

                    EditStatus yES =
                        (rowEditStatus != null && rowEditStatus.Count > y) ?
                        rowEditStatus[y] : EditStatus.Fixed;


                    EditStatus fieldES = (EditStatus)Math.Max((int)xES, (int)yES);

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
                            Matrix.Height + 2
                        ),
                        bgColor,
                        borderColor,
                        textColor
                    );

                }

                for (int y = 0; y < Dimension.Height; y++)
                {
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
                            Matrix.Width + 2,
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

            rowEditStatus = new List<EditStatus>(Dimension.Height);
            colEditStatus = new List<EditStatus>(Dimension.Width);

            for (int r = 0; r < Dimension.Height; r++)
            {
                rowEditStatus.Add(EditStatus.Fixed);
            }

            for (int r = 0; r < Dimension.Width; r++)
            {
                colEditStatus.Add(EditStatus.Fixed);
            }

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

            rowEditStatus = null;
            colEditStatus = null;

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

            rowEditStatus = null;
            colEditStatus = null;

            IsEditing = false;
            UpdateSize();
            Refresh();
        }

    }
}
