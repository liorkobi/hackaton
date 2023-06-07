using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;
using System.Threading.Tasks.Dataflow;
using static System.Windows.Forms.AxHost;
using System.Collections.Concurrent;
using System.Collections;

namespace hackaton
{
    public partial class firstUC : UserControl
    {
        //   secondUC second;
        private Timer updateTimer1;
        private Timer updateTimer2;
        private Timer updateTimer3;
        private Timer updateTimer4;
        private Timer updateTimer5;
        private bool isRunning = false;
        public BlockingCollection<int> queue1;
        public BlockingCollection<int> queue2;
        public BlockingCollection<int> queue3;
       
        public firstUC()
        {

            InitializeComponent();
            //       second = new secondUC();

            // Add secondUC to the form
            //       second.Location = new Point(0, 0);
            //          second.Visible = false;
            //          Controls.Add(second);
            queue1 = new BlockingCollection<int>();
            queue2 = new BlockingCollection<int>();
            queue3 = new BlockingCollection<int>();



        }

        public int bufferSize = 15;
        public int numOfBuffers = 3;
        public int ConsumerSize = 5;
        public int ProducerSize = 5;
        public int Ctime = 2000;
        public int Ptime = 1000;
        public int numOfFull = 0;
        private DateTime buttonClickTime;
        public object[] bufferLocks;
        static int prodnum = 0;
        static int consnum = 0;
        private int waiting = 0;
        private int total =0;
        private TimeSpan waitTime;
       




