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


            int trialCount = 0; 
            try
            {
                trialCount = Int32.Parse(textBox1.Text); // numero di extrazioni
            }
            catch (Exception ec)
            {
                // in case of invalid input we force a valid integer
                trialCount = 100; // numero di extrazioni
                textBox1.Text = "100";
            }

            double d; // double
            int y = 0; // number of successes (face up coin)

            int[] extraction = new int[trialCount+1];
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
            for (int x = 1; x <= trialCount; x++)
            {
                d = r.NextDouble();
                if (d >= 0.5)
                {
                    y++;
                    extraction[x] = 1;
                }
                else extraction[x] = 0; 

                // graph 1: relative frequency 
                int xDevice1 = fromXRealToXVirtual(x, minX, maxX, virtualWindow1.Left, virtualWindow1.Width);
                int yDevice1 = fromYRealToYVirtual(y, minY, maxY, virtualWindow1.Top, virtualWindow1.Height);
                points1.Add(new Point(xDevice1, yDevice1));

                richTextBox1.AppendText("Trial n. " + x + ": " + extraction[x] + Environment.NewLine);

            }
            richTextBox2.AppendText(
                "Results of " + trialCount.ToString() + " coin tosses" + Environment.NewLine +
                "----------------------------" + Environment.NewLine + 
                "Number of HEADS: " + y + Environment.NewLine +
                "Number of TAILS: " + (trialCount - y));

            g1.DrawLines(pen1, points1.ToArray());
            pictureBox1.Image = b1;

            int sum = 0; 
            for (int j = 1; j<=trialCount; j++)
            {
                sum += extraction[j];
                // graph 2: absolute frequency 
                int xDevice2 = fromXRealToXVirtual(j, minX, maxX, virtualWindow2.Left, virtualWindow2.Width);
                int yDevice2 = fromYRealToYVirtual(sum, minY, y, virtualWindow2.Top, virtualWindow2.Height);
                points2.Add(new Point(xDevice2, yDevice2));

                // graph 2: normalized frequency 
                int xDevice3 = fromXRealToXVirtual(j, minX, maxX, virtualWindow3.Left, virtualWindow3.Width); 
                int yDevice3 = fromYRealToYVirtual((int) (sum/Math.Sqrt(y)), minY, y, virtualWindow3.Top, virtualWindow3.Height);
                points3.Add(new Point(xDevice3, yDevice3));
            }
            g2.DrawLines(pen2, points2.ToArray());
            pictureBox2.Image = b2;
            g3.DrawLines(pen3, points3.ToArray());
            pictureBox3.Image = b3;

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