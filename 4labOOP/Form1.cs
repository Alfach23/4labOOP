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
        private Bitmap bmp;
        private bool isMoves = false;
        private bool isPaint = true;

        public int ZIndextoForm = 0;
        public Point PositionStart = new Point(0, 0);

        public int Xline1;
        public int Yline1;
        public int Xline2;
        public int Yline2;

        public Form1()
        {
            InitializeComponent();

            comboBox3.Items.Add("Перетаскивание");
            comboBox3.Items.Add(EFigures.Rectangle);
            comboBox3.Items.Add(EFigures.Rectangle2);
            comboBox3.Items.Add(EFigures.Line);

            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PositionStart = new Point(e.X, e.Y);
            Trace.WriteLine(String.Format("Начало {0}", PositionStart));
            //движение
            if (comboBox3.SelectedIndex == 0)
                GetPositionPointer(ref PositionStart);

            if (comboBox3.SelectedIndex == 3)
                GetDrawLine(ref PositionStart);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (CFigure FigureList in m_pCanvas.FigureList.Where(n => n.isMove))
            {
                FigureList.X0 = e.X;
                FigureList.Y0 = e.Y;
                FigureList.X1 = FigureList.X1 + e.X;
                FigureList.Y1 = FigureList.Y1 + e.Y;
                Redraw();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoves = false;
            Trace.WriteLine(String.Format("Конец {0}", PositionStart));

            foreach (CFigure FigureList in m_pCanvas.FigureList.Where(n => n.isMove))
            {
                isMoves = false;
                FigureList.isMove = false;
            }

            if (comboBox3.SelectedIndex == 1)
            {
                Draw_And_AddRectangleIsBlack(ref e);
            }

            if (comboBox3.SelectedIndex == 2)
            {
                Draw_And_AddRectangleIsOrange(ref e);
            }

            if (comboBox3.SelectedIndex == 3)
            {
                Draw_And_AddLine(ref e);
            }
            Redraw();
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
            if (fFigure is CRectangleIsBlack) h_DrawFigure(pG, (CRectangleIsBlack) fFigure);
            if (fFigure is CRectangleIsOrange) h_DrawFigure(pG, (CRectangleIsOrange) fFigure);
            if (fFigure is CLine) h_DrawFigure(pG, (CLine) fFigure);
        }

        private static void h_DrawFigure(Graphics rect, CRectangleIsBlack fRect)
        {
            rect.DrawRectangle(Pens.Black, fRect.X0, fRect.Y0, fRect.RWidth, fRect.RHeight);
        }

        private static void h_DrawFigure(Graphics rect2, CRectangleIsOrange fRect2)
        {
            rect2.DrawRectangle(Pens.Orange, fRect2.X0, fRect2.Y0, fRect2.R2Width, fRect2.R2Height);
        }

        private static void h_DrawFigure(Graphics lines, CLine flines)
        {
            lines.DrawLine(Pens.Green, flines.X0, flines.Y0, flines.X1, flines.Y1);
        }

        private void GetPositionPointer(ref Point pS)
        {
            foreach (CFigure FigureList in m_pCanvas.FigureList)
            {
                if (FigureList != null)
                {
                    if (pS.X >= FigureList.X0 && pS.X <= FigureList.X1 &&
                        pS.Y >= FigureList.Y0 && pS.Y <= FigureList.Y1)
                    {
                        isMoves = true;
                        FigureList.isMove = true;
                    }
                    else
                    {
                        isMoves = false;
                        FigureList.isMove = false;
                    }
                }
                Redraw();
            }
        }

        private void GetDrawLine(ref Point pS)
        {
            if (m_pCanvas.FigureList != null)
            {
                foreach (CFigure FigureList in m_pCanvas.FigureList)
                {
                    if (pS.X >= FigureList.X0 && pS.X <= FigureList.X1 &&
                        pS.Y >= FigureList.Y0 && pS.Y <= FigureList.Y1)
                    {
                        isPaint = true;
                        Xline1 = (FigureList.X0 + FigureList.X1)/2;
                        Yline1 = (FigureList.Y0 + FigureList.Y1)/2;
                    }
                    else
                    {
                        isPaint = false;
                    }
                }
            }
        }

        private void Draw_And_AddLine(ref MouseEventArgs e)
        {
            //xline1 = PositionStart.X;
            //yline1 = PositionStart.Y;

            foreach (CFigure FigureList in m_pCanvas.FigureList)
            {
                if (FigureList != null)
                {
                    if (e.X >= FigureList.X0 && e.X <= FigureList.X1 &&
                        e.Y >= FigureList.Y0 && e.Y <= FigureList.Y1)
                    {
                        isPaint = true;
                        Xline2 = (FigureList.X0 + FigureList.X1)/2;
                        Yline2 = (FigureList.Y0 + FigureList.Y1)/2;
                    }
                    else
                    {
                        isPaint = false;
                    }
                }
            }
            if (isPaint)
            {
                //xline2 = PositionEnd.X;
                //yline2 = PositionEnd.Y;
                CLine fLine = new CLine()
                {
                    Color = EColor.Green,
                    X0 = Xline1,
                    Y0 = Yline1,
                    X1 = Xline2,
                    Y1 = Yline2,
                    isMove = false
                };
                m_pCanvas.Add(fLine);
                Redraw();
            }
        }

        private void Draw_And_AddRectangleIsOrange(ref MouseEventArgs e)
        {
            int Xr2 = PositionStart.X;
            int Yr2 = PositionStart.Y;
            int Wr2 = e.X - Xr2;
            int Hr2 = e.Y - Yr2;

            if (e.X < e.X)
            {
                Xr2 = e.X;
                Wr2 = Wr2*(-1);
            }
            if (e.Y < e.Y)
            {
                Yr2 = e.Y;
                Hr2 = Hr2*(-1);
            }

            ZIndextoForm++;
            CRectangleIsOrange f2Rectangle = new CRectangleIsOrange()
            {
                Color = EColor.Orange,
                R2Height = Hr2,
                R2Width = Wr2,
                X0 = Xr2,
                Y0 = Yr2,
                X1 = e.X,
                Y1 = e.Y,
                isMove = false
            };
            m_pCanvas.Add(f2Rectangle);
            Redraw();
        }

        private void Draw_And_AddRectangleIsBlack(ref MouseEventArgs e)
        {
            int Xr = PositionStart.X;
            int Yr = PositionStart.Y;
            int Wr = e.X - Xr;
            int Hr = e.Y - Yr;

            if (e.X < PositionStart.X)
            {
                Xr = e.X;
                Wr = Wr*(-1);
            }
            if (e.Y < PositionStart.Y)
            {
                Yr = e.Y;
                Hr = Hr*(-1);
            }


            ZIndextoForm++;
            CRectangleIsBlack fRectangle = new CRectangleIsBlack()
            {
                Color = EColor.Black,
                RHeight = Hr,
                RWidth = Wr,
                X0 = Xr,
                Y0 = Yr,
                X1 = e.X,
                Y1 = e.Y,
                isMove = false,
                ZIndex = ZIndextoForm
            };
            m_pCanvas.Add(fRectangle);
            Redraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_pCanvas.FigureList.Clear();
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = bmp;
            comboBox2.Items.Clear();
        }
    }
}