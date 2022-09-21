using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class PrikazSlike : Form
    {
        public Bitmap MyImage;
        
        public PrikazSlike(string putanja)
        {
            InitializeComponent();
            
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            MyImage = new Bitmap(putanja);
            //MyImage = new Bitmap("plaza.jpg");
            //MyImage = new Bitmap("plazacb.jpg");
            //MyImage = new Bitmap("thresh.jpg");
            //MyImage = new Bitmap("prilthresh.jpg");
            //MyImage = new Bitmap("krug.jpg");
            //MyImage = new Bitmap("skola.jpg");
            //MyImage = new Bitmap("mgcb.jpg");
            //MyImage = new Bitmap("mg.jpg");
            //MyImage = new Bitmap("mgsacrnim.jpg");
            //MyImage = new Bitmap("mgsabelim.jpg");
            //MyImage = new Bitmap("odeljenje.jpg");
            pictureBox1.ClientSize = new Size(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = (Image)MyImage;
        }

        IEffect[] m;
        private void Form3_Load(object sender, EventArgs e)
        {
            m = new IEffect[51];
            m[0] = new Negative();
            m[1] = new Crnobela1();
            m[2] = new Crnobela2();
            m[3] = new Crnobela3();
            m[4] = new Crnobela4();
            m[5] = new Osvetljenost1(-0.2);
            m[6] = new Osvetljenost2(-0.3);
            m[7] = new Saturacija1(-0.2);
            m[8] = new Saturacija2(-0.3);
            m[9] = new Contrast(-0.5);
            m[10] = new Temperature(-50);
            m[11] = new Blue(-50, 0, 0);
            m[12] = new gamma(1.5);

            m[13] = new JednostavanThresholding(0.5);
            m[14] = new PrilagodjavajuciThresholding(2, 0.01);
            m[15] = new PrilagodjavajuciThresholding2(0.01);

            m[16] = new Blur(3);
            m[17] = new ZamucivanjeGausom(2);

            m[18] = new Erozija(2);
            m[19] = new Dilatacija(2);
            m[20] = new Otvaranje(1);
            m[21] = new Zatvaranje(1);
            m[22] = new MorfoloskiGradijent(1);

            m[23] = new HorizontalnaDetIv(1);
            m[24] = new VertikalnaDetIv(1);
            m[25] = new Sobel(1);
            m[26] = new Canny(0.15, 0.25);

            m[27] = new LinearnaNormalizacija();
            m[28] = new SigmoidNormalizacija(255/8, 255/2);
            m[29] = new IzjednacavanjeHistograma();
            m[30] = new IzjHistUBoji1();
            m[31] = new IzjHistUBoji2();
            for (int i = 0; i < 32; i++)
            {
                string imeEfekta = m[i].ToString();
                int poz = imeEfekta.IndexOf(".");
                imeEfekta = imeEfekta.Substring(poz + 1);
                comboBox1.Items.Add(imeEfekta);
            }
            comboBox1.Text = "Izaberite efekat";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < 32)
            {
                comboBox1.Text = comboBox1.SelectedItem.ToString();
                Bitmap edit = m[comboBox1.SelectedIndex].Apply(MyImage);
                pictureBox1.Image = edit;
                //edit.Save("path");
            }
            else comboBox1.Text = "Izaberite efekat";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.bmp|*.jpg";
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if(saveFileDialog1.FileName!="")
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}
