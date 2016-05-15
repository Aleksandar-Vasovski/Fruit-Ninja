using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Fruit_Ninja
{
    class CustomToolTip : ToolTip
    {
        private Size size;

        public CustomToolTip(Size size)
        {
            this.size = size;
            OwnerDraw = true;
            Popup += new PopupEventHandler(OnPopup);
            Draw += new DrawToolTipEventHandler(OnDraw);
        }

        private void OnPopup(object sender, PopupEventArgs e) 
        {
            e.ToolTipSize = size;
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) 
        {
            Graphics g = e.Graphics;

            LinearGradientBrush b = new LinearGradientBrush(e.Bounds,
                Color.LightBlue, Color.Blue, 45f);

            g.FillRectangle(b, e.Bounds);

            g.DrawString(e.ToolTipText, new Font("Elephant",24), Brushes.Black,
                new PointF(e.Bounds.X + 5, e.Bounds.Y + 5)); 

            b.Dispose();
        }
    }
}
