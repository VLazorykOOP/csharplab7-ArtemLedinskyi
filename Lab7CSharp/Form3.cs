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
    public partial class Form3 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private Random random = new Random();
        public Form3()
        {
            InitializeComponent();
        }

        public enum ShapeType
        {
            Hexagon, // Шестикутник
            Triangle,
            Arc, // Дуга
            Rhombus
        }
        public abstract class Shape
        {
            protected int x;
            protected int y;
            protected Color color;  

            public Shape(int x, int y, Color color)  
            {
                this.x = x;
                this.y = y;
                this.color = color; 
            }

            public abstract void Draw(Graphics g);
            public void MoveTo(int newX, int newY)
            {
                x = newX;
                y = newY;
            }
        }
        public class HexagonShape : Shape
        {
            private readonly int sideLength;

            public HexagonShape(int x, int y, int sideLength, Color color) : base(x, y, color)  // Додано параметр Color
            {
                this.sideLength = sideLength;
            }

            public override void Draw(Graphics g)
            {
                // Точки для шестикутника
                Point[] points = new Point[6];
                for (int i = 0; i < 6; i++)
                {
                    points[i] = new Point(
                        x + (int)(sideLength * Math.Cos(i * Math.PI / 3)),
                        y + (int)(sideLength * Math.Sin(i * Math.PI / 3))
                    );
                }
                g.DrawPolygon(new Pen(color), points);  // Використано колір
            }
        }
        
        public class TriangleShape : Shape
        {
            private readonly int baseLength;
            private readonly int height;

            public TriangleShape(int x, int y, int baseLength, int height, Color color) : base(x, y, color)  // Додано параметр Color
            {
                this.baseLength = baseLength;
                this.height = height;
            }

            public override void Draw(Graphics g)
            {
                Point[] points =
                {
            new Point(x, y),
            new Point(x - baseLength / 2, y + height),
            new Point(x + baseLength / 2, y + height)
        };
                g.DrawPolygon(new Pen(color), points);  // Використано колір
            }
        }
        public class ArcShape : Shape
        {
            private readonly int width;
            private readonly int height;
            private readonly int startAngle;
            private readonly int sweepAngle;

            public ArcShape(int x, int y, int width, int height, int startAngle, int sweepAngle, Color color) : base(x, y, color)  // Додано параметр Color
            {
                this.width = width;
                this.height = height;
                this.startAngle = startAngle;
                this.sweepAngle = sweepAngle;
            }

            public override void Draw(Graphics g)
            {
                g.DrawArc(new Pen(color), new Rectangle(x, y, width, height), startAngle, sweepAngle);  // Використано колір
            }
        }
        public class RhombusShape : Shape
        {
            private readonly int sideLength;

            public RhombusShape(int x, int y, int sideLength, Color color) : base(x, y, color)  // Додано параметр Color
            {
                this.sideLength = sideLength;
            }

            public override void Draw(Graphics g)
            {
                Point[] points =
                {
            new Point(x, y + sideLength / 2),
            new Point(x + sideLength / 2, y),
            new Point(x + sideLength, y + sideLength / 2),
            new Point(x + sideLength / 2, y + sideLength)
        };
                g.DrawPolygon(new Pen(color), points);  // Використано колір
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            random = new Random();
            ShapeType shapeType = (ShapeType)random.Next(4); // Змінено на 4 для врахування нових фігур

            int x = random.Next(pictureBox1.Width);
            int y = random.Next(pictureBox1.Height);

            int param1 = random.Next(10, 100);
            int param2 = random.Next(10, 100);

            // Отримуємо колір з TextBox
            string colorName1 = textBox1.Text; 
            Color color1 = Color.FromName(colorName1);
            string colorName2 = textBox2.Text; 
            Color color2 = Color.FromName(colorName2);
            string colorName3 = textBox3.Text; 
            Color color3 = Color.FromName(colorName3);
            string colorName4 = textBox4.Text; 
            Color color4 = Color.FromName(colorName4);


            Shape shape;
            switch (shapeType)
            {
                case ShapeType.Hexagon:
                    shape = new HexagonShape(x, y, param1, color1);  
                    break;
                case ShapeType.Triangle:
                    shape = new TriangleShape(x, y, param1, param2, color3); 
                    break;
                case ShapeType.Arc:
                    shape = new ArcShape(x, y, param1, param2, 0, 180, color4);  
                    break;
                case ShapeType.Rhombus:
                    shape = new RhombusShape(x, y, param1, color2);  
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            shapes.Add(shape);

            RefreshDrawing();
        }
        private void RefreshDrawing()
        {
            // Створюємо нове зображення
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Рисуємо всі фігури на зображенні
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(g);
                }
            }

            // Відображаємо зображення на PictureBox
            pictureBox1.Image = bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);
            }
        }
    }
}
