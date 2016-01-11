using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Bitmap bmp;
        bool isMoves = false;
        bool isPaint = true;
        public Point PositionStart = new Point(0,0);
        public Point PositionEnd = new Point(0, 0);
        public Form1()
        {
            InitializeComponent();

            comboBox3.Items.Add("Перетаскивание");
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
            Trace.WriteLine(String.Format("Начало {0}", PositionStart.ToString()));
            //движение
            if (comboBox3.SelectedIndex==0)
            foreach (CFigure FigureList in m_pCanvas.FigureList)
            {
                if (FigureList != null)
                {
                    if (PositionStart.X >= FigureList.X0 && PositionStart.X <= FigureList.X1 &&
                        PositionStart.Y >= FigureList.Y0 && PositionStart.Y <= FigureList.Y1)
                    {
                        isMoves = true;
                        FigureList.isMove = true;
                        isPaint = false;
                    }
                    else
                    {
                        isMoves = false;
                        FigureList.isMove = false;
                        isPaint = true;
                    }
                }
                Redraw();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (CFigure FigureList in m_pCanvas.FigureList.Where(n => n.isMove))
            {
                Point newPosition = new Point(e.X, e.Y);
                FigureList.X0 = newPosition.X;
                FigureList.Y0 = newPosition.Y;
                FigureList.X1 = PositionEnd.X;
                FigureList.Y1 = PositionEnd.Y;
                Redraw();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            PositionEnd = new Point(e.X, e.Y);
            Trace.WriteLine(String.Format("Конец {0}", PositionStart.ToString()));
            //if(isMoves)
                foreach (CFigure FigureList in m_pCanvas.FigureList.Where(n => n.isMove))
                {
                    FigureList.isMove = false;
                }
            //if (isPaint)
                //{
                  if (comboBox3.SelectedIndex == 1)
                    {
                        int Xr = PositionStart.X;
                        int Yr = PositionStart.Y;
                        int Wr = PositionEnd.X - Xr;
                        int Hr = PositionEnd.Y - Yr;

                        if (PositionEnd.X < PositionStart.X)
                        {
                            Xr = PositionEnd.X;
                            Wr = Wr * (-1);
                        }
                        if (PositionEnd.Y < PositionStart.Y)
                        {
                            Yr = PositionEnd.Y;
                            Hr = Hr * (-1);
                        }

                        CRectangle fRectangle = new CRectangle()
                        {
                            Color = EColor.Black,
                            RHeight = Hr,
                            RWidth = Wr,
                            X0 = Xr,
                            Y0 = Yr,
                            X1 = PositionEnd.X,
                            Y1 = PositionEnd.Y,
                            isMove = false
                        };
                        m_pCanvas.Add(fRectangle);
                        Redraw();
                    }

                    if (comboBox3.SelectedIndex == 2)
                    {
                        int Xr2 = PositionStart.X;
                        int Yr2 = PositionStart.Y;
                        int Wr2 = PositionEnd.X - Xr2;
                        int Hr2 = PositionEnd.Y - Yr2;

                        if (PositionEnd.X < PositionStart.X)
                        {
                            Xr2 = PositionEnd.X;
                            Wr2 = Wr2 * (-1);
                        }
                        if (PositionEnd.Y < PositionStart.Y)
                        {
                            Yr2 = PositionEnd.Y;
                            Hr2 = Hr2 * (-1);
                        }

                        CRectangle2 f2Rectangle = new CRectangle2()
                        {
                            Color = EColor.Orange,
                            R2Height = Hr2,
                            R2Width = Wr2,
                            X0 = Xr2,
                            Y0 = Yr2,
                            X1 = PositionEnd.X,
                            Y1 = PositionEnd.Y,
                            isMove = false
                        };
                        m_pCanvas.Add(f2Rectangle);
                        Redraw();
                    }

                    if (comboBox3.SelectedIndex == 3)
                    {
                        int xl1 = PositionStart.X;
                        int yl1 = PositionStart.Y;
                        int xl2 = PositionEnd.X;
                        int yl2 = PositionEnd.Y;
                        CLine fLine = new CLine()
                        {
                            Color = EColor.Green,
                            X0 = xl1,
                            Y0 = yl1,
                            X1 = xl2,
                            Y1 = yl2,
                            isMove = false
                        };
                        m_pCanvas.Add(fLine);
                        Redraw();
                    }
                //}
                //isPaint = false;
                Redraw();
        }
    }
}