using System.Drawing;

namespace ImageEditor
{
    class Contrast : Effect
    {
        public double power;
        public Contrast(double p)
        {
            power = p;
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
                    rr = r;
                    gg = g;
                    bb = b;
                    r += (int)(power * (rr - 255 / 2));
                    g += (int)(power * (gg - 255 / 2));
                    b += (int)(power * (bb - 255 / 2));
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, r, g, b));
                }
            }
            return edit;
        }
    }
}
