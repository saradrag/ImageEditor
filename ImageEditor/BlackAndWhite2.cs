using System.Drawing;

namespace ImageEditor
{
    class BlackAndWhite2 : Effect
    {
        public BlackAndWhite2(){}
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);
            int avg;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    avg = (myImage.GetPixel(i, j).R + myImage.GetPixel(i, j).G + myImage.GetPixel(i, j).B)/3;

                    edit.SetPixel(i, j, Color.FromArgb(myImage.GetPixel(i, j).A, avg, avg, avg));
                }
            }
            return edit;
        }
    }
}