using System.Drawing;

namespace ImageEditor
{
    class Saturation2 : Effect
    {
        double p;
        public Saturation2(double cons)
        {
            p = cons;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsl;
            if (p > 0)
            {
                for (int i = 0; i < myImage.Width; i++)
                {
                    for (int j = 0; j < myImage.Height; j++)
                    {
                        hsl = new HSV(myImage.GetPixel(i, j));
                        hsl.S = hsl.S + (1 - hsl.S) * p;
                        edit.SetPixel(i, j, hsl.ToColor());
                    }
                }
            }
            else
            {
                for (int i = 0; i < myImage.Width; i++)
                {
                    for (int j = 0; j < myImage.Height; j++)
                    {
                        hsl = new HSV(myImage.GetPixel(i, j));
                        hsl.S = hsl.S + hsl.S * p;
                        edit.SetPixel(i, j, hsl.ToColor());
                    }
                }
            }
            return edit;
        }
    }
}
