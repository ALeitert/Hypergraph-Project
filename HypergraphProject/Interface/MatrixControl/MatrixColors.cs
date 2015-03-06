using System;
using System.Collections.Generic;
using System.Drawing;

namespace HypergraphProject.Interface
{
    class MatrixColors
    {

        private Color[, ,] fieldColorSet;
        private Color[, ,] bgColorSet;

        public MatrixColors()
        {
            // Zero/One, EditMode, ColorFunction
            fieldColorSet = new Color[2, 3, 3];

            // ----------
            // 0 - Fixed

            this[false, EditMode.Fixed, ColorFunction.Background] = Color.Transparent;
            this[false, EditMode.Fixed, ColorFunction.Border] = Color.Transparent;
            this[false, EditMode.Fixed, ColorFunction.Text] = Color.Gray;


            // ----------
            // 1 - Fixed

            this[true, EditMode.Fixed, ColorFunction.Background] = Color.FromArgb(0x8DB3E2);
            this[true, EditMode.Fixed, ColorFunction.Border] = Color.FromArgb(0x1F497D);
            this[true, EditMode.Fixed, ColorFunction.Text] = Color.Black;


            // ------------------------------


            // Matrix EditMode, Row EditMode, Mouse
            bgColorSet = new Color[3, 3, 2];

            this[EditMode.Fixed, EditMode.Fixed, false] = Color.Transparent;
            this[EditMode.Fixed, EditMode.Fixed, true] = Color.LightYellow;

            // ToDo: Remaining colors.

        }

        /// <summary>
        /// Returns the color of a field in the matrix.
        /// </summary>
        /// <param name="isOne">
        /// Is the field set to 1.
        /// </param>
        /// <param name="editMode">
        /// The edit mode of this field, i.e. of this row or column.
        /// </param>
        /// <param name="function">
        /// What will be drawn with this color.
        /// </param>
        /// <returns></returns>
        public Color this[bool isOne, EditMode editMode, ColorFunction function]
        {
            get
            {
                return fieldColorSet[
                    isOne ? 1 : 0,
                    (int)editMode,
                    (int)function
                ];
            }
            set
            {
                fieldColorSet[
                    isOne ? 1 : 0,
                    (int)editMode,
                    (int)function
                ] = value;
            }
        }

        /// <summary>
        /// Returns the background color for rows and colums.
        /// </summary>
        /// <param name="matrixEditMode">
        /// The global edit mode.
        /// </param>
        /// <param name="rowEditMode">
        /// The edit mode of this field, i.e. of this row or column.
        /// </param>
        /// <param name="mouseHover">
        /// Is the mouse over this row or column.
        /// </param>
        /// <returns></returns>
        public Color this[EditMode matrixEditMode, EditMode rowEditMode, bool mouseHover]
        {
            get
            {
                return bgColorSet[
                    (int)matrixEditMode,
                    (int)rowEditMode,
                    mouseHover ? 1 : 0
                ];
            }
            set
            {
                bgColorSet[
                    (int)matrixEditMode,
                    (int)rowEditMode,
                    mouseHover ? 1 : 0
                ] = value;
            }
        }

    }
}
