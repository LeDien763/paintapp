using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DCircle:Shape
    {
        public DCircle()
        {
            name = "Circle";
        }
        public DCircle(Color color,DashStyle style)
        {
            name = "Circle";
            this.color = color;
            this.style = style;
        }
        public override object Clone()
        {
            return new DCircle
            {
                pointHead = pointHead,
                pointTail = pointTail,
                contourWidth = contourWidth,
                isSelect = isSelect,
                isFill = isFill,
                color = color,
                style = style,
                name = name
            };
        }
        public override void drawShape(Graphics g)
        {
            using (GraphicsPath path = graphicsPath)
            {
                if (isFill)
                {
                    using (Brush b = new SolidBrush(color))
                    {
                        g.FillPath(b, path);
                    }
                }
                else
                {
                    using (Pen p = new Pen(color, contourWidth))
                    {
                        p.DashStyle = style;
                        g.DrawPath(p, path);
                    }
                }
            }
        }
        public override bool isInclude(Point p)
        {
            bool inside = false;
            using (GraphicsPath path = graphicsPath)
            {
                if (isFill)
                {
                    inside = path.IsVisible(p);
                }
                else
                {
                    using (Pen pen = new Pen(color, contourWidth + 3))
                    {
                        inside = path.IsOutlineVisible(p, pen);
                    }
                }
            }
            return inside;
        }
        protected override GraphicsPath graphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();

                    int radius = (int)Math.Sqrt(Math.Pow(pointTail.X - pointHead.X, 2) + Math.Pow(pointTail.Y - pointHead.Y, 2)) / 2;
                    Point center = new Point((pointHead.X + pointTail.X) / 2 - radius, (pointHead.Y + pointTail.Y) / 2 - radius);

                Rectangle rect = new Rectangle(center.X, center.Y, radius * 2, radius * 2);

                path.AddEllipse(rect);
                return path;
            }
        }
        public override void AddToPath(GraphicsPath path)
        {
            int radius = (int)Math.Sqrt(Math.Pow(pointTail.X - pointHead.X, 2)
                            + Math.Pow(pointTail.Y - pointHead.Y, 2)) / 2;
            Point center = new Point((pointHead.X + pointTail.X) / 2
                            - radius, (pointHead.Y + pointTail.Y) / 2 - radius);
            Rectangle rect = new Rectangle(center.X, center.Y, radius * 2, radius * 2);
            path.AddEllipse(rect);
        }
    }
}
