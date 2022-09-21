using System;
using System.Drawing;

namespace ImageEditor
{
    class BlackAndWhite3 : Effect
    {
        public BlackAndWhite3() { }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int a;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    a = Math.Max(Math.Max(myImage.GetPixel(i, j).R ,myImage.GetPixel(i, j).G) ,myImage.GetPixel(i, j).B);

                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, a, a, a));
                }
            }
            return edit;
        }
    }
}
