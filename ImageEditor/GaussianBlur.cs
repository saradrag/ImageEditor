using System.Drawing;

namespace ImageEditor
{
    class GaussianBlur : Effect
    {
        public double[,] mat;
        public int number;
        public GaussianBlur(int n)
        {
            if (n == 2)
            {
                mat = new double[5, 5] { { 1, 4, 6, 4, 1 }, { 4, 16, 24, 16, 4 }, { 6, 24, 36, 24, 6 }, { 4, 16, 24, 16, 4 }, { 1, 4, 6, 4, 1 } };
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        mat[i, j] /= 256;
                    }
                }
                number = 2;
            }
            else
            {
                number = 1;
                mat = new double[3, 3] { { (double)1 / 16, (double)2 / 16, (double)1 / 16 }, { (double)2 / 16, (double)4 / 16, (double)2 / 16 }, { (double)1 / 16, (double)2 / 16, (double)1 / 16 } };
            }
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double r, g, b;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    if (i >= number && i < myImage.Width - number && j >= number && j < myImage.Height - number)
                    {
                        for (int k = -number; k <= number; k++)
                        {
                            for (int l = -number; l <= number; l++)
                            {
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + number, l + number];
                                g += (double)myImage.GetPixel(i + k, j + l).G * mat[k + number, l + number];
                                b += (double)myImage.GetPixel(i + k, j + l).B * mat[k + number, l + number];
                            }
                        }
                        if (r > 255) r = 255;
                        if (g > 255) g = 255;
                        if (b > 255) b = 255;
                        if (r < 0) r = 0;
                        if (g < 0) g = 0;
                        if (b < 0) b = 0;
                    }
                    edit.SetPixel(i, j, Color.FromArgb(255, (int)r, (int)g, (int)b));
                }
            }
            return edit;
        }
    }
}
