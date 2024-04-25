using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Button centralButton;
        private Panel rotatingPanel;
        private System.Windows.Forms.Timer animationTimer;
        private double angle = 0;
        private Random random;


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SetupComponents()
        {
            centralButton = new Button();
            centralButton.Size = new Size(100, 100);
            centralButton.Location = new Point((this.ClientSize.Width - centralButton.Width) / 2,
                                               (this.ClientSize.Height - centralButton.Height) / 2);
            this.Controls.Add(centralButton);

            rotatingPanel = new Panel();
            rotatingPanel.Size = new Size(50, 50);
            this.Controls.Add(rotatingPanel);

            random = new Random();


        }

        private void InitializeAnimation()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            angle += 0.1; // Adjust for speed
            double radius = 100; // Adjust for distance
            int newX = centralButton.Location.X + centralButton.Width / 2 + (int)(radius * Math.Cos(angle)) - rotatingPanel.Width / 2;
            int newY = centralButton.Location.Y + centralButton.Height / 2 + (int)(radius * Math.Sin(angle)) - rotatingPanel.Height / 2;
            rotatingPanel.Location = new Point(newX, newY);

            // Random color and "border width" changes using Padding
            rotatingPanel.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            rotatingPanel.BorderStyle = BorderStyle.FixedSingle;
            int paddingSize = random.Next(0, 10);  // Generating a random number for padding size
            rotatingPanel.Padding = new Padding(paddingSize);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetupComponents();
            InitializeAnimation();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.ShowDialog();
            this.Close();
        }
    }
}