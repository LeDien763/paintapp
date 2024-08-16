using Paint_21110421.Model;
using Paint_21110421.Present;
using Paint_21110421.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_21110421
{
    public partial class Form1 : Form, ViewPaint
    {
        private Drawing presenterDraw;

        private Setup presenterSetup;

        private Update presenterUpdate;
        private Graphics gr;
        public Form1()
        {
            InitializeComponent();
            initComponents();
            gr = ptbDrawing.CreateGraphics();
            this.ptbDrawing.MouseWheel += PtbDrawing_MouseWheel; ;
        }

        private void PtbDrawing_MouseWheel(object sender, MouseEventArgs e)
        {
            presenterDraw.ClickMouseWheel(e, ptbDrawing);
        }
        private void initComponents()
        {
            presenterDraw = new Drawing(this);
            presenterSetup = new Setup(this);
            presenterUpdate = new Update(this);
            presenterUpdate.ClickSelectColor(ptbColor.BackColor, gr);
            presenterUpdate.ClickSelectSize(tBSize.Value + 1, gr);
        }
        

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawEllipse();
        }

        private void ptbDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            presenterDraw.ClickMouseDown(e.Location);
        }

        private void ptbDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            presenterDraw.ClickMouseMove(e.Location);
        }
        public void refreshDrawing()
        {
            ptbDrawing.Invalidate();
        }

        private void ptbDrawing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            presenterDraw.getDrawing(e.Graphics);
        }
        public void setDrawing(Shape shape, Graphics g)
        {
            shape.drawShape(g);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawLine();
        }

        private void ptbDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            presenterDraw.ClickMouseUp();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            presenterUpdate.ClickSelectMode();
        }
        public void setCursor(Cursor cursor)
        {
            ptbDrawing.Cursor = cursor;
        }
        public void setDrawingLineSelect(Shape shape, Brush brush, Graphics g)
        {
            g.FillRectangle(brush, new System.Drawing.Rectangle(shape.pointHead.X - 4, shape.pointHead.Y - 4, 8, 8));
            g.FillRectangle(brush, new System.Drawing.Rectangle(shape.pointTail.X - 4, shape.pointTail.Y - 4, 8, 8));
        }
        public void movingShape(Shape shape, Point distance)
        {
            shape.moveShape(distance);
            refreshDrawing();
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawRectangle();
        }
        public void setDrawingRegionRectangle(Pen p, Rectangle rectangle, Graphics g)
        {
            g.DrawRectangle(p, rectangle);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            presenterSetup.ClickDrawGroup();
        }

        private void btnUnGroup_Click(object sender, EventArgs e)
        {
            presenterSetup.ClickDrawUnGroup();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            presenterSetup.ClickDelete();
        }

        private void btnArc_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawArc();
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawPolygon();
        }
        public void setDrawingArcSelect(List<Point> points, Brush brush, Graphics g)
        {
            for (int i = 0; i < points.Count; ++i)
            {
                g.FillRectangle(brush, new System.Drawing.Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8));
            }

        }

        private void ptbEditColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                presenterUpdate.ClickSelectColor(colorDialog.Color, gr);
            }
        }
        public void setColor(Color color)
        {
            ptbColor.BackColor = color;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            presenterDraw.ClickDrawCircle();
        }

        private void cmbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DashStyle style = DashStyle.Solid;
            if (cmbStyle.SelectedIndex == 1) style = DashStyle.Dot;
            else if (cmbStyle.SelectedIndex == 2) style = DashStyle.Dash;
            else if (cmbStyle.SelectedIndex == 3) style = DashStyle.DashDot;
            else if (cmbStyle.SelectedIndex == 4) style = DashStyle.DashDotDot;
            presenterUpdate.ClickSelectStyle(style, gr);
        }
        public void wheelingShape(Shape shape, int distanceH, int distanceT)
        {
            shape.wheelShape(distanceH, distanceT);
            refreshDrawing();
        }

        private void ptbDrawing_MouseClick(object sender, MouseEventArgs e)
        {
            presenterDraw.ClickStopDrawing(e.Button);
        }

        private void tBSize_Scroll(object sender, EventArgs e)
        {
            presenterUpdate.ClickSelectSize(tBSize.Value + 1, gr);
        }
        public void movingControlPoint(Shape shape, Point pointCurrent, Point previous, int indexPoint)
        {
            shape.moveControlPoint(pointCurrent, previous, indexPoint);
            refreshDrawing();
        }
        public void setColor(Button btn, Color color)
        {
            btn.BackColor = color;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            presenterSetup.ClickClearAll(ptbDrawing);
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            presenterUpdate.ClickSelectFill(btn, gr);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            PictureBox ptb = sender as PictureBox;
            ptbColor.BackColor = ptb.BackColor;
            presenterUpdate.ClickSelectColor(ptb.BackColor, gr);
        }
    }
}
