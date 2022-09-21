using System.Drawing;

namespace ImageEditor
{
    class HistogramEqualizationColour1 : Effect
    {
        public HistogramEqualizationColour1()
        {
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int[] rarr = new int[258];
            rarr[256] = 256;
            rarr[257] = -1;

            int[] garr = new int[258];
            garr[256] = 256;
            garr[257] = -1;

            int[] barr = new int[258];
            barr[256] = 256;
            barr[257] = -1;

            int r;
            int g;
            int b;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = myImage.GetPixel(i, j).R;
                    rarr[r]++;
                    if (r > rarr[257]) rarr[257] = r;
                    if (r < rarr[256]) rarr[256] = r;

                    g = myImage.GetPixel(i, j).G;
                    garr[r]++;
                    if (g > garr[257]) garr[257] = g;
                    if (g < garr[256]) garr[256] = g;

                    b = myImage.GetPixel(i, j).B;
                    barr[b]++;
                    if (b > barr[257]) barr[257] = b;
                    if (b < barr[256]) barr[256] = b;
                }
            }
            double[] rf = new double[256];
            double[] gf = new double[256];
            double[] bf = new double[256];

            rf[0] = (double)rarr[0] / myImage.Width / myImage.Height;
            gf[0] = (double)garr[0] / myImage.Width / myImage.Height;
            bf[0] = (double)barr[0] / myImage.Width / myImage.Height;
            for (int i = 1; i < 256; i++)
            {
                rf[i] = rf[i - 1] + (double)rarr[i] / myImage.Width / myImage.Height;
                gf[i] = gf[i - 1] + (double)garr[i] / myImage.Width / myImage.Height;
                bf[i] = bf[i - 1] + (double)barr[i] / myImage.Width / myImage.Height;
            }
            int rr, gg, bb;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    rr = (int)((rf[myImage.GetPixel(i, j).R] - rf[rarr[256]]) * 255 / (1 - rf[rarr[256]]));
                    gg = (int)((gf[myImage.GetPixel(i, j).G] - gf[garr[256]]) * 255 / (1 - gf[garr[256]]));
                    bb = (int)((bf[myImage.GetPixel(i, j).B] - bf[barr[256]]) * 255 / (1 - bf[barr[256]]));
                    edit.SetPixel(i, j, Color.FromArgb(255, rr, gg, bb));
                }
            }
            return edit;
        }
    }
}
