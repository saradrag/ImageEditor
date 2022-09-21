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
    public partial class StartnaForma : Form
    {
        public StartnaForma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string putanja = openFileDialog1.FileName;
                PrikazSlike f = new PrikazSlike(putanja);
                f.Show();
            }    
        }
    }
}
