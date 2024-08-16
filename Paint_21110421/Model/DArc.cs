using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DArc:Shape
    {
        public List<Point> points;
        public DArc()
        {
            name = "Curve";
            points = new List<Point>();
        }
        public DArc(Color color, DashStyle style)
        {
            name = "Curve";
            points = new List<Point>();
            this.color = color;
            this.style = style;
        }
        public override object Clone()
        {
            DArc Arc = new DArc
            {
                pointHead = pointHead,
                pointTail = pointTail,
                isSelect = isSelect,
                name = name,
                color = color,
                contourWidth = contourWidth,
                style=style

            };
            points.ForEach(point => Arc.points.Add(point));
            return Arc;
        }
        public override void drawShape(Graphics g)
        {   
            using (GraphicsPath path = graphicsPath)
            {
                using(Pen pen =new Pen(color, contourWidth))
                {
                    pen.DashStyle = style;
                    g.DrawPath(pen, path);
                }
            }

        }
        public override bool isInclude(Point p)
        {
            bool inside = false;
            using(GraphicsPath path=graphicsPath)
            {
                using (Pen pen = new Pen(color, contourWidth + 3))
                {
                    inside = path.IsOutlineVisible(p, pen);
                }
            }
            return inside;
        }
        protected override GraphicsPath graphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddCurve(points.ToArray());
                return path;
            }
        }
        public override void moveShape(Point distance)
        {
            base.moveShape(distance);
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Point(points[i].X + distance.X, points[i].Y + distance.Y);
            }
        }
        public override int isHitControlsPoint(Point p)
        {
            for(int i=0;i<points.Count;i++)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8));
                if (path.IsVisible(p)) return i;
            }
            return -1;
        }
        public override void moveControlPoint(Point pointCurrent, Point pointPrevious, int index)
        {
            int deltaX=pointCurrent.X - pointPrevious.X;
            int deltaY=pointCurrent.Y - pointPrevious.Y;
            points[index] = new Point(points[index].X + deltaX, points[index].Y + deltaY);
        }
        public override void AddToPath(GraphicsPath path)
        {
            path.AddCurve(points.ToArray());
        }
    }
}
