using System;

namespace _4labOOP
{
    /// <summary>
    /// �������������
    /// </summary>
    public class CRectangleIsBlack : CFigure
    {
        public int RWidth;
        public int RHeight;

        public override string DrawText()
        {
            return String.Format("������������� ({0}), X:{1}, Y:{2}, ������:{3}, ������:{4}", Color, X0, Y0, RHeight, RWidth);
        }
    }
}