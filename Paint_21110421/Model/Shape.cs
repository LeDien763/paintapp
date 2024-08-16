using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public abstract class Shape : ICloneable
    {
        public abstract object Clone();
        public string name { get; set; }   
        public Point pointHead { get; set; }   
        public Point pointTail { get; set; }
        public bool isSelect { get; set; }
        public bool isFill{ get; set; }
        public int contourWidth { get; set; }//Độ dày
        public Color color { get; set; }
        public DashStyle style { get; set; }
        // Hàm vẽ hình 
        public abstract void drawShape(Graphics g);
        //Hàm di chuyển hình
        public virtual void moveShape(Point distance)
        {
            pointHead = new Point(pointHead.X +distance.X, pointHead.Y+distance.Y);
            pointTail= new Point(pointTail.X +distance.X, pointTail.Y+distance.Y);
        }
        //Cuộn chuột
        public virtual void wheelShape(int distanceH, int distanceT)
        {
            pointHead = new Point(pointHead.X - distanceH, pointHead.Y - distanceH);
            pointTail = new Point(pointTail.X + distanceT, pointTail.Y + distanceT);
        }
        //Hàm kiểm tra xem điểm được chọn có nằm trong hình hay không
        public abstract bool isInclude(Point p);

        //Hàm tạo ra đối tượng graphicsPath của hình
        protected abstract GraphicsPath graphicsPath { get; }
        //Hàm cho biết hình có được bao bọc bởi một hình chữ nhật hay không
        public virtual bool isInRegion(Rectangle rectangle)
        {
            return pointHead.X>=rectangle.X&&
                pointTail.X<=rectangle.X+rectangle.Width&&
                pointHead.Y>=rectangle.Y&&
                pointTail.Y<=rectangle.Y+rectangle.Height;
        }
        //Hàm tạo ra một hình chữ nhật bao quanh hình 
        public Rectangle getRectangle()
        {
            return new Rectangle(pointHead.X,pointHead.Y,pointTail.X-pointHead.X,pointTail.Y-pointHead.Y);
        }
        //Hàm vẽ hình chữ nhật từ 2 điểm a và b
        public Rectangle getRectangle(Point a,Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);
            return new Rectangle(x, y, width, height);
        }
        //Hàm cho biết điểm p có phải là điểm để thao tác hay không
        public virtual int isHitControlsPoint(Point p)
        {
            List<Point> points = FindRegion.getControlPoints(this);
            for(int i = 0; i < 8; i++)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new Rectangle(points[i].X - 4, points[i].Y-4,8,8));
                if(path.IsVisible(p)) return i;
            }
            return -1;
        }
        //Hàm thay đổi điểm đầu và điểm cuối theo điểm thao tác 
        //0: Điểm góc trái trên.
        //1: Điểm giữa cạnh trên.
        //2: Điểm góc phải trên.
        //3: Điểm giữa cạnh trái.
        //4: Điểm giữa cạnh phải.
        //5: Điểm góc trái dưới.
        //6: Điểm giữa cạnh dưới.
        //7: Điểm góc phải dưới.
        public virtual void changePoint(int index)
        {
            if (index == 0 || index == 1 || index == 3)
            {
                Point point = pointHead;
                pointHead = pointTail;
                pointTail = point;
            }
            if (index == 2)
            {
                int a = pointTail.X;
                int b = pointHead.Y;
                pointHead = new Point(pointHead.X, pointTail.Y);
                pointTail = new Point(a, b);
            }
            if (index == 5)
            {
                int a = pointHead.X;
                int b = pointTail.Y;
                pointHead = new Point(pointTail.X, pointHead.Y);
                pointTail = new Point(a, b);
            }
        }
        //Hàm thay đổi kích thước 
        public virtual void moveControlPoint(Point pointCurrent,Point pointPrevious, int index)
        {
            int deltaX = pointCurrent.X - pointPrevious.X;
            int deltaY = pointCurrent.Y - pointPrevious.Y;

            bool isEndPoint = (index == 1 || index == 6 || index == 3 || index == 4);

            if (isEndPoint)
            {
                if (index == 1 || index == 6)
                {
                    pointTail = new Point(pointTail.X, pointTail.Y + deltaY);
                }
                else if (index == 3 || index == 4)
                {
                    pointTail = new Point(pointTail.X + deltaX, pointTail.Y);
                }
            }
            else
            {
                pointTail = pointCurrent;
            }
        }
        public abstract void AddToPath(GraphicsPath path);
    }
}
