using System.Drawing;

namespace ImageEditor
{
    class HistogramEqualization : Effect
    {
        public HistogramEqualization()
        {
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int[] arr = new int[258];
            arr[256] = 256;
            arr[257] = -1;
            int r;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = myImage.GetPixel(i, j).R;
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
                    a = (int)((f[myImage.GetPixel(i, j).R] - f[arr[256]]) * 255 / (1 - f[arr[256]]));
                    edit.SetPixel(i, j, Color.FromArgb(255, a, a, a));
                }
            }
            return edit;
        }
    }
}
