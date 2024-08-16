using Paint_21110421.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421
{
    public class DLine:Shape
    {

        public DLine()
        {
            name = "Line";
        }
        public DLine(Color color,DashStyle style)
        {
            name = "Line";
            this.color = color;
            this.style = style;
        }

        public override object Clone()
        {
            return new DLine
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
            using (GraphicsPath path = graphicsPath)
            {
                using (Pen pen = new Pen(color, contourWidth))
                {
                    pen.DashStyle = style;
                    g.DrawPath(pen, path);
                }
            }
        }

        protected override GraphicsPath graphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddLine(pointHead, pointTail);
                return path;
            }
        }
        public override bool isInclude(Point p)
        {
            bool inside = false;
            using(GraphicsPath path=graphicsPath)
            {
                using(Pen pen = new Pen(color, contourWidth+3))
                {
                    inside = path.IsOutlineVisible(p,pen);
                }
            }
            return inside;
        }
        public override int isHitControlsPoint(System.Drawing.Point p)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(pointHead.X - 4, pointHead.Y - 4, 8, 8));
            if (path.IsVisible(p)) return 0;
            path.AddRectangle(new Rectangle(pointTail.X - 4, pointTail.Y - 4, 8, 8));
            if (path.IsVisible(p)) return 7;
            return -1;
        }
        public override void AddToPath(GraphicsPath path)
        {
            path.AddLine(pointHead, pointTail);
        }
    }
}
