using System;
using System.Drawing;

namespace ImageEditor
{
    class Cyan : Effect
    {
        public int hue;
        public int saturation;
        public int luminescence;
        public Cyan(int h, int s, int l)
        {
            hue = h;
            saturation = s;
            luminescence = l;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsl;
            double n, d;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsl = new HSV(myImage.GetPixel(i, j));
                    d = Math.Floor(Math.Abs(hsl.H - 180));
                    if (d > 180) d = 360 - d;
                    if (d == 0) d = 1;
                    if (d <= 60)
                    {
                        n = hsl.H;
                        n += (double)hue * 1 / Math.Sqrt(d);
                        hsl.H = (360 + n) % 360;
                        n = 255 * hsl.S;
                        if (saturation > 0) n += saturation / Math.Sqrt(d);
                        else n += saturation * n / 255;
                        if (n > 255) n = 255;
                        if (n < 0) n = 0;
                        hsl.S = n / 255;
                        n = hsl.V * 255;
                        n += luminescence * (255 - n) / 255 / Math.Sqrt(d) * hsl.S * 2;
                        if (n > 255) n = 255;
                        if (n < 0) n = 0;
                        hsl.V = n / 255;
                    }
                    edit.SetPixel(i, j, hsl.ToColor());
                }
            }
            return edit;
        }
    }
}
