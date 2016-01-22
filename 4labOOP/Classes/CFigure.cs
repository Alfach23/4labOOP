namespace _4labOOP
{
    public abstract class CFigure
    {
        public int X0;
        public int Y0;
        public EColor Color;
        public int X1;
        public int Y1;
        public bool isMove=false;

        public int ZIndex ;
        
        /// <summary>
        /// Содержимое холста
        /// </summary>
        /// <returns></returns>
        public abstract string DrawText();
    }
}