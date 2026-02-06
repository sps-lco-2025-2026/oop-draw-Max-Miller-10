using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OOPDraw
{
    public class CompositeShape : Shape
    {
        private List<Shape> Components { get; set; }
        public CompositeShape(List<Shape> components) : base(new Pen(Color.Black),0,0,0,0)
        {
            Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Components = components;
            CalculateEnclosingRectangle();
        }

        private void CalculateEnclosingRectangle()
        {
            X1 = Components.Min(m => Math.Min(m.X1, m.X2));
            Y1 = Components.Min(m => Math.Min(m.Y1, m.Y2));
            X2 = Components.Max(m => Math.Max(m.X1, m.X2));
            Y2 = Components.Max(m => Math.Max(m.Y1, m.Y2));


        }
        public override void Draw(Graphics g)
        {
            foreach (Shape m in Components)
            {
                m.Draw(g);
            }
            if (Selected) g.DrawRectangle(Pen, X1, Y1, X2 - X1, Y2 - Y1);
        }

        public override void MoveBy(int xDelta, int yDelta)
        {
            foreach (Shape m in Components)
            {
                m.MoveBy(xDelta, yDelta);
            }
            CalculateEnclosingRectangle();
        }

        public override Shape Clone()
        {
            List<Shape> clonesList = new List<Shape>();

            foreach (Shape m in Components)
            {
                clonesList.Add(m.Clone());
            }
            return new CompositeShape(clonesList);
        }
    }
}
