using System;
using System.Drawing;

namespace ImageEditor
{
    class Gamma : Effect
    {
        public double pow;
        public Gamma(double p)
        {
            pow = p;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int r, g, b;
            double rr, gg, bb;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = myImage.GetPixel(i, j).R;
                    g = myImage.GetPixel(i, j).G;
                    b = myImage.GetPixel(i, j).B;
                    rr = (double)r / 255;
                    gg = (double)g / 255;
                    bb = (double)b / 255;
                    rr = Math.Pow(rr, 1 / pow);
                    gg = Math.Pow(gg, 1 / pow);
                    bb = Math.Pow(bb, 1 / pow);
                    r = Convert.ToInt32(Math.Floor(rr * 255));
                    g = Convert.ToInt32(Math.Floor(gg * 255));
                    b = Convert.ToInt32(Math.Floor(bb * 255));
                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, r, g, b));
                }
            }
            return edit;
        }
    }
}
