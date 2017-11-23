using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 计时器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 标签也绑定双击事件
            label1.MouseDoubleClick += Form1_MouseDoubleClick;
        }

        private int time = 0;
        private bool isplayed = false;
        // 开始
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!isplayed)
            {
                timer1.Start();
                isplayed = true;
            }
        }

        // 暂停
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isplayed)
            {
                timer1.Stop();
                isplayed = false;
            }
        }

        // 停止
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label2.Text = "00:00:00";
            time = 0;
            isplayed = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval / 1000;
            int h = time / 3600;
            int m = time / 60 - h * 60;
            int s = time % 60;
            label2.Text = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", m) + ":" + string.Format("{0:D2}", s);
        }

        private bool hide = false;
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!hide)
            {
                this.Size = new Size(this.Size.Width, this.Size.Height - 55);
                this.FormBorderStyle = FormBorderStyle.None;
                Rectangle rec = Screen.GetWorkingArea(this);
                this.Location = new Point((rec.Width - this.Size.Width) / 2, 0);
                this.label2.MouseClick += label_Start_Pause;
                this.label2.MouseDoubleClick += label_Stop;
            }
            else
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 55);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Rectangle rec = Screen.GetWorkingArea(this);
                this.Location = new Point((rec.Width - this.Size.Width) / 2, (rec.Height - this.Size.Height) / 2);
                this.label2.MouseClick -= label_Start_Pause;
                this.label2.MouseDoubleClick -= label_Stop;
            }
            hide = !hide;
        }

        // 标签开始暂停手势
        private void label_Start_Pause(object sender, MouseEventArgs e)
        {
            if (!isplayed)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
            isplayed = !isplayed;
        }

        // 标签停止手势
        private void label_Stop(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            label2.Text = "00:00:00";
            time = 0;
            isplayed = false;
        }
    }
}
