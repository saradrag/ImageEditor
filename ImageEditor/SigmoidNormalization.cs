using System;
using System.Drawing;

namespace ImageEditor
{
    class SigmoidNormalization : Effect
    {
        int alfa;
        double beta;
        public SigmoidNormalization(int a, double b)
        {
            alfa = a;
            beta = b;
        }
        public override Bitmap Apply(Bitmap myImage)
        {
            Bitmap edit = new Bitmap(myImage.Width, myImage.Height);

            int a;
            for (int i = 0; i < myImage.Width; i++)
            {
                for (int j = 0; j < myImage.Height; j++)
                {
                    a = (int)(255 / (double)(1 + (double)Math.Exp(-(double)(((double)myImage.GetPixel(i, j).R - beta) / alfa))));

                    edit.SetPixel(i, j, Color.FromArgb(255, a, a, a));
                }
            }
            return edit;
        }
    }
}
