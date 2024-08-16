using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DEllipse:DRectangle
    {
        public DEllipse()
        {
            name = "Ellipse";
        }
        public DEllipse(Color color, DashStyle style)
        {
            name = "Ellipse";
            this.color = color;
            this.style = style; 
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
                RectangleF rect = RectangleF.FromLTRB(startX, startY,endX,endY);
                path.AddEllipse(rect);
                return path;
            }
        }
        public override void AddToPath(GraphicsPath path)
        {
            int startX = Math.Min(pointHead.X, pointTail.X);
            int endX = Math.Max(pointHead.X, pointTail.X);
            int startY = Math.Min(pointHead.Y, pointTail.Y);
            int endY = Math.Max(pointHead.Y, pointTail.Y);
            path.AddEllipse(new RectangleF(startX, startY, endX- startX, endY-startY));
        }
    }
}
