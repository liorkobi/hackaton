using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Concurrent;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Image = System.Drawing.Image;

namespace hackaton
{
    public partial class secondUC : UserControl
    {
        FlowLayoutPanel[] queue = new FlowLayoutPanel[3];

        private System.Threading.Timer animationTimer;
        private int animationSpeed = 10;
        public secondUC()
        {


            InitializeComponent();
            queue[0]=flowLayoutPanel1;
            queue[1]=flowLayoutPanel2;
            queue[2]=flowLayoutPanel3;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public void SetTextBox(string text, string statics)
        {
            if (statics == "capacity")
            {
                if (textBox4.InvokeRequired)
                {
                    textBox4.BeginInvoke((MethodInvoker)(() => textBox4.Text = text));
                }
                else
                {
                    textBox4.Text = text;
                }


            }

            else if (statics == "time")
            {
                if (textBox5.InvokeRequired)
                {
                    textBox5.BeginInvoke((MethodInvoker)(() => textBox5.Text = text));
                }
                else
                {
                    textBox5.Text = text;
                }
            }

            else if (statics == "waiting")
            {
                if (textBox3.InvokeRequired)
                {
                    textBox3.BeginInvoke((MethodInvoker)(() => textBox3.Text = text));
                }
                else
                {
                    textBox3.Text = text;
                }
            }
            else if (statics == "waitingAvgtime")
            {
                if (textBox2.InvokeRequired)
                {
                    textBox2.BeginInvoke((MethodInvoker)(() => textBox2.Text = text));
                }
                else
                {
                    textBox2.Text = text;
                }
            }


        }

        public void Timer1()
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls[0].Visible = true; // Show the firstUC
            Parent.Controls[1].Visible = false; // Hide the secondUC

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox6.Left -= 1;
            if (pictureBox6.Left == 400) pictureBox6.Hide();

        }

        private void secondUC_Load(object sender, EventArgs e)
        {
//            ThreadPool.QueueUserWorkItem(UpdateQueueVisualization1);
   //         ThreadPool.QueueUserWorkItem(UpdateQueueVisualization2);
     //       ThreadPool.QueueUserWorkItem(UpdateQueueVisualization3);

            animationTimer = new System.Threading.Timer(AnimatePictureBoxes, null, animationSpeed, animationSpeed);


        }

        private void AnimatePictureBoxes(object? state)
        {
         // throw new NotImplementedException();
        }



        internal void flowLayoutPanelRemove(int queueIndex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    if (queueIndex == 0 && flowLayoutPanel1.Controls.Count>0)
                        flowLayoutPanel1.Controls.RemoveAt(0);
                    else if (queueIndex == 1 && flowLayoutPanel2.Controls.Count > 0)
                        flowLayoutPanel2.Controls.RemoveAt(0);
                    if (queueIndex == 2 && flowLayoutPanel3.Controls.Count > 0)
                        flowLayoutPanel3.Controls.RemoveAt(0);




                }));
            }
            else
            {
                if (queueIndex == 0 && flowLayoutPanel1.Controls.Count > 0)
                    flowLayoutPanel1.Controls.RemoveAt(0);
                else if (queueIndex == 1 && flowLayoutPanel2.Controls.Count > 0)
                    flowLayoutPanel2.Controls.RemoveAt(0);
                if (queueIndex == 2 && flowLayoutPanel3.Controls.Count > 0)
                    flowLayoutPanel3.Controls.RemoveAt(0);
            }
        }

        private PictureBox CreatePictureBox(int i)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 100);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            Label label = new Label();
            label.Text = i.ToString();
            label.AutoSize = true;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Fill;
            label.BackColor = Color.Transparent;

            //amnon
            if (i == 0) {
                //pictureBox.Image = Image.FromFile("C:\\Users\\liork\\Downloads\\amnon-removebg-preview.png");
            }


            pictureBox.Controls.Add(label);

            return pictureBox;
        }


        public void UpdateQueueVisualization1(object state)
        {
            
                // Update the UI to display the current queue
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        queue[0].Controls.Clear();
                        if (Parent.Controls[2] is firstUC first)
                        {

                            foreach (int item in first.queue1)
                            {
                                PictureBox pictureBox = CreatePictureBox(item);
                                queue[0].Controls.Add(pictureBox);
                            }
                        }
                    }));
                }
                else
                {
                    queue[0].Controls.Clear();
                    if (Parent.Controls[2] is firstUC first)

                        foreach (int item in first.queue1)
                        {
                            PictureBox pictureBox = CreatePictureBox(item);
                            queue[0].Controls.Add(pictureBox);
                        }
                }
            
                Thread.Sleep(500); // Update the visualization every 500 milliseconds
            }
        

    public void UpdateQueueVisualization2(object state)
    {
            // Update the UI to display the current queue
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    queue[1].Controls.Clear();
                    if (Parent.Controls[2] is firstUC first)
                    {

                        foreach (int item in first.queue2)
                        {
                            PictureBox pictureBox = CreatePictureBox(item);
                            queue[1].Controls.Add(pictureBox);
                        }
                    }
                }));
            }
            else
            {
                queue[1].Controls.Clear();
                if (Parent.Controls[2] is firstUC first)

                    foreach (int item in first.queue2)
                    {
                        PictureBox pictureBox = CreatePictureBox(item);
                        queue[1].Controls.Add(pictureBox);
                    }
            }
        
        Thread.Sleep(5000); // Update the visualization every 500 milliseconds
    }

    public void UpdateQueueVisualization3(object state)
    {

            // Update the UI to display the current queue
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    queue[2].Controls.Clear();
                    if (Parent.Controls[2] is firstUC first)
                    {

                        foreach (int item in first.queue3)
                        {
                            PictureBox pictureBox = CreatePictureBox(item);
                            queue[2].Controls.Add(pictureBox);
                        }
                    }
                }));
            }
            else
            {
                queue[2].Controls.Clear();
                if (Parent.Controls[2] is firstUC first)

                    foreach (int item in first.queue3)
                    {
                        PictureBox pictureBox = CreatePictureBox(item);
                        queue[2].Controls.Add(pictureBox);
                    }
            }
        
        Thread.Sleep(500); // Update the visualization every 500 milliseconds
    }
}


    }