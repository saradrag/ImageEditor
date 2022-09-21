using System;
using System.Drawing;

namespace ImageEditor
{
    class Sobel : Effect
    {
        public int number;

        public Sobel(int n)
        {
            number = 1;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            int n = 1;
            double[,] mat1;
            double[,] mat2;
            mat2 = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            mat1 = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double gx;
            double gy;
            double g;
            HSV hsv = new HSV();
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    gx = 0;
                    gy = 0;
                    if (i >= number && i < myImage.Width - number && j >= number && j < myImage.Height - number)
                    {
                        for (int k = -number; k <= number; k++)
                        {
                            for (int l = -number; l <= number; l++)
                            {
                                gx += (double)(myImage.GetPixel(i + k, j + l).R - 255 / 2) / 255 * 2 * mat1[k + number, l + number];
                                gy += (double)(myImage.GetPixel(i + k, j + l).R - 255 / 2) / 255 * 2 * mat2[k + number, l + number];
                            }
                        }
                        if (gx > 1) gx = 1;
                        if (gx < -1) gx = -1;
                        if (gy > 1) gy = 1;
                        if (gy < -1) gy = -1;
                        g = Math.Sqrt((gx * gx + gy * gy) / 2);
                        if (g > 1) g = 1;
                        hsv.V = g;
                        if (gx > 0) hsv.H = (360 + Math.Atan(gy / gx) * 180 / Math.PI) % 360;
                        else if (gx < 0) hsv.H = 180 + Math.Atan(gy / gx) * 180 / Math.PI;
                        else if (gy > 0) hsv.H = 90;
                        else hsv.H = 270;
                        hsv.S = 0;
                        edit.SetPixel(i, j, hsv.ToColor());
                    }
                }
            }
            return edit;
        }
    }
}
