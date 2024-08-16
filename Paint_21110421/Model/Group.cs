using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Paint_21110421.Model
{
    public class Group : Shape, IEnumerable
    {
        private List<Shape> shapes;
        public Group()
        {
            name = "Group";
            shapes= new List<Shape>();
            
        }
        public Shape this[int index]
        {
            get
            {
                return shapes[index];
            }
            set
            {
                shapes[index] = value;
            }
        }
        public override void AddToPath(GraphicsPath path)
        {
            foreach (var shape in shapes)
            {
                if (shape is Group group)
                {
                    group.AddToPath(path);
                }
                else
                {
                    shape.AddToPath(path);
                }
            }
        }
        private GraphicsPath[] graphicsPaths
        {
            get
            {
                GraphicsPath[] paths = new GraphicsPath[shapes.Count];

                for (int i = 0; i < shapes.Count; i++)
                {
                    GraphicsPath path = new GraphicsPath();
                    shapes[i].AddToPath(path);
                    paths[i] = path;
                }

                return paths;
            }
        }
        public void addShape(Shape shape)
        {
            shapes.Add(shape);
        }
        public override object Clone()
        {
            Group group = new Group
            {
                pointHead = pointHead,
                pointTail = pointTail,
                isSelect = isSelect,
                name = name,
                color = color,
                contourWidth = contourWidth,
                style=style
            };
            for (int i = 0; i < shapes.Count; i++)
            {
                group.shapes.Add(shapes[i].Clone() as Shape);
            }
            return group;
        }
        //public Color GroupColor { get; set; }
        public override void drawShape(Graphics g)
        {
            GraphicsPath[] paths = graphicsPaths;
            for (int i = 0; i < paths.Length; i++)
            {
                using (GraphicsPath path = paths[i])
                {
                    if (shapes[i] is DRectangle || shapes[i] is DEllipse || shapes[i] is DPolygon|| shapes[i]is DCircle)
                    {
                        if (shapes[i].isFill)
                        {
                            using (Brush brush = new SolidBrush(shapes[i].color))
                            {
                                g.FillPath(brush, path);
                            }
                        }
                        else
                        {
                            using (Pen pen = new Pen(shapes[i].color, shapes[i].contourWidth))
                            {
                                g.DrawPath(pen, path);
                            }
                        }
                    }
                    else if (shapes[i] is Group)
                    {
                        Group group = (Group)shapes[i];
                        group.drawShape(g);
                    }
                    else
                    {
                        using (Pen pen = new Pen(shapes[i].color, shapes[i].contourWidth))
                        {
                            g.DrawPath(pen, path);
                        }
                    }
                }
            }
        }
        public override bool isInclude(Point p)
        {
            GraphicsPath[] paths = graphicsPaths;
            for (int i = 0; i < paths.Length; i++)
            {
                using (GraphicsPath path = paths[i])
                {
                    if (shapes[i] is DRectangle || shapes[i] is DEllipse || shapes[i] is DCircle || shapes[i] is DPolygon || shapes[i] is DArc)
                    {
                        if (shapes[i].isFill == false)
                        {
                            using (Pen pen = new Pen(Color.Black, contourWidth + 3))
                            {
                                if (path.IsOutlineVisible(p, pen))
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            using (Brush brush = new SolidBrush(Color.Black))
                            {
                                if (path.IsVisible(p))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else if (!(shapes[i] is Group))
                    {
                        using (Pen pen = new Pen(Color.Black, contourWidth + 3))
                        {
                            if (path.IsVisible(p))
                            {
                                return true;
                            }
                        }
                    }
                    else if (shapes[i] is Group)
                    {
                        Group group = (Group)shapes[i];
                        return group.isInclude(p);
                    }
                }
            }
            return false;
        }
        protected override System.Drawing.Drawing2D.GraphicsPath graphicsPath
        {
            get { throw new NotImplementedException(); }
        }
        public IEnumerator GetEnumerator()
        {
            return shapes.GetEnumerator();
        }
        public int Count
        {
            get
            {
                return shapes.Count;
            }
        }
        public override void moveShape(Point distance)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is Group)
                {
                    Group group = (Group)shapes[i];
                    group.moveShape(distance);
                }
                else
                {
                    shapes[i].moveShape(distance);
                }
            }
            base.moveShape(distance);
        }

        


    }
}
