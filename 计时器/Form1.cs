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
                this.Size = new Size(this.Size.Width, this.Size.Height - 60);
                this.FormBorderStyle = FormBorderStyle.None;
                Rectangle rec = Screen.GetWorkingArea(this);
                this.Location = new Point((rec.Width - this.Size.Width) / 2, 0);

                // 绑定拖动事件
                this.MouseDown += Drag_MouseDown;
                this.MouseMove += Drag_MouseMove;
                this.MouseUp += Drag_MouseUp;

                // 绑定标签拖动事件
                this.label1.MouseDown += Drag_MouseDown;
                this.label1.MouseMove += Drag_MouseMove;
                this.label1.MouseUp += Drag_MouseUp;
                //this.label2.MouseDown += Drag_MouseDown;
                //this.label2.MouseMove += Drag_MouseMove;
                //this.label2.MouseUp += Drag_MouseUp;

                // 绑定点击事件
                this.label2.MouseClick += label_Start_Pause;
                this.label2.MouseDoubleClick += label_Stop;
            }
            else
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 60);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Rectangle rec = Screen.GetWorkingArea(this);
                this.Location = new Point((rec.Width - this.Size.Width) / 2, (rec.Height - this.Size.Height) / 2);

                // 解绑拖动事件
                this.MouseDown -= Drag_MouseDown;
                this.MouseMove -= Drag_MouseMove;
                this.MouseUp -= Drag_MouseUp;

                // 解绑标签拖动事件
                this.label1.MouseDown -= Drag_MouseDown;
                this.label1.MouseMove -= Drag_MouseMove;
                this.label1.MouseUp -= Drag_MouseUp;
                //this.label2.MouseDown -= Drag_MouseDown;
                //this.label2.MouseMove -= Drag_MouseMove;
                //this.label2.MouseUp -= Drag_MouseUp;

                // 解绑点击事件
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

        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键

        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender.GetType().ToString().IndexOf("Label") >= 0)
                {
                    Label label = (Label)sender;
                    mouseOff = new Point(-label.Location.X - e.X, -label.Location.Y - e.Y); //得到变量的值
                }
                else
                {
                    mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                }
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Drag_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
            if(this.Location.Y < 0)
            {
                this.Location = new Point(this.Location.X, 0);
            }
        }
    }
}
