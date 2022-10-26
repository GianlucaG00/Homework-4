using static System.Windows.Forms.DataFormats;
using System.Security.Cryptography;
using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace Homework_4
{
    public partial class Form1 : Form
    {
        Random r;
        Bitmap b, b1;
        Graphics g, g1;
        Pen pen1 = new Pen(Color.IndianRed, 2);
        Pen pen2 = new Pen(Color.LightBlue, 2);
        Pen pen3 = new Pen(Color.Olive, 2);
        Pen pen = new Pen(Color.Black, 2);
        Brush brush1 = new SolidBrush(Color.IndianRed); 
        Brush brush2 = new SolidBrush(Color.LightBlue);
        Brush brush3 = new SolidBrush(Color.Olive); 
        List<Point> points; 
        List<Rectangle> rectangles;
        Rectangle virtualWindow;
        double treshold = 0.5;
        int trialCount = 100;
        int repeat = 20;
        int step = 3;
        public Form1()
        {
            InitializeComponent();
            r = new Random();
            timer1.Interval = 500;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // RELATIVE FREQUENCY
        private void button2_Click(object sender, EventArgs e)
        {
            // useful if the button is pressed more than once
            richTextBox2.Clear();

            // graphic 3
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            b1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g1 = Graphics.FromImage(b1);
            g1.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            virtualWindow = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, virtualWindow);

            parseInput();

            double d; // double
            int y = 0; // number of successes (face up coin)
            int minX = 0;
            int maxX = trialCount;
            int minY = 0;
            int maxY = trialCount;
            points = new List<Point>();
            y = 0;
            int[] count = new int[b1.Height / step];
            int i = 0;
            foreach (int c in count)
            {
                count[i] = 0;
                i++;
            }
            i = 0;
            rectangles = new List<Rectangle>();
            richTextBox2.AppendText(
                    "Results of " + trialCount.ToString() + " coin tosses" + Environment.NewLine +
                    "Success: Head" + Environment.NewLine +
                    "----------------------------" + Environment.NewLine
            );

            for (int j = 0; j < repeat; j++)
            {
                y = 0;
                points = new List<Point>();

                for (int x = 1; x <= trialCount; x++)
                {
                    d = r.NextDouble();
                    if (d <= treshold)
                    {
                        y++;
                    }
                    
                    // Graph 2: Relative frequency
                    int xDevice2 = fromXRealToXVirtual(x, minX, maxX, virtualWindow.Left, virtualWindow.Width);
                    int yDevice2 = fromYRealToYVirtual(((double)y / (double)x) * 100, minY, maxY, virtualWindow.Top, virtualWindow.Height);
                    points.Add(new Point(xDevice2, yDevice2));
                }
                // ISTOGRAM 3: points to plot the istrogram 3  
                count[(int)((((double)y / (double)trialCount) * 100) / step)]++; // increments the current step 

                // Summary of the extractions
                richTextBox2.AppendText(
                    "----------------------------" + Environment.NewLine +
                    "Extraction n." + j.ToString() + Environment.NewLine +
                    "Number of HEADS: " + y + Environment.NewLine +
                    "Number of TAILS: " + (trialCount - y) + Environment.NewLine
                    );

                // Draw the Chart 3
                g.DrawLines(pen2, points.ToArray());
                pictureBox1.Image = b;

                // Draw the Histogram 3
                int kk = 1;
                foreach (int k in count)
                {
                    int wr = fromXRealToXVirtual(k, minX, repeat, 0, b1.Width);
                    int sr = fromXRealToXVirtual(step, minX, trialCount, 0, b1.Height);
                    int yr = fromYRealToYVirtual(kk * step, minY, trialCount, 0, b1.Height);
                    rectangles.Add(new Rectangle(0, yr, wr, sr));
                    kk++;
                }
                kk = 1;
                g1.FillRectangles(brush2, rectangles.ToArray());
                g1.DrawRectangles(pen, rectangles.ToArray());
                pictureBox2.Image = b1;
            }

        }



        // NORMALIZED FREQUENCY
        private void button3_Click(object sender, EventArgs e)
        {
            // useful if the button is pressed more than once
            richTextBox2.Clear();

            // graphic 3
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            b1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g1 = Graphics.FromImage(b1);
            g1.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            virtualWindow = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, virtualWindow);

            parseInput();

            double d; // double
            int y = 0; // number of successes (face up coin)
            int minX = 0;
            int maxX = trialCount;
            int minY = 0;
            int maxY = trialCount;
            points = new List<Point>();
            y = 0;
            int[] count = new int[b1.Height / step];
            int i = 0;
            foreach (int c in count)
            {
                count[i] = 0;
                i++;
            }
            i = 0; 
            rectangles = new List<Rectangle>();
            richTextBox2.AppendText(
                    "Results of " + trialCount.ToString() + " coin tosses" + Environment.NewLine +
                    "Success: Head" + Environment.NewLine +
                    "----------------------------" + Environment.NewLine
            );

            for (int j = 0; j < repeat; j++)
            {
                y = 0;
                points = new List<Point>();

                for (int x = 1; x <= trialCount; x++)
                {
                    d = r.NextDouble();
                    if (d <= treshold)
                    {
                        y++;
                    }

                    // GRAPH 3: NORMALIZED frequency 
                    int xDevice3 = fromXRealToXVirtual(x, minX, maxX, virtualWindow.Left, virtualWindow.Width);
                    int yDevice3 = fromYRealToYVirtual((int)((double)y / Math.Sqrt(y)), minY, 100, virtualWindow.Top, virtualWindow.Height);
                    points.Add(new Point(xDevice3, yDevice3));
                }
                // ISTOGRAM 3: points to plot the istrogram 3  
                count[((int)((double)y / Math.Sqrt(y))) / step]++; // increments the current step 

                // Summary of the extractions
                richTextBox2.AppendText(
                    "----------------------------" + Environment.NewLine +
                    "Extraction n." + j.ToString() + Environment.NewLine +
                    "Number of HEADS: " + y + Environment.NewLine +
                    "Number of TAILS: " + (trialCount - y) + Environment.NewLine
                    );

                // Draw the Chart 3
                g.DrawLines(pen3, points.ToArray());
                pictureBox1.Image = b;

                // Draw the Histogram 3
                int kk = 1;
                foreach (int k in count)
                {
                    int wr = fromXRealToXVirtual(k, minX, repeat, 0, b1.Width);
                    int sr = fromXRealToXVirtual(step, minX, trialCount, 0, b1.Height);
                    int yr = fromYRealToYVirtual(kk * step, minY, trialCount, 0, b1.Height);
                    rectangles.Add(new Rectangle(0, yr, wr, sr));
                    kk++;
                }
                kk = 1;
                g1.FillRectangles(brush3, rectangles.ToArray());
                g1.DrawRectangles(pen, rectangles.ToArray());
                pictureBox2.Image = b1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // richTextBox1.AppendText(values.ElementAt(random.Next() % values.Count()) + Environment.NewLine);
        }

        // ABSOLUTE FREQUENCY
        private void button1_Click(object sender, EventArgs e)
        {

            // useful if the button is pressed more than once
            richTextBox2.Clear();  


            // graphic 1
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap b1 = new Bitmap(pictureBox2.Width, pictureBox2.Height); 

            g = Graphics.FromImage(b);
            Graphics g1 = Graphics.FromImage(b1);
            g1.Clear(Color.White); 
            

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White); // colore in Background
            virtualWindow = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            g.DrawRectangle(Pens.Black, virtualWindow);

            parseInput(); 

            double d; // double
            int y = 0; // number of successes (face up coin)

            List<Point> points = new List<Point>();
            rectangles = new List<Rectangle>();

            
            int[] count = new int[b1.Height / step];
            int i = 0; 
            foreach(int c in count)
            {
                count[i] = 0;
                i++;
            }
            i = 0; 
            int minX = 0;
            int maxX = trialCount;
            int minY = 0;
            int maxY = trialCount;

            
            points = new List<Point>();


            y = 0;
            richTextBox2.AppendText(
                    "Results of " + trialCount.ToString() + " coin tosses" + Environment.NewLine +
                    "Success: Head" + Environment.NewLine +
                    "----------------------------" + Environment.NewLine
            );
         

            for (int j = 0; j < repeat; j++)
            {
                y = 0;
                points = new List<Point>();

                for (int x = 1; x <= trialCount; x++)
                {
                    d = r.NextDouble();
                    if (d <= treshold)
                    {
                        y++;
                    }

                    // GRAPH 1: points to plot ABSOLUTE frequency 
                    int xDevice1 = fromXRealToXVirtual(x, minX, maxX, virtualWindow.Left, virtualWindow.Width);
                    int yDevice1 = fromYRealToYVirtual(y, minY, maxY, virtualWindow.Top, virtualWindow.Height);
                    points.Add(new Point(xDevice1, yDevice1));
                }

                // ISTOGRAM 1: points to plot the istrogram 1   
                count[y / step]++; // increments the current step 

                // Summary of the extractions
                richTextBox2.AppendText(
                    "----------------------------" + Environment.NewLine +
                    "Extraction n." + j.ToString() + Environment.NewLine +
                    "Number of HEADS: " + y + Environment.NewLine +
                    "Number of TAILS: " + (trialCount - y) + Environment.NewLine
                    );

                // Draw the chart
                g.DrawLines(pen1, points.ToArray());
                pictureBox1.Image = b;

                // Draw the Histogram
                int kk = 1; 
                foreach(int k in count)
                {
                    int wr = fromXRealToXVirtual(k, 0, repeat, 0, b1.Width);
                    int sr = fromXRealToXVirtual(step, minX, trialCount, 0, b1.Height);
                    int yr = fromYRealToYVirtual(kk*step, minY, trialCount, 0, b1.Height); 
                    rectangles.Add(new Rectangle(0, yr, wr, sr));
                    kk++; 
                }
                kk = 1;
                g1.FillRectangles(brush1, rectangles.ToArray());
                g1.DrawRectangles(pen, rectangles.ToArray());
                pictureBox2.Image = b1; 
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

        private void parseInput()
        {

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
            try
            {
                treshold = ((double)Int32.Parse(textBox2.Text)) / 100; // probabilità di successo
            }
            catch (Exception ex)
            {
                treshold = 0.5;
                textBox2.Text = "50";
            }
            try
            {
                repeat = Int32.Parse(textBox3.Text); // number of repetitions
            }
            catch (Exception ex)
            {
                repeat = 20;
                textBox3.Text = "20";
            }
            try
            {
                step = Int32.Parse(textBox4.Text);
            }
            catch (Exception ex)
            {
                step = 3;
                textBox4.Text = "3";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}