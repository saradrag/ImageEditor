using System.Drawing;

namespace ImageEditor
{
    class AdaptiveThresholding2 : Effect
    {
        public double[,] mat;
        public double c;
        public AdaptiveThresholding2(double con)
        {
            mat = new double[5, 5] { { 1, 4, 6, 4, 1 }, { 4, 16, 24, 16, 4 }, { 6, 24, 36, 24, 6 }, { 4, 16, 24, 16, 4 }, { 1, 4, 6, 4, 1 } };
            for (int i = 0; i <5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mat[i, j] /= 256;
                }
            }

            c = con;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsv;
            double prag;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsv = new HSV(myImage.GetPixel(i, j));
                    prag = 0;
                    hsv.S = 0;
                    if (i >= 2 && i < myImage.Width - 2 && j >= 2 && j < myImage.Height - 2)
                    {
                        for (int k = -2; k <= 2; k++)
                        {
                            for (int l = -2; l <= 2; l++)
                            {
                                prag += mat[2 + k, 2 + l] * (new HSV(myImage.GetPixel(i + k, j + l))).V;
                            }
                        }
                        prag -= c;
                        if (hsv.V >= prag) hsv.V = 1;
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
