using System;

namespace _4labOOP
{
    /// <summary>
    /// �������������2 - ����� �������
    /// </summary>
    public class CRectangleIsOrange : CFigure
    {
        public int R2Width;
        public int R2Height;

        public override string DrawText()
        {
            return String.Format("�������������2 ({0}), X:{1}, Y:{2}, ������:{3}, ������:{4}", Color, X0, Y0, R2Height, R2Width);
        }
    }
}