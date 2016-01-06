using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Serialization;

namespace _4labOOP
{
    public enum EColor
    {
        Black, Orange, Green
    }

    public enum EFigures
    {
        Line, Rectangle, Rectangle2
    }
    
    public abstract class CFigure
    {
        public int X0;
        public int Y0;
        public EColor Color;
        
        /// <summary>
        /// Содержимое холста
        /// </summary>
        /// <returns></returns>
    //    public abstract void DrawGraphics(Graphics pG);
        public abstract string DrawText();
    }

    /// <summary>
    /// Линия
    /// </summary>
    public class CLine : CFigure
    {
        public int X1;
        public int Y1;

        public override string DrawText()
        {
            return String.Format("Линия ({0}), X1:{1}, Y1:{2}, X2:{3}, Y2:{4}", Color, X0, Y0, X1, Y1);
        }
    }

    /// <summary>
    /// Прямоугольник
    /// </summary>
    public class CRectangle : CFigure
    {
        public int RWidth;
        public int RHeight;

        public override string DrawText()
        {
            return String.Format("Прямоугольник ({0}), X:{1}, Y:{2}, Высота:{3}, Ширина:{4}", Color, X0, Y0, RHeight, RWidth);
        }
    }
    public class CRectangle2 : CFigure
    {
        public int R2Width;
        public int R2Height;

        public override string DrawText()
        {
            return String.Format("Прямоугольник2 ({0}), X:{1}, Y:{2}, Высота:{3}, Ширина:{4}", Color, X0, Y0, R2Height, R2Width);
        }
    }
    
    public class CCanvas
    {
        /*/// <summary>
        /// Место рисования
        /// </summary>*/

        public int Picture;
        public  List<CFigure> FigureList =new List<CFigure>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pAddition"></param>
        public CCanvas(int iPicture)
        {
            Picture = iPicture;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public CCanvas()
        {
        }
        public void Add(CFigure pAddition)
        {
            if(FigureList.Count!=3)
            FigureList.Add(pAddition);
        }
        //public string GetCanvas()
        //{
        //    return String.Join(", ", FigureList.Select(p => p.DrawText()));
        //}
    }

    static class Program
    {
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
