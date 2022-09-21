using System.Drawing;

namespace ImageEditor
{
    class LinearNormalization : Effect
    {
        public LinearNormalization() { }
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
            int a;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    a = (myImage.GetPixel(i, j).R - arr[256]) * 255 / (arr[257] - arr[256]);
                    edit.SetPixel(i, j, Color.FromArgb(255, a, a, a));
                }
            }
            return edit;
        }
    }
}
