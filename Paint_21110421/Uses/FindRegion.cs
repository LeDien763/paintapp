using Paint_21110421.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421
{
    public class FindRegion
    {
        //Hàm tìm điểm đầu điểm cuối cho Group
        public static void setPointHeadTail(Group group)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (Shape shape in group)
            {
                if (shape is DCircle)
                {
                    DCircle circle = (DCircle)shape;
                    int radius = (int)Math.Sqrt(Math.Pow(circle.pointTail.X - circle.pointHead.X, 2) + Math.Pow(circle.pointTail.Y - circle.pointHead.Y, 2)) / 2;
                    Point center = new Point((circle.pointHead.X + circle.pointTail.X) / 2 - radius, (circle.pointHead.Y + circle.pointTail.Y) / 2 - radius);
                    minX = Math.Min(minX, center.X);
                    minY = Math.Min(minY, center.Y);
                    maxX = Math.Max(maxX, center.X + radius * 2);
                    maxY = Math.Max(maxY, center.Y + radius * 2);
                }
                else
                {
                    minX = Math.Min(minX, shape.pointHead.X);
                    minY = Math.Min(minY, shape.pointHead.Y);
                    maxX = Math.Max(maxX, shape.pointTail.X);
                    maxY = Math.Max(maxY, shape.pointTail.Y);
                }
            }

            group.pointHead = new Point(minX, minY);
            group.pointTail = new Point(maxX, maxY);
        }
        //Hàm tìm điểm đầu điểm cuối cho Arc
        public static void setPointHeadTail(DArc arc)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;
            foreach (Point p in arc.points)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }
            arc.pointHead = new Point(minX, minY);
            arc.pointTail = new Point(maxX, maxY);
        }
        //Hàm tìm điểm đầu điểm cuối cho Polygon
        public static void setPointHeadTail(DPolygon polygon)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;
            foreach (Point p in polygon.points)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            };
            polygon.pointHead = new Point(minX, minY);
            polygon.pointTail = new Point(maxX, maxY);
        }
        //Hàm tìm tất cả các điểm có thể thao tác của một hình
        public static List<Point> getControlPoints(Shape shape)
        {
            if(shape is DCircle)
            {
                DCircle circle = (DCircle)shape;
                int radius = (int)Math.Sqrt(Math.Pow(circle.pointTail.X - circle.pointHead.X, 2) + Math.Pow(circle.pointTail.Y - circle.pointHead.Y, 2)) / 2;
                Point center = new Point((circle.pointHead.X + circle.pointTail.X) / 2 , (circle.pointHead.Y + circle.pointTail.Y) / 2 );

                return new List<Point>()
            {
                 new Point(center.X-radius, center.Y-radius),
                new Point(center.X, center.Y-radius),
                new Point(center.X+radius, center.Y-radius),
                new Point(center.X-radius, center.Y),
                new Point(center.X+radius, center.Y),
                new Point(center.X-radius, center.Y+radius),
                new Point(center.X, center.Y+radius),
                 new Point(center.X+radius, center.Y+radius),
                /*
                 * shape.pointHead,
                new Point(center.X, center.Y-radius),
                new Point(shape.pointTail.X, shape.pointHead.Y),
                new Point(center.X-radius, center.Y),
                new Point(center.X+radius, center.Y),
                new Point(shape.pointHead.X, shape.pointTail.Y),
                new Point(center.X, center.Y+radius),
                shape.pointTail
                 */
            };
            }
            int xCenter = (shape.pointHead.X + shape.pointTail.X) / 2;
            int yCenter = (shape.pointHead.Y + shape.pointTail.Y) / 2;
            return new List<Point>()
            {
                shape.pointHead,
                new Point(xCenter, shape.pointHead.Y),
                new Point(shape.pointTail.X, shape.pointHead.Y),
                new Point(shape.pointHead.X, yCenter),
                new Point(shape.pointTail.X, yCenter),
                new Point(shape.pointHead.X, shape.pointTail.Y),
                new Point(xCenter, shape.pointTail.Y),
                shape.pointTail
            };
        }
        //Hàm tìm điểm đầu và điểm cuối cho hình thuộc lớp GroupShape
    }
}
