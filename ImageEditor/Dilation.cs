using System.Drawing;

namespace ImageEditor
{
    class Dilation : Effect
    {
        public double[,] mat;
        public int number;
        public Dilation(int n)
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
            double r;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = 0;
                    if (i >= number && i < myImage.Width - number && j >= number && j < myImage.Height - number)
                    {
                        for (int k = -number; k <= number; k++)
                        {
                            for (int l = -number; l <= number; l++)
                            {
                                r += (double)myImage.GetPixel(i + k, j + l).R * mat[k + number, l + number];
                            }
                        }
                        if (r > 0) r = 255;
                    }
                    edit.SetPixel(i, j, Color.FromArgb(255, (int)r, (int)r, (int)r));
                }
            }
            return edit;
        }
    }
}
