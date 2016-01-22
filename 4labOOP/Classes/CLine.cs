using System;

namespace _4labOOP
{
    /// <summary>
    /// Линия
    /// </summary>
    public class CLine : CFigure
    {
        //public int X1;
        //public int Y1;

        public override string DrawText()
        {
            return String.Format("Линия ({0}), X1:{1}, Y1:{2}, X2:{3}, Y2:{4}", Color, X0, Y0, X1, Y1);
        }
    }
}