﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DPolygon:Shape
    {
        public List<Point> points;
        public DPolygon()
        {
            name = "Polygon";
            points = new List<Point>();
        }
        public DPolygon(Color color, DashStyle style)
        {
            name = "Polygon";
            this.color = color;
            this.style = style; 
            points = new List<Point>();
        }
        public override object Clone()
        {
            DPolygon polygon = new DPolygon
            {
                pointHead = pointHead,
                pointTail = pointTail,
                isSelect = isSelect,
                name = name,
                color = color,
                contourWidth = contourWidth,
                isFill = isFill,
                style = style
            };
            points.ForEach(point => polygon.points.Add(point));
            return polygon;
        }
        public override void drawShape(Graphics g)
        {
            using(GraphicsPath path=graphicsPath)
            {
                if(!isFill)
                    using(Pen pen=new Pen(color,contourWidth))
                    {
                        pen.DashStyle = style;
                        g.DrawPath(pen, path);
                    }
                else
                {
                    using(Brush brush=new SolidBrush(color))
                    {
                        if(points.Count<3)
                        {
                            using (Pen pen = new Pen(color, contourWidth))
                            {
                                g.DrawPath(pen, path);
                            }
                        }
                        else
                        {
                            g.FillPath(brush, path);
                        }
                    }
                }
            }
        }
        public override bool isInclude(Point p)
        {
            bool inside = false;
            using(GraphicsPath path=graphicsPath)
            {
                if (!isFill)
                {
                    using(Pen pen=new Pen(color,contourWidth+3))
                    {
                        inside = path.IsOutlineVisible(p, pen);
                    }
                }
                else
                {
                    inside=path.IsVisible(p);
                }
            }
            return inside;
        }
        protected override GraphicsPath graphicsPath
        {
            get
            {
                GraphicsPath path=new GraphicsPath();
                if(points.Count<3)
                {
                    path.AddLine(points[0],points[1]);  
                }
                else
                {
                    path.AddPolygon(points.ToArray());
                }
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
            int deltaX= pointCurrent.X - pointPrevious.X;
            int deltaY= pointCurrent.Y - pointPrevious.Y; 
            points[index]=new Point(deltaX + points[index].X, deltaY + points[index].Y);
        }
        public override void AddToPath(GraphicsPath path)
        {
            path.AddPolygon(points.ToArray());
        }
    }
}
