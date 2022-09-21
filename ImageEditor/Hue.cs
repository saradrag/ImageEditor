using System.Drawing;

namespace ImageEditor
{
    class Hue : Effect
    {
        public int number;
        public Hue(int n) { }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsl;
            double h;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsl = new HSV(myImage.GetPixel(i, j));
                    h = hsl.H;
                    h += (double)number / 255 * 180;
                    hsl.H = (360 + h) % 360;

                    edit.SetPixel(i, j, hsl.ToColor());
                }
            }
            return edit;
        }
    }
}
