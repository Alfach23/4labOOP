using System;

namespace _4labOOP
{
    /// <summary>
    /// Прямоугольник
    /// </summary>
    public class CRectangleIsBlack : CFigure
    {
        public int RWidth;
        public int RHeight;

        public override string DrawText()
        {
            return String.Format("Прямоугольник ({0}), X:{1}, Y:{2}, Высота:{3}, Ширина:{4}", Color, X0, Y0, RHeight, RWidth);
        }
    }
}