using Paint_21110421.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_21110421.View
{
    interface ViewPaint
    {
        //Hàm vẽ lại
        void refreshDrawing();
        //Hàm đặt hình dạng con trỏ chuột
        void setCursor(Cursor cursor);
        //Hàm đặt màu sắc cho background
        void setColor(Color color);
        //Hàm vẽ hình lên graphics
        void setDrawing(Shape shape, Graphics g);
        //Hàm đặt màu các nút
        void setColor(Button button,Color color);
        //Hàm vẽ điểm điều khiển đường thẳng
        void setDrawingLineSelect(Shape shape,Brush brush,Graphics g);
        //Hàm vẽ điểm điều khiển cho cung tròn
        void setDrawingArcSelect(List<Point>points,Brush brush,Graphics g);
        //Hàm vẽ hình chữ nhật bao quanh các hình
        void setDrawingRegionRectangle(Pen p, Rectangle rectangle, Graphics g);
        //Hàm di chuyển một hình
        void movingShape(Shape shape,Point distance);
        //Hàm thay đổi kích thước hình khi di chuyển các điểm
        void movingControlPoint(Shape shape,Point pointCurrent,Point pointPrevious, int indexPoint);
        //Hàm thay đổi kích thước khi cuộn chuột
        void wheelingShape(Shape shape, int distanceH, int distanceT);
    }
}
