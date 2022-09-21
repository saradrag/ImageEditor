using System.Drawing;

namespace ImageEditor
{
    class SimpleThresholding : Effect
    {
        double p;
        public SimpleThresholding(double power)
        {
            p = power;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsv;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsv = new HSV(myImage.GetPixel(i, j));
                    hsv.S = 0;
                    if (hsv.V >= p) hsv.V = 1;
                    else hsv.V = 0;
                    edit.SetPixel(i, j, hsv.ToColor());
                }
            }
            return edit;
        }
    }
}
