using System.Drawing;

namespace ImageEditor
{
    class Negative : Effect
    {
        public Negative() { }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A,
                                                        255 - myImage.GetPixel(i, j).R,
                                                        255 - myImage.GetPixel(i, j).G,
                                                        255 - myImage.GetPixel(i, j).B));
                }
            }
            return edit;
        }
    }
}
