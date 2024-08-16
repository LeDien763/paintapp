using Paint_21110421.Model;
using Paint_21110421.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_21110421.Present
{
     class Setup
    {
        ViewPaint viewPaint;
        DataManager dataManager;
        public Setup(ViewPaint viewPaint)
        {
            this.viewPaint = viewPaint;
            dataManager = DataManager.getInstance();
        }
        //Các hàm thực thi khi nhấn các nút tương ứng
        //Nút Group
        public void ClickDrawGroup()
        {
            if(dataManager.shapeList.Count(shape=>shape.isSelect)>1)
            {
                Group group = new Group();
                for(int i=0;i<dataManager.shapeList.Count; i++)

                {
                    if (dataManager.shapeList[i].isSelect)
                    {
                        group.addShape(dataManager.shapeList[i]);
                        dataManager.shapeList.RemoveAt(i--);
                    }
                }
                FindRegion.setPointHeadTail(group);
                group.isSelect = true;
                dataManager.shapeList.Add(group);
                viewPaint.refreshDrawing();
            }
        }
        //Nút UnGroup
        public void ClickDrawUnGroup()
        {
            if(dataManager.shapeList.Find(shape=>shape.isSelect) is Group)
            {
                Group group=(Group)dataManager.shapeList.Find(shape=>shape.isSelect);
                foreach(Shape shape in group)
                {
                    dataManager.shapeList.Add(shape);
                }
                dataManager.shapeList.Remove(group);
            }
            viewPaint.refreshDrawing();
        }
        //Nút xóa một hình
        public void ClickDelete()
        {
            for(int i=0;i<dataManager.shapeList.Count;++i)
            {
                if (dataManager.shapeList[i].isSelect)
                        dataManager.shapeList.RemoveAt(i--);
            }
            viewPaint.refreshDrawing();
        }
        //Nút xóa toàn bộ màn hình
        public void ClickClearAll(PictureBox picturebox)
        {
            picturebox.Image = null;
            dataManager.shapeList.Clear();
            dataManager.isNotNone = false;
            viewPaint.refreshDrawing();
        }
    }
}
