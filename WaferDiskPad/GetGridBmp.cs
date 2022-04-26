using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaferDiskPad
{
    public class GetGridBmp
    {
        public Bitmap GetGridImage( int x, int y, int w, int h, Color color, int intel_Bin)
        {
                using (Brush brush = new SolidBrush(color))
                {
                    Bitmap destBitmap = new Bitmap(w,h);
                    Graphics g = Graphics.FromImage(destBitmap);
                    //g.DrawImage(bt, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);
                    //g.FillEllipse(new SolidBrush(color), new Rectangle(x, y, w, h));
                    //g.DrawEllipse(new Pen(Color.BlueViolet, (float)(2)), new Rectangle(x - intel_Bin, y - intel_Bin, w + intel_Bin * 2, h + intel_Bin * 2));
                    //g.DrawEllipse(new Pen(Color.Red, (float)(2)), new Rectangle(x, y, w, h));
                    g.FillRectangle(brush, new Rectangle(x+intel_Bin, y+intel_Bin , w-intel_Bin, h-intel_Bin));

                    //bt.Dispose();
                    g.Dispose();
                    //destBitmap.Save("@\\ikj.bmp");
                    return destBitmap;

                }
        }

    }
}
