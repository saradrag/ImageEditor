using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageEditor
{
    class Canny : Effect
    {
        public int number;
        public double lower_threshold;
        public double upper_threshold;

        public Canny(double d, double g)
        {
            number = 1;
            lower_threshold = d;
            upper_threshold = g;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            double[,] mat1;
            double[,] mat2;
            mat2 = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            mat1 = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };


            // Gaussian blur
            double[,] mat = new double[5, 5] { { 1, 4, 6, 4, 1 }, { 4, 16, 24, 16, 4 }, { 6, 24, 36, 24, 6 }, { 4, 16, 24, 16, 4 }, { 1, 4, 6, 4, 1 } };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mat[i, j] /= 256;
                }
            }
            int num = 2;
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double r;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = 0;
                    if (i >= num && i < myImage.Width - num && j >= num && j < myImage.Height - num)
                    {
                        for (int k = -num; k <= num; k++)
                        {
                            for (int l = -num; l <= num; l++)
                            {
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + num, l + num];
                            }
                        }
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                    }
                    edit.SetPixel(i, j, Color.FromArgb(255, (int)r, (int)r, (int)r));
                }
            }
            // Sobel
            Bitmap edit1 = new Bitmap(edit);
            double gx;  // x gradient
            double gy;  // y gradient
            double g;   // total gradient
            HSV hsv = new HSV();
            for (int i = 0; i < edit.Width; i++)
            {
                for (int j = 0; j < edit.Height; j++)
                {
                    gx = 0;
                    gy = 0;
                    if (i >= number && i < edit.Width - number && j >= number && j < edit.Height - number)
                    {
                        for (int k = -number; k <= number; k++)
                        {
                            for (int l = -number; l <= number; l++)
                            {
                                gx += (double)(edit.GetPixel(i + k, j + l).R - 255 / 2) / 255 * 2 * mat1[k + number, l + number];
                                gy += (double)(edit.GetPixel(i + k, j + l).R - 255 / 2) / 255 * 2 * mat2[k + number, l + number];
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
                        edit1.SetPixel(i, j, hsv.ToColor());
                    }
                }
            }
            // Edge thinning
            HSV hsv1;
            HSV hsv2;
            for (int i = 1; i < edit1.Width - 1; i++)
            {
                for (int j = 1; j < edit1.Height - 1; j++)
                {
                    hsv = new HSV(edit1.GetPixel(i, j));
                    if ((0 <= hsv.H + 30 && hsv.H + 30 < 60) || (180 <= hsv.H + 30 && hsv.H + 30 < 240)) { hsv1 = new HSV(edit1.GetPixel(i + 1, j)); hsv2 = new HSV(edit1.GetPixel(i - 1, j)); }
                    else if ((60 <= hsv.H + 30 && hsv.H + 30 < 90) || (240 <= hsv.H + 30 && hsv.H + 30 < 270)) { hsv1 = new HSV(edit1.GetPixel(i + 1, j + 1)); hsv2 = new HSV(edit1.GetPixel(i - 1, j - 1)); }
                    else if ((90 <= hsv.H + 30 && hsv.H + 30 < 150) || (270 <= hsv.H + 30 && hsv.H + 30 < 330)) { hsv1 = new HSV(edit1.GetPixel(i, j + 1)); hsv2 = new HSV(edit1.GetPixel(i, j - 1)); }
                    else { hsv1 = new HSV(edit1.GetPixel(i - 1, j + 1)); hsv2 = new HSV(edit1.GetPixel(i + 1, j - 1)); }
                    if (hsv.V < hsv1.V || hsv.V < hsv2.V)
                        hsv.V = 0;

                    edit.SetPixel(i, j, hsv.ToColor());

                }
            }
            // Double thresholding
            for (int i = 0; i < edit.Width; i++)
            {
                for (int j = 0; j < edit.Height; j++)
                {
                    hsv = new HSV(edit.GetPixel(i, j));
                    if (hsv.V > upper_threshold) hsv.V = 1;
                    else if (hsv.V < lower_threshold) hsv.V = 0;
                    else hsv.V = 0.5;
                    edit.SetPixel(i, j, hsv.ToColor());
                }
            }
            // Edge tracking
            List<int> list_x = new List<int>();
            List<int> list_y = new List<int>();
            int ix, jy;
            for (int i = 0; i < edit.Width; i++)
            {
                for (int j = 0; j < edit.Height; j++)
                {
                    hsv = new HSV(edit.GetPixel(i, j));
                    if (hsv.V == 1)
                    {
                        if (i > 0 && (new HSV(edit.GetPixel(i - 1, j))).V == 0.5)
                        {
                            list_x.Add(i - 1);
                            list_y.Add(j);
                        }
                        if (i > 0 && j > 0 && (new HSV(edit.GetPixel(i - 1, j - 1))).V == 0.5)
                        {
                            list_x.Add(i - 1);
                            list_y.Add(j - 1);
                        }
                        if (i > 0 && j < edit.Height - 1 && (new HSV(edit.GetPixel(i - 1, j + 1))).V == 0.5)
                        {
                            list_x.Add(i - 1);
                            list_y.Add(j + 1);
                        }
                        if (j < edit.Height - 1 && (new HSV(edit.GetPixel(i, j + 1))).V == 0.5)
                        {
                            list_x.Add(i);
                            list_y.Add(j + 1);
                        }
                        if (i < edit.Width - 1 && j < edit.Height - 1 && (new HSV(edit.GetPixel(i + 1, j + 1))).V == 0.5)
                        {
                            list_x.Add(i + 1);
                            list_y.Add(j + 1);
                        }
                        if (i < edit.Width - 1 && (new HSV(edit.GetPixel(i + 1, j))).V == 0.5)
                        {
                            list_x.Add(i + 1);
                            list_y.Add(j);
                        }
                        if (i < edit.Width - 1 && j > 0 && (new HSV(edit.GetPixel(i + 1, j - 1))).V == 0.5)
                        {
                            list_x.Add(i + 1);
                            list_y.Add(j - 1);
                        }
                        if (j > 0 && (new HSV(edit.GetPixel(i, j - 1))).V == 0.5)
                        {
                            list_x.Add(i);
                            list_y.Add(j - 1);
                        }
                        while (list_x.Count() > 0)
                        {
                            ix = list_x.First();
                            jy = list_y.First();
                            edit.SetPixel(ix, jy, Color.FromArgb(255, 255, 255, 255));
                            list_x.RemoveAt(0);
                            list_y.RemoveAt(0);
                            if (ix > 0 && (new HSV(edit.GetPixel(ix - 1, jy))).V == 0.5)
                            {
                                list_x.Add(ix - 1);
                                list_y.Add(jy);
                            }
                            if (ix > 0 && jy > 0 && (new HSV(edit.GetPixel(ix - 1, jy - 1))).V == 0.5)
                            {
                                list_x.Add(ix - 1);
                                list_y.Add(jy - 1);
                            }
                            if (ix > 0 && jy < edit.Height - 1 && (new HSV(edit.GetPixel(ix - 1, jy + 1))).V == 0.5)
                            {
                                list_x.Add(ix - 1);
                                list_y.Add(jy + 1);
                            }
                            if (jy < edit.Height - 1 && (new HSV(edit.GetPixel(ix, jy + 1))).V == 0.5)
                            {
                                list_x.Add(ix);
                                list_y.Add(jy + 1);
                            }
                            if (ix < edit.Width - 1 && jy < edit.Height - 1 && (new HSV(edit.GetPixel(ix + 1, jy + 1))).V == 0.5)
                            {
                                list_x.Add(ix + 1);
                                list_y.Add(jy + 1);
                            }
                            if (ix < edit.Width - 1 && (new HSV(edit.GetPixel(ix + 1, jy))).V == 0.5)
                            {
                                list_x.Add(ix + 1);
                                list_y.Add(jy);
                            }
                            if (ix < edit.Width - 1 && jy > 0 && (new HSV(edit.GetPixel(ix + 1, jy - 1))).V == 0.5)
                            {
                                list_x.Add(ix + 1);
                                list_y.Add(jy - 1);
                            }
                            if (jy > 0 && (new HSV(edit.GetPixel(ix, jy - 1))).V == 0.5)
                            {
                                list_x.Add(ix);
                                list_y.Add(jy - 1);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < edit.Width; i++)
            {
                for (int j = 0; j < edit.Height; j++)
                {
                    hsv = new HSV(edit.GetPixel(i, j));
                    if (hsv.V != 1) hsv.V = 0;
                    edit.SetPixel(i, j, hsv.ToColor());
                }
            }
            return edit;
        }
    }
}
