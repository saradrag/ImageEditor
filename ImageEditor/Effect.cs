using System.Drawing;

namespace ImageEditor
{
    abstract class Effect
    {
        public abstract Bitmap Apply(Bitmap myImage);
    }
}
