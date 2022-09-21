using System.Drawing;

namespace ImageEditor
{
    class Saturation1 : Effect
    {
        double c;
        public Saturation1(double cons)
        {
            c = cons;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsl;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsl = new HSV(myImage.GetPixel(i, j));
                    hsl.S += c;
                    if (hsl.S > 1) hsl.S = 1;
                    if (hsl.S < 0) hsl.S = 0;
                    edit.SetPixel(i, j, hsl.ToColor());
                }
            }
            return edit;
        }
    }
}
