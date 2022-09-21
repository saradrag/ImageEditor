using System.Drawing;

namespace ImageEditor
{
    class HorizontalEdgeDetection : Effect
    {
        public double[,] mat;
        public int number;
        public HorizontalEdgeDetection(int n)
        {
            n = 1;
            mat = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            number = n;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            double r;
            int rr;
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
                                r += (double)(myImage.GetPixel(i + k, j + l).R-255/2)/255*2 * mat[k + number, l + number];
                            }
                        }
                    }
                    
                    rr = (int)(r*255/2+255/2);
                    if (rr > 255) rr = 255;
                    if (rr < 0) rr = 0;
                    edit.SetPixel(i, j, Color.FromArgb(255, rr, rr, rr));
                }
            }
            return edit;
        }
    }
}
