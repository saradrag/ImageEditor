using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form3 : Form
    {
        int selectedEffect;
        int effectType;
        Effect effect;
        Bitmap MyImage;
        Bitmap edit;
        public Form3(Bitmap img, int idx)
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            MyImage = img;
            effectType = idx;
            pictureBox1.Image = MyImage;
            label2.Text = $"Amount of colour change: {hScrollBar1.Value.ToString()}";
            switch (idx)
            {
                case 0:
                    {
                        comboBox1.Items.Add("Black and White 1");
                        comboBox1.Items.Add("Black and White 2");
                        comboBox1.Items.Add("Black and White 3");
                        comboBox1.Items.Add("Black and White 4");
                        comboBox1.Items.Add("Brightness 1");
                        comboBox1.Items.Add("Brightness 2");
                        comboBox1.Items.Add("Saturation 1");
                        comboBox1.Items.Add("Saturation 2");
                        comboBox1.Items.Add("Contrast");
                        comboBox1.Items.Add("Temperature");
                        comboBox1.Items.Add("Gamma");
                        break;
                    }
                case 1:
                    {
                        comboBox1.Items.Add("Red");
                        comboBox1.Items.Add("Orange");
                        comboBox1.Items.Add("Yellow");
                        comboBox1.Items.Add("Green");
                        comboBox1.Items.Add("Cyan");
                        comboBox1.Items.Add("Blue");
                        comboBox1.Items.Add("Purple");
                        comboBox1.Items.Add("Magenta");
                        break;
                    }

                case 2:
                    {
                        comboBox1.Items.Add("Simple Thresholding");
                        comboBox1.Items.Add("Adaptive thresholding 1");
                        comboBox1.Items.Add("Adaptive thresholding 2");
                        break;
                    }
                case 3:
                    {
                        comboBox1.Items.Add("Blur");
                        comboBox1.Items.Add("Gaussian Blur");
                        break;
                    }
                case 4:
                    {
                        comboBox1.Items.Add("Erosion");
                        comboBox1.Items.Add("Dilation");
                        comboBox1.Items.Add("Opening");
                        comboBox1.Items.Add("Closing");
                        comboBox1.Items.Add("Morphoogical gradient");
                        break;
                    }
                case 5:
                    {
                        comboBox1.Items.Add("Horisontal Edge Detection");
                        comboBox1.Items.Add("Vertical Edge Detection");
                        comboBox1.Items.Add("Sobel");
                        comboBox1.Items.Add("Canny");
                        break;
                    }
                case 6:
                    {
                        comboBox1.Items.Add("Linear Normalization");
                        comboBox1.Items.Add("Sigmoid Normalization");
                        comboBox1.Items.Add("Histogram Equalization");
                        comboBox1.Items.Add("Histogram Equalization Colour 1");
                        comboBox1.Items.Add("Histogram Equalization Colour 2");
                        break;
                    }
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (effectType)
            {
                case 0:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new BlackAndWhite1();
                                break;
                            case 1:
                                effect = new BlackAndWhite2();
                                break;
                            case 2:
                                effect = new BlackAndWhite3();
                                break;
                            case 3:
                                effect = new BlackAndWhite4();
                                break;
                            case 4:
                                effect = new Brightness1(-0.2);
                                break;
                            case 5:
                                effect = new Brightness2(-0.3);
                                break;
                            case 6:
                                effect = new Saturation1(-0.2);
                                break;
                            case 7:
                                effect = new Saturation2(-0.3);
                                break;
                            case 8:
                                effect = new Contrast(-0.5);
                                break;
                            case 9:
                                effect = new Temperature(-50);
                                break;
                            default:
                                effect = new Gamma(1.5);
                                break;
                        }
                        break;
                    }
                case 1:
                    {
                        int p = hScrollBar1.Value;
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new Red(p, 0, 0);
                                break;
                            case 1:
                                effect = new Orange(p, 0, 0);
                                break;
                            case 2:
                                effect = new Yellow(p, 0, 0);
                                break;
                            case 3:
                                effect = new Green(p, 0, 0);
                                break;
                            case 4:
                                effect = new Cyan(p, 0, 0);
                                break;
                            case 5:
                                effect = new Blue(p, 0, 0);
                                break;
                            case 6:
                                effect = new Purple(p, 0, 0);
                                break;
                            default:
                                effect = new Magenta(p, 0, 0);
                                break;
                        }
                        break;
                    }
                case 2:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new SimpleThresholding(0.5);
                                break;
                            case 1:
                                effect = new AdaptiveThresholding1(2, 0.01);
                                break;
                            default:
                                effect = new AdaptiveThresholding2(0.01);
                                break;
                        }
                        break;
                    }
                case 3:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new Blur(3);
                                break;
                            default:
                                effect = new GaussianBlur(2);
                                break;
                        }
                        break;
                    }
                case 4:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new Erosion(2);
                                break;
                            case 1:
                                effect = new Dilation(2);
                                break;
                            case 2:
                                effect = new Opening(1);
                                break;
                            case 3:
                                effect = new Closing(1);
                                break;
                            case 4:
                                effect = new MorphologicalGradient(1);
                                break;
                        }
                        break;
                    }
                case 5:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new HorizontalEdgeDetection(1);
                                break;
                            case 1:
                                effect = new VerticalEdgeDetection(1);
                                break;
                            case 2:
                                effect = new Sobel(1);
                                break;
                            case 3:
                                effect = new Canny(0.15, 0.25);
                                break;
                        }
                        break;
                    }
                case 6:
                    {
                        switch (selectedEffect)
                        {
                            case 0:
                                effect = new LinearNormalization();
                                break;
                            case 1:
                                effect = new SigmoidNormalization(255 / 8, 255 / 2);
                                break;
                            case 2:
                                effect = new HistogramEqualization();
                                break;
                            case 3:
                                effect = new HistogramEqualizationColour1();
                                break;
                            case 4:
                                effect = new HistogramEqualizationColour2();
                                break;
                        }
                    }
                    break;
            }
            edit = effect.Apply(MyImage);
            pictureBox1.Image = edit;
            button2.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            selectedEffect = comboBox1.SelectedIndex;
            if(effectType == 1)
            {
                label2.Visible = true;
                hScrollBar1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";
            sfd.DefaultExt = "jpg";
            sfd.AddExtension = true;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName;
                    edit.Save(path);
                }
            }
            catch
            {
                MessageBox.Show("Not an valid path.");
                throw new Exception("Couldn't save to the file.");
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label2.Text = $"Amount of colour change: {hScrollBar1.Value.ToString()}";
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
