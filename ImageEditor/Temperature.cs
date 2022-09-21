using System.Drawing;

namespace ImageEditor
{
    class Temperature : Effect
    {
        public int power;
        public Temperature(int p)
        {
            power = p;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int r, g, b;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    r = myImage.GetPixel(i, j).R ;
                    g = myImage.GetPixel(i, j).G - power/2;
                    b = myImage.GetPixel(i, j).B - power;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, r, g, b));
                }
            }
            return edit;
        }
    }
}