        public object bufferLock = new object();
        public List<Queue<int>> queues = new List<Queue<int>>();
        //static Form2 form2 = new Form2();
        //static UserControl1 userControl1 = new UserControl1();


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string producers = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string consumers = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string buffer = textBox3.Text;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {


        }
        private void UpdateTextBox_CAPACITY(object state)
        {
            if (Parent.Controls[3] is secondUC second)
            {
                // Call the SetTextBox method on the secondUC control
                double stat = numOfFull / ((numOfBuffers * bufferSize) * 0.01);
                second.SetTextBox(stat.ToString(), "capacity");
                Console.WriteLine("set text" + stat.ToString());
            }
        }
        private void UpdateTextBox_TIME(object state)
        {
            if (Parent.Controls[3] is secondUC second)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan timePassed = currentTime - buttonClickTime;

                // Call the SetTextBox method on the secondUC control
                second.SetTextBox(timePassed.ToString(), "time"); ;
                //Console.WriteLine("set text" + stat.ToString());
            }
        }
        private void UpdateTextBox_Wait(object? state)
        {
            if (Parent.Controls[3] is secondUC second)
            {
                second.SetTextBox(waiting.ToString(), "waiting"); ;
                //Console.WriteLine("set text" + stat.ToString());
            }
        }
        private void UpdateTextBox_WaitAVG(object? state)
        {
            if (Parent.Controls[3] is secondUC second)
            {
                total += (int)waitTime.TotalMilliseconds;
                // Call the SetTextBox method on the secondUC control
                if (prodnum == 0) second.SetTextBox(total.ToString(), "waitingAvgtime");
                else
                    second.SetTextBox((total / prodnum).ToString(), "waitingAvgtime"); ;
                //Console.WriteLine("set text" + stat.ToString());
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            buttonClickTime = DateTime.Now;


            this.Visible = false; // Hide the firstUC
            this.Parent.Controls[3].Visible = true;
            Console.WriteLine(numOfFull);

            if (Parent.Controls[3] is secondUC second)
            {
                // Call the SetTextBox method on the secondUC control
                double stat = numOfFull / ((numOfBuffers * bufferSize) * 0.01);
                updateTimer1 = new Timer(UpdateTextBox_CAPACITY, null, 0, 200);
                updateTimer2 = new Timer(UpdateTextBox_TIME, null, 0, 200);
                updateTimer3 = new Timer(UpdateTextBox_Wait, null, 0, 200);
                updateTimer4 = new Timer(UpdateTextBox_WaitAVG, null, 0, 200);

                Console.WriteLine("set text" + stat.ToString());



            }







            Console.WriteLine("started");
            if (textBox3.Text != "")
            {

                bufferSize = Convert.ToInt32(textBox3.Text);
            }
            if (textBox1.Text != "")
            {

                ProducerSize = Convert.ToInt32(textBox1.Text);
            }
            if (textBox2.Text != "")
            {

                ConsumerSize = Convert.ToInt32(textBox2.Text);
            }
            if (textBox4.Text != "")
            {

                Ctime = Convert.ToInt32(textBox4.Text);
            }
            if (textBox5.Text != "")
            {

                Ptime = Convert.ToInt32(textBox5.Text);

            }
            for (int i = 0; i < numOfBuffers; i++)
            {
                queues.Add(new Queue<int>());

            }
            isRunning = true;
            bufferLocks = new object[numOfBuffers];
            for (int i = 0; i < numOfBuffers; i++)
            {
                bufferLocks[i] = new object();
            }




            for (int i = 0; i < ConsumerSize; i++)
            {
                ThreadPool.QueueUserWorkItem(state => Consumer());
            }

            for (int i = 0; i < ProducerSize; i++)
            {
                ThreadPool.QueueUserWorkItem(state => Producer());

            }




        }



        private void Producer()
        {
            DateTime startW;
            while (isRunning)
            {

                int shortestQueueIndex = GetShortestQueueIndex();
            if (shortestQueueIndex >= 0 && shortestQueueIndex < bufferLocks.Length)
            {
                lock (bufferLocks[shortestQueueIndex])
                {
                        startW = DateTime.Now;
                        while (queues[shortestQueueIndex].Count >= bufferSize)
                    {
                        Monitor.Wait(bufferLocks[shortestQueueIndex]);
                            waiting++;
                    }

                    queues[shortestQueueIndex].Enqueue(prodnum);
                    numOfFull++;
                    prodnum++;
                        DateTime finishW = DateTime.Now;
                        waitTime = finishW - startW;

                        if (waiting>0)waiting--;
                       

                            if (shortestQueueIndex == 0) {
                                queue1.Add(1);
                            }
                       
                       else if (shortestQueueIndex == 1) {
                                queue2.Add(1);

                            }

                            else if (shortestQueueIndex == 2) {
                                queue3.Add(1);

                            }



                        Console.WriteLine($"Produced: {prodnum} in Queue: {shortestQueueIndex}");

                    Monitor.PulseAll(bufferLocks[shortestQueueIndex]);
                }
            }

            Thread.Sleep(Ptime); // Simulate some processing time
        } }

        private void Consumer()
        {
            while (isRunning)
            {

                int queueIndex = GetLongestQueueIndex();

                if (queueIndex >= 0 && queueIndex < bufferLocks.Length)
                {
                    lock (bufferLocks[queueIndex])
                    {
                        while (queues[queueIndex].Count == 0)
                        {
                            Monitor.Wait(bufferLocks[queueIndex]);
                        }

                        int item = queues[queueIndex].Dequeue();
                        numOfFull--;
                        consnum++;
                        if (Parent.Controls[3] is secondUC second)
                        {
                            ThreadPool.QueueUserWorkItem(second.UpdateQueueVisualization1);
                            ThreadPool.QueueUserWorkItem(second.UpdateQueueVisualization2);
                            ThreadPool.QueueUserWorkItem(second.UpdateQueueVisualization3);



                            if (queueIndex == 0)
                            {
                                queue1.TryTake(out int i);
                            }

                            else if (queueIndex == 1)
                            {
                                queue2.TryTake(out int i);

                            }

                            else if (queueIndex == 2)
                            {
                                queue3.TryTake(out int i);

                            }

                            second.flowLayoutPanelRemove(queueIndex);


                        }



                        Console.WriteLine($"Consumed: {item} from Queue: {queueIndex}");

                        Monitor.PulseAll(bufferLocks[queueIndex]);
                    }
                }

                Thread.Sleep(Ctime); // Simulate some processing time
            }
        }


        private int GetShortestQueueIndex()
        {
            int shortestQueueIndex = 0;
            int shortestQueueLength = int.MaxValue;

            for (int i = 0; i < queues.Count; i++)
            {
                if (queues[i].Count < shortestQueueLength)
                {
                    shortestQueueLength = queues[i].Count;
                    shortestQueueIndex = i;
                }
            }

            return shortestQueueIndex;
        }
        private int GetLongestQueueIndex()
        {
            int longestQueueIndex = 0;
            int longestQueueLength = 0;

            for (int i = 0; i < queues.Count; i++)
            {
                if (queues[i].Count > longestQueueLength)
                {
                    longestQueueLength = queues[i].Count;
                    longestQueueIndex = i;
                }
            }

            return longestQueueIndex;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
