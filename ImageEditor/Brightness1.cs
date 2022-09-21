using System.Drawing;

namespace ImageEditor
{
    class Brightness1 : Effect
    {
        double c;
        public Brightness1(double cons)
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
                    hsl.V += c;
                    if (hsl.V > 1) hsl.V = 1;
                    if (hsl.V < 0) hsl.V = 0;
                    edit.SetPixel(i, j, hsl.ToColor());
                }
            }
            return edit;
        }
    }
}
