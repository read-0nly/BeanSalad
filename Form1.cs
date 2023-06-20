using Microsoft.VisualBasic.Devices;
using System.Windows.Forms;

namespace BeanSalad
{
    public partial class Form1 : Form
    {
        public static Label[,] labels = new Label[64, 32];
        struct Player
        {
            public char Icon = '@';
            public int x = 0;
            public int y = 0;
            public int nextX = 0;
            public int nextY = 0;
            public int health = 3;
            public int knockback = 0;
            public int jump = -1;
            public Label label;

            public Player(Form f, int x1 = 0, int y1 = 0)
            {
                x = x1;
                y = y1;
                label = new Label();
                label.BackColor = Color.FromArgb(196,66,11);
                label.Location = new Point(12 + (16 * x)+2, 9 + (16 * y)+2);
                label.Margin = new Padding(0);
                label.Padding = new Padding(0);
                label.Name = "label1";
                label.Size = new Size(12, 12);
                label.BorderStyle = BorderStyle.Fixed3D;
                
                label.TabIndex = 0;
                //label.Text = Icon.ToString();
                label.TextAlign=ContentAlignment.TopLeft;
                label.Font = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
                label.ForeColor = Color.Black;
                f.Controls.Add(label);

            }
            public void updatePos(int x1, int y1)
            {

                if (Form1.labels != null && Form1.labels[x1, y1] != null && !(Form1.labels[x1, y1].BackColor == Color.Black))
                {
                    x = x1; y = y1;
                }

                if (Form1.labels != null && y < labels.GetLength(1) - 1 && Form1.labels[x, y + 1] != null)
                {
                    if (!(Form1.labels[x, y + 1].BackColor == Color.Black))
                    {
                        if (jump > 0)
                        {
                            jump--;
                            y += -1;
                            jump += -2;
                            jump = Math.Max(0, jump);
                        }
                        else
                        {
                            y += 1;
                        }
                    }
                    else
                    {
                        if (jump > -1)
                        {
                            jump--;
                            y += -1;
                        }
                        else
                        {
                            jump = -1;
                        }
                    }
                }
                if (x + nextX > -1 && x + nextX < labels.GetLength(0) && Form1.labels != null && Form1.labels[x + nextX, y + nextY] != null && !(Form1.labels[x + nextX, y + nextY].BackColor == Color.Black)) { x += nextX; }
                if (y + nextY > -1 && y + nextY < labels.GetLength(1) && Form1.labels != null && Form1.labels[x + nextX, y + nextY] != null && !(Form1.labels[x + nextX, y + nextY].BackColor == Color.Black)) { y += nextY; }

                label.Location = new Point(12 + (16 * x) + 2, 9 + (16 * y) + 2);
                if (jump == -1) { 
                   // nextX = 0;
                }
                nextY = 0;
            }
        }

        Player p;


        public Form1()
        {
            InitializeComponent();

            SuspendLayout();
            for (int i = 0; i < labels.GetLength(0); i++)
            {

                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    label1 = new Label();
                    label1.BackColor = Color.FromArgb(11, 4 * j, 255- (4 * j));
                    if (j == 31 && i%13!=5) { label1.BackColor = Color.Black; }
                    label1.Location = new Point(12 + (16 * i), 9 + (16 * j));
                    label1.Margin = new Padding(0);
                    label1.Name = "label1";
                    label1.Size = new Size(16, 16);
                    label1.TabIndex = 0;
                    label1.ForeColor = Color.Red;
                    label1.SendToBack();
                    Controls.Add(label1);
                    labels[i, j] = label1;

                }
            }
            ClientSize = new Size(labels.GetLength(0) * 16 + 24, labels.GetLength(1) * 16 + 18);
            BackColor = Color.Black;
            ResumeLayout(false);
            p = new Player(this,0,30);
            p.label.BringToFront();
            p.label.Click += new EventHandler(LB_Click);
            startLoop();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==(Keys.D))
            {
                p.nextX = 1;
            }
            if (e.KeyCode == (Keys.W))
            {
                if (p.jump < 0)
                {
                    p.jump = 5;
                }
            }
            if (e.KeyCode == (Keys.A))
            {
                p.nextX = -1;
            }
            e.SuppressKeyPress = true;

        }
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void startLoop()
        {
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 100; // every 5 seconds
            this.timer.Tick += new EventHandler(MyIntervalFunction);
            this.timer.Enabled = true;

        }
        private void checkControls()
        {
        }
        private void MyIntervalFunction(object sender, EventArgs e)
        {
            p.updatePos(p.x, p.y);
        }
        protected void LB_Click(object sender, EventArgs e)
        {
            p.updatePos(p.x+1, p.y-2);
        }
    }
}
