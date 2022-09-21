using System.Drawing;

namespace ImageEditor
{
    class AdaptiveThresholding1 : Effect
    {
        public double[,] mat;
        public int number;
        public double c;
        public AdaptiveThresholding1(int n, double con)
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
            c = con;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsv;
            double thrshld;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsv = new HSV(myImage.GetPixel(i, j));
                    thrshld = 0;
                    hsv.S = 0;
                    if (i >= number && i < myImage.Width - number && j >= number && j < myImage.Height - number)
                    {
                        for (int k = -number; k <= number; k++)
                        {
                            for (int l = -number; l <= number; l++)
                            {
                                thrshld += mat[number + k, number + l] * (new HSV(myImage.GetPixel(i + k, j + l))).V;
                            }
                        }
                        thrshld -= c;
                        if (hsv.V >= thrshld) hsv.V = 1;
                        else hsv.V = 0;
                        edit.SetPixel(i, j, hsv.ToColor());
                    }
                    else
                    {
                        hsv.V = 0;
                        edit.SetPixel(i, j, hsv.ToColor());
                    }
                }
            }
            return edit;
        }
    }
}
