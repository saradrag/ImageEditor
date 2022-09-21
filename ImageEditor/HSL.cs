using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ImageEditor
{
    class HSL
    {
        public double H;
        public double S;
        public double L;
        public HSL(Color c)
        {
            int r=c.R, g=c.G, b=c.B;
            double rr, gg, bb;
            rr = (double)r / 255;
            gg = (double)g / 255;
            bb = (double)b / 255;
            double maxx, minn, dif;
            maxx = Math.Max(Math.Max(rr, gg), bb);
            minn = Math.Min(Math.Min(rr, gg), bb);
            dif = maxx - minn;
            if (dif == 0) H = 0;
            else if (maxx == rr) H = (360 + 60 * ((gg - bb) / dif)) % 360;
            else if (maxx == gg) H = (360 + 60 * ((bb - rr) / dif + 2))% 360;
            else if (maxx == bb) H = (360 + 60 * ((rr - gg) / dif + 4))% 360;

            L = (maxx + minn) / 2;

            if (dif == 0) S = 0;
            else S = dif / (1 - Math.Abs(2 * L - 1));
        }
        public Color ToColor()
        {
            double c = (1 - Math.Abs(2 * L - 1)) * S;
            double x = c * (1 - Math.Abs((H / 60) % 2 - 1));
            double m = L - c / 2;
            double rr, gg, bb;
            if (H < 60) { rr = c; gg = x; bb = 0; }
            else if (H >= 60 && H < 120) { rr = x; gg = c; bb = 0; }
            else if (H >= 120 && H < 180) { rr = 0; gg = c; bb = x; }
            else if (H >= 180 && H < 240) { rr = 0; gg = x; bb = c; }
            else if (H >= 240 && H < 300) { rr = x; gg = 0; bb = c; }
            else { rr = c; gg = 0; bb = x; }
            int r = (int)((rr + m) * 255);
            int g = (int)((gg + m) * 255);
            int b = (int)((bb + m) * 255);
            return Color.FromArgb(255, r, g, b);
        }
    }
}
