using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DRectangle:Shape
    {
        public DRectangle()
        {
            name = "Rectangle";
        }
        public DRectangle(Color color,DashStyle style)
        {
            name = "Rectangle";
            this.color = color;
            this.style = style; 
        }
        public override object Clone()
        {
            return new DRectangle
            {
                pointHead = pointHead,
                pointTail = pointTail,
                contourWidth = contourWidth,
                isSelect = isSelect,
                color = color,
                name = name,
                style = style
            };
        }
        public override void drawShape(Graphics g)
        {
            using(GraphicsPath path=graphicsPath)
            {
                if(isFill)
                {
                    using(Brush b=new SolidBrush(color))
                    {
                        g.FillPath(b, path);
                    }
                }
                else
                {
                    using(Pen p=new Pen(color,contourWidth))
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
            using(GraphicsPath path=graphicsPath)
            {
                if(isFill)
                {
                    inside=path.IsVisible(p);
                }
                else
                {
                    using(Pen pen=new Pen(color, contourWidth+3))
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

                int startX = Math.Min(pointHead.X, pointTail.X);
                int endX = Math.Max(pointHead.X, pointTail.X);
                int startY = Math.Min(pointHead.Y, pointTail.Y);
                int endY = Math.Max(pointHead.Y, pointTail.Y);

                path.AddRectangle(new Rectangle(startX, startY, endX - startX, endY - startY));

                return path;
            }
        }
        public override void AddToPath(GraphicsPath path)
        {
            int startX = Math.Min(pointHead.X, pointTail.X);
            int endX = Math.Max(pointHead.X, pointTail.X);
            int startY = Math.Min(pointHead.Y, pointTail.Y);
            int endY = Math.Max(pointHead.Y, pointTail.Y);
            path.AddRectangle(new RectangleF (pointHead.X, pointHead.Y,pointTail.X-pointHead.X, pointTail.Y-pointHead.Y));
        }
    }
}
