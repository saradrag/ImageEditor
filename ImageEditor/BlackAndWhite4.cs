using System.Drawing;

namespace ImageEditor
{
    class BlackAndWhite4 : Effect
    {
        public BlackAndWhite4() { }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int avg;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    avg = (myImage.GetPixel(i, j).R + myImage.GetPixel(i, j).G + myImage.GetPixel(i, j).B) / 3;
                    avg += ((myImage.GetPixel(i, j).G - myImage.GetPixel(i, j).R) + (myImage.GetPixel(i, j).B - myImage.GetPixel(i, j).R)) / 2;
                    if (avg > 255) avg = 255;
                    if (avg < 0) avg = 0;
                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, avg, avg, avg));
                }
            }
            return edit;
        }
    }
}
