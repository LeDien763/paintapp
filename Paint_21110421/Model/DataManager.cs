using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class DataManager
    {
        public static DataManager instance;
        
        //Danh sách các hình vẽ
        public List<Shape> shapeList { get; set; }
        //Hình được chọn
        public Shape shapeSelect { get; set; }
        
        //Tạo vùng được chọn
        public Rectangle rectangleRegion;

        //Các thao tác
        public bool isMouseDown { get; set; }
        public bool isMovingShape { get; set; }
        public bool isMovingMouse { get; set; }
        public bool isDrawingArc {get; set; }
        public bool isDrawingPolygon {get; set; }
        public bool isFill { get; set; }
        public bool isNotNone { get; set; }
        public bool isSelectAll { get; set; }
        public bool isWheelMouse { get; set; }
        public int pointToResize {get; set; }

        //Hình hiện tại
        public CurrentShapeStatus currentShape { get; set; }
        //Vị trí con trỏ chuột hiện tại
        public Point cursorCurrent { get; set; }
        //Màu sắc hiện tại
        public Color colorCurrent { get; set; }
        //Độ dày của nét hiện tại
        public int lineSize { get; set; }
        public DashStyle lineStyle { get; set; }
        private DataManager()
        {
            shapeList=new List<Shape>();
            pointToResize = -1;
        }
        public static DataManager getInstance()
        {
            if(instance == null) instance=new DataManager();
            return instance;
        }
        //Hàm cập nhật điểm cuối (pointTail)
        public void updatePointTail(Point p)
        {
            shapeList[shapeList.Count - 1].pointTail = p;
        }
        //Hàm thêm đối tượng vào danh sách
        public void addEntity(Shape shape)
        {
            shapeList.Add(shape);
        }
        //Hàm đặt trạng thái được chọn của tất cả các hình về false
        public void offAllShapeSelect()
        {
            shapeList.ForEach(shape=>shape.isSelect=false);
        }
        //Hàm tính khoảng cách
        public Point distanceXY(Point x, Point y)
        {
            return new Point(y.X - x.X, y.Y - x.Y);
        }
        //Hàm cập nhật hình chữ nhật bao quanh hình vẽ
        public void updateRectangleRegion(Point P)
        {
            rectangleRegion.Width=P.X-rectangleRegion.X;
            rectangleRegion.Height=P.Y-rectangleRegion.Y;
        }
    }
}
