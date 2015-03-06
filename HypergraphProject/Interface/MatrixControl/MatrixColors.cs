using System;
using System.Collections.Generic;
using System.Drawing;

namespace HypergraphProject.Interface
{
    class MatrixColors
    {

        private Color[, , ,] colorSet;

        public MatrixColors()
        {
            // Zero/One, EditMode, ColorFunction, Mouse
            colorSet = new Color[2, 3, 3, 2];

            // ----------
            // 0 - Fixed

            this[false, EditMode.Fixed, ColorFunction.Background, false] = Color.White;
            this[false, EditMode.Fixed, ColorFunction.Background, true] = Color.LightYellow;

            this[false, EditMode.Fixed, ColorFunction.Border, false] = Color.Transparent;
            this[false, EditMode.Fixed, ColorFunction.Border, true] = Color.Transparent;

            this[false, EditMode.Fixed, ColorFunction.Text, false] = Color.Gray;
            this[false, EditMode.Fixed, ColorFunction.Text, true] = Color.Gray;


            // ----------
            // 1 - Fixed

            this[true, EditMode.Fixed, ColorFunction.Background, false] = Color.FromArgb(0x8DB3E2);
            this[true, EditMode.Fixed, ColorFunction.Background, true] = Color.FromArgb(0x8DB3E2);

            this[true, EditMode.Fixed, ColorFunction.Border, false] = Color.FromArgb(0x1F497D);
            this[true, EditMode.Fixed, ColorFunction.Border, true] = Color.FromArgb(0x1F497D);
                 
            this[true, EditMode.Fixed, ColorFunction.Text, false] = Color.Black;
            this[true, EditMode.Fixed, ColorFunction.Text, true] = Color.Black;

            // ToDo: Remaining colors.

        }

        public Color this[bool isOne, EditMode editMode, ColorFunction function, bool mouseHover]
        {
            get
            {
                return colorSet[
                    isOne ? 1 : 0,
                    (int)editMode,
                    (int)function,
                    mouseHover ? 1 : 0
                ];
            }
            set
            {
                colorSet[
                    isOne ? 1 : 0,
                    (int)editMode,
                    (int)function,
                    mouseHover ? 1 : 0
                ] = value;
            }
        }
    }
}
