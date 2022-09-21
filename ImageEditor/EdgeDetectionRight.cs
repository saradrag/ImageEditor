using System.Drawing;

namespace ImageEditor
{
    class EdgeDetectionRight : Effect
    {
        public double[,] mat;
        public int number;
        public int power;
        public EdgeDetectionRight(int n, int p)
        {
            mat = new double[2 * n + 1, 2 * n + 1];
            for (int i = 0; i < 2 * n + 1; i++)
            {
                for (int j = 0; j < 2 * n + 1; j++)
                {
                    if (j == n) mat[i, j] = (double)0;
                    else if (j < n) mat[i, j] = (double)-1;
                    else mat[i, j] = -mat[i, 2 * n - j];
                }
            }
            number = n;
            power = p;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double r, g, b;
            int rr, gg, bb;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int k = -number; k <= number; k++)
                    {
                        for (int l = -number; l <= number; l++)
                        {
                            if (i + k >= 0 && i + k < myImage.Width && j + l >= 0 && j + l < myImage.Height)
                            {
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + number, l + number] * (double)power;
                                g += (double)myImage.GetPixel(i + k, j + l).G * mat[k + number, l + number] * (double)power;
                                b += (double)myImage.GetPixel(i + k, j + l).B * mat[k + number, l + number] * (double)power;
                            }
                        }
                    }
                    rr = (int)(r);
                    gg = (int)(g);
                    bb = (int)(b);
                    if (rr > 255) rr = 255;
                    if (gg > 255) gg = 255;
                    if (bb > 255) bb = 255;
                    if (rr < 0) rr = 0;
                    if (gg < 0) gg = 0;
                    if (bb < 0) bb = 0;
                    edit.SetPixel(i, j, Color.FromArgb(255, rr, gg, bb));
                }
            }
            return edit;
        }
    }
}
