using System.Drawing;

namespace ImageEditor
{
    class BlackAndWhite1 : Effect
    {
        public BlackAndWhite1() { }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            HSV hsl;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    hsl = new HSV(myImage.GetPixel(i, j));
                    hsl.S = 0;
                    edit.SetPixel(i, j, hsl.ToColor());
                }
            }
            return edit;
        }
    }
}
