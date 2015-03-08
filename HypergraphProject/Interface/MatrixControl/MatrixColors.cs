using System;
using System.Collections.Generic;
using System.Drawing;

namespace HypergraphProject.Interface
{
    public class MatrixColors
    {

        private Color[, , ,] fieldColorSet;
        private Color[,] bgColorSet;

        public MatrixColors()
        {
            // Zero/One, Toggle, EditStatus, ColorFunction
            fieldColorSet = new Color[2, 2, 3, 3];

            // ----------
            // 0 - Fixed

            this[false, false, EditStatus.Fixed, ColorFunction.Background] = Color.Transparent;
            this[false, false, EditStatus.Fixed, ColorFunction.Border] = Color.Transparent;
            this[false, false, EditStatus.Fixed, ColorFunction.Text] = Color.Gray;


            // ----------
            // 1 - Fixed

            this[true, false, EditStatus.Fixed, ColorFunction.Background] = Color.FromArgb(0xC6, 0xD9, 0xF0);
            this[true, false, EditStatus.Fixed, ColorFunction.Border] = Color.FromArgb(0x1F, 0x49, 0x7D);
            this[true, false, EditStatus.Fixed, ColorFunction.Text] = Color.Black;


            // ----------
            // 0 - Fixed (Toggled)

            this[false, true, EditStatus.Fixed, ColorFunction.Background] = Color.FromArgb(0xE5, 0xB9, 0xB7);
            this[false, true, EditStatus.Fixed, ColorFunction.Border] = Color.FromArgb(0x95, 0x37, 0x34);
            this[false, true, EditStatus.Fixed, ColorFunction.Text] = Color.Black;


            // ----------
            // 1 - Fixed (Toggled)

            this[true, true, EditStatus.Fixed, ColorFunction.Background] = Color.FromArgb(0xC3, 0xD6, 0x9B);
            this[true, true, EditStatus.Fixed, ColorFunction.Border] = Color.FromArgb(0x4F, 0x61, 0x28);
            this[true, true, EditStatus.Fixed, ColorFunction.Text] = Color.Black;


            // ----------
            // 0 - Remove

            this[false, false, EditStatus.Remove, ColorFunction.Background] = Color.Transparent;
            this[false, false, EditStatus.Remove, ColorFunction.Border] = Color.Transparent;
            this[false, false, EditStatus.Remove, ColorFunction.Text] = Color.Gray;


            // ----------
            // 1 - Remove

            this[true, false, EditStatus.Remove, ColorFunction.Background] = Color.FromArgb(0xE5, 0xB9, 0xB7);
            this[true, false, EditStatus.Remove, ColorFunction.Border] = Color.FromArgb(0x95, 0x37, 0x34);
            this[true, false, EditStatus.Remove, ColorFunction.Text] = Color.Black;


            // ------------------------------


            // Row EditStatus, Mouse
            bgColorSet = new Color[3, 2];

            this[EditStatus.Fixed, false] = Color.Transparent;
            this[EditStatus.Fixed, true] = Color.LightYellow;

            this[EditStatus.Add, false] = Color.FromArgb(0xEB, 0xF1, 0xDD);
            this[EditStatus.Add, true] = Color.FromArgb(0xD7, 0xE3, 0xBC);

            this[EditStatus.Remove, false] = Color.FromArgb(0xF2, 0xDC, 0xDB);
            this[EditStatus.Remove, true] = Color.FromArgb(0xE5, 0xB9, 0xB7);

            // ToDo: Remaining colors.

        }

        /// <summary>
        /// Returns the color of a field in the matrix.
        /// </summary>
        /// <param name="isOne">
        /// Is the field set to 1.
        /// </param>
        /// <param name="toggle">
        /// Is the value of the field changed?
        /// </param>
        /// <param name="EditStatus">
        /// The edit status of this field, i.e. of this row or column.
        /// </param>
        /// <param name="function">
        /// What will be drawn with this color.
        /// </param>
        /// <returns></returns>
        public Color this[bool isOne, bool toggle, EditStatus status, ColorFunction function]
        {
            get
            {
                return fieldColorSet[
                    isOne ? 1 : 0,
                    toggle ? 1 : 0,
                    (int)status,
                    (int)function
                ];
            }
            set
            {
                fieldColorSet[
                    isOne ? 1 : 0,
                    toggle ? 1 : 0,
                    (int)status,
                    (int)function
                ] = value;
            }
        }

        /// <summary>
        /// Returns the background color for rows and colums.
        /// </summary>
        /// <param name="rowEditStatus">
        /// The edit mode of this field, i.e. of this row or column.
        /// </param>
        /// <param name="mouseHover">
        /// Is the mouse over this row or column.
        /// </param>
        /// <returns></returns>
        public Color this[EditStatus rowEditStatus, bool mouseHover]
        {
            get
            {
                return bgColorSet[
                    (int)rowEditStatus,
                    mouseHover ? 1 : 0
                ];
            }
            set
            {
                bgColorSet[
                    (int)rowEditStatus,
                    mouseHover ? 1 : 0
                ] = value;
            }
        }

    }
}
