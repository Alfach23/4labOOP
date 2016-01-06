using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace _4labOOP
{
    public partial class Form1 : Form
    {
        private CCanvas m_pCanvas = new CCanvas();
        Bitmap bmp;//Здесь рисуем
        private CRectangle fRectangle;
        private CRectangle2 f2Rectangle;
        private CLine fLine;
        bool isMove = false;
        public Point PositionStart = new Point(0,0);
        public Form1()
        {
            InitializeComponent();
            
            comboBox3.Items.Add(EFigures.Rectangle);
            comboBox3.Items.Add(EFigures.Rectangle2);
            comboBox3.Items.Add(EFigures.Line);

            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        }
        private void Redraw()
        {
            using (Graphics GFigures = Graphics.FromImage(bmp))
            {
                GFigures.Clear(Color.White);
                comboBox2.Items.Clear();
                foreach (CFigure FigureList in m_pCanvas.FigureList)
                {
                    h_DrawFigure(GFigures, FigureList);
                    comboBox2.Items.Add(FigureList.DrawText());
                }
            }
            pictureBox1.Image = bmp;
        }

        //фигуры
        private static void h_DrawFigure(Graphics pG, CFigure fFigure)
        {
            if (fFigure is CRectangle) h_DrawFigure(pG, (CRectangle)fFigure);
            if (fFigure is CRectangle2) h_DrawFigure(pG, (CRectangle2)fFigure);
            if (fFigure is CLine) h_DrawFigure(pG, (CLine)fFigure);
        }
        private static void h_DrawFigure(Graphics rect, CRectangle fRect)
        {
            rect.DrawRectangle(Pens.Black, fRect.X0, fRect.Y0, fRect.RWidth, fRect.RHeight);
        }
        private static void h_DrawFigure(Graphics rect2, CRectangle2 fRect2)
        {
            rect2.DrawRectangle(Pens.Orange, fRect2.X0, fRect2.Y0, fRect2.R2Width, fRect2.R2Height);
        }
        private static void h_DrawFigure(Graphics lines, CLine flines)
        {
            lines.DrawLine(Pens.Green, flines.X0, flines.Y0, flines.X1, flines.Y1);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PositionStart = new Point(e.X, e.Y);
            if (fRectangle!=null)
            if (e.X >= fRectangle.X0 && e.X <= fRectangle.X0 + fRectangle.RWidth &&
                e.Y >= fRectangle.Y0 && e.Y <= fRectangle.Y0 + fRectangle.RHeight)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
            if (f2Rectangle != null)
            if (e.X >= f2Rectangle.X0 && e.X <= f2Rectangle.X0 + f2Rectangle.R2Width &&
                e.Y >= f2Rectangle.Y0 && e.Y <= f2Rectangle.Y0 + f2Rectangle.R2Height)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
            Redraw();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Redraw();
            if (isMove) //если фигуру можно двигать
            {
                fRectangle.X0 = e.X;
                fRectangle.Y0 = e.Y;
                //CFigure= new PointF(e.X, e.Y); //меняем ее координаты, на координаты мыши
                //if(e.X)
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
            if (comboBox3.SelectedIndex == 0)
            {
                isMove = false;
                int Xr = PositionStart.X;
                int Yr = PositionStart.Y;
                int Wr = e.X - Xr;
                int Hr = e.Y - Yr;

                if (e.X < PositionStart.X)
                {
                    Xr = e.X;
                    Wr = Wr * (-1);
                }
                if (e.Y < PositionStart.Y)
                {
                    Yr = e.Y;
                    Hr = Hr * (-1);
                }

                fRectangle = new CRectangle()
                {
                    Color = EColor.Black,
                    RHeight = Hr,
                    RWidth = Wr,
                    X0 = Xr,
                    Y0 = Yr
                };
                m_pCanvas.Add(fRectangle);
                Redraw();
            }

            if (comboBox3.SelectedIndex == 1)
            {
                isMove = false;
                int Xr2 = PositionStart.X;
                int Yr2 = PositionStart.Y;
                int Wr2 = e.X - Xr2;
                int Hr2 = e.Y - Yr2;

                if (e.X < PositionStart.X)
                {
                    Xr2 = e.X;
                    Wr2 = Wr2 * (-1);
                }
                if (e.Y < PositionStart.Y)
                {
                    Yr2 = e.Y;
                    Hr2 = Hr2 * (-1);
                }

                f2Rectangle = new CRectangle2()
                {
                    Color = EColor.Orange,
                    R2Height = Hr2,
                    R2Width = Wr2,
                    X0 = Xr2,
                    Y0 = Yr2
                };
                m_pCanvas.Add(f2Rectangle);
                Redraw();
            }

            if (comboBox3.SelectedIndex == 2)
            {
                isMove = false;
                int xl1 = PositionStart.X;
                int yl1 = PositionStart.Y;
                int xl2 = e.X;
                int yl2 = e.Y;
                fLine = new CLine()
                {
                    Color = EColor.Green,
                    X0 = xl1,
                    Y0 = yl1,
                    X1 = xl2,
                    Y1 = yl2
                };
                m_pCanvas.Add(fLine);
                Redraw();
            }
        }
    }
}