
using Paint_21110421.Model;
using Paint_21110421.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Paint_21110421.Present
{
    class Drawing
    {
        ViewPaint viewPaint;

        DataManager dataManager;
        public Drawing(ViewPaint viewPaint)
        {
            this.viewPaint = viewPaint;
            dataManager = DataManager.getInstance();
        }
        //Nhấn để Chọn
        public void ClickToSelect(Point p)      
        {
            for (int i=0;i<dataManager.shapeList.Count;++i)
            {
                dataManager.pointToResize = dataManager.shapeList[i].isHitControlsPoint(p);
                if (dataManager.pointToResize!=-1)
                {
                    dataManager.shapeList[i].changePoint(dataManager.pointToResize);
                    dataManager.shapeSelect=dataManager.shapeList[i];
                    break;
                }
                else if(dataManager.shapeList[i].isInclude(p))
                {
                    dataManager.shapeSelect = dataManager.shapeList[i];
                    dataManager.shapeList[i].isSelect = true;
                    break;
                }   
            }
            if (dataManager.pointToResize != -1)
            {
                dataManager.cursorCurrent = p;
            }
            else if (dataManager.shapeSelect != null)
            {
                dataManager.isMovingShape = true;
                dataManager.cursorCurrent = p;
            }
            else
            {
                dataManager.isMovingMouse = true;
                dataManager.rectangleRegion = new Rectangle(p, new Size(0, 0));
            }
        }
        //Vẽ
        private void ClickToDraw(Point p)
        {
            dataManager.isMouseDown = true;
            dataManager.offAllShapeSelect();
            if (dataManager.currentShape.Equals(CurrentShapeStatus.Line))
            {
                dataManager.addEntity(new DLine
                {
                    pointHead = p,
                    pointTail = p,
                    contourWidth = dataManager.lineSize,
                    color = dataManager.colorCurrent,
                    style = dataManager.lineStyle,
                }) ;
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Arc))
            {
                if (!dataManager.isDrawingArc)
                {
                    dataManager.isDrawingArc = true;
                    DArc arc = new DArc
                    {
                        color = dataManager.colorCurrent,
                        contourWidth = dataManager.lineSize,
                        style = dataManager.lineStyle,
                    };
                    arc.points.Add(p);
                    arc.points.Add(p);
                    dataManager.shapeList.Add(arc);
                }
                else
                {
                    DArc arc = dataManager.shapeList[dataManager.shapeList.Count - 1] as DArc;
                    arc.points[arc.points.Count - 1] = p;
                    arc.points.Add(p);
                }
                dataManager.isMouseDown = false;
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Rectangle))
            {
                dataManager.addEntity(new DRectangle
                {
                    pointHead = p,
                    pointTail = p,
                    contourWidth = dataManager.lineSize,
                    color = dataManager.colorCurrent,
                    style = dataManager.lineStyle,
                    isFill = dataManager.isFill
                });
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Ellipse))
            {
                dataManager.addEntity(new DEllipse
                {
                    pointHead = p,
                    pointTail = p,
                    contourWidth = dataManager.lineSize,
                    color = dataManager.colorCurrent,
                    style= dataManager.lineStyle,
                    isFill = dataManager.isFill
                });
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Circle))
            {
                dataManager.addEntity(new DCircle
                {
                    pointHead = p,
                    pointTail = p,
                    contourWidth = dataManager.lineSize,
                    color = dataManager.colorCurrent,
                    style = dataManager.lineStyle,
                    isFill= dataManager.isFill
                });
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Polygon))
            {
                if (!dataManager.isDrawingPolygon)
                {
                    dataManager.isDrawingPolygon = true;
                    DPolygon polygon = new DPolygon
                    {
                        color = dataManager.colorCurrent,
                        contourWidth = dataManager.lineSize,
                        isFill = dataManager.isFill,
                        style = dataManager.lineStyle 
                    };
                    polygon.points.Add(p);
                    polygon.points.Add(p);
                    dataManager.shapeList.Add(polygon);
                }
                else
                {
                    DPolygon polygon = dataManager.shapeList[dataManager.shapeList.Count - 1] as DPolygon;
                    polygon.points[polygon.points.Count - 1] = p;
                    polygon.points.Add(p);
                }
                dataManager.isMouseDown = false;
            }
        }

        //Nhấn chuột
        public void ClickMouseDown(Point p)
        {
            dataManager.isNotNone = true;
            if (dataManager.currentShape.Equals(CurrentShapeStatus.Void))
            {
                if (!(Control.ModifierKeys == Keys.Control))
                    dataManager.offAllShapeSelect();
                viewPaint.refreshDrawing();
                ClickToSelect(p);
            }
            else
            {
                ClickToDraw(p);
            }
        }
        //Di chuyển chuột
        public void ClickMouseMove(Point p)
        {
            if(dataManager.isMouseDown)
            {
                dataManager.updatePointTail(p);
                viewPaint.refreshDrawing();
            }
            else if(dataManager.pointToResize!=-1)
            {
                if(!(dataManager.shapeSelect is Group))
                {
                    viewPaint.movingControlPoint(dataManager.shapeSelect, p, dataManager.cursorCurrent, dataManager.pointToResize);
                    dataManager.cursorCurrent = p;
                }
            }
            else if (dataManager.isMovingShape)
            {   
                viewPaint.movingShape(dataManager.shapeSelect, dataManager.distanceXY(dataManager.cursorCurrent, p));
                dataManager.cursorCurrent = p;
            }
            else if (dataManager.currentShape.Equals(CurrentShapeStatus.Void))
            {
                if (dataManager.isMovingMouse)
                {
                    dataManager.updateRectangleRegion(p);
                    viewPaint.refreshDrawing();
                }
                else
                {

                    
                    if (dataManager.shapeList.Exists(shape => isInside(shape, p)))
                    {
                        viewPaint.setCursor(Cursors.SizeAll);
                    }
                    else
                    {
                        viewPaint.setCursor(Cursors.Default);
                    }

                }
            }
            if (dataManager.isDrawingArc)
            {
                DArc arc = dataManager.shapeList[dataManager.shapeList.Count - 1] as DArc;
                arc.points[arc.points.Count - 1] = p;
                viewPaint.refreshDrawing();
            }
            else if (dataManager.isDrawingPolygon)
            {
                DPolygon polygon = dataManager.shapeList[dataManager.shapeList.Count - 1] as DPolygon;
                polygon.points[polygon.points.Count - 1] = p;
                viewPaint.refreshDrawing();
            }

        }
        private bool isInside(Shape shape, Point p)
        {
            return shape.isInclude(p);
        }
        //Thả chuột
        public void ClickMouseUp()
        {
            dataManager.isMouseDown = false;
            if (dataManager.pointToResize != -1)
            {
                dataManager.pointToResize = -1;
                dataManager.shapeSelect = null;
            }
            else if (dataManager.isMovingShape)
            {
                dataManager.isMovingShape = false;
                dataManager.shapeSelect = null;
            }
            else if (dataManager.isMovingMouse)
            {
                dataManager.isMovingMouse = false;
                dataManager.offAllShapeSelect();

                // kiểm tra khi kéo chuột chọn một vùng thì có hình nào tồn tại bên trong hay là không, nếu có thì hình đó được chọn
                for (int i = 0; i < dataManager.shapeList.Count; ++i)
                {
                    if (dataManager.shapeList[i].isInRegion(dataManager.rectangleRegion))
                    {
                        dataManager.shapeList[i].isSelect = true;
                    }

                }
                viewPaint.refreshDrawing();
            }
        }
        public void ClickMouseWheel(MouseEventArgs e, PictureBox ptbDrawing)
        {
            Point point = e.Location;
            Point endPoint = new Point(ptbDrawing.Location.X + ptbDrawing.Size.Width, ptbDrawing.Location.Y + ptbDrawing.Size.Height);

            dataManager.isWheelMouse = true;
          
            if (dataManager.isWheelMouse)
            {
                int delta = Math.Sign(e.Delta) * 10;
                int newHeight = ptbDrawing.Height + delta;
                int newWidth = ptbDrawing.Width + delta;

                // Kiểm tra giới hạn độ thu nhỏ
                //2000, 1000
                if (newHeight >= 800 && newHeight <= 1000
                    && newWidth >= 1000 && newWidth <= 2100)
                {

                    ptbDrawing.Height = newHeight;
                    ptbDrawing.Width = newWidth;
                    
                    foreach (var shape in dataManager.shapeList)
                    {
                        viewPaint.wheelingShape(shape, delta, delta);
                    }
                    viewPaint.refreshDrawing();
                }

                dataManager.isWheelMouse = false;
            }
        }
        public void getDrawing(Graphics g)
        {
            dataManager.shapeList.ForEach(shape =>
            {
                viewPaint.setDrawing(shape, g);
                if (shape.isSelect)
                {
                    drawRegionForShape(shape, g);
                }

            });
            if (dataManager.isMovingMouse)
            {
                using (Pen pen = new Pen(Color.DarkBlue, 1)
                {
                    DashPattern = new float[] { 3, 3, 3, 3 },
                    DashStyle = DashStyle.Custom
                })
                {
                    viewPaint.setDrawingRegionRectangle(pen, dataManager.rectangleRegion, g);
                }

            }
            if (dataManager.pointToResize != -1)
            {
                if (dataManager.shapeSelect is Group) return;
                drawRegionForShape(dataManager.shapeSelect, g);
            }
        }
        private void drawRegionForShape(Shape shape, Graphics g)
        {
            if (shape is DLine)
            {
                viewPaint.setDrawingLineSelect(shape, new SolidBrush(Color.DarkBlue), g);

            }
            else if (shape is DArc)
            {
                DArc arc = (DArc)shape;
                int minX = int.MaxValue;
                int minY = int.MaxValue;
                int maxX = int.MinValue;
                int maxY = int.MinValue;
                foreach (Point point in arc.points)
                {
                    if (point.X < minX)
                        minX = point.X;
                    if (point.X > maxX)
                        maxX = point.X;
                    if (point.Y < minY)
                        minY = point.Y;
                    if (point.Y > maxY)
                        maxY = point.Y;
                }
                Rectangle boundingRect = new Rectangle(minX, minY, maxX - minX+(arc.contourWidth/2), maxY - minY+(arc.contourWidth / 2));
                viewPaint.setDrawingArcSelect(arc.points, new SolidBrush(Color.DarkBlue), g);
                using (Pen pen = new Pen(Color.DarkBlue, 1)
                {
                    DashPattern = new float[] { 3, 3, 3, 3 },
                    DashStyle = DashStyle.Custom
                })
                {
                    g.DrawRectangle(pen, boundingRect);
                }
            }
            else if (shape is DPolygon)
            {
                DPolygon polygon = (DPolygon)shape;
                Rectangle boundingRect = new Rectangle();
                int maxX = int.MinValue;
                int maxY = int.MinValue;
                int minX = int.MaxValue;
                int minY = int.MaxValue;
                for (int i = 0; i < polygon.points.Count; i++)
                {
                    if (polygon.points[i].X > maxX) maxX = polygon.points[i].X;
                    if (polygon.points[i].Y > maxY) maxY = polygon.points[i].Y;
                    if (polygon.points[i].X < minX) minX = polygon.points[i].X;
                    if (polygon.points[i].Y < minY) minY = polygon.points[i].Y;
                    
                    viewPaint.setDrawingArcSelect(polygon.points, new SolidBrush(Color.DarkBlue), g);
                }
                boundingRect.X = minX;
                boundingRect.Y = minY;
                boundingRect.Width = maxX - minX + (polygon.contourWidth / 2);
                boundingRect.Height = maxY - minY + (polygon.contourWidth / 2);
                using (Pen pen = new Pen(Color.DarkBlue, 1)
                {
                    DashPattern = new float[] { 3, 3, 3, 3 },
                    DashStyle = DashStyle.Custom
                })
                {
                    viewPaint.setDrawingRegionRectangle(pen, boundingRect, g);
                }
            }
            else if (shape is DCircle)
            {
                DCircle circle = (DCircle)shape;
                int radius = (int)Math.Sqrt(Math.Pow(circle.pointTail.X - circle.pointHead.X, 2) + Math.Pow(circle.pointTail.Y - circle.pointHead.Y, 2)) / 2;
                Point center = new Point((circle.pointHead.X + circle.pointTail.X) / 2 - radius, (circle.pointHead.Y + circle.pointTail.Y) / 2 - radius);
                int x = center.X ;
                int y = center.Y ;
                int width = radius * 2;
                int height = radius * 2;

                Rectangle boundingRect = new Rectangle(x, y, width, height);
                using (Pen pen = new Pen(Color.DarkBlue, 2)
                {
                    DashPattern = new float[] { 3, 3, 3, 3 },
                    DashStyle = DashStyle.Custom
                })
                {
                    viewPaint.setDrawingRegionRectangle(pen, boundingRect, g);
                    viewPaint.setDrawingArcSelect(FindRegion.getControlPoints(shape), new SolidBrush(Color.DarkBlue), g);
                }
            }
            else
            {
                using (Pen pen = new Pen(Color.DarkBlue, 1)
                {
                    DashPattern = new float[] { 3, 3, 3, 3 },
                    DashStyle = DashStyle.Custom
                })
                {
                    viewPaint.setDrawingRegionRectangle(pen, shape.getRectangle(shape.pointHead, shape.pointTail), g);
                    viewPaint.setDrawingArcSelect(FindRegion.getControlPoints(shape),
                        new SolidBrush(Color.DarkBlue), g);
                }
            }
        }
        private void setDefaultToDraw()
        {
            dataManager.offAllShapeSelect();
            viewPaint.refreshDrawing();
            viewPaint.setCursor(Cursors.Default);
        }
        public void ClickDrawLine()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Line;
        }
        public void ClickDrawRectangle()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Rectangle;
        }
        public void ClickDrawEllipse()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Ellipse;
        }
        public void ClickDrawCircle()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Circle;
        }
        public void ClickDrawArc()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Arc;
        }
        public void ClickDrawPolygon()
        {
            setDefaultToDraw();
            dataManager.currentShape = CurrentShapeStatus.Polygon;
        }
        //Dừng vẽ đường cong và polygon bằng chuột phải
        public void ClickStopDrawing(MouseButtons mouse)
        {
            if (mouse == MouseButtons.Right)
            {
                if (dataManager.currentShape.Equals(CurrentShapeStatus.Polygon))
                {
                    DPolygon polygon = dataManager.shapeList[dataManager.shapeList.Count - 1] as DPolygon;
                    polygon.points.Remove(polygon.points[polygon.points.Count - 1]);
                    dataManager.isDrawingPolygon = false;
                    FindRegion.setPointHeadTail(polygon);
                }
                else if (dataManager.currentShape.Equals(CurrentShapeStatus.Arc))
                {
                    DArc curve = dataManager.shapeList[dataManager.shapeList.Count - 1] as DArc;
                    curve.points.Remove(curve.points[curve.points.Count - 1]);
                    dataManager.isDrawingArc = false;
                    FindRegion.setPointHeadTail(curve);
                }
            }
        }
        
    }
}
