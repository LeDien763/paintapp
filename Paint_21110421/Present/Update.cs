using Paint_21110421.Model;
using Paint_21110421.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_21110421.Present
{
    class Update
    {
        ViewPaint viewPaint;

        DataManager dataManager;

        public Update(ViewPaint viewPaint)
        {
            this.viewPaint = viewPaint;
            dataManager = DataManager.getInstance();
        }

        public void ClickSelectMode()
        {
            dataManager.offAllShapeSelect();
            viewPaint.refreshDrawing();
            dataManager.currentShape = CurrentShapeStatus.Void;
            viewPaint.setCursor(Cursors.Default);
        }

        public void ClickSelectColor(System.Drawing.Color color, Graphics g)
        {
            dataManager.colorCurrent = color;
            viewPaint.setColor(color);
            foreach (Shape item in dataManager.shapeList)
            {
                if (item.isSelect)
                {
                    item.color = color;
                    viewPaint.setDrawing(item, g);
                }
            }
        }

        public void ClickSelectSize(int size, Graphics g)
        {
            dataManager.lineSize = size;
            foreach (Shape item in dataManager.shapeList)
            {
                if (item.isSelect)
                {
                    item.contourWidth = size;
                    viewPaint.setDrawing(item, g);
                    viewPaint.refreshDrawing();
                }
            }
        }

        public void ClickSelectFill(Button btn, Graphics g)
        {
            dataManager.isFill = !dataManager.isFill;
            if (btn.BackColor.Equals(Color.LightCyan))
                viewPaint.setColor(btn, SystemColors.Control);
            else
                viewPaint.setColor(btn, Color.LightCyan);
        }
        public void ClickSelectStyle(DashStyle style, Graphics g)
        {
            dataManager.lineStyle = style;
            foreach (Shape item in dataManager.shapeList)
            {
                if (item.isSelect)
                {
                    item.style = style;
                    viewPaint.setDrawing(item, g);
                    viewPaint.refreshDrawing();
                }
            }
        }
    }
}
