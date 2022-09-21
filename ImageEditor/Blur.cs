using System;
using System.Drawing;

namespace ImageEditor
{
    class Blur : Effect
    {
        public double[,] mat;
        public int number;
        public Blur(int n)
        {
            mat = new double[2 * n + 1, 2 * n + 1];
            for (int i = 0; i < 2 * n + 1; i++)
            {
                for (int j = 0; j < 2 * n + 1; j++)
                {
                    mat[i, j] = (double)1 / (4 * n * n + 4 * n + 1);
                }
            }
            number = n;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double r, g, b;
            double counter;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
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
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + number, l + number];
                                g += (double)myImage.GetPixel(i + k, j + l).G * mat[k + number, l + number];
                                b += (double)myImage.GetPixel(i + k, j + l).B * mat[k + number, l + number];
                                counter += mat[k + number, l + number];
                            }
                        }
                    }

                    edit.SetPixel(i, j, Color.FromArgb(255,
                                                       Convert.ToByte(r / counter),
                                                       Convert.ToByte(g / counter),
                                                       Convert.ToByte(b / counter)));
                }
            }
            return edit;
        }
    }
}
