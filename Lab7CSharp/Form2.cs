using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form2 : Form
    {
        private PictureBox pictureBox;
        private Label rgbLabel;

        public Form2()
        {
            InitializeComponent();
            InitializeComponents();
        }

      
        private void InitializeComponents()
        {
            // PictureBox
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(500, 300);
            pictureBox.Location = new Point(50, 50);
            pictureBox.BackColor = Color.White; // Початковий колір
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Click += PictureBox_Click;
            this.Controls.Add(pictureBox);

            // Label для відображення RGB
            rgbLabel = new Label();
            rgbLabel.AutoSize = true;
            rgbLabel.Location = new Point(50, 270);
            this.Controls.Add(rgbLabel);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Bitmap bmp = new Bitmap(pictureBox.Image);

            if (me.Button == MouseButtons.Left)
            {
                Color pickedColor = bmp.GetPixel(me.X, me.Y);
                pictureBox.BackColor = pickedColor;

                // Виведення інформації про RGB на Label
                rgbLabel.Text = $"RGB: ({pickedColor.R}, {pickedColor.G}, {pickedColor.B})";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIF";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file. Original error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image.Save(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file. Original error: " + ex.Message);
                }
            }
        }
    }
}
