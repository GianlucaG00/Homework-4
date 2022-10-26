namespace Homework_4
{
    public partial class Form1 : Form
    {
        Random r;
        Bitmap b1, b2, b3;
        Graphics g1, g2, g3;
        Pen pen1 = new Pen(Color.Red, 2);
        Pen pen2 = new Pen(Color.Blue, 2);
        Pen pen3 = new Pen(Color.Green, 2);
        double treshold = 0.5;
        int trialCount = 100;
        int repeat = 3; 
        public Form1()
        {
            InitializeComponent();
            r = new Random();
            timer1.Interval = 500;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           // richTextBox1.AppendText(values.ElementAt(random.Next() % values.Count()) + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // useful if the button is pressed more than once
            richTextBox1.Clear();
            richTextBox2.Clear();  


            // graphic 1
            b1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g1 = Graphics.FromImage(b1);
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g1.Clear(Color.White); // colore in Background
            Rectangle virtualWindow1 = new Rectangle(20, 20, b1.Width - 40, b1.Height - 40);
            g1.DrawRectangle(Pens.Black, virtualWindow1); 

            // graphic 2
            b2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(b2);
            g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g2.Clear(Color.White);
            Rectangle virtualWindow2 = new Rectangle(20, 20, b2.Width - 40, b2.Height - 40);
            g2.DrawRectangle(Pens.Black, virtualWindow2); 

            // graphic 3
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            g3 = Graphics.FromImage(b3);
            g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Rectangle virtualWindow3 = new Rectangle(20, 20, b3.Width - 40, b3.Height - 40);
            g3.Clear(Color.White); 
            g3.DrawRectangle(Pens.Black, virtualWindow3);


            try
            {
                trialCount = Int32.Parse(textBox1.Text); // numero di estrazioni
            }
            catch (Exception ec)
            {
                // in case of invalid input we force a valid integer
                trialCount = 100; // numero di extrazioni
                textBox1.Text = "100";
            }

            double d; // double
            int y = 0; // number of successes (face up coin)

            List<Point> points1 = new List<Point>();
            List<Point> points2 = new List<Point>();
            List<Point> points3 = new List<Point>();
            int minX = 0;
            int maxX = trialCount;
            int minY = 0;
            int maxY = trialCount;

            
            points1 = new List<Point>();
            points2 = new List<Point>();
            points3 = new List<Point>();

            y = 0;
            try
            {
                treshold = ((double) Int32.Parse(textBox2.Text)) / 100; // probabilità di successo
            }
            catch(Exception ex)
            {
                treshold = 0.5;
                textBox2.Text = "50"; 
            }
            richTextBox2.AppendText(
                    "Results of " + trialCount.ToString() + " coin tosses" + Environment.NewLine +
                    "Success: Head" + Environment.NewLine +
                    "----------------------------" + Environment.NewLine
            );
            try
            {
                repeat = Int32.Parse(textBox3.Text); // number of repetitions
            }
            catch (Exception ex)
            {
                repeat = 3;
                textBox3.Text = "3";
            }

            for (int j = 0; j < repeat; j++)
            {
                y = 0;
                points1 = new List<Point>(); 
                points2 = new List<Point>();
                points3 = new List<Point>();

                for (int x = 1; x <= trialCount; x++)
                {
                    d = r.NextDouble();
                    if (d <= treshold)
                    {
                        y++;
                    }

                    // GRAPH 1: ABSOLUTE frequency 
                    int xDevice1 = fromXRealToXVirtual(x, minX, maxX, virtualWindow1.Left, virtualWindow1.Width);
                    int yDevice1 = fromYRealToYVirtual(y, minY, maxY, virtualWindow1.Top, virtualWindow1.Height);
                    points1.Add(new Point(xDevice1, yDevice1));

                    // GRAPH 2: RELATIVE frequency 
                    int xDevice2 = fromXRealToXVirtual(x, minX, maxX, virtualWindow2.Left, virtualWindow2.Width);
                    int yDevice2 = fromYRealToYVirtual(((double)y / (double)x) * 100, minY, 100, virtualWindow2.Top, virtualWindow2.Height);
                    points2.Add(new Point(xDevice2, yDevice2));

                    // GRAPH 3: NORMALIZED frequency 
                    int xDevice3 = fromXRealToXVirtual(x, minX, maxX, virtualWindow3.Left, virtualWindow3.Width);
                    int yDevice3 = fromYRealToYVirtual((int)((double)y / Math.Sqrt(y)), minY, 100, virtualWindow3.Top, virtualWindow3.Height);
                    points3.Add(new Point(xDevice3, yDevice3));
                }
                // Summary of the extractions
                richTextBox2.AppendText(
                    "----------------------------" + Environment.NewLine +
                    "Extraction n." + j.ToString() + Environment.NewLine +
                    "Number of HEADS: " + y + Environment.NewLine +
                    "Number of TAILS: " + (trialCount - y) + Environment.NewLine
                    );

                g1.DrawLines(pen1, points1.ToArray());
                pictureBox1.Image = b1;
                g2.DrawLines(pen2, points2.ToArray());
                pictureBox2.Image = b2;
                g3.DrawLines(pen3, points3.ToArray());
                pictureBox3.Image = b3;
            }

        }

        private int fromXRealToXVirtual(double x, int minX, int maxX, int left, int w)
        {
            return (int)(left + w * (x - minX) / (maxX - minX)); // left --> traslazione grafico
        }

        private int fromYRealToYVirtual(double y, int minY, int maxY, int top, int h)
        {
            return (int)(top + h - h * (y - minY) / (maxY - minY)); // top --> traslazione grafico 
        }
    }
}