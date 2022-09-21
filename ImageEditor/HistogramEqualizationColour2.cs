using System.Drawing;

namespace ImageEditor
{
    class HistogramEqualizationColour2 : Effect
    {
        public HistogramEqualizationColour2()
        {
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int[] arr = new int[258];
            arr[256] = 256;
            arr[257] = -1;
            HSV hsv;
            int r;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsv = new HSV(myImage.GetPixel(i, j));
                    r = (int)(hsv.V * 255);
                    arr[r]++;
                    if (r > arr[257]) arr[257] = r;
                    if (r < arr[256]) arr[256] = r;
                }
            }
            double[] f = new double[256];
            f[0] = (double)arr[0] / myImage.Width / myImage.Height;
            for (int i = 1; i < 256; i++)
            {
                f[i] = f[i - 1] + (double)arr[i] / myImage.Width / myImage.Height;
            }
            int a;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsv = new HSV(myImage.GetPixel(i, j));
                    hsv.V = (f[(int)(hsv.V*255)] - f[arr[256]])/ (1 - f[arr[256]]);
                    edit.SetPixel(i, j, hsv.ToColor());
                }
            }
            return edit;
        }
    }
}
