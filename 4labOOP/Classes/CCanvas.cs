using System.Collections.Generic;

namespace _4labOOP
{
    public class CCanvas
    {
        public int Picture;
        public  List<CFigure> FigureList =new List<CFigure>();
        public CCanvas()
        {
        }
        public void Add(CFigure pAddition)
        {
            //if(FigureList.Count!=3)
            FigureList.Add(pAddition);
        }
    }
}