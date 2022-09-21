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
    public partial class Form2 : Form
    {
        public Bitmap myImage;
        int effectType;
        public Form2(Bitmap img)
        {
            InitializeComponent();
            myImage = img;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            effectType = comboBox1.SelectedIndex;
            if (!button1.Visible) button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3(myImage, effectType);
            f3.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
