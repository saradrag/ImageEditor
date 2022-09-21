using System.Drawing;

namespace ImageEditor
{
    class Sharpen: Effect
    {
        public double[,] mat;
        public int number;
        public Sharpen(int n)
        {
            mat = new double[2 * n + 1, 2 * n + 1];
            for (int i = 0; i < 2 * n + 1; i++)
            {
                for (int j = 0; j < 2 * n + 1; j++)
                {
                    if (i == n && j == n) mat[i, j] = (double)2;
                    else mat[i, j] = (double)-1 / (4 * n * n + 4 * n);
                }
            }
            number = n;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double a, r, g, b;
            int aa, rr, gg, bb;
            double counter;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    a = 0;
                    r = 0;
                    g = 0;
                    b = 0;
                    counter = 0;
                    for (int k = -number; k <= number; k++)
                    {
                        for (int l = -number; l <= number; l++)
                        {
                            if (i + k >= 0 && i + k < myImage.Width && j + l >= 0 && j + l < myImage.Height)
                            {
                                a += (double)myImage.GetPixel(i + k, j + l).A * mat[k + number, l + number];
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + number, l + number];
                                g += (double)myImage.GetPixel(i + k, j + l).G * mat[k + number, l + number];
                                b += (double)myImage.GetPixel(i + k, j + l).B * mat[k + number, l + number];
                                counter += mat[k + number, l + number];
                            }
                        }
                    }
                    aa = (int)(a / counter);
                    rr = (int)(r / counter);
                    gg = (int)(g / counter);
                    bb = (int)(b / counter);
                    if (aa > 255) aa = 255;
                    if (rr > 255) rr = 255;
                    if (gg > 255) gg = 255;
                    if (bb > 255) bb = 255;
                    if (aa < 0) aa = 0;
                    if (rr < 0) rr = 0;
                    if (gg < 0) gg = 0;
                    if (bb < 0) bb = 0;
                    edit.SetPixel(i, j, Color.FromArgb(aa,rr,gg,bb));
                }
            }
            return edit;
        }
    }
}
